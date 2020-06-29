using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Finish : MonoBehaviour {

    public GameObject mainMenu, androidControls, androidJump, menu;

    public void Restart(){
        SceneManager.LoadScene ("game", LoadSceneMode.Single);
		ScoreSystem.score = 0;
	}

	public void ExitGame(){
		Application.Quit ();
	}

    public void Resume()
    {
        if (PlayerController.android == true)
        {
            androidControls.SetActive(true);
            androidJump.SetActive(true);
        }

        menu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Menu()
    {
        mainMenu.SetActive(true);
        androidControls.SetActive(false);
        androidJump.SetActive(false);
        menu.SetActive(false);
    }
}
