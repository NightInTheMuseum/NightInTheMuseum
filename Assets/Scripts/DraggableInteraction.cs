﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableInteraction : MonoBehaviour {

	[SerializeField]
	private float boundaryX = 8.9f;
	[SerializeField]
	private float boundaryY = 5;

	public Camera targetCamera;

    private Rigidbody2D thisRigidbody;

    private bool dragging = false;
    private float distance;

    void Start() {
        thisRigidbody = this.GetComponent<Rigidbody2D>();
    }

    void OnMouseDown() {
        distance = Vector3.Distance(transform.position, targetCamera.transform.position);
        dragging = true;
    }

    void OnMouseUp() {
        thisRigidbody.velocity = Vector2.zero;
        thisRigidbody.angularVelocity = 0;
        dragging = false;
    }

    void Update() {
        if (dragging) {
			Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
			rayPoint.z = 0;
            transform.position = rayPoint;
        }
    }

	void LateUpdate() {
		float currentX = transform.localPosition.x;
		float currentY = transform.localPosition.y;
		currentX = Mathf.Clamp (currentX, -boundaryX, boundaryX);
		currentY = Mathf.Clamp (currentY, -boundaryY, boundaryY);
		transform.localPosition = new Vector2 (currentX, currentY);
	}
}
