using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour {

    /*[SerializeField]
    private bool isPastable = false;
    [SerializeField]
    private bool isBreakable = false;
    [SerializeField]
    private bool isRotatable = false;
    [SerializeField]
    private bool isClickable = false;*/
    [SerializeField]
    private string flavourText = "Insert flavour text here...";

    // Run-time variables
    private Vector3 startingPosition;
    private bool isDestroyed = false;

    void Start() {
        startingPosition = this.transform.position;
    }

    // Public Methods

    public string GetFlavourText() {
        return flavourText;
    }

    public Vector2 GetStartingPosition() {
        return startingPosition;
    }

    public void DestroyItem() {
        if (!isDestroyed) {
            InteractWithItem();
        }
    }

    public void InteractWithItem() {
        this.GetComponent<SpriteRenderer>().color = Color.black;
    }

}
