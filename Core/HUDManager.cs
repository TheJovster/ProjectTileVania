using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    string starterArea = "Dungeon Cathedral";
    [SerializeField] private Slider healthBar;
    //[SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI areaText;
    [SerializeField] private TextMeshProUGUI interactPrompt;
    private Health playerHealth;
    private GameManager gameManager;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        SetUpHealthBar();
        UpdateAreaText(starterArea);
    }

    
    void Update()
    {
        UpdateHealthBar();
        //UpdateScoreText();
    }

    private void SetUpHealthBar() 
    {
        healthBar.maxValue = playerHealth.GetMaxHealth();
    }

    private void UpdateHealthBar() 
    {
        healthBar.value = playerHealth.GetCurrentHealth();
    }

    public void UpdateAreaText(string areaName)
    {
        areaText.text = "Area: " + areaName;
    }

/*    void UpdateScoreText()
    {
        scoreText.text = "Score: " + gameManager.GetCurrentScore();
    }*/

    public void UpdateInteractableObjectPrompt(string prompt) 
    {
        interactPrompt.text = prompt;
    }
}
