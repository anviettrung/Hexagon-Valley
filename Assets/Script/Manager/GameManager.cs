using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public Board boardModel;
	public GameObject playerCharacterModel;

	public Vector2Int playerFirstPositionInBoard;

	protected BoardEntity player;
	protected PlayerInput playerInput;

	void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		boardModel.NewBoard();
	}


	private void Start()
	{
		player = Instantiate(playerCharacterModel).GetComponent<BoardEntity>();
		boardModel.AddEntity(player, playerFirstPositionInBoard);
		playerInput.Init(player);
	}
}
