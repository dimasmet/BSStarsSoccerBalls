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
        Warning,
        SkinsChoiced
    }

    private GameObject _curActiveScreen;

    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _warningScreen;
    [SerializeField] private GameObject _homeScreen;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _skinsScreen;

    [SerializeField] private Button _startGameBtn;
    [SerializeField] private Button _exitBtn;

    [SerializeField] private Button _openSkinScreen;
    [SerializeField] private Button _closeSkinScreen;

    [SerializeField] private RulesGameHandler _rulesGameHandler;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _startGameBtn.onClick.AddListener(() =>
        {
            OpenScreenGame(NameScreen.Game);
            _rulesGameHandler.ActiveRules();

            SoundsGame.I.RunSound(SoundsGame.Sound.Click);
        });

        _exitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        _openSkinScreen.onClick.AddListener(() =>
        {
            OpenScreenGame(NameScreen.SkinsChoiced);
        });

        _closeSkinScreen.onClick.AddListener(() =>
        {
            OpenScreenGame(NameScreen.Menu);
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
            case NameScreen.Warning:
                ScreenOpen(_warningScreen);
                break;
            case NameScreen.SkinsChoiced:
                ScreenOpen(_skinsScreen);
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
