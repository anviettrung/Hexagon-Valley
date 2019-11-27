using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public BoardEntity player;
	protected Board controlBoard;

	public void Init(BoardEntity ent)
	{
		if (controlBoard != null) { 
			for (int i = 0; i < player.board.hexMap.Count; i++)
				player.board.hexMap[i].OnTouched.RemoveListener(Move);
		}

		player = ent;
		controlBoard = ent.board;
		player.OnStartMovingFrom.AddListener(OnStartMoving);
		player.OnMoveDone.AddListener(OnMoveDone);

		for (int i = 1; i < controlBoard.hexMap.Count; i++) {
			controlBoard.hexMap[i].OnTouched.AddListener(Move);
		}

		ChangeHexpointStateAroundPosition(player.positionInBoard, HexPoint.State.CAN_BE_SELECT);
	}

	private void Move(int x, int y)
	{
		if (controlBoard.GetPoint(x, y).state == HexPoint.State.CAN_BE_SELECT) {
			Vector2Int startPos = player.positionInBoard;
			player.MoveToPoint(x, y);
		}
	}

	protected void ChangeHexpointStateAroundPosition(Vector2Int pos, HexPoint.State state)
	{
		for (int i = 0; i < ExdMath.DIRECTION_SIX.Length; i++) {
			HexPoint p = controlBoard.GetPoint(pos + ExdMath.DIRECTION_SIX[i]);
			if (p != null)
				p.ChangeStateTo(state);
		}
	}

	void OnStartMoving(int x, int y)
	{
		ChangeHexpointStateAroundPosition(controlBoard.GetPoint(x, y).positionInBoard, HexPoint.State.NORMAL);
	}

	void OnMoveDone(int x, int y)
	{
		ChangeHexpointStateAroundPosition(controlBoard.GetPoint(x, y).positionInBoard, HexPoint.State.CAN_BE_SELECT);
	}
}
