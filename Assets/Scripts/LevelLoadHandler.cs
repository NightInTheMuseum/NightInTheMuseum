using UnityEngine;
using System.Collections;

//Use this to store all stats and load them from here
public class LevelLoadHandler : MonoBehaviour {

	private static LevelLoadHandler _instance;

	int numDetectives = 0;

	public static LevelLoadHandler Instance
	{
		get { return _instance; }
	}

	void Awake() {

		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}


	public void setDect (int i)
	{
        numDetectives = i;
	}

	public int returnDect()
	{
		return numDetectives;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
