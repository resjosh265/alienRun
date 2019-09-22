using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollect : MonoBehaviour {

	public int healAmmount = 2;

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			col.gameObject.GetComponent<PlayerHealth> ().HeartCollect (healAmmount);
			col.gameObject.GetComponentInChildren<SoundController> ().HeartCollectSound ();
			Destroy (gameObject);
		}
	}
}
