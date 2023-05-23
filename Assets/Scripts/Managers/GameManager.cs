using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] ScoreManager scoreManager;
    [SerializeField] TimeManager timeManager;
    [SerializeField] MatchedToyCounter toyCounter;
    [SerializeField] GameStateController gameState;
    [SerializeField] LevelStatsSO levelStatsSo;
    [SerializeField] private UIManager UIManager;

    LevelStats currentLevelStats;
    ToyManager toyManager;

    private void Start()
    {
        gameState.OnLevelSuccess += OnLevelSuccess;
        gameState.OnLevelFail += OnPlayerFailed;
        toyManager = GameReferenceHandler.instance.ToyManager;
        OnLevelStart();
    }

    public void OnLevelStart()
    {
        gameState.ActivateGameState(true);
        ResetLevel();
        LoadCurrentLevel();
        toyCounter.StartTimeCounter(currentLevelStats.MultiplierTimerStart);
        StartCoroutine(WaitForToySpawn());
    }


    private void LoadCurrentLevel()
    {
        SetCurrentLevelStats();
        timeManager.SetTimer(currentLevelStats.LevelSeconds);
        toyManager.SetToyPool(currentLevelStats.ToyCount);
    }

    private void OnLevelSuccess()
    {
        PlayerDataHandler.instance.IncreasePlayerLevel(levelStatsSo.LevelStats.Length);
        PlayerDataHandler.instance.SavePlayerStats();
    }

    private void OnPlayerFailed()
    {
        timeManager.StopTimer();
        UIManager.OpenLosePopUp();

    }

    private void ResetLevel()
    {
        timeManager.ResetTimer();
        toyManager.ResetToyPool();
        scoreManager.ResetScore();
        toyCounter.PauseToyMatchCounter();

    }

    private void SetCurrentLevelStats()
    {
        currentLevelStats = levelStatsSo.GetLevelStats(PlayerDataHandler.instance.GetPlayerCurrentLevel());
    }


    IEnumerator WaitForToySpawn()
    {
        yield return new WaitForSeconds(0.1f);
        toyManager.DisableToyRigidbody();
        timeManager.StartTimer(); 
        toyCounter.StartToyMatchCounter();
        toyManager.EnableToyRigidbody();
    }
}
