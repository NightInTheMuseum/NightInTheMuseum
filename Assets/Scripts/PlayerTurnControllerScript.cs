﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnControllerScript : MonoBehaviour {

	const float TIME_LIMIT = 120;		// 2 minutes, in seconds
	const int MAX_NUM_TURNS_PER_PLAYER = 3;
	const int MAX_MOVABLE_OBJECTS = 5;

	/* A turn-tracking variable.
	 * True => ghost takes the turn, False => police takes the turn.
	 * 
	 * This variable needs to be set by the interactive rooms and
	 * is, therefore, made public.
	 */
	public bool isGhostTurn = true;
	public int turnsTakenPlace;
	public List<Transform> movedObjects;
	public int numDetectives;
	public Image blackScreen;
	public float timer;
	public Image ghost;
	public Image[] detectiveIcons;
	public Sprite ghostNormal;
	public Sprite ghostActive;
	public Sprite detectiveNormal;
	public Sprite detectiveActive;

	public int answer = 2;		// for storing the killer solution
	public bool hasCorrectGuess;

	private int turnId;
	private int totalPermissibleTurns;
	private bool allTurnsUsedUp;
    LevelLoadHandler _levelHandler;

    private void Awake()
    {
        _levelHandler = FindObjectOfType<LevelLoadHandler>();

        if (_levelHandler != null)
        {
            numDetectives = _levelHandler.returnDect() - 1;
			_levelHandler.initializeDetectives();

            Destroy(_levelHandler.gameObject);
        }
    }

    void Start () {
		movedObjects = new List<Transform>();
		totalPermissibleTurns = (numDetectives + 1) * MAX_NUM_TURNS_PER_PLAYER;
		timer = TIME_LIMIT;
		ghost.sprite = ghostActive;
		for (int i = 2; i >= numDetectives; i--) {
			detectiveIcons [i].enabled = false;
		}
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			if (!RoomTransitionScript.isPaused) {
				UIManager.Instance.pause_btn ();
			}
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			if (RoomTransitionScript.isPaused) {
				UIManager.Instance.pause_btn ();
			}
		}
		if (!isGhostTurn) {
			timer -= Time.deltaTime;
		}
	}

	// Changes the turn to the other player.
	public void SwapTurns () {
		if (turnId > 0) {
			PlayerPolice p = _levelHandler.detectives [turnId - 1];
			p.TurnsTaken += 1;
			p.TimeLeft = timer;
			print ("Detective " + turnId.ToString() + ": Turns taken = " + p.TurnsTaken.ToString() + ", Time left = " + p.TimeLeft.ToString());
			detectiveIcons [turnId - 1].sprite = detectiveNormal;
		}
		turnsTakenPlace += 1;
		turnId = turnsTakenPlace % (numDetectives + 1);
		isGhostTurn = (turnId == 0);

		if (isGhostTurn) {
			ResetObjectList ();
			ghost.sprite = ghostActive;
		} else {
			ghost.sprite = ghostNormal;
			detectiveIcons [turnId - 1].sprite = detectiveActive;
			timer = TIME_LIMIT;
		}

		StartCoroutine(FadeScreen (RoomTransitionScript.WHITE_TRANSPARENT, RoomTransitionScript.WHITE_OPAQUE, 0.5f));
		if (isGameEnding ()) {
			// set deduction screen to be active
			RoomTransitionScript.isPaused = true;		// quickhack to disable all room transitions at this stage
			UIManager.Instance.displayText.enabled = false;
			GameObject.Find ("Scroll View").SetActive (false);
			GameObject.Find ("exit_btn").SetActive (false);
			ghost.enabled = false;
			for (int i = 0; i < numDetectives; i++) {
				detectiveIcons [i].enabled = false;
			}
			UIManager.Instance.deduce_btn();
			GameObject.Find ("back_btn").SetActive (false);
		}
		StartCoroutine(FadeScreen (RoomTransitionScript.WHITE_OPAQUE, RoomTransitionScript.WHITE_TRANSPARENT, 0.5f));
	}

	public void ShowDetectiveTurn() {
		// TODO: light up detective icon based on turnId.
	}

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

	public void ResetObjectList() {
		movedObjects = new List<Transform> ();
	}

	public bool CanMoveObject(Transform obj) {
		bool isInsideList = movedObjects.Contains (obj);
		bool isListWithinLimit = movedObjects.Count < MAX_MOVABLE_OBJECTS;

		// if list is full and object is not inside
		if (!isListWithinLimit && !isInsideList) {
			return false;
		}
		// else if list is not full and object is not inside: add object in list
		else if (isListWithinLimit && !isInsideList) {
			movedObjects.Add (obj);
			return true;
		}
		// else if object is ALREADY inside list
		else if (isInsideList) {
			return true;
		}
		// reject everything else
		else {
			return false;
		}
	}
		
	/*
	 * Checks if the game is ending. By this, we mean to say
	 * whether the game should continue alternating turns between
	 * both players or not.
	 * 
	 * This DOES NOT check if the player has won or lost the game.
	 */
	public bool isGameEnding() {
		allTurnsUsedUp = turnsTakenPlace >= totalPermissibleTurns;
		return allTurnsUsedUp;
	}

	// Handles the endings depending on whether the player has
	// made the correct guess to win the game or not.
	// TODO: implement this
	void checkHasPlayerWon() {
		// if a correct guess is made, the game ends immediately with a win
		if (hasCorrectGuess) {
			// TODO: show winning cutscene(s)
		} else {
			// if all turns are used up, AND player still makes a wrong guess
			if (isGameEnding () && !hasCorrectGuess) {
				// TODO: show game-over cutscene
			}
			// else, the game continues as usual
		}
	}
}
