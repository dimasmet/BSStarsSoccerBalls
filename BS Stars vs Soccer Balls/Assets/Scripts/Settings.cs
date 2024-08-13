using UnityEngine;
using UnityEngine.UI;
using UnityEngine.iOS;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;

    [SerializeField] private Button _openBtn;
    [SerializeField] private Button _closeBtn;

    [SerializeField] private Button _rateUs;
    [SerializeField] private Button _privacy;
    [SerializeField] private Button _terms;

    [Header("Sounds")]
    [SerializeField] private Button _soundsBtn;
    [SerializeField] private Button _musicBtn;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _noActiveSprite;

    [Header("FirstRun")]
    [SerializeField] private GameObject _panelWarn;
    [SerializeField] private Button _privacyOpenBtn;
    [SerializeField] private Button _checkerPrivacyBtn;
    [SerializeField] private Button _termsOpenBtn;
    [SerializeField] private Button _checkerTermsBtn;

    private bool isCheckPrivacy = false;
    private bool isCheckTerms = false;

    private void Awake()
    {
        _soundsBtn.onClick.AddListener(() =>
        {
            bool resultClick = SoundsGame.I.ActivitySounds();
            Sprite nS;
            if (resultClick)
                nS = _activeSprite;
            else
                nS = _noActiveSprite;

            _soundsBtn.transform.GetChild(0).GetComponent<Image>().sprite = nS;
        });

        _musicBtn.onClick.AddListener(() =>
        {
            bool resultClick = SoundsGame.I.ActivityMusic();
            Sprite nS;
            if (resultClick)
                nS = _activeSprite;
            else
                nS = _noActiveSprite;

            _musicBtn.transform.GetChild(0).GetComponent<Image>().sprite = nS;
        });

        _openBtn.onClick.AddListener(() =>
        {
            _settingsPanel.SetActive(true);
        });

        _closeBtn.onClick.AddListener(() =>
        {
            _settingsPanel.SetActive(false);
        });

        _rateUs.onClick.AddListener(() =>
        {
            Device.RequestStoreReview();
        });

        _privacy.onClick.AddListener(() =>
        {
            TextPanelView.Instance.OpenTextViewer(TextPanelView.NameType.Privacy);
        });

        _terms.onClick.AddListener(() =>
        {
            TextPanelView.Instance.OpenTextViewer(TextPanelView.NameType.Terms);
        });

        if (PlayerPrefs.GetInt("Agree") != 1)
        {
            _panelWarn.SetActive(true);

            _privacyOpenBtn.onClick.AddListener(() =>
            {
                TextPanelView.Instance.OpenTextViewer(TextPanelView.NameType.Privacy);
            });

            _termsOpenBtn.onClick.AddListener(() =>
            {
                TextPanelView.Instance.OpenTextViewer(TextPanelView.NameType.Terms);
            });

            _checkerPrivacyBtn.onClick.AddListener(() =>
            {
                switch (isCheckPrivacy)
                {
                    case true:
                        _checkerPrivacyBtn.transform.GetChild(0).gameObject.SetActive(false);
                        isCheckPrivacy = false;
                        break;
                    case false:
                        _checkerPrivacyBtn.transform.GetChild(0).gameObject.SetActive(true);
                        isCheckPrivacy = true;
                        break;
                }

                CheckAllCheckerWarning();
            });

            _checkerTermsBtn.onClick.AddListener(() =>
            {
                switch (isCheckTerms)
                {
                    case true:
                        _checkerTermsBtn.transform.GetChild(0).gameObject.SetActive(false);
                        isCheckTerms = false;
                        break;
                    case false:
                        _checkerTermsBtn.transform.GetChild(0).gameObject.SetActive(true);
                        isCheckTerms = true;
                        break;
                }

                CheckAllCheckerWarning();
            });
        }
    }

    private void CheckAllCheckerWarning()
    {
        if (isCheckTerms && isCheckPrivacy)
        {
            PlayerPrefs.SetInt("Agree", 1);
            _panelWarn.SetActive(false);
        }
    }
}
