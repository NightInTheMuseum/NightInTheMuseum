using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnControllerScript : MonoBehaviour {

	const int MAX_NUM_TURNS = 6;
	const int MAX_MOVABLE_OBJECTS = 5;
	const float TIME_LIMIT = 120;		// 2 minutes, in seconds

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

	// Changes the turn to the other player.
	public void SwapTurns () {
		isGhostTurn = !isGhostTurn;
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
