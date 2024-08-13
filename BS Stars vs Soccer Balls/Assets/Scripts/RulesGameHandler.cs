using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesGameHandler : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject[] _rulesGame;
    private GameObject _currentPanel;

    [SerializeField] private Button _okButton;

    private int numRules = 0;

    private void Awake()
    {
        _okButton.onClick.AddListener(() =>
        {
            if (_currentPanel != null) _currentPanel.SetActive(false);

            numRules++;
            if (numRules < _rulesGame.Length)
                OpenRulesPanel(numRules);
            else
            {
                _mainPanel.SetActive(false);
                GameSessionHandler.OnStartSessionGame?.Invoke();
            }

            SoundsGame.I.RunSound(SoundsGame.Sound.Click);
        });
    }

    public void ActiveRules()
    {
        numRules = 0;
        OpenRulesPanel(0);
        _mainPanel.SetActive(true);
    }

    private void OpenRulesPanel(int number)
    {
        _currentPanel = _rulesGame[number];
        _currentPanel.SetActive(true);
    }
}
