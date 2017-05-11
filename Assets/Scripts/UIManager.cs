using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

//detect which scene
public class UIManager : MonoBehaviour
{
    [SerializeField]
    Image pausePnl, DialogPnl, deducePnl;
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


    private bool paused, deduce;

	private int menuState = 0;

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
        paused = false;
        deduce = false;
    }

	// Update is called once per frame
	void Update () {
		if (turnScript.isGhostTurn) {
			displayText.text = "Remaining movable objects: " + (5 - turnScript.movedObjects.Count).ToString ();
		} else {
			displayText.text = "Remaining time: " + (Mathf.RoundToInt(turnScript.timer)).ToString() + " seconds";

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
