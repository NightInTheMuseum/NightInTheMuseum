using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class NotificationManager : MonoBehaviour {

	private static NotificationManager _instance = null;
	public Camera targetCamera;

	[SerializeField]
	Text _messageText;

	int currentMessageIndex;

	[SerializeField]
	ScrollRect _scrollRect;

	[SerializeField]
	Scrollbar _scrollbar;

	// Run-time variables
	[SerializeField]
	private float displayLingerTime = .25f;
	private float currentLingerTime = 0;

	public static NotificationManager Instance //can call from any other class w/o reference
	{
		get { return _instance; }
	}


	void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
		}
		_instance = this;
		
		targetCamera = Camera.main;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyUp (KeyCode.A)) {
			NotifyText("hi " + currentMessageIndex);
		}

		if (currentLingerTime <= 0) {
			NotifyText("");
		} else {
			currentLingerTime -= Time.deltaTime;
		}

		Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit;
		if (hit = Physics2D.Raycast(ray.origin, ray.direction)) {
			if (hit.collider && hit.collider.tag.Equals("Interactables")) {
				hit.collider.gameObject.GetComponent<ObjectInteraction>().ShowFlavourText();
				currentLingerTime = displayLingerTime;
			}
		}
	
	}

	public void NotifyText(string s)
	{

		_messageText.text = s;

		_scrollbar.value = 0;

		/*
		if (currentMessageIndex >= textPoints.Count)
		{


			PushTextUp();




			messageCollection.RemoveAt(0);

		}

		GameObject g = Instantiate(messagePrefab, new Vector3(), Quaternion.identity) as GameObject;

		messageCollection.Add (g);

		g.transform.SetParent(parentObject.transform);



		if (g != null) {
			g.GetComponent<RectTransform>().anchoredPosition = startPos.anchoredPosition;
			g.transform.localScale = new Vector3 (1, 1, 1);
			g.GetComponent<Text>().text = s;


			g.GetComponent<RectTransform> ().DOAnchorPos (textPoints [messageCollection.Count - 1].anchoredPosition, 0.5f, false);

			currentMessageIndex ++;


		}
		*/

	}

}
