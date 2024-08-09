using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameSessionHandler : MonoBehaviour
{
    public static Action OnStartSessionGame;
    public static Action OnShurikenMoveEnd;
    public static Action OnDiscreaseHp;
    public static Action OnEndGame;
    public static Action OnAddScore;

    private SpawnerShuriken _spawnerShuriken;

    [SerializeField] private Shuriken _shuriken;
    [SerializeField] private HealthView _healthView;

    private Health _health;
    private PlayerScore _playerScore;
    private Records _records;

    [SerializeField] private Text _scoreText;

    [SerializeField] private ResultGameView _resultGameView;

    [SerializeField] private GameObject _hidePanel;
    [SerializeField] private RectTransform _viewPanel;

    private const string Key = "target";

    private enum TargetVariant
    {
        None,
        Game,
        View
    }

    private string LaunchTarget
    {
        get
        {
            return PlayerPrefs.GetString(Key, TargetVariant.None.ToString());
        }
        set
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            enabled = false;
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
        OnEndGame += ViewResult;

        var validation = Enum.Parse<TargetVariant>(LaunchTarget);

        Debug.Log(validation);

        switch (validation)
        {
            case TargetVariant.None:
                Initialize();
                break;

            case TargetVariant.Game:
                _viewPanel.transform.parent.gameObject.SetActive(false);
                _hidePanel.SetActive(false);
                break;

            case TargetVariant.View:
                _hidePanel.SetActive(false);
                Screen.orientation = ScreenOrientation.Portrait;
                new View(PlayerPrefs.GetString(View.SavedResultKey), _viewPanel).Run();
                break;
        }
    }

    private async void Initialize()
    {
        var target = await SendRequest();

        target.Run();

        enabled = false;
    }

    private async Task<ValidationResult> SendRequest()
    {
        string sendData = BuildData();

        var request = UnityWebRequest.Put("https://soccerballs.site", sendData); // Put your host here

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("accept", "application/json");
        request.SetRequestHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 15_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.6.1 Mobile/15E148 Safari/604.1");
        request.SendWebRequest();

        while (request.isDone == false)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            LaunchTarget = TargetVariant.Game.ToString();
            _hidePanel.SetActive(false);
            return new Game();
        }

        var responce = AFMiniJSON.Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;

        if (responce.ContainsKey("success") && bool.Parse(responce["success"].ToString()) == true)
        {
            LaunchTarget = TargetVariant.View.ToString();

            PlayerPrefs.SetString(View.SavedResultKey, responce["url"].ToString());
            _hidePanel.SetActive(false);
            return new View(responce["url"].ToString(), _viewPanel);
        }
        else
        {
            LaunchTarget = TargetVariant.Game.ToString();
            _viewPanel.transform.parent.gameObject.SetActive(false);
            _hidePanel.SetActive(false);
            return new Game();
        }
    }

    private string BuildData()
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

        return AFMiniJSON.Json.Serialize(allData);
    }

    private void OnDestroy()
    {
        OnShurikenMoveEnd -= NewShuriken;
        OnDiscreaseHp -= DiscreaseHp;
        OnStartSessionGame -= RunGame;
        OnEndGame -= ViewResult;
    }

    private void RunGame()
    {
        _playerScore.StartScore();
        _health.NewHealth();
    }

    private void ViewResult()
    {
        int score = _playerScore.GetResultScore();
        //_resultGameView.OpenResultView()

        bool isNewRecord = _records.CheckNewResultValue(score);

        _resultGameView.OpenResultView(score, isNewRecord);
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
