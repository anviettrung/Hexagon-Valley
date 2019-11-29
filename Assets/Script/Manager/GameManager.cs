using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public Board modelBoard;
	public GameObject playerCharacterModel;
	public Vector2Int playerFirstPositionInBoard;

	public GoalDoor goal;

	[HideInInspector]
	public BoardEntity player;
	protected PlayerInput playerInput;
	protected Board controlBoard;

	void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		controlBoard = Instantiate(modelBoard.gameObject).GetComponent<Board>();
	}

	private void Init()
	{
		
	}

	private void Start()
	{
		goal = GameObject.FindObjectOfType<GoalDoor>();
		player = Instantiate(playerCharacterModel).GetComponent<BoardEntity>();
		player.OnDead.AddListener(RestartUIPopUp);
		controlBoard.AddEntity(player, playerFirstPositionInBoard);
		playerInput.Init(player);

		goal.AddTriggerEntity(player);
		goal.OnEntityEnter.AddListener(OnEntityEnterGoal);

		SnakeAI[] snakes = GameObject.FindObjectsOfType<SnakeAI>();
		for (int i=0; i < snakes.Length; i++) {
			controlBoard.AddEntity(snakes[i].body);
			snakes[i].StartHunting(player);
		}

		
	}

	void OnEntityEnterGoal(BoardEntity ent)
	{
		if (ent == player)
			WonUIPopUp();
	}

	void WonUIPopUp()
	{
		Debug.Log("Won");
	}

	void RestartUIPopUp()
	{
		Debug.Log("Restart UI Pop Up");
	}
}
