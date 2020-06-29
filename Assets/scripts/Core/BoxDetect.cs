using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetect : MonoBehaviour {

	public bool box, coin, ground;

	void OnTriggerEnter2D(Collider2D col){
		if (box) {
			if (col.tag == "box") {
				transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
			}
		}

		if (col.tag == "Player") {
		}

		if (coin) {
			if (col.tag == "CoinCollector") {
				Destroy (gameObject);
			}
		}

		if (ground) {
			if (col.tag == "ground") {
				Destroy (gameObject);
			}
		}
	}
}
