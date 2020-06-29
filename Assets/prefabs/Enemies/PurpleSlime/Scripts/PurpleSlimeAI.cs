using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleSlimeAI : MonoBehaviour {

	public bool debugMode;
	public float jumpDist = 300f;
	public float jumpTime = 4f;
	private float jumpTimer;
	public float jumpSpeed = 1f;
	public float playerDetectRange = 10f;

	private float speedTimer;
	private bool hasJumped;

	private Rigidbody2D mainBody;
	private GameObject player;
	private SpriteRenderer rend;
	private Animator anim;
	private SoundController sound;
	
	[HideInInspector]
	public bool isDead;

	// Use this for initialization
	void Start () {
		//get all the components needed
		mainBody = GetComponentInParent<Rigidbody2D> ();
		rend = GetComponentInParent<SpriteRenderer> ();
		anim = GetComponentInParent<Animator> ();
		player = GameObject.Find("Player");
		sound = GameObject.Find("AudioController").GetComponent<SoundController>();
	}
	
	// Update is called once per frame
	void Update () {
		//if debug mode is enabled, ignore the jumptimer and jump when Z is pressed
		if (debugMode) {
			if (Input.GetKeyDown (KeyCode.Z)) {
				hasJumped = true;
			}
		} else {
			//if slime is not dead allow everything to run
			if (!isDead) {
				//timer that counts up for jump
				jumpTimer += Time.deltaTime;

				//make the AI jump when the timer reaches max
				if (jumpTimer >= jumpTime) {
					hasJumped = true;
				}
			}
		}

		//run the jump function
		if (hasJumped) {
			Jump ();
		}

		if (isDead) {
			Die ();
		}


	}

	void Jump(){
		
		if (hasJumped) {
			//find the direction of the player
			Vector2 direction = transform.position - player.transform.position;
			//find the distance of the player
			float distance = Vector2.Distance (transform.position, player.transform.position);

			//set the animation to jumped
			anim.SetBool ("hasJumped", true);
			jumpSpeed = Random.Range (0.5f, 2);
			//timer of the about to jump animation before the actual jump
			speedTimer += Time.deltaTime;

			if (speedTimer >= jumpSpeed) {
				//jump up
				mainBody.AddForce (Vector2.up * jumpDist);

				//if the player is within range, check to see what direction he is then jump towards the player
				if (direction.x > 0 && distance < playerDetectRange) {
					//jump left at 1/3 the force of the main jump
					mainBody.AddForce (Vector2.left * (jumpDist / 3));
					//do not flip the image on the X
					rend.flipX = false;

				} else if (direction.x < 0 && distance < playerDetectRange) {
					//jump right at 1/3 the force of the main jump
					mainBody.AddForce (Vector2.right * (jumpDist / 3));
					//flip the image on the x
					rend.flipX = true;

					//If the player is not within range
				} else {
					//get a random value and jump in a random direction 50/50
					if (Random.value > .50f) {
						//jump right at 1/3 the force of the main jump
						mainBody.AddForce (Vector2.right * (jumpDist / 3));
						//flip the image on the x
						rend.flipX = true;
					} else {
						//jump left at 1/3 the force of the main jump
						mainBody.AddForce (Vector2.left * (jumpDist / 3));
						//do not flip the image on the X
						rend.flipX = false;
					}
				}

				//reset the animation, speedTimer, jumpTimer and hasJumped
				anim.SetBool ("hasJumped", false);
				speedTimer = 0;
				jumpTimer = 0;
				hasJumped = false;

			}
		}


	}

	void Die(){
		//set the animation to dead
		anim.SetBool ("isDead", true);
		//disable the colliders so that the slime can fall
		GetComponentInParent<CircleCollider2D> ().enabled = false;
		mainBody.gameObject.GetComponentInParent<BoxCollider2D> ().enabled = false;

	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "saw" || col.tag == "spikes") {
			isDead = true;
		}

		//if the slime is jumped on by the player
		if (col.tag == "Player") {
			isDead = true;
			if (col.tag == "Player") {
				col.SendMessage ("HeadHit");
				col.gameObject.GetComponentInChildren<SoundController> ().SlimeDieSound ();
			}
			ScoreSystem.score += 100;
		}
		//if the slime hits the garbage collector (large invisible zone below the level
		if (col.tag == "GarbageCollector") {
			Destroy (mainBody.gameObject);
		}

		if (col.gameObject.tag == "ground") {
			if (!isDead) {
				if(player == null) return;
				if (Vector2.Distance(player.transform.position, transform.position) < 13) {
					sound.SlimeSplatSound();
				}
			}
		}
	}
}
