using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnControllerScript : MonoBehaviour {

	const int MAX_NUM_TURNS = 6;

	public GameObject playerGhost;
	public GameObject playerPolice;
	public PlayerMovementScript ghostMovement;
	public PlayerMovementScript policeMovement;
	public Camera playerGhostCamera;
	public Camera playerPoliceCamera;

	/* A turn-tracking variable.
	 * True => ghost takes the turn, False => police takes the turn.
	 * 
	 * This variable needs to be set by the interactive rooms and
	 * is, therefore, made public.
	 */
	public bool isGhostTurn = true;
	public int turnsTakenPlace = 1;

	private bool hasCorrectGuess;
	private bool allTurnsUsedUp;

	/*
	 * At game start, the ghost is the one who takes the first turn.
	 * Therefore, we hide the police and disable his/her camera.
	 */
	void Start () {
		playerPolice.SetActive (false);
		playerPoliceCamera.enabled = false;
		policeMovement.canMove = false;
	}

	// Changes the turn to the other player.
	public void SwapTurns () {
		isGhostTurn = !isGhostTurn;
	}

	// Enables the camera for the player who is going to take the next turn.
	public void EnableNextPlayerCamera () {
		turnsTakenPlace += 1;
		//print("Turns taken place: " + turnsTakenPlace.ToString());

		if (isGameEnding ()) {
			// The final turn has ended, so there is no next turn.
		} else if (isGhostTurn) {
			playerGhost.SetActive (true);
			playerGhostCamera.enabled = true;
		} else {
			// Enable the police
			playerPolice.SetActive (true);
			playerPoliceCamera.enabled = true;
		}
	}

	// Enables movement for the player who is going to take the next turn.
	public void EnableNextPlayerMovement () {
		if (isGameEnding ()) {
			// The final turn has ended, so there is no next turn.
		} else if (isGhostTurn) {
			ghostMovement.canMove = true;
		} else {
			// Enable the police's movement.
			policeMovement.canMove = true;
		}
	}

	/*
	 * Checks if the game is ending. By this, we mean to say
	 * whether the game should continue alternating turns between
	 * both players or not.
	 * 
	 * This DOES NOT check if the player has won or lost the game.
	 */
	bool isGameEnding() {
		allTurnsUsedUp = turnsTakenPlace >= MAX_NUM_TURNS;
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
