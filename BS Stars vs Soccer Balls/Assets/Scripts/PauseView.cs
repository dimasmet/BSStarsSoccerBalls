using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Button _openPauseBtn;

    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _exitBtn;

    private void Awake()
    {
        _openPauseBtn.onClick.AddListener(() =>
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
        });

        _continueBtn.onClick.AddListener(() =>
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
        });

        _restartBtn.onClick.AddListener(() =>
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;

            GameSessionHandler.OnStartSessionGame?.Invoke();
        });

        _exitBtn.onClick.AddListener(() =>
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
            GameSessionHandler.OnEndGame?.Invoke();

            ScreenManager.Instance.OpenScreenGame(ScreenManager.NameScreen.Menu);
        });
    }
}
