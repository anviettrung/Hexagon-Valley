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
		body.OnMoveDone.AddListener(KillPlayerInThisPoint);
	}

	public void Chase(int x, int y)
	{
		//HexPoint[] path;
		//int totalStep = body.board.Path(body.positionInBoard, target.positionInBoard, out path);
		//if (stepPerMove <= totalStep && moveType == MoveType.MoveType1) {
		//	if (path[stepPerMove].canStay)
		//		body.MoveToPoint(path[stepPerMove].positionInBoard.x, path[stepPerMove].positionInBoard.y);
		//}
		//if (totalStep > 0 && moveType == MoveType.MoveType2) {
		//	Vector2Int direct = path[1].positionInBoard - body.positionInBoard;
		//	Vector2Int des;

		//	bool doMovement = true;
		//	for (int i = 1; i <= stepPerMove; i++) {
		//		des = body.positionInBoard + direct * i;
		//		if (body.board.GetPoint(des).canStay == false) {
		//			doMovement = false;
		//			break;
		//		}
		//	}

		//	if (doMovement) {
		//		des = body.positionInBoard + direct * stepPerMove;
		//		body.MoveToPoint(des.x, des.y);
		//	}
		//}

		int directIndex = 0;
		Vector2 direct = body.parentBoard.GetPoint(x,y).worldPosition - (Vector2)body.transform.position;
		float smallestAngle = Vector2.Angle(direct, ExdMath.DIRECTION_SIX_WORLD_COORD[0]);
		for (int i = 1; i < 6; i++) {
			float angle = Vector2.Angle(direct, ExdMath.DIRECTION_SIX_WORLD_COORD[i]);
			if (angle < smallestAngle) {
				smallestAngle = angle;
				directIndex = i;
			}
		}


		body.MoveStraight(ExdMath.DIRECTION_SIX[directIndex], stepPerMove, 1);
	}

	public void KillPlayerInThisPoint(int x, int y)
	{
		if (GameManager.Instance.player.positionInBoard == new Vector2Int(x,y))
			GameManager.Instance.player.Kill();
	}
}
