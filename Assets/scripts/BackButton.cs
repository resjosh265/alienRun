using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {

    public GameObject mainMenu, andControls, andJump, menu;

    public void Update()
    {
        if (mainMenu.active == false)
        {
            menu.SetActive(true);
        }
    }

    public void Menu()
    {
        
        mainMenu.SetActive(true);

        andControls.SetActive(false);
        andJump.SetActive(false);
        menu.SetActive(false);
    }
}
