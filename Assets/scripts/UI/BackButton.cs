using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {

    public GameObject mainMenu, androidControls, androidJump, menuButton;

    public void Update()
    {
        if (!mainMenu.active)
        {
            menuButton.SetActive(true);
        }
    }

    public void Menu()
    {
        mainMenu.SetActive(true);
        androidControls.SetActive(false);
        androidJump.SetActive(false);
        menuButton.SetActive(false);
    }
}

