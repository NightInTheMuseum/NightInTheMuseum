using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This script handles the transition when any player (ghost or police)
 * enters an interactive room.
 * 
 * This script will invoke some methods in PlayerTurnControllerScript for the
 * transition when the player (ghost or police) leaves the interactive room.
 */
public class RoomEnterScript : MonoBehaviour {

	public int roomNumber;
	public float fadeDuration;
	public Image blackScreen;
	public PlayerTurnControllerScript turnTracker;

	static string ROOM = "Room";
	static string CAMERA = "Camera";

	// Screen fading is done by adjusting opacity of the black screen
	static Color WHITE_OPAQUE = new Color(1, 1, 1, 1);
	static Color WHITE_TRANSPARENT = new Color (1, 1, 1, 0);

	GameObject playerGhost;
	GameObject playerPolice;
	PlayerMovementScript ghostMovement;
	PlayerMovementScript policeMovement;
	Camera playerGhostCamera;
	Camera playerPoliceCamera;

	void Awake () {
		playerGhost = GameObject.FindGameObjectWithTag ("PlayerGhost");
		playerPolice = GameObject.FindGameObjectWithTag ("PlayerPolice");
		ghostMovement = playerGhost.GetComponent<PlayerMovementScript> ();
		policeMovement = playerPolice.GetComponent<PlayerMovementScript> ();
		playerGhostCamera = GameObject.Find ("PlayerGhostCamera").GetComponent<Camera> ();
		playerPoliceCamera = GameObject.Find ("PlayerPoliceCamera").GetComponent<Camera> ();
		turnTracker = GameObject.Find ("PlayerTurnController").GetComponent<PlayerTurnControllerScript> ();
	}

	IEnumerator OnTriggerEnter2D (Collider2D other) {
		if (isPlayer (other.gameObject)) {
			yield return StartCoroutine(TransitToRoom());
		}
	}

	IEnumerator TransitToRoom () {
		/*
			 * 1. Disable player movement
			 * 2. Fade screen to black
			 * 3. Disable player camera and hide player
			 * 4. Enable the room's camera
			 * 5. Fade screen to visibility
		*/
		DisablePlayerMovement ();
		yield return StartCoroutine(FadeScreen (WHITE_TRANSPARENT, WHITE_OPAQUE, fadeDuration));
		DisablePlayerCamera ();
		HidePlayer ();
		turnTracker.SwapTurns ();
		EnableRoomCamera ();
		yield return StartCoroutine(FadeScreen (WHITE_OPAQUE, WHITE_TRANSPARENT, fadeDuration));
	}

	IEnumerator ExitRoom () {
		/*
			 * 1. Fade screen to black
			 * 2. Disable room's camera
			 * 3. Enable next player's camera and reveal next player
			 * 4. Fade screen to visibility
			 * 5. Enable next player's movement
		*/
		yield return StartCoroutine(FadeScreen (WHITE_TRANSPARENT, WHITE_OPAQUE, fadeDuration));
		DisableRoomCamera ();
		turnTracker.EnableNextPlayerCamera ();
		yield return StartCoroutine(FadeScreen (WHITE_OPAQUE, WHITE_TRANSPARENT, fadeDuration));
		turnTracker.EnableNextPlayerMovement ();
	}

	/* ===== UTILITY/COMPUTATION FUNCTIONS DEFINITIONS  ===== */

	// Checks if game object is ghost or police player.
	bool isPlayer (GameObject obj) {
		return obj.gameObject.CompareTag ("PlayerGhost") || obj.gameObject.CompareTag ("PlayerPolice");
	}

	// Disables the current player's movement.
	void DisablePlayerMovement () {
		if (playerGhost.activeSelf) {
			ghostMovement.canMove = false;
		} else if (playerPolice.activeSelf) {
			policeMovement.canMove = false;
		}
	}

	// Hides the current player by disabling him/her.
	void HidePlayer () {
		if (playerGhost.activeSelf) {
			playerGhost.SetActive (false);
		} else if (playerPolice.activeSelf) {
			playerPolice.SetActive (false);
		}
	}

	// Performs the fade-screen effect using a coroutine.
	IEnumerator FadeScreen (Color startColor, Color endColor, float duration) {
		float start = Time.time;
		float elapsed = 0;
		while (elapsed < duration) {
			elapsed = Time.time - start;

			// derive parameter t based on how much time has elapsed since start time
			float normalizedTime = Mathf.Clamp (elapsed / duration, 0, 1);

			// perform the fading
			blackScreen.color = Color.Lerp (startColor, endColor, normalizedTime);

			// wait for next frame
			yield return null;
		}
	}

	// Disables the current player's camera.
	void DisablePlayerCamera () {
		if (playerGhostCamera.enabled) {
			playerGhostCamera.enabled = false;
		} else if (playerPoliceCamera.enabled) {
			playerPoliceCamera.enabled = false;
		}
	}

	// Enable a target room's camera.
	void EnableRoomCamera () {
		string roomCameraName = ROOM + roomNumber.ToString () + CAMERA;
		GameObject.Find (roomCameraName).GetComponent<Camera> ().enabled = true;
	}

	// Disable a target room's camera.
	void DisableRoomCamera () {
		string roomCameraName = ROOM + roomNumber.ToString () + CAMERA;
		GameObject.Find (roomCameraName).GetComponent<Camera> ().enabled = false;
	}
}
