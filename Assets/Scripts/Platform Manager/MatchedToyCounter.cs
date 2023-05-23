using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class MatchedToyCounter : MonoBehaviour
{
    int multiplier = 0;
    private float timer;
    float currentTime;

    public event Action<int> OnTimerChanged;

    private bool isGameActive;

    bool isPowerUpUsed = false;

    [SerializeField] private List<RectTransform> multiplierGameObjects;

    private MultiplierEffect multiplierEffect = new MultiplierEffect();
    private RectTransform multiplierRect;
    
    private void Update()
    {
        if(!isGameActive) { return; }
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            isPowerUpUsed = false;
            OnTimerChanged?.Invoke(multiplier);
        }
    }

    private void CloseMultiplierObject(float time, RectTransform rect)
    {
        rect.DORotate(multiplierEffect.baseRotation, time).SetEase(Ease.InBounce);
        rect.DOScale(multiplierEffect.baseScale, time).SetEase(Ease.InBounce).OnComplete(delegate
        {
            rect.gameObject.GetComponent<Image>().DOFade(0, time / 2).OnComplete(delegate
            {
                rect.localScale = multiplierEffect.baseScale;
                rect.localRotation = Quaternion.Euler(multiplierEffect.baseRotation);
                rect.gameObject.SetActive(false);
            });
        });
    }

    public void StartToyMatchCounter() => isGameActive = true;
    public void PauseToyMatchCounter() => isGameActive = false;

    public void StartTimeCounter(float timerStartStat)
    {
        //Multiplier sıfırlama

        foreach (RectTransform r in multiplierGameObjects)
        {
            CloseMultiplierObject(0.2f,r);
        }

        multiplierRect = null;
        timer = timerStartStat;
        currentTime = timer;
        multiplier = 0;
    }
}
