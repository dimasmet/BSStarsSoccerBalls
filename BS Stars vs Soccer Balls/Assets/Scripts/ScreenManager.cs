using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    private GameObject _curActiveScreen;

    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _warningScreen;
    [SerializeField] private GameObject _homeScreen;
    [SerializeField] private GameObject _gameScreen;

    [SerializeField] private Button _startGameBtn;

    private void Awake()
    {
        _startGameBtn.onClick.AddListener(() =>
        {
            OpenScreen(_gameScreen);
            GameSessionHandler.OnStartSessionGame?.Invoke();
        });
    }

    private void Start()
    {
        OpenScreen(_homeScreen);
    }

    public void OpenScreen(GameObject screen)
    {
        if (_curActiveScreen != null)
        {
            _curActiveScreen.SetActive(false);
        }

        _curActiveScreen = screen;

        _curActiveScreen.SetActive(true);
    }
}
