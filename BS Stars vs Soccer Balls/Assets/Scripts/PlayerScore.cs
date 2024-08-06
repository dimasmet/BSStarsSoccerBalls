using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore
{
    private int _currentScore;
    private Text _text;

    public PlayerScore(Text _fieldText)
    {
        _text = _fieldText;
        GameSessionHandler.OnAddScore += AddScore;
    }

    public void StartScore()
    {
        _currentScore = 0;
        _text.text = _currentScore.ToString();
    }

    private void AddScore()
    {
        _currentScore++;
        _text.text = _currentScore.ToString();
    }

    public int GetResultScore()
    {
        return _currentScore;
    }
}
