using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour {

	private Rigidbody2D mainBody;
	private Animator anim;
	private BoxCollider2D deadBox;
	private CapsuleCollider2D hitBox;
	private Vector3 spawnedCoords;

	public float jumpTimer = 3;
	private float timer;
	public float jumpForce = 600;

	private bool isDead;

	// Use this for initialization
	void Start () {
		//get the components needed later in the script
		mainBody = GetComponentInParent<Rigidbody2D> ();
		anim = GetComponentInParent<Animator> ();
		deadBox = GetComponent<BoxCollider2D> ();
		hitBox = GetComponentInChildren<CapsuleCollider2D> ();

		//set the original spawned location of the fish
		spawnedCoords = mainBody.transform.position;
	}

	void FixedUpdate () {
		//typical timer that controls when the fish jumps
		timer += Time.deltaTime;

		if (timer >= jumpTimer) {
			Jump ();
		} else {
			//if the mainbody Y is equal to or less than the spawned location, check if the fish is dead, if not then turn off the simulation to stop the fish
			if (mainBody.transform.position.y <= spawnedCoords.y) {
				if (isDead == false) {
					mainBody.simulated = false;
				}
			}
		}

		//set the animation based on the velocity of the fish
		anim.SetFloat ("direction", mainBody.velocity.y);

		if (isDead == false) {
			//enable and disable colliders based on velocity
			if (mainBody.velocity.y < 0) {
				deadBox.enabled = false;
				hitBox.enabled = true;
			} else {
				deadBox.enabled = true;
				hitBox.enabled = false;
			}
		} else {
			deadBox.enabled = true;
		}


	}

	void Jump(){
		//turn on the simulation to allow movement
		mainBody.simulated = true;
		//make the fish jump
		mainBody.AddForce (Vector2.up * jumpForce);
		//reset the timer to 0
		timer = 0;
	}

	void Die(){
		//stop the fish from finishing its jump
		mainBody.velocity = new Vector2(0,0);


		//set the animation to dead
		anim.SetBool ("isDead", true);
		//make sure the fish doesnt stop simulation when its dead and falling
		isDead = true;
		ScoreSystem.score += 100;
	}

	void OnTriggerEnter2D(Collider2D col){
		//if the player hits the fish in the head
		if (col.tag == "Player") {
			//run the Die function
			Die ();
			//make the player run its Hit function
			col.SendMessage ("HeadHit");
		}
		//if the fish hits the garbage collector (large invisible zone below the level)
		if (col.tag == "GarbageCollector") {
			Destroy (mainBody.gameObject);
		}
	}
}
