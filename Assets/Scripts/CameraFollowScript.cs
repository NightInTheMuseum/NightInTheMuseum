using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

	public Transform target;
	public float smoothing;
	public float offsetX;
	public float offsetY;

	static Vector3 offset = new Vector3(0, 0, -10);

	void FixedUpdate () {
		Vector3 targetPosition = target.position + offset;
		if (Mathf.Abs (targetPosition [0]) > offsetX) {
			if (targetPosition [0] > offsetX) {
				targetPosition [0] = offsetX;
			} else if (targetPosition [0] < -offsetX) {
				targetPosition [0] = -offsetX;
			}
		}
		if (Mathf.Abs (targetPosition [1]) > offsetY) {
			if (targetPosition [1] > offsetY) {
				targetPosition [1] = offsetY;
			} else if (targetPosition [1] < -offsetY) {
				targetPosition [1] = -offsetY;
			}
		}

		transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing * Time.deltaTime);
	}
}
