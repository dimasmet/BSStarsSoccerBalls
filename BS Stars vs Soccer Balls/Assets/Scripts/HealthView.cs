using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private GameObject[] _healthItems;

    public void SetCurrentValueHealth(int value)
    {
        int valueHealth = 2 - (value - 1);

        if (value >= 0)
        {
            for (int i = 0; i < valueHealth; i++)
            {
                _healthItems[i].SetActive(false);
            }
        }
    }

    public void ResetHealthView()
    {
        for (int i = 0; i < _healthItems.Length; i++)
        {
            _healthItems[i].SetActive(true);
        }
    }
}
