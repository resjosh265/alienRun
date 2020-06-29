using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class SoundController : MonoBehaviour {

	private AudioSource soundSource;

	public AudioClip coinCollect, jump, slimeSplat, heart, slimeDie, hit;

	// Use this for initialization
	void Start () {
		soundSource = GetComponent<AudioSource> ();
	}

	public void CoinCollectSound(){
		soundSource.PlayOneShot (coinCollect, 0.3f);
	}

	public void JumpSound(){
		soundSource.PlayOneShot (jump, 0.3f);
	}
		
	public void SlimeSplatSound(){
		soundSource.PlayOneShot (slimeSplat, 0.3f);
	}

	public void HeartCollectSound(){
		soundSource.PlayOneShot (heart, 0.3f);
	}

	public void SlimeDieSound(){
		soundSource.PlayOneShot (slimeDie, 0.3f);
	}

	public void Damaged(){
		soundSource.PlayOneShot (hit, 0.3f);
	}
}
