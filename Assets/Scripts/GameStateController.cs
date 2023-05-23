using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    ToyManager toyManager;
    [SerializeField] TimeManager timeManager;

    public event Action OnLevelSuccess;
    public event Action OnLevelFail;

    bool isGameActive = false;


    private void Start()
    {
        toyManager = GameReferenceHandler.instance.ToyManager;
    }

    private void Update()
    {
        if (!isGameActive) { return; }
        CheckTimeLeft();
        CheckToysLeft();
    }

    private void CheckTimeLeft()
    {
        if (timeManager.GetCurrentSeconds() <= 0 && toyManager.ToyPool.Count > 0)
        {
            OnLevelFail?.Invoke();
            isGameActive = false;
        }
    }


    private void CheckToysLeft()
    {
        if (toyManager.ToyPool.Count <= 0 && timeManager.GetCurrentSeconds() > 0)
        {
            OnLevelSuccess?.Invoke();
            
            isGameActive = false;
        }
    }
    
    public void ActivateGameState(bool isActive)
    {
        isGameActive = isActive;
    }
}
