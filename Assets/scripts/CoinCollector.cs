using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			ScoreSystem.score += 1;
			col.gameObject.GetComponentInChildren<SoundController> ().CoinCollectSound ();
			Destroy (gameObject);
		}
	}
}
