using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

	public string role;
	public bool canMove;
	public float speed;
	public float offsetXFromPlayer;
	public float offsetYFromPlayer;

	private Rigidbody2D rb;

	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (canMove) {
			if (role == "Ghost") {
				if (Input.GetKey (KeyCode.A)) {
					MoveLeft ();
				} else if (Input.GetKey (KeyCode.D)) {
					MoveRight ();
				} else if (Input.GetKey (KeyCode.W)) {
					MoveUp ();
				} else if (Input.GetKey (KeyCode.S)) {
					MoveDown ();
				}
			} else if (role == "Police") {
				if (Input.GetKey (KeyCode.LeftArrow)) {
					MoveLeft ();
				} else if (Input.GetKey (KeyCode.RightArrow)) {
					MoveRight ();
				} else if (Input.GetKey (KeyCode.UpArrow)) {
					MoveUp ();
				} else if (Input.GetKey (KeyCode.DownArrow)) {
					MoveDown ();
				}
			}
		}
	}

	/* ===== PRIMITIVE METHODS DEFINITIONS ===== */

	void MoveLeft () {
		rb.MovePosition(transform.position + Vector3.left * speed);
	}

	void MoveRight () {
		rb.MovePosition(transform.position + Vector3.right * speed);
	}

	void MoveUp () {
		rb.MovePosition(transform.position + Vector3.up * speed);
	}

	void MoveDown () {
		rb.MovePosition(transform.position + Vector3.down * speed);
	}
}
