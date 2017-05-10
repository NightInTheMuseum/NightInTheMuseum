using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralInteraction : MonoBehaviour {

    [SerializeField]
    private float spookyActionTime = 2;
    [SerializeField]
    private int spookyActionIntervals = 10;

    // Components
    [SerializeField]
    private Text flavourTextDisplayBox;

    // Run-time variables
    [SerializeField]
    private List<Transform> objectList;
    private float displayLingerTime = .25f;
    private float currentLingerTime = 0;
    private float currentSpookyActionObjects = 0;

	void Update () {

        Debug.Log(currentSpookyActionObjects);

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else if (Input.GetKeyDown(KeyCode.T) && currentSpookyActionObjects == 0) {
            SpookyAction(spookyActionTime, spookyActionIntervals);
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (currentLingerTime <= 0) {
            flavourTextDisplayBox.text = "";
        } else {
            currentLingerTime -= Time.deltaTime;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit;
        if (hit = Physics2D.Raycast(ray.origin, ray.direction)) {
            if (hit.collider && hit.collider.tag.Equals("Interactables")) {
                string stringToDisplay = hit.collider.gameObject.GetComponent<ObjectInteraction>().GetFlavourText();
                Debug.Log(stringToDisplay);
                flavourTextDisplayBox.text = stringToDisplay;
                currentLingerTime = displayLingerTime;
            }
        }
	}

    private void SpookyAction(float time, int intervals) {
        currentSpookyActionObjects = objectList.Count;
        for (int i = 0; i < objectList.Count; i++) {
            StartCoroutine(SpookyInterpolation(objectList[i], time, intervals));
        }
    }

    private IEnumerator SpookyInterpolation(Transform objectTransform, float timeTaken, int intervals) {
        bool isKinematicObject = false;
        Rigidbody2D objectRigidbody = objectTransform.GetComponent<Rigidbody2D>();
        if (objectRigidbody.isKinematic) {
            isKinematicObject = true;
        } else {
            objectTransform.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        Vector2 objectOriginPosition = objectTransform.GetComponent<ObjectInteraction>().GetStartingPosition();
        Vector2 objectCurrentPosition = objectTransform.position;
        float intervalTime = timeTaken / intervals;
        float intervalInterpolation = (objectCurrentPosition - objectOriginPosition).magnitude / intervals;
        Debug.Log(intervalInterpolation);
        objectTransform.position = objectOriginPosition;
        for (int i = 0; i < intervals; i++) {
            objectTransform.position = Vector2.MoveTowards(objectTransform.position, objectCurrentPosition, intervalInterpolation);
            yield return new WaitForSeconds(intervalTime);
        }
        currentSpookyActionObjects -= 1;
        if (!isKinematicObject) {
            objectTransform.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
