using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BoardBuilder : MonoBehaviour
{
	public string mapName;
	public Board mapDataToOpen;
	public Board standardBoard;
	public bool isEditingMap;
	public List<BoardEntity> availableEntities;
	public BoardEntity selectingEntity;

	protected Board currentBoard;

	public Vector2Int vecA;
	public Vector2Int vecB;

	private void Start()
	{
		OnChangeSelectingEntity(0);
	}


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
		//currentBoard.NewBoard();
		for (int i = 0; i < currentBoard.hexPoints.Count; i++) {
			currentBoard.hexPoints[i].OnTouched.AddListener(OnClickPoint);
		}

		for (int i = 0; i < currentBoard.edges.Count; i++) {
			currentBoard.edges[i].OnTouched.AddListener(OnClickEdge);
		}
	}

	public Edge NewEdge(Vector2Int pa, Vector2Int pb)
	{
		Edge e = currentBoard.AddEdge(pa, pb);
		if (e != null)
			e.OnTouched.AddListener(OnClickEdge);

		return e;
	}

	public void AddEdge()
	{
		NewEdge(vecA, vecB);
	}


	public void AddFullEdge()
	{
		 for (int i = 0; i < currentBoard.hexPoints.Count; i++) {
			for (int j = 0; j < 6; j++) {
			 	Edge e = NewEdge(
					currentBoard.hexPoints[i].positionInBoard,
					currentBoard.hexPoints[i].positionInBoard + ExdMath.DIRECTION_SIX[j]
				);
				if (e != null)
					e.ForceChangeStateTo(Edge.State.WIREFRAME);
			}
		}
	}

	public void SwitchEditMapMode(bool x)
	{
		isEditingMap = x;
	}

	public void OnChangeSelectingEntity(int x)
	{
		Debug.Log(x);
		selectingEntity = availableEntities[x];
	}

	public void OnClickEdge(Edge e)
	{
		if (isEditingMap) {

			if (e.state == Edge.State.WIREFRAME) {
				e.ForceChangeStateTo(Edge.State.NORMAL);
			} else {
				e.ForceChangeStateTo(Edge.State.WIREFRAME);
			}

		} else {

		}
	}


	public void OnClickPoint(int x, int y)
	{
		if (isEditingMap) {

			HexPoint p = currentBoard.GetPoint(x, y);

			if (p.state == HexPoint.State.WIREFRAME) {
				p.ForceChangeStateTo(HexPoint.State.NORMAL);
			} else {
				p.ForceChangeStateTo(HexPoint.State.WIREFRAME);
			}

		} else {
			BoardEntity ent = currentBoard.FindEntityAtPoint(currentBoard.GetPoint(x, y));
			if (ent != null) {
				currentBoard.RemoveEntity(ent);
			} else {
				ent = Instantiate(selectingEntity.gameObject).GetComponent<BoardEntity>();
				currentBoard.AddEntity(ent, x, y);
			}
		}

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
