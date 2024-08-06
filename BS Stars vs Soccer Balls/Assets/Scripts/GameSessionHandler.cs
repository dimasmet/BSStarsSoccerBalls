using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
