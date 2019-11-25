using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	public int width;
	public int height;
	public float twoPointDistance;
	public HexPoint hexPointModel;

	[HideInInspector]
	public List<HexPoint> hexMatrix = new List<HexPoint>();
	public List<BoardEntity> entities = new List<BoardEntity>();

	public void NewBoard(int w, int h)
	{
		hexMatrix.Clear();

		width  = w;
		height = h;

		hexMatrix = new List<HexPoint>(width * height);

		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				HexPoint point = Instantiate(hexPointModel.gameObject).GetComponent<HexPoint>();

				point.SetParentBoard(this);
				point.SetPoint(j, i);

				hexMatrix.Add(point);
			}
		}

		hexMatrix.TrimExcess();
	}

	public void NewBoard()
	{
		NewBoard(width, height);
	}

	public HexPoint GetPoint(int x, int y)
	{
		if (x < 0 || x >= width)
			return null;
		if (y < 0 || y >= height)
			return null;

		int index = x + y * width;

		if (index < hexMatrix.Count)
			return hexMatrix[index];

		// Log error
		Debug.Log("error");
		return null;
	}

	public HexPoint GetPoint(Vector2Int pos)
	{
		
		return GetPoint(pos.x, pos.y);
	}

	public bool AddEntity(BoardEntity ent, Vector2Int firstPos)
	{
		entities.Add(ent);
		ent.board = this;
		ent.positionInBoard = firstPos;
		ent.transform.position = GetPoint(firstPos).worldPosition;

		return true;
	}

	public bool AddEntity(BoardEntity ent, int x, int y)
	{
		return AddEntity(ent, new Vector2Int(x, y));
	}

	public bool AddEntity(BoardEntity ent)
	{
		return AddEntity(ent, Vector2Int.zero);
	}
}
