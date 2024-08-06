using UnityEngine;
using UnityEngine.UI;

public class RecordView : MonoBehaviour
{
    [SerializeField] private GameObject _recordPanel;

    [SerializeField] private Button _recordOpenBtn;
    [SerializeField] private Button _recordCloseBtn;

    [SerializeField] private Text _recordText;

    private void Awake()
    {
        _recordOpenBtn.onClick.AddListener(() =>
        {
            _recordPanel.SetActive(true);

            _recordText.text = PlayerPrefs.GetInt("Record").ToString();
        });

        _recordCloseBtn.onClick.AddListener(() =>
        {
            _recordPanel.SetActive(false);
        });
    }
}
