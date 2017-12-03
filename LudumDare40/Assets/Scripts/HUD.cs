using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour {

    public static HUD instance;

    [SerializeField]
    private Slider healthBar;

    [SerializeField]
    private Text weightText;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject victoryPanel;
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private Text countdownText;

    private bool inventoryShown;

	// Use this for initialization
	private void Awake () {
        instance = this;
	}
	
	// Update is called once per frame
	private void Update () {
        if (Input.GetKey(KeyCode.Escape) && !pauseMenu.activeSelf)
        {
            ShowPauseMenu();
        }
	}

    public void SetHealthBar(float value)
    {
        healthBar.value = value;
    }

    public void SetCountdownText(int value)
    {
        countdownText.text = "Time until big explosion: " + value.ToString();
    }

    public void SetWeightText(float totalWeight)
    {
        weightText.text = "Total Weight: " + totalWeight;
    }

    public void ShowGameoverScreen()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowVictoryScreen()
    {
        victoryPanel.SetActive(true);
    }

    public void RestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        GameManager.instance.PauseGame(true);
        inventoryShown = GameManager.instance.GetInventoryUI().gameObject.activeSelf;
        GameManager.instance.GetInventoryUI().gameObject.SetActive(false);
    }

    public void ResumeClicked()
    {
        pauseMenu.SetActive(false);
        GameManager.instance.PauseGame(false);
        GameManager.instance.GetInventoryUI().gameObject.SetActive(inventoryShown);
    }

    public void ReturnClicked()
    {
        SceneManager.LoadScene(0);
    }
}
