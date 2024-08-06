using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private int _health;
    private HealthView _healthView;

    public Health(HealthView healthView)
    {
        _healthView = healthView;
    }

    public void NewHealth()
    {
        _health = 3;
        _healthView.ResetHealthView();
    }

    public void DiscreaseHealth()
    {
        if (_health > 0)
        {
            _health--;

            _healthView.SetCurrentValueHealth(_health);

            if (_health <= 0)
            {
                GameSessionHandler.OnEndGame?.Invoke();
            }
        }
    }
}
