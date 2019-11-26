using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HexPoint : MonoBehaviour
{
	public Vector2Int positionInBoard;
	public Vector2 worldPosition;
	public Board parentBoard;

	public State state;

	[HideInInspector]
	public Vector2IntEvent OnTouched = new Vector2IntEvent();

	private SpriteRenderer spriteRenderer;
	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		ChangeStateTo(State.NORMAL);
	}

	public void SetPoint(int x, int y)
	{
		gameObject.name = "Point [" + x + ", " + y + "]";

		positionInBoard = new Vector2Int(x, y);
		SetPositionInWorldCoordinate();
	}

	public void SetParentBoard(Board b)
	{
		parentBoard = b;
		transform.SetParent(b.transform);
	}

	public void SetPositionInWorldCoordinate()
	{
		float d = parentBoard.twoPointDistance;
		float deltaX = d * (positionInBoard.x - positionInBoard.y * ExdMath.COS60);
		float deltaY = -d * positionInBoard.y * ExdMath.SIN60;

		worldPosition = new Vector2(
			parentBoard.transform.position.x + deltaX, 
			parentBoard.transform.position.y + deltaY
		);
		transform.position = worldPosition;
	}

	public static int Distance(Vector2Int pointA, Vector2Int pointB)
	{
		Vector2Int delta = pointA - pointB;
		int d = 0;

		if (delta.x * delta.y > 0) {
			d = Mathf.Abs(delta.x) + Mathf.Abs(delta.y) + Mathf.Abs(delta.x - delta.y);
			d = (int)(d * 0.5f);
		} else {
			d = Mathf.Abs(delta.x) + Mathf.Abs(delta.y);
		}

		Debug.Log(d);
		return d;
	}

	void ToggleVisible()
	{
		if (state == State.NORMAL)
			ChangeStateTo(State.WIREFRAME);
		else if (state == State.WIREFRAME)
			ChangeStateTo(State.CAN_BE_SELECT);
		else
			ChangeStateTo(State.NORMAL);
	}

	private void OnMouseDown()
	{
		OnTouched.Invoke(positionInBoard.x, positionInBoard.y);
	}

	public void ChangeStateTo(State s)
	{
		state = s;
		anim.SetInteger("StateCode",(int)s);
	}

	public enum State
	{
		NORMAL,
		WIREFRAME,
		CAN_BE_SELECT,
	};
}
