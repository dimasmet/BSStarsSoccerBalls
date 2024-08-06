using UnityEngine;
using UnityEngine.UI;

public class ResultGameView : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Text _resultText;
    [SerializeField] private Animator _animation;

    [SerializeField] private GameObject _rewardObj;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _exitBtn;

    private void Awake()
    {
        _restartBtn.onClick.AddListener(() =>
        {
            _animation.Play("idle");
        });

        _exitBtn.onClick.AddListener(() =>
        {
            _animation.Play("idle");
        });
    }

    public void OpenResultView(int scoreValue, bool isRecordNew)
    {
        _rewardObj.SetActive(isRecordNew);

        _panel.SetActive(true);

        _resultText.text = scoreValue.ToString();

        Debug.Log("OpenResult");
        _animation.Play("Open");
    }
}
