using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class SaveData
{
    public int highestKillCount;

    public SaveData(int highestKillCount)
    {
        this.highestKillCount = highestKillCount;
    }

    public SaveData() {
        highestKillCount = 0;
    }
}
