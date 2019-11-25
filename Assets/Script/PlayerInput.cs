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
			for (int i = 0; i < player.board.hexMatrix.Count; i++)
				player.board.hexMatrix[i].OnTouched.RemoveListener(Move);
		}

		player = ent;
		controlBoard = ent.board;

		for (int i = 1; i < controlBoard.hexMatrix.Count; i++) {
			Debug.Log(controlBoard.hexMatrix[i]);
			controlBoard.hexMatrix[i].OnTouched.AddListener(Move);
		}
	}

	private void Move(int x, int y)
	{
		player.MoveToPoint(x, y);
	}
}
