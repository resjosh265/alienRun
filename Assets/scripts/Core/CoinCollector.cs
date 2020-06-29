using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			var soundController = col.gameObject.GetComponentInChildren<SoundController>();
			
			ScoreSystem.score += 1;
			soundController.CoinCollectSound ();
			Destroy (gameObject);
		}
	}
}
