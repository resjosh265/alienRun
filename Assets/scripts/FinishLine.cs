using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

	public GameObject endGameGUI;

	void Start(){
		endGameGUI = GameObject.Find ("endgame");
	}

	void OnTriggerEnter2D(){
		endGameGUI.transform.GetChild (2).gameObject.SetActive (true);
		endGameGUI.SetActive (true);
	}
}