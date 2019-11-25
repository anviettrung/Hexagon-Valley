using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public Board boardModel;
	public GameObject playerCharacterModel;

	public Vector2 playerFirstPositionInBoard;

	protected BoardEntity player;
	protected PlayerInput playerInput;

	void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
	}


	private void Start()
	{
		player = Instantiate(playerCharacterModel).GetComponent<BoardEntity>();
		player.positionInBoard= boardModel.GetPoint(playerFirstPositionInBoard).positionInBoard;
		player.board = boardModel;
		playerInput.Init(player);
	}
}
