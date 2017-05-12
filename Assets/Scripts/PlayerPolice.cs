using UnityEngine;
using System.Collections;

public class PlayerPolice
{
	//Member variables can be referred to as
	//fields.
	private int id;
	private bool canPlay;
	private int turnsTaken;
	private float timeLeft;
	private bool hasWon;

	public int Id
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
		}
	}

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

	public static int GetWinner(PlayerPolice a, PlayerPolice b)
	{
		if (a == null && b == null) {
			return 0;
		} else if (a == null && b != null) {
			return b.Id;
		} else if (a != null && b == null) {
			return a.Id;
		}
		// if A wins by default
		else if (a.HasWon && !b.HasWon) {
			return a.Id;
		}
		// else if B wins by default
		else if (!a.HasWon && b.HasWon) {
			return b.Id;
		}
		// else if both A & B wins, first check turns taken by A and B
		else if (a.HasWon && b.HasWon && a.TurnsTaken < b.TurnsTaken) {
			return a.Id;
		}
		// else if both A & B wins and ties in turns taken, check for time left for A and B
		else if (a.HasWon && b.HasWon && a.TurnsTaken == b.TurnsTaken && a.TimeLeft > b.TimeLeft) {
			return a.Id;
		}
		// else if both A and B did not win, no one won
		else if (!a.HasWon && !b.HasWon) {
			return 0;
		}
		// else, B has won.
		else {
			return b.Id;
		}
	}
}