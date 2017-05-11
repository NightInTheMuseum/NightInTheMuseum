using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour {

	[SerializeField]
	Transform _transform;


	[SerializeField]
	Text txt_description,txt_Difficulty;

	[SerializeField]
	Button btn_start,btn_quit,btn_credits;

	[SerializeField]
	Image img_Character, img_statusPlaceholder;

	[SerializeField]
	Image pnl_MainMenu,pnl_Fader, pnl_Creditss;


	[SerializeField]
	List<string> descriptionStrings;


	int currentSelection = 0;
	int currentTutorial = 0;
	int currentDifficulty = 0;



	void Awake() {
		//pnl_MainMenu.gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {

	}


	// Update is called once per frame
	void Update () {
	
	}

	public void LoadProperLevel(string s) {
		Sequence sequence = DOTween.Sequence();
		sequence.Append(ShrinkLevelSelectPanel());
		sequence.AppendCallback(() =>
		                        {
			Application.LoadLevel (s);
		});
	}

    public Tween ShrinkLevelSelectPanel() {

        pnl_Fader.gameObject.SetActive(true);

        Camera.main.DOOrthoSize(0.01f, 1.5f).SetEase(Ease.InQuint);
        return pnl_Fader.DOFade(1, 1.5f);
    }

    public void quitgame() {
        Application.Quit();
    }

}
