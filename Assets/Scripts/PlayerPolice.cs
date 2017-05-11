using UnityEngine;
using System.Collections;

public class PlayerPolice
{
	//Member variables can be referred to as
	//fields.
	private bool canPlay;
	private int turnsTaken;
	private float timeLeft;

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

	public PlayerPolice GetWinner(PlayerPolice a, PlayerPolice b)
	{
		if (a.TurnsTaken < b.TurnsTaken) {
			return a;
		} else if (a.TurnsTaken == b.TurnsTaken && a.TimeLeft > b.TimeLeft) {
			return a;
		} else {
			return b;
		}
	}
}