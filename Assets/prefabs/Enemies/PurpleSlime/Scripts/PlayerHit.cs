using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour {

	private Rigidbody2D RB;
	public int jumpDist = 200;

	private PurpleSlimeAI ai;

	// Use this for initialization
	void Start () {
		RB = GetComponentInParent<Rigidbody2D> ();
		ai = transform.parent.gameObject.GetComponentInChildren<PurpleSlimeAI> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "PlayerHB") {
			col.transform.parent.GetComponent<PlayerHealth> ().Damage(2);
			col.gameObject.SendMessageUpwards("Hit");

			if (Random.value >= 0.5f) {
				RB.AddForce (Vector2.up * (jumpDist * 1.3f));
				RB.AddForce (Vector2.right * (jumpDist * 1.3f));
			} else {
				RB.AddForce (Vector2.up * (jumpDist * 1.3f));
				RB.AddForce (Vector2.left * (jumpDist * 1.3f));
			}
		}

		if(col.transform.parent == null) return;

		if (col.transform.parent.tag == "saw") {
			ai.isDead = true;
		}
	}
}
