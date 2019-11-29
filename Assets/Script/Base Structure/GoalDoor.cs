using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalDoor : BoardEntity
{
	public List<BoardEntity> triggerEntities = new List<BoardEntity>();
	public BoardEntityEvent OnEntityEnter = new BoardEntityEvent();

	private void Start()
	{
		//for (int i = 0; i < triggerEntities.Count; i++) {
		//	triggerEntities[i].OnMoveDone.AddListener(OnTriggerEntityMove);
		//}
	}

	public void AddTriggerEntity(BoardEntity ent)
	{
		if (ent == null)
			return;

		ent.OnMoveDone.AddListener(OnTriggerEntityMove);
		triggerEntities.Add(ent);
	}

	public void OnTriggerEntityMove(int x, int y)
	{
		Debug.Log(x+" "+y);
		HexPoint pos = parentBoard.GetPoint(x, y);
		if (pos.positionInBoard != positionInBoard)
			return;

		List<BoardEntity> ents = parentBoard.FindEntitiesAtPoint(pos);
		if (ents == null)
			return;
			
		for (int i = 0; i < triggerEntities.Count; i++) {
			for (int j = 0; j < ents.Count; j++) {
				if (ents[j] == triggerEntities[i]) {
					Debug.Log("Wow");
					OnEntityEnter.Invoke(ents[j]);
				}
			}
		}
	}
}
