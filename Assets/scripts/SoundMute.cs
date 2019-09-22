using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMute : MonoBehaviour {

	public GameObject music;
	public Sprite soundOn, soundOff;
	public Image sound;

	public static bool soundMute;

	// Use this for initialization
	public void Start(){
		music = GameObject.Find ("Music");
	}

	public void Sound(){
		if (soundMute == false) {
			sound.sprite = soundOff;
			soundMute = true;
			music.GetComponent<AudioSource> ().Stop ();
		} else {
			sound.sprite = soundOn;
			soundMute = false;
			music.GetComponent<AudioSource> ().Play ();
		}
	}
}
