using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movable : MonoBehaviour
{
	protected bool isMoving;
	public bool IsMoving {
		get {
			return isMoving;
		}
	}

	public void MoveTo(HexPoint destination, float time, Vector2IntEvent moveDoneCallback)
	{
		if (isMoving == false)
			StartCoroutine(moveTo(destination, time, moveDoneCallback));
	}

	public void MoveTo(HexPoint destination, float time)
	{
		if (isMoving == false)
			StartCoroutine(moveTo(destination, time, null));
	}

	protected IEnumerator moveTo(HexPoint destination, float time, Vector2IntEvent moveDoneCallback)
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

		if (moveDoneCallback != null)
			moveDoneCallback.Invoke(destination.positionInBoard.x, destination.positionInBoard.y);

		isMoving = false;
	}
}
