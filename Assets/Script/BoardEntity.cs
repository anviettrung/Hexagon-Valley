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

	protected Movable mover;

	private void Awake()
	{
		mover = GetComponent<Movable>();
	}

	private void Start()
	{
		MoveToPoint(positionInBoard.x, positionInBoard.y);
	}

	public void MoveToPoint(int x, int y)
	{
		if (mover.isMoving)
			return;

		HexPoint point = board.GetPoint(x, y);

		if (point == null)
			return;

		positionInBoard = point.positionInBoard;
		OnMoving.Invoke(x, y);
		mover.MoveTo(point.worldPosition, moveTime);
	}

	public void MoveRight(int step)
	{
		MoveToPoint((int)positionInBoard.x + step, (int)positionInBoard.y);
	}

	public void MoveDown(int step)
	{
		MoveToPoint((int)positionInBoard.x, (int)positionInBoard.y + step);
	}

	public void MoveRightDown(int step)
	{
		MoveToPoint((int)positionInBoard.x + step, (int)positionInBoard.y + step);
	}
}
