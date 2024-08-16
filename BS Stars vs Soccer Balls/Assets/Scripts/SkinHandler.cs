using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinHandler : MonoBehaviour
{
    public static SkinHandler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    [SerializeField] private Shuriken shuriken;
    [SerializeField] private Sprite[] _skins;
    [SerializeField] private ButtonSkin[] _button;
    [SerializeField] private ButtonSkin _currentButtonSkin;

    public void SetSkin(int number)
    {
        if (_currentButtonSkin != null)
        {
            _currentButtonSkin.Active(false);
        }

        _currentButtonSkin = _button[number];
        _currentButtonSkin.Active(true);

        shuriken.SetSkin(number);
    }

}
