using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;

    [SerializeField] private Button _openBtn;
    [SerializeField] private Button _closeBtn;

    private void Awake()
    {
        _openBtn.onClick.AddListener(() =>
        {
            _settingsPanel.SetActive(true);
        });

        _closeBtn.onClick.AddListener(() =>
        {
            _settingsPanel.SetActive(false);
        });
    }
}
