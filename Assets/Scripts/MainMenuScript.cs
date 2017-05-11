﻿using UnityEngine;
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
    Image pnl_MainMenu, pnl_Fader, pnl_Cutscene, pnl_Players;

    [SerializeField]
    Text numOfDetectives;

    [SerializeField]
    List<Sprite> cutscenes;

    [SerializeField]
    LevelLoadHandler _levelHandler;

    int currentCutscene = 0, currentSelection = 2;


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
        numOfDetectives.text = currentSelection.ToString();
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
        pnl_Players.gameObject.SetActive(false);
        pnl_Cutscene.gameObject.SetActive(true);
        pnl_C1.gameObject.SetActive(true);
        btn_play.gameObject.SetActive(false);
        btn_back.gameObject.SetActive(false);
        btn_forward.gameObject.SetActive(true);

    }

    public void proceedPlayersSelect()
    {
        pnl_MainMenu.gameObject.SetActive(false);
        pnl_Players.gameObject.SetActive(true);

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
        pnl_C1.gameObject.GetComponent<Image>().sprite = cutscenes[currentCutscene];
    }

    public void updatePlayerCountForward()
    {
        currentSelection++;
        if (currentSelection >= 5)
        {
            currentSelection = 2;
        }
        _levelHandler.setDect(currentSelection);
    }



    public void updatePlayerCountBackward()
    {
        currentSelection--;
        if (currentSelection < 2)
        {
            currentSelection = 4;
        }
        _levelHandler.setDect(currentSelection);
    }

}
