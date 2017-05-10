using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script automatically computes the attributes of the boundary colliders
 * whenever the sprite image for the top-down map is changed.
 * 
 * Note that the width and the height of the top-down map MUST be specified
 * in the Unity editor, otherwise the automatic computation will not work as intended.
 * 
 * This automatic computation is only done once.
 */
public class TopDownMapScript : MonoBehaviour {

	// These attributes are actual unscaled attributes
	// (i.e. 1920 x 1080, 1020 x 760, etc)
	public float width;
	public float height;

	// These attributes are used to compute the colliders, and are in the form
	// [offsetX, offsetY, sizeX, sizeY]
	float[] leftCollider;
	float[] rightCollider;
	float[] topCollider;
	float[] bottomCollider;

	BoxCollider2D[] boxColliders;

	void Awake () {
		ComputeColliderAttributes ();
		ComputeColliders ();
	}

	void ComputeColliderAttributes () {
		ComputeLeftAndRightColliderAttributes (width, height);
		ComputeTopAndBottomColliderAttributes (width, height);
	}

	void ComputeColliders () {
		boxColliders = GetComponents<BoxCollider2D> ();
		SetLeftCollider ();
		SetRightCollider ();
		SetTopCollider ();
		SetBottomCollider ();
	}

	/* ===== UTILITY/COMPUTATION FUNCTION DEFINITIONS ===== */

	void SetLeftCollider () {
		boxColliders [0].offset = new Vector2 (leftCollider [0], leftCollider [1]);
		boxColliders [0].size = new Vector2 (leftCollider [2], leftCollider [3]);
	}

	void SetRightCollider () {
		boxColliders [1].offset = new Vector2 (rightCollider [0], rightCollider [1]);
		boxColliders [1].size = new Vector2 (rightCollider [2], rightCollider [3]);
	}

	void SetTopCollider () {
		boxColliders [2].offset = new Vector2 (topCollider [0], topCollider [1]);
		boxColliders [2].size = new Vector2 (topCollider [2], topCollider [3]);
	}

	void SetBottomCollider() {
		boxColliders [3].offset = new Vector2 (bottomCollider [0], bottomCollider [1]);
		boxColliders [3].size = new Vector2 (bottomCollider [2], bottomCollider [3]);
	}

	void ComputeLeftAndRightColliderAttributes (float width, float height) {
		float scaledWidth = width / 100.0f;
		float scaledHeight = height / 100.0f;

		leftCollider = new float[4];
		leftCollider [0] = -scaledWidth / 2.0f - 0.5f;	// offsetX
		leftCollider [1] = 0f;							// offsetY
		leftCollider [2] = 1.0f;						// sizeX
		leftCollider [3] = scaledHeight;				// sizeY

		rightCollider = new float[4];
		rightCollider [0] = -leftCollider [0];		// offsetX
		rightCollider [1] = leftCollider [1];		// offsetY
		rightCollider [2] = leftCollider [2];		// sizeX
		rightCollider [3] = leftCollider [3];		// sizeY
	}

	void ComputeTopAndBottomColliderAttributes (float width, float height) {
		float scaledWidth = width / 100.0f;
		float scaledHeight = height / 100.0f;

		topCollider = new float[4];
		topCollider [0] = 0f;								// offsetX
		topCollider [1] = scaledHeight / 2.0f + 0.5f;		// offsetY
		topCollider [2] = scaledWidth + 2.0f;				// sizeX
		topCollider [3] = 1.0f;								// sizeY

		bottomCollider = new float[4];
		bottomCollider [0] = topCollider [0];		// offsetX
		bottomCollider [1] = -topCollider [1];		// offsetY
		bottomCollider [2] = topCollider [2];		// sizeX
		bottomCollider [3] = topCollider [3];		// sizeY
	}
}
