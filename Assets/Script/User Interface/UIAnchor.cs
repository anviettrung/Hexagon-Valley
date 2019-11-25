using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnchor : MonoBehaviour {

	// Automatic destroy this gameObject if objectToFollow is null
	public bool destroyWhenNoObjectToFollow;

	// Assign this to the object you want the health bar to track:
	public Transform objectToFollow;

//	// This lets us tweak the anchoring position in our canvas space
//	// eg. if we want the UI to sit off to the right on our screen.
//	public Vector3 screenOffset;

	// Cached reference to the canvas containing this object.
	// We'll use this to position it correctly
	RectTransform _myCanvas;


	// Cache a reference to our parent canvas, so we don't repeatedly search for it.
	void Start () {
		_myCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
	}

	// Use LateUpdate to apply the UI follow after all movement & animation
	// for the frame has been applied, so we don't lag behind the unit.
	void LateUpdate () {

		if (objectToFollow == null || !objectToFollow.gameObject.activeInHierarchy) {

			if (destroyWhenNoObjectToFollow)
				ObjectPool.Instance.PushToPool(gameObject);

			return;
		}

		// Ensures objectToFollow doesn't point to null

		// Translate the world position into viewport space.
		Vector3 viewportPoint = Camera.main.WorldToViewportPoint(objectToFollow.position);

		// Canvas local coordinates are relative to its center, 
		// so we offset by half. We also discard the depth.
		viewportPoint -= 0.5f * Vector3.one; 
		viewportPoint.z = 0;

		// Scale our position by the canvas size, 
		// so we line up regardless of resolution & canvas scaling.
		Rect rect = _myCanvas.rect;
		viewportPoint.x *= rect.width;
		viewportPoint.y *= rect.height;

		// Add the canvas space offset and apply the new position.
		transform.localPosition = viewportPoint;
	}

}