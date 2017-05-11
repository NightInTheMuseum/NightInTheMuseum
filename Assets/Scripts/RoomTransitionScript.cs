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
public class RoomTransitionScript : MonoBehaviour {

	public int roomNumber;
	public float fadeDuration;
	public bool isExit;
	public bool isEndTurn;

	public static bool isChosen;
	public static bool isPaused;

	public Image blackScreen;
	public Sprite highlightedSprite;
	public Canvas uiPrefabCanvas;
	public Camera mainCamera;
	public Camera[] roomCameras;

	public PlayerTurnControllerScript turnTracker;

	private SpriteRenderer spriteRenderer;
	private Sprite normalSprite;

	static string ROOM = "Room";
	static string CAMERA = "Camera";

	// Screen fading is done by adjusting opacity of the black screen
	public static Color WHITE_OPAQUE = new Color(1, 1, 1, 1);
	public static Color WHITE_TRANSPARENT = new Color (1, 1, 1, 0);

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		normalSprite = spriteRenderer.sprite;
		isPaused = false;
	}

	void OnMouseOver () {
		if (!isPaused) {
			spriteRenderer.sprite = highlightedSprite;
		}
	}

	void OnMouseExit () {
		if (!isPaused) {
			spriteRenderer.sprite = normalSprite;
		}
	}

	void OnMouseDown () {
		if (!isPaused) {
			isChosen = true;

			if (isExit) {
				ExitRoom ();
			} else {
				EnterRoom ();
			}
		}
	}

	void EnterRoom () {
		/*
			 * 1. Fade screen to black
			 * 2. Inform turn tracker of who is taking next turn
			 * 3. Switch cameras (disable main camera, enable room camera)
			 * 4. Fade screen to visibility
		*/
		if (isChosen) {
			StartCoroutine (FadeScreen (WHITE_TRANSPARENT, WHITE_OPAQUE, fadeDuration));
			EnableRoomCamera ();
			SetUICameraToRoomCamera ();
			StartCoroutine (FadeScreen (WHITE_OPAQUE, WHITE_TRANSPARENT, fadeDuration));
		}
		isChosen = false;
	}

	void ExitRoom () {
		/*
			 * 1. Fade screen to black
			 * 2. Switch cameras (disable room camera, enable main camera)
			 * 3. Fade screen to visibility
		*/
		if (isChosen) {
			StartCoroutine (FadeScreen (WHITE_TRANSPARENT, WHITE_OPAQUE, fadeDuration));
			DisableRoomCamera ();
			SetUICameraToMainCamera ();
			if (isEndTurn) {
				turnTracker.SwapTurns ();
			}
			StartCoroutine (FadeScreen (WHITE_OPAQUE, WHITE_TRANSPARENT, fadeDuration));
		}
		isChosen = false;
	}

	/* ===== UTILITY/COMPUTATION FUNCTIONS DEFINITIONS  ===== */

	// Performs the fade-screen effect using a coroutine.
	public IEnumerator FadeScreen (Color startColor, Color endColor, float duration) {
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

	// Enable a target room's camera.
	void EnableRoomCamera () {
		string roomCameraName = ROOM + roomNumber.ToString () + CAMERA;
		roomCameras[roomNumber - 1].enabled = true;
	}

	// Disable a target room's camera.
	void DisableRoomCamera () {
		string roomCameraName = ROOM + roomNumber.ToString () + CAMERA;
		roomCameras[roomNumber - 1].enabled = false;
	}

	void SetUICameraToRoomCamera () {
		NotificationManager.Instance.targetCamera = roomCameras [roomNumber - 1];
		uiPrefabCanvas.worldCamera = roomCameras[roomNumber - 1];
	}

	void SetUICameraToMainCamera () {
		NotificationManager.Instance.targetCamera = mainCamera;
		uiPrefabCanvas.worldCamera = mainCamera;
	}
}
