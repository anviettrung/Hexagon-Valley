using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Movable))]
public class BoardEntity : MonoBehaviour
{
	public Board parentBoard;
	public Vector2Int positionInBoard;
	public float moveTime;
	public MovementFilter movementFilter = new MovementFilter();

	public Vector2IntEvent OnStartMovingFrom = new Vector2IntEvent();
	public Vector2IntEvent OnMoving = new Vector2IntEvent();
	public Vector2IntEvent OnMoveDone = new Vector2IntEvent();
	public UnityEvent OnDead = new UnityEvent();

	[HideInInspector]
	public Movable mover;

	protected HexPoint[] path = new HexPoint[16];

	private void Awake()
	{
		mover = GetComponent<Movable>();
	}

	public void SetParentBoard(Board b)
	{
		parentBoard = b;
		transform.SetParent(b.entityHolder);
	}

	// moveMode = 0: move exactly number or steps
	// moveMode = 1: move as far as possible
	public bool MoveStraight(Vector2Int direct, int step, int moveMode)
	{
		if (mover.IsMoving)
			return false;
			
		int pathLength = parentBoard.PathStraight(parentBoard.GetPoint(positionInBoard), direct, step, movementFilter, out path);
		if (moveMode == 0) {
			if (step <= pathLength)
				return MoveToPoint(path[step].positionInBoard.x, path[step].positionInBoard.y);
			else
				return false;
		}

		if (moveMode == 1) {
			if (step <= pathLength)
				return MoveToPoint(path[step].positionInBoard.x, path[step].positionInBoard.y);
			else if (pathLength > 0)
				return MoveToPoint(path[pathLength].positionInBoard.x, path[pathLength].positionInBoard.y);
		}

		return true;
	}

	public bool MoveToPoint(int x, int y)
	{
		if (mover.IsMoving)
			return false;

		HexPoint point = parentBoard.GetPoint(x, y);

		if (point == null)
			return false;

		OnStartMovingFrom.Invoke(positionInBoard.x, positionInBoard.y);

		positionInBoard = point.positionInBoard;
		OnMoving.Invoke(positionInBoard.x, positionInBoard.y);

		mover.MoveTo(point, moveTime, OnMoveDone);

		return true;
	}

	public void Kill()
	{
		OnDead.Invoke();
		gameObject.SetActive(false); 
	}
}
