using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movable : MonoBehaviour
{
	public bool isMoving = false;

	public void MoveTo(HexPoint destination, float time)
	{
		if (isMoving == false)
			StartCoroutine(moveTo(destination, time, null, null));
	}

	public void MoveTo(HexPoint destination, float time, Vector2IntEvent onMovingCallback, Vector2IntEvent onMoveDoneCallback)
	{
		if (isMoving == false)
			StartCoroutine(moveTo(destination, time, onMoveDoneCallback, onMoveDoneCallback));
	}

	protected IEnumerator moveTo(HexPoint destination, float time, Vector2IntEvent onMovingCallback, Vector2IntEvent onMoveDoneCallback)
	{
		isMoving = true;

		Vector2 moveDirection = destination.worldPosition - (Vector2)transform.position;
		float speed = moveDirection.magnitude / time;
		moveDirection = moveDirection.normalized;

		while (time > 0) {
			time -= Time.deltaTime;
			transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
			yield return new WaitForFixedUpdate();	 
		}

		if (onMoveDoneCallback != null)
			onMoveDoneCallback.Invoke(destination.positionInBoard.x, destination.positionInBoard.y);

		isMoving = false;
	}
}
