using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawAI : MonoBehaviour {

	public bool debugMode;

	private Rigidbody2D mainBody;
	public GameObject leftEdge, rightEdge;
	private Animator anim;

	public float moveSpeed = 0.05f;
	public float chaseSpeed = 0.10f;
	public float stopTime = 1;
	[Range (0.0f, 1f)]
	public float chaserChance = 0.5f;
	public float chaserDistance = 3;
	private float move, timer;

	private bool isStopped, isRight, isChasing, isChaser, soundChange;

	private int maskLayer = 9;

	// Use this for initialization
	void Start () {
		//get the rigidbody of the main gameobject
		mainBody = GetComponentInParent<Rigidbody2D> ();
		anim = GetComponentInParent<Animator> ();
		//figure out if this particular saw is a chaser
		if (Random.value < chaserChance) {
			isChaser = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//monitor distance to player
		float distance = Vector2.Distance (transform.position, GameObject.Find ("Player").transform.position);
		//monitor direction of the player
		Vector2 direction = transform.position - GameObject.Find("Player").transform.position;




		//if debug is enabled show the edge detection and rays
		if (debugMode == true) {
			Debug.DrawRay (leftEdge.transform.position, Vector2.down, Color.red);
			leftEdge.GetComponent<MeshRenderer> ().enabled = true;
			Debug.DrawRay (rightEdge.transform.position, Vector2.down, Color.red);
			rightEdge.GetComponent<MeshRenderer> ().enabled = true;
		}

		//move the saw
		mainBody.MovePosition (mainBody.position + Vector2.left * move);

		//if the saw is a chaser and the distance is less than specified
		if (isChaser == true && distance < chaserDistance) {
			//if the direction is left
			if (direction.x > 0) {
				isRight = false;
				move = chaseSpeed;
			} else {
				isRight = true;
				move = -chaseSpeed;
			}
			if (GetComponentInParent<AudioSource> ().pitch == 1) {
				GetComponentInParent<AudioSource>().pitch = 1.2f;
			}

			//set the animation to mad
			anim.SetBool("isChasing", true);
		} else {
			if (GetComponentInParent<AudioSource> ().pitch == 1.2f) {
				GetComponentInParent<AudioSource>().pitch = 1;
			}
			//set the animation to normal
			anim.SetBool("isChasing", false);

			//if the saw stops
			if (isStopped == true) {
				//set the move speed to 0
				//begin a timer to remain stopped
				timer += Time.deltaTime;
				if (timer >= stopTime) {
					isStopped = false;
					timer = 0;
				}
				//if not stopped
			} else {
				//set the move speed to go either left or right depending on last known direction
				if (isRight == false) {
					move = moveSpeed;
				} else {
					move = -moveSpeed;
				}


			}
		}
			


		//edge detection raycasts
		RaycastHit2D hitLeft = Physics2D.Raycast (leftEdge.transform.position, Vector2.down, 0.5f, 1 << maskLayer);
		RaycastHit2D hitRight = Physics2D.Raycast (rightEdge.transform.position, Vector2.down, 0.5f, 1 << maskLayer);

		//if is going left check if the raycast no longer detects the ground
		if (isRight == false) {
			if (hitLeft.collider == null) {
				if (debugMode == true) {
				}
				isStopped = true;
				move = 0;
				//if saw is a chaser, do not change the direction when chasing
				if (isChasing == false) {
					isRight = true;
				}
			}


		}

		//if is going right check if the raycast no longer detects the ground
		if (isRight == true) {
			if (hitRight.collider == null) {
				if (debugMode == true) {
				}
				isStopped = true;
				move = 0;
				//if saw is a chaser, do not change the direction when chasing
				if (isChasing == false) {
					isRight = false;
				}
			}
		}

	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.tag == "Player") {
			col.transform.GetComponent<PlayerHealth> ().Damage(3);
			col.gameObject.SendMessage ("Hit");
		}

		if (col.collider.tag == "box") {
			Destroy (col.gameObject);
		}

		if (col.collider.transform.parent.tag == "saw") {
			Destroy (transform.parent.gameObject);
		}

	}

	void ChangeSoundPitch(){
		if (isChasing == true) {
			GetComponent<AudioSource> ().pitch = 1.2f;
		} else {
			GetComponent<AudioSource> ().pitch = 1f;
		}

		soundChange = false;
	}
}
