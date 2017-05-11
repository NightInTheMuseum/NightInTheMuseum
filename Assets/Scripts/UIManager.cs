using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

//detect which scene
public class UIManager : MonoBehaviour
{

    public const float TIME_LIMIT = 120;		// 2 minutes, in seconds

    [SerializeField]
    Image pausePnl, DialogPnl, deducePnl, profilePnl;
    [SerializeField]
    GameObject screenUI;
    [SerializeField]
    List<string> Dialogs, Dialogs_2, Dialogs_3;
    [SerializeField]
    GameObject Notifier;
	[SerializeField]
	PlayerTurnControllerScript turnScript;
	[SerializeField]
	public Text displayText;
	[SerializeField]
	public Button deduceButton;

    public List<GameObject> iconsForTime;

    private bool paused, deduce, viewProfile;

	private int suspect = 0;

	private static UIManager _instance = null;

	bool isDebug = false;

	public static UIManager Instance {
		get { return _instance; }
	}

	void Awake() { 
		if (_instance != null && _instance != this) {
			Destroy (gameObject);
		}
		_instance = this;

	}

	// Use this for initialization
	void Start () {

		//TPhase = Tutorial1Phase.PreStart;
		if (pausePnl != null) {
			pausePnl.gameObject.SetActive (false);
		}
        if (deducePnl != null)
        {
            deducePnl.gameObject.SetActive(false);
        }
		if (profilePnl != null)
		{
			profilePnl.gameObject.SetActive (false);
		}
        paused = false;
        deduce = false;
		viewProfile = false;
		deduceButton.gameObject.SetActive (false);
    }

	// Update is called once per frame
	void Update () {
		if (turnScript.isGhostTurn) {
			deduceButton.gameObject.SetActive(false);
			displayText.text = "Remaining movable objects: " + (5 - turnScript.movedObjects.Count).ToString ();
		} else {
			deduceButton.gameObject.SetActive(true);
			displayText.text = "Remaining time: " + (Mathf.RoundToInt(turnScript.timer)).ToString() + " seconds";

            setTimerIcon(turnScript.timer/ TIME_LIMIT);

            if (turnScript.timer <= 0) {
				turnScript.SwapTurns ();
			}
		}
	}

	public void pause_btn () {//in-game pause button 
		if (paused) {
            //a seperate panel is needed for both the player
			Sequence sequence = DOTween.Sequence ();
			sequence.Append (pausePnl.rectTransform.DOLocalMoveY(-1050, 1.0f, false));

			Time.timeScale = 1.0f;
			paused = false;
			pausePnl.gameObject.SetActive (false);
            screenUI.gameObject.SetActive(true);
			RoomTransitionScript.isPaused = false;
        } else {
            screenUI.gameObject.SetActive(false);
            pausePnl.gameObject.SetActive (true);
			Sequence sequence = DOTween.Sequence ();
			sequence.SetUpdate (true);
			sequence.Append (pausePnl.rectTransform.DOLocalMoveY (0, 1.0f, false));

			Time.timeScale = 0.0f;
			paused = true;
			RoomTransitionScript.isPaused = true;
		}				
	}

	public void profile_btn () {//in-game view-profile button 
		if (viewProfile) {
			//a seperate panel is needed for both the player
			Sequence sequence = DOTween.Sequence ();
			sequence.Append (profilePnl.rectTransform.DOLocalMoveY(-1050, 1.0f, false));

			viewProfile = false;
			profilePnl.gameObject.SetActive (false);
			screenUI.gameObject.SetActive(true);
			RoomTransitionScript.isPaused = false;
		} else {
			screenUI.gameObject.SetActive(false);
			profilePnl.gameObject.SetActive (true);
			Sequence sequence = DOTween.Sequence ();
			sequence.SetUpdate (true);
			sequence.Append (profilePnl.rectTransform.DOLocalMoveY (0, 1.0f, false));

			viewProfile = true;
			RoomTransitionScript.isPaused = true;
		}				
	}
    
	public void quit_btn () {
		// check if in main menu or in-game
		//loadLevel("SelectionScene_2");
		Application.Quit ();
	}

 
    public void deduce_btn() {
        if (deduce)
        {
			if (!turnScript.isGameEnding ()) {
				Sequence sequence = DOTween.Sequence ();
				sequence.Append (deducePnl.rectTransform.DOLocalMoveY (-1050, 1.0f, false));

				Time.timeScale = 1.0f;
				deduce = false;
				deducePnl.gameObject.SetActive (false);
			}
        }
        else
        {
            deducePnl.gameObject.SetActive(true);
            Sequence sequence = DOTween.Sequence();
            sequence.SetUpdate(true);
            sequence.Append(deducePnl.rectTransform.DOLocalMoveY(0, 1.0f, false));

            Time.timeScale = 0.0f;
            deduce = true;
        }
        //confirm the selection of which player is selecting
    }

    public void selectSuspect(GameObject pic)
    {
        suspect = (int)pic.GetComponent<Profile>().getProfile();
        Debug.Log(suspect.ToString());
       
    }

    public void onDeduction()
    {
        if (suspect == turnScript.answer)
        {
            //Correct, can arrest suspect
			// TODO: mark the detective with correct guess as 'cannot play' and 'has won'
			turnScript.MakeCorrectGuessForCurrentPlayer();
        }
        else
        {
            //Wrong, you failed!
			// TODO: mark the detective with wrong guess as 'cannot play' and 'did not win'
			turnScript.MakeWrongGuessForCurrentPlayer();
        }
    }

    public void setTimerIcon(float time)
    {
        iconsForTime[0].transform.localScale = new Vector3(iconsForTime[0].transform.localScale.x, Mathf.Clamp(time, 0f, 1f), iconsForTime[0].transform.localScale.z);
    }

    private void OnMouseDown()
    {
        Notifier.GetComponentInChildren<NotificationManager>().NotifyText("Looked at Profile");
    }

    /*
    public void dialogNext() {
        triggerNextDialog();
    }

    public void dialogComplete() {
        HideDialogBox();
    }

    public void HideDialogBox()
    {
        //TPhase = Tutorial1Phase.Action;
        DialogPnl.gameObject.SetActive(false);
    }

    public void setUpDialog(int i)
    {
        //TPhase = Tutorial1Phase.Dialog;
        DialogPnl.gameObject.SetActive(true);
        dlogCom_btn.gameObject.SetActive(false);
        current = 0;
        Dialogs.Clear();
        switch (i)
        {
            case 1:
                for (int j = 0; j < Dialogs_2.Count; j++)
                {
                    Dialogs.Add(Dialogs_2[j]);
                }
                break;
            case 2:
                for (int j = 0; j < Dialogs_3.Count; j++)
                {
                    Dialogs.Add(Dialogs_3[j]);
                }
                break;
        }

        StopAllCoroutines();
        StartCoroutine(TypeText(Dialogs[current]));


        //tutNextBtn.onClick.AddListener (() => triggerNextDialog_2());
    }

    public void setUpDialog(List<string> newDialog)
    {
        //TPhase = Tutorial1Phase.Dialog;
        DialogPnl.gameObject.SetActive(true);
        dlogCom_btn.gameObject.SetActive(false);
        current = 0;
        Dialogs.Clear();

        for (int j = 0; j < newDialog.Count; j++)
        {
            Dialogs.Add(newDialog[j]);
        }

        StopAllCoroutines();
        StartCoroutine(TypeText(Dialogs[current]));

        //tutNextBtn.onClick.AddListener (() => triggerNextDialog_2());
    }


    public void triggerNextDialog()
    {
        txtInstruction1.text = "";
        if (current < Dialogs.Count - 1)
        {
            current += 1;
            StopAllCoroutines();
            StartCoroutine(TypeText(Dialogs[current]));
        }

        //Last line of dialog!

        if (current == Dialogs.Count - 1)
        {
            //if (dialogCount < 2) {
            //	dialogCount++;
            //Debug.Log (dialogCount);
            //setUpDialog (dialogCount);
            //} else {				
            dlogCom_btn.gameObject.SetActive(true);
            //}						
        }
    }*/


}
