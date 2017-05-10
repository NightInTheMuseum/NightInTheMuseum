using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableInteraction : MonoBehaviour {

    private Rigidbody2D thisRigidbody;

    private bool dragging = false;
    private float distance;

    void Start() {
        thisRigidbody = this.GetComponent<Rigidbody2D>();
    }

    void OnMouseDown() {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
    }

    void OnMouseUp() {
        thisRigidbody.velocity = Vector2.zero;
        thisRigidbody.angularVelocity = 0;
        dragging = false;
    }

    void Update() {
        if (dragging) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }
}
