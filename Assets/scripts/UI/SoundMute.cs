using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMute : MonoBehaviour {

	public static bool soundMute;

	public Sprite soundOn, soundOff;
	public Image sound;

	private AudioSource _audioSource;

	// Use this for initialization
	public void Start(){
		_audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
	}

	public void Sound(){
		if (!soundMute) {
			sound.sprite = soundOff;
			soundMute = true;
			_audioSource.Stop();
		} else {
			sound.sprite = soundOn;
			soundMute = false;
			_audioSource.Play();
		}
	}
}
