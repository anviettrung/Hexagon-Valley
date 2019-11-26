using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movable))]
public class BoardEntity : MonoBehaviour
{
	public Board board;
	public Vector2Int positionInBoard;
	public float moveTime;

	public Vector2IntEvent OnMoving = new Vector2IntEvent();

	[HideInInspector]
	public Movable mover;

	private void Awake()
	{
		mover = GetComponent<Movable>();
	}

	public bool MoveToPoint(int x, int y)
	{
		if (mover.isMoving)
			return false;

		HexPoint point = board.GetPoint(x, y);

		if (point == null)
			return false;

		positionInBoard = point.positionInBoard;
		OnMoving.Invoke(x, y);
		mover.MoveTo(point.worldPosition, moveTime);

		return true;
	}
}
