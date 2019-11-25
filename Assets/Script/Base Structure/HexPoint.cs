using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HexPoint : MonoBehaviour
{
	public Vector2Int positionInBoard;
	public Vector2 worldPosition;
	public Board parentBoard;

	public bool isWireframeMode;

	[HideInInspector]
	public Vector2IntEvent OnTouched = new Vector2IntEvent();

	private SpriteRenderer spriteRenderer;
	private SpriteMask spriteMask;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteMask = GetComponent<SpriteMask>();
		spriteMask.enabled = isWireframeMode;
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
		float deltaX = d * (positionInBoard.x - positionInBoard.y * ExdMath.cos60);
		float deltaY = -d * positionInBoard.y * ExdMath.sin60;

		worldPosition = new Vector2(
			parentBoard.transform.position.x + deltaX, 
			parentBoard.transform.position.y + deltaY
		);
		transform.position = worldPosition;
	}

	void ToggleVisible()
	{
		isWireframeMode = !isWireframeMode;
		spriteMask.enabled = isWireframeMode ? true : false;
	}

	private void OnMouseDown()
	{
		OnTouched.Invoke((int)positionInBoard.x, (int)positionInBoard.y);
		ToggleVisible();
	}
}
