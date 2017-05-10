using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableInteraction : MonoBehaviour {

    void OnMouseDown() {
        GetComponent<SpriteRenderer>().color = Color.black;
    }
}
