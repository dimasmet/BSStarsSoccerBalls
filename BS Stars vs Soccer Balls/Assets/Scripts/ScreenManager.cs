using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    public enum NameScreen
    {
        Menu,
        Game,
    }

    private GameObject _curActiveScreen;

    [SerializeField] private GameObject _homeScreen;
    [SerializeField] private GameObject _gameScreen;

    [SerializeField] private Button _startGameBtn;
    [SerializeField] private Button _exitBtn;

    [SerializeField] private RulesGameHandler _rulesGameHandler;

    private void Awake()
    {
        Application.targetFrameRate = 90;

        if (Instance == null)
        {
            Instance = this;
        }

        _startGameBtn.onClick.AddListener(() =>
        {
            OpenScreenGame(NameScreen.Game);
            _rulesGameHandler.ActiveRules();
        });

        _exitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    private void Start()
    {
        OpenScreenGame(NameScreen.Menu);
    }

    public void OpenScreenGame(NameScreen nameScreen)
    {
        switch (nameScreen)
        {
            case NameScreen.Menu:
                ScreenOpen(_homeScreen);
                break;
            case NameScreen.Game:
                ScreenOpen(_gameScreen);
                break;
        }
    }

    private void ScreenOpen(GameObject screen)
    {
        if (_curActiveScreen != null)
        {
            _curActiveScreen.SetActive(false);
        }

        _curActiveScreen = screen;

        _curActiveScreen.SetActive(true);
    }
}
