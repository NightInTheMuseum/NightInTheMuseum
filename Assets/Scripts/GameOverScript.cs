using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    List<Sprite> winner;
    [SerializeField]
    Image _winnerHolder;
    [SerializeField]
    Image pnl_MainMenu;

    public LevelLoadHandler _levelHandler;
    int p;
    //int id = 0;
    //bool gotWinner = false;


    private void Awake()
    {
        _levelHandler = FindObjectOfType<LevelLoadHandler>();
        if (_levelHandler != null)
        {
            sotin();
            refreshWinscene();
        }
    }

    public void BackToMenu() {
		SceneManager.LoadScene ("Title Screen");
	}

    public void refreshWinscene()
    {
        if (p != 0)
        {
            _winnerHolder.gameObject.GetComponent<Image>().sprite = winner[p-1];
        }
        else
        {
            pnl_MainMenu.gameObject.SetActive(true);
        }
    }

    void sotin()
    {
        if(_levelHandler.detectives.Length == 1)
        {
            //gotWinner = _levelHandler.detectives[0].HasWon;
            if (_levelHandler.detectives[0].HasWon)
                p = 1;
            else
                p = 0;

        }
        else if (_levelHandler.detectives.Length == 2)
        {
            p = PlayerPolice.GetWinner(_levelHandler.detectives[0], _levelHandler.detectives[1]);
        }
        else
        {
            p = PlayerPolice.GetWinner(_levelHandler.detectives[0], _levelHandler.detectives[1]);
            if (p == 0)
            {
                if (_levelHandler.detectives[2].HasWon)
                    p = 3;
                else
                    p = 0;
            }
            else
            {
                p = PlayerPolice.GetWinner(_levelHandler.detectives[p-1], _levelHandler.detectives[2]);
            }
        }
    }
}

