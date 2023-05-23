using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] MatchedToyCounter toyCounter;

    public int CurrentScore => _currentScore;
    int _currentScore = 0;

    int _baseIncrementValue = 1;
    int _multiplierValue = 1;

    private void Start()
    {
        toyCounter.OnTimerChanged += SetMultiplierValue;
    }

    public void ChangeScoreValue()
    {
        _currentScore += GetIncrementValue();
    }

    int GetIncrementValue()
    {
        return _baseIncrementValue * _multiplierValue;
    }

    void SetMultiplierValue(int multiplier)
    {
        _multiplierValue = multiplier;
    }

    public void ResetScore()
    {
        _currentScore = 0;
        _multiplierValue = 0;
    }

}
