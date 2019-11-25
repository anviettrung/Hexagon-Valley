using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
	public bool isMoving = false;

	public void MoveTo(Vector2 destination, float time)
	{
		if (isMoving == false)
			StartCoroutine(moveTo(destination, time));
	}

	protected IEnumerator moveTo(Vector2 destination, float time)
	{
		isMoving = true;

		Vector2 moveDirection = destination - (Vector2)transform.position;
		float speed = moveDirection.magnitude / time;
		moveDirection = moveDirection.normalized;

		while (time > 0) {
			time -= Time.deltaTime;
			transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
			yield return new WaitForFixedUpdate();	 
		}

		isMoving = false;
	}
}
