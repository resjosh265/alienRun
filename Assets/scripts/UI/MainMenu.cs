using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class MainMenu : MonoBehaviour {

	public Sprite soundOn, soundOff;
	public Image sound;

	public static bool soundMute;

	public void Start(){
		DontDestroyOnLoad (this.gameObject);
	}

	public void StartGame(){
        SceneManager.LoadScene ("game", LoadSceneMode.Single);
	}

	public void ExitGame(){
		Application.Quit ();
	}

	public void Sound(){
		if (soundMute == false) {
			sound.sprite = soundOff;
			soundMute = true;
			GetComponent<AudioSource> ().Stop ();
		} else {
			sound.sprite = soundOn;
			soundMute = false;
			GetComponent<AudioSource> ().Play ();
		}
	}

}
