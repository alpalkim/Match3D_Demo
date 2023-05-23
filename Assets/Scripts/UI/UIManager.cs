using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameStateController gameState;
    [SerializeField] GameObject winPanel;
    [SerializeField] private GameObject LosePopUp;
    [SerializeField] private GameManager gameManager;

    private static UIManager instance { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameState.OnLevelSuccess += DisplayWinScreen;
    }

    public void OpenLosePopUp()
    {
        LosePopUp.SetActive(true);
    }
    public void RetryButton()
    {
        LosePopUp.SetActive(false);
        gameManager.OnLevelStart();
    }

    private void DisplayWinScreen()
    {
        winPanel.SetActive(true);
    }
}