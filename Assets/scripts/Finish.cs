using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Finish : MonoBehaviour {

    public GameObject mainMenu, andControls, andJump, menu;

    public void Restart(){
        /*
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        */
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
            andControls.SetActive(true);
            andJump.SetActive(true);
        }
        menu.SetActive(true);
        mainMenu.SetActive(false);

    }

    public void Menu()
    {
        mainMenu.SetActive(true);
        andControls.SetActive(false);
        andJump.SetActive(false);
        menu.SetActive(false);
    }



}
