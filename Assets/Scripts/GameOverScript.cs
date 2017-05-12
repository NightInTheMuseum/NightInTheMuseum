using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverScript : MonoBehaviour
{

	public void BackToMenu() {
		SceneManager.LoadScene ("Title Screen");
	}
}

