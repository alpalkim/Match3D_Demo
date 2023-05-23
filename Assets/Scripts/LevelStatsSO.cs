using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Level Stats")]
public class LevelStatsSO : ScriptableObject
{
    public LevelStats[] LevelStats;
    public LevelStats GetLevelStats(int playerLevel)
    {
        if (playerLevel <= 0)
        {
            return LevelStats[0];
        }

        switch (LevelStats[playerLevel - 1].Difficulty)
        {
            case 1:
                LevelStats[playerLevel - 1].MultiplierTimerStart = 6;
                break;
            case 2:
                LevelStats[playerLevel - 1].MultiplierTimerStart = 6;
                break;
            case 3:
                LevelStats[playerLevel - 1].MultiplierTimerStart = 8;
                break;
            case 4:
                LevelStats[playerLevel - 1].MultiplierTimerStart = 10;
                break;
            case 5:
                LevelStats[playerLevel - 1].MultiplierTimerStart = 10;
                break;
        }

        return LevelStats[playerLevel - 1];
    }
}