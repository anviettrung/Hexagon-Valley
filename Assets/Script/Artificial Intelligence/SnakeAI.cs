using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAI : MonoBehaviour
{
	public BoardEntity body;
	public BoardEntity target;
	public int stepPerMove;
	public MoveType moveType;

	[System.Serializable]
	public enum MoveType
	{
		MoveType1,
		MoveType2
	}

	private void Awake()
	{
		body = GetComponent<BoardEntity>();
	}

	public void StartHunting(BoardEntity tar)
	{
		target = tar;
		target.OnMoving.AddListener(Chase);
	}

	public void Chase(int x, int y)
	{
		HexPoint[] path;
		int totalStep = body.board.Path(body.positionInBoard, target.positionInBoard, out path);
		if (totalStep >= stepPerMove && moveType == MoveType.MoveType1) {
			body.MoveToPoint(path[stepPerMove].positionInBoard.x, path[stepPerMove].positionInBoard.y);
		}
		if (totalStep > 0 && moveType == MoveType.MoveType2) {
			Vector2Int direct = path[1].positionInBoard - body.positionInBoard;
			Vector2Int des = body.positionInBoard + direct * stepPerMove;
			body.MoveToPoint(des.x, des.y);
		}

	}
}
