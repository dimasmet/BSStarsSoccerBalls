using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Records
{
    private int currentRecordValue;

    public Records()
    {
        currentRecordValue = PlayerPrefs.GetInt("Record");
    }

    public int GetRecordValue()
    {
        return currentRecordValue;
    }

    public bool CheckNewResultValue(int value)
    {
        if (value > currentRecordValue)
        {
            currentRecordValue = value;
            PlayerPrefs.SetInt("Record", currentRecordValue);
            return true;
        }
        else
            return false;
    }
}
