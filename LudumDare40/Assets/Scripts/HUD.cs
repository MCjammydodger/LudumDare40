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

	// Use this for initialization
	private void Awake () {
        instance = this;
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

    public void SetHealthBar(float value)
    {
        healthBar.value = value;
    }

    public void SetWeightText(float totalWeight)
    {
        weightText.text = "Total Weight: " + totalWeight;
    }

    public void ShowGameoverScreen()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
