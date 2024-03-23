using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] ShopManager shopManager;

    [Header("Elemenets")]
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] GameObject shopPanel;
    [SerializeField] Slider progressBar;
    [SerializeField] TMP_Text levelText;
    void Start()
    {
        progressBar.value = 0;
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        settingsPanel.SetActive(false);
        shopPanel.SetActive(false);
        levelText.text = "Level: " + (RoadManager.instance.GetLevel() + 1);

        GameManager.onGameStateChanged += GameStateCallback;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChanged -= GameStateCallback;
    }

    private void GameStateCallback(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.GameOver)
        {
            ShowGameOverPanel(); 
        }
        else if(gameState == GameManager.GameState.LevelComplete)
        {
            ShowLevelCompletePanel();
        }
    }

    void Update()
    {
        UpdateProgressBar();
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void TryAgainButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowGameOverPanel()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    private void ShowLevelCompletePanel()
    {
        StartCoroutine(WaitBeforeShowingCompletePanel());
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.instance.IsGameState()) { return; }

        float progress = PlayerController.instance.transform.position.z / RoadManager.instance.GetFinishPositionZ();

        progressBar.value = progress;
    }

    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void HideSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void ShowShop()
    {
        shopPanel.SetActive(true);
        menuPanel.SetActive(false);
        shopManager.UpdatePurchaseButton();
    }

    public void HideShop()
    {
        menuPanel.SetActive(true);
        shopPanel.SetActive(false);
    }

    private IEnumerator WaitBeforeShowingCompletePanel()
    {
        yield return new WaitForSeconds(1.5f);
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }

    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
