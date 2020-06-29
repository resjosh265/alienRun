using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static bool android;

	public float moveSpeed = 5f;
	public float jumpDist = 350f;

	private bool isFlipped;
	private bool isGrounded;
	private float isMoving;

	private Rigidbody2D playerRB;
	private SpriteRenderer rend;
	private Animator anim;
	private Renderer bgRend;
	private SoundController sound;
	private PlayerHealth health;

	public GameObject background;
	public GameObject cam, endGameGUI;
	private float camYPos;

	float move;

	public bool moveLeft, moveRight, isAndroid;

	public GameObject androidMove, jumpButton;

	private bool beenHit;

	// Use this for initialization
	void Start () {
        android = isAndroid;

		playerRB = GetComponent<Rigidbody2D> ();
		rend = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		bgRend = background.GetComponent<Renderer> ();
		sound = GetComponentInChildren<SoundController> ();
		health = GetComponent<PlayerHealth> ();

		if (isAndroid == true) {

			androidMove.SetActive (true);
			jumpButton.SetActive (true);
		}
	}

	void Update(){
		if (!health.isDead) {
			if (isGrounded) {
				if (Input.GetButtonDown ("Jump")) {
					Jump ();
				}
			}

			float test = move * .005f;
			Vector2 offset = new Vector2 (test, 0);
			//stop the bg from moving when the player is stuck yet still moving (NOT FAST ENOUGH)
			if (transform.position.x != isMoving) {
			
				bgRend.material.mainTextureOffset += offset;
				isMoving = transform.position.x;
			}
		}
	}

	public void MoveLeft(){
		moveLeft = true;
	}

	public void StopMoveLeft(){
		moveLeft = false;
	}

	public void MoveRight(){
		moveRight = true;
	}

	public void StopMoveRight(){
		moveRight = false;
	}

	public void Jump(){
		if (playerRB.velocity.y < 1) {
			if (isGrounded) {
				playerRB.AddForce (Vector2.up * jumpDist);
				isGrounded = false;
				sound.JumpSound ();
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (beenHit) {
			if (playerRB.velocity.y < 0.1f) {
				beenHit = false;
			}
		}

		if (!health.isDead){
			if (isAndroid) {
				float t = 0f;
				if (moveLeft) {
					move = -1;
				}

				if (moveRight) {
					move = 1;
				}

				if (!moveRight && !moveLeft) {
					move = 0;
				}
			} else {
				move = Input.GetAxis ("Horizontal");
			}

			Vector2 raycastPos = new Vector2(transform.position.x, transform.position.y - 1);
			if (Physics2D.Raycast(raycastPos, Vector2.left, 0.45f)) {
				if (move < 0) {
					playerRB.velocity = new Vector2 (0, playerRB.velocity.y);
				}

				if (move > 0) {
					playerRB.velocity = new Vector2 (move * moveSpeed, playerRB.velocity.y);
				}
			} else if (Physics2D.Raycast(raycastPos, Vector2.right, 0.45f)) {
				if (move > 0) {
					playerRB.velocity = new Vector2 (0, playerRB.velocity.y);
				}

				if (move < 0) {
					playerRB.velocity = new Vector2 (move * moveSpeed, playerRB.velocity.y);
				}
			} else {
				playerRB.velocity = new Vector2 (move * moveSpeed, playerRB.velocity.y);
			}

		if (move < 0) {
			isFlipped = true;
		} else if(move > 0) {
			isFlipped = false;
		}

		if (move != 0) {
			anim.SetBool ("isWalking", true);
		}else
			anim.SetBool ("isWalking", false);

		if (!isGrounded){
			anim.SetBool ("hasJumped", true);
		}else
			anim.SetBool ("hasJumped", false);

		Flip ();
		}
			


	}

	void Flip(){
		rend.flipX = isFlipped;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "ground" || col.gameObject.tag == "box") {
			isGrounded = true;
		}

		if (col.gameObject.tag == "spikes") {
			Hit ();
			GetComponent<PlayerHealth> ().Damage (1);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "EndGame") {
			endGameGUI.transform.GetChild (3).gameObject.SetActive (true);
			endGameGUI.SetActive (true);
		}
	}

	void Hit(){
		if (!beenHit) {
			if (Random.value >= 0.5f) {
				playerRB.AddForce (Vector2.up * (jumpDist * 1.3f));
				playerRB.AddForce (Vector2.right * (jumpDist * 1.3f));

			} else {
				playerRB.AddForce (Vector2.up * (jumpDist * 1.3f));
				playerRB.AddForce (Vector2.left * (jumpDist * 1.3f));
			}
			beenHit = true;
		}

		anim.SetTrigger ("Hit");
		sound.Damaged ();
	}

	void HeadHit(){
		playerRB.AddForce (Vector2.up * (jumpDist * 1.3f));
	}
}
