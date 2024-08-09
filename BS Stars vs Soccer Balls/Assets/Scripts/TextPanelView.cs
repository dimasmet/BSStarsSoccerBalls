using UnityEngine;
using UnityEngine.UI;

public class TextPanelView : MonoBehaviour
{
    public enum NameType
    {
        Privacy,
        Terms
    }

    public static TextPanelView Instance;

    [SerializeField] private Text _titleText;
    [SerializeField] private GameObject _panel;

    [SerializeField] private GameObject _privacyText;
    [SerializeField] private GameObject _termsOfUse;

    [SerializeField] private Button _closePanelBtn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _closePanelBtn.onClick.AddListener(() =>
        {
            _panel.SetActive(false);
        });
    }

    public void OpenTextViewer(NameType nameType)
    {
        _panel.SetActive(true);

        switch (nameType)
        {
            case NameType.Privacy:
                _titleText.text = "PRIVACY POLICY";
                _privacyText.SetActive(true);
                _termsOfUse.SetActive(false);
                break;
            case NameType.Terms:
                _titleText.text = "TERMS OF USE";
                _privacyText.SetActive(false);
                _termsOfUse.SetActive(true);
                break;
        }

        _privacyText.transform.parent.transform.position = new Vector2(_privacyText.transform.parent.transform.position.x, 0);
    }
}
