using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour {

    [SerializeField]
    Button btn_start, btn_quit, btn_play, btn_forward, btn_back;

    [SerializeField]
    Image pnl_C1, pnl_C2, pnl_C3;

    [SerializeField]
    Image pnl_MainMenu, pnl_Fader, pnl_Cutscene;

    [SerializeField]
    List<Sprite> cutscenes;

    int currentCutscene = 0;


    void Awake()
    {
        //pnl_MainMenu.gameObject.SetActive (false);
    }

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }

    public void LoadProperLevel(string s)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(ShrinkLevelSelectPanel());
        sequence.AppendCallback(() =>
        {
            Application.LoadLevel(s);
        });
    }

    public Tween ShrinkLevelSelectPanel()
    {

        pnl_Fader.gameObject.SetActive(true);

        Camera.main.DOOrthoSize(0.01f, 1.5f).SetEase(Ease.InQuint);
        return pnl_Fader.DOFade(1, 1.5f);
    }

    public void quitgame()
    {
        Application.Quit();
    }

    public void proceedCutScene()
    {
        pnl_MainMenu.gameObject.SetActive(false);
        pnl_Cutscene.gameObject.SetActive(true);
        pnl_C1.gameObject.SetActive(true);
        btn_play.gameObject.SetActive(false);
        btn_back.gameObject.SetActive(false);
        btn_forward.gameObject.SetActive(true);

    }

    public void updateCutSceneForward()
    {
        currentCutscene++;
        btn_forward.gameObject.SetActive(true);
        btn_back.gameObject.SetActive(true);
        if (currentCutscene >= cutscenes.Count-1)
        {
            currentCutscene = cutscenes.Count -1;
            btn_forward.gameObject.SetActive(false);
            btn_play.gameObject.SetActive(true);
        }
        refreshCutscene(currentCutscene);

    }

    public void updateCutSceneBackward()
    {
        currentCutscene--;
        btn_play.gameObject.SetActive(false);
        btn_forward.gameObject.SetActive(true);
        btn_back.gameObject.SetActive(true);
        if (currentCutscene <= 0)
        {
            currentCutscene = 0;
            btn_back.gameObject.SetActive(false);
        }
        refreshCutscene(currentCutscene);

    }

    public void refreshCutscene(int i)
    {
        /*pnl_C1.gameObject.SetActive(false);
        pnl_C2.gameObject.SetActive(false);
        pnl_C3.gameObject.SetActive(false);
        switch (i)
        {
            case 0:
                pnl_C1.gameObject.SetActive(true);
                break;
            case 1:
                pnl_C2.gameObject.SetActive(true);
                break;
            case 2:
                pnl_C3.gameObject.SetActive(true);
                break;
        }*/

        pnl_C1.gameObject.GetComponent<Image>().sprite = cutscenes[currentCutscene];
    }

}
