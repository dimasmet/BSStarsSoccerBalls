using UnityEngine;
using UnityEngine.UI;

public class ButtonSkin : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private Button _button;

    [SerializeField] private GameObject _choicedGo;

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            SkinHandler.Instance.SetSkin(number);
        });
    }

    public void Active(bool isActive)
    {
        _choicedGo.SetActive(isActive);
    }
}
