using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetect : MonoBehaviour {

	public bool box, coin, ground;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (box == true) {
			if (col.tag == "box") {
				transform.position = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
			}
		}

		if (col.tag == "Player") {
		}

		if (coin == true) {
			if (col.tag == "CoinCollector") {
				Destroy (gameObject);
			}
		}

		if (ground == true) {
			if (col.tag == "ground") {
				Destroy (gameObject);
			}
		}
	}
}
