using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    [SerializeField]
    private PlayableDirector director;
    [SerializeField]
    private GameObject menuUI;

    private static bool firstTime = true;

    private bool started = false;
	// Use this for initialization
	void Start () {
        started = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (started)
        {
            if(!firstTime || director.state != PlayState.Playing)
            {
                firstTime = false;
                SceneManager.LoadScene(1);
            }
        }
	}

    public void StartButtonClicked()
    {
        menuUI.SetActive(false);
        director.enabled = true;
        started = true;
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
