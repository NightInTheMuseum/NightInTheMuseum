using UnityEngine;
using System.Collections;

public class PlayerPolice
{
	//Member variables can be referred to as
	//fields.
	private bool canPlay;
	private int turnsTaken;
	private float timeLeft;
	private bool hasWon;

	public bool CanPlay
	{
		get
		{
			return canPlay;
		}
		set
		{
			canPlay = value;
		}
	}

	public bool HasWon
	{
		get
		{
			return hasWon;
		}
		set
		{
			hasWon = value;
		}
	}

	public int TurnsTaken
	{
		get
		{
			return turnsTaken;
		}
		set
		{
			turnsTaken = value;
		}
	}

	public float TimeLeft
	{
		get
		{
			return timeLeft;
		}
		set
		{
			timeLeft = value;
		}
	}

	public static PlayerPolice GetWinner(PlayerPolice a, PlayerPolice b)
	{
		// if A wins by default
		if (a.HasWon && !b.HasWon) {
			return a;
		}
		// else if B wins by default
		else if (!a.HasWon && b.HasWon) {
			return b;
		}
		// else if both A & B wins, first check turns taken by A and B
		else if (a.HasWon && b.HasWon && a.TurnsTaken < b.TurnsTaken) {
			return a;
		}
		// else if both A & B wins and ties in turns taken, check for time left for A and B
		else if (a.HasWon && b.HasWon && a.TurnsTaken == b.TurnsTaken && a.TimeLeft > b.TimeLeft) {
			return a;
		}
		// else if both A and B did not win, no one won
		else if (!a.HasWon && !b.HasWon) {
			return null;
		}
		// else, B has won.
		else {
			return b;
		}
	}
}