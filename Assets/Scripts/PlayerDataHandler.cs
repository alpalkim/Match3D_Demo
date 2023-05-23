using System.Collections;
using UnityEngine;

public class PlayerDataHandler : MonoBehaviour
{
    private Player player;

    public static PlayerDataHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadPlayerStats();
        DontDestroyOnLoad(gameObject);
    }

    public int GetPlayerCurrentLevel()
    {
        return player.GetPlayerLevel();
    }

    public void IncreasePlayerLevel(int maxLevelsCount)
    {
        player.IncreasePlayerLevel(maxLevelsCount);
        SavePlayerStats();
    }

    public void SavePlayerStats()
    {
        PlayerDataObject playerDataObject = new PlayerDataObject();

        playerDataObject.level = player.GetPlayerLevel();
        // Save scripts..
    }

    private void CreateNewPlayer()
    {
        player = new Player(1);
        SavePlayerStats();
    }


    private void LoadPlayerStats()
    {
        StartCoroutine(LoginUnityEditor());
    }

    private IEnumerator LoginUnityEditor()
    {
        CreateNewPlayer();
        yield break;
    }
}