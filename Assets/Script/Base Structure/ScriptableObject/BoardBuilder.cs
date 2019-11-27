using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BoardBuilder : MonoBehaviour
{
	public string mapName;
	public Board mapDataToOpen;
	public Board standardBoard;

	protected Board currentBoard;


	public void NewMap()
	{
		GameObject clone = Instantiate(standardBoard.gameObject);
		InitBoard(clone.GetComponent<Board>());
	}

	public void InitBoard(Board b)
	{
		if (currentBoard != null)
			Destroy(currentBoard.gameObject);

		currentBoard = b;
		for (int i = 0; i < currentBoard.hexMap.Count; i++) {
			currentBoard.hexMap[i].OnTouched.AddListener(OnClickPoint);
		}
	}

	public void OnClickPoint(int x, int y)
	{
		currentBoard.GetPoint(x, y).ToggleState();
	}

	public void Open()
	{
		if (mapDataToOpen == null)
			return;

		GameObject clone = Instantiate(mapDataToOpen.gameObject);
		InitBoard(clone.GetComponent<Board>()); 
	}

	public void Export()
	{
		PrefabUtility.SaveAsPrefabAsset(currentBoard.gameObject, "Assets/Prefab/Map/[Hexmap]" + mapName + ".prefab");
	}
}
