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

	/// <summary>
	/// Create a new board size WxH
	/// </summary>
	/// <param name="w">The width.</param>
	/// <param name="h">The height.</param>
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

	/// <summary>
	/// Create a new board
	/// </summary>
	public void NewBoard()
	{
		NewBoard(width, height);
	}

	/// <summary>
	/// Gets a hex point at position (x, y) in hex matrix
	/// </summary>
	/// <returns>The hex point.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public HexPoint GetPoint(int x, int y)
	{
		if (x < 0 || x >= width)
			return null;
		if (y < 0 || y >= height)
			return null;

		int index = x + y * width;

		if (index < hexMatrix.Count)
			return hexMatrix[index];
			
		return null;
	}

	/// <summary>
	/// Gets a hex point in hex matrix
	/// </summary>
	/// <returns>The hex point.</returns>
	/// <param name="pos">Position in board.</param>
	public HexPoint GetPoint(Vector2Int pos)
	{
		
		return GetPoint(pos.x, pos.y);
	}

	/// <summary>
	/// Puts the entity on the board
	/// </summary>
	/// <returns><c>true</c>, if entity was added, <c>false</c> otherwise.</returns>
	/// <param name="ent">Entity.</param>
	/// <param name="firstPos">First position.</param>
	public bool AddEntity(BoardEntity ent, Vector2Int firstPos)
	{
		entities.Add(ent);
		ent.board = this;
		ent.positionInBoard = firstPos;
		ent.transform.position = GetPoint(firstPos).worldPosition;

		return true;
	}

	/// <summary>
	/// Puts the entity on the board
	/// </summary>
	/// <returns><c>true</c>, if entity was added, <c>false</c> otherwise.</returns>
	/// <param name="ent">Entity.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public bool AddEntity(BoardEntity ent, int x, int y)
	{
		return AddEntity(ent, new Vector2Int(x, y));
	}

	/// <summary>
	/// Puts the entity on the board at origin (0, 0)
	/// </summary>
	/// <returns><c>true</c>, if entity was added, <c>false</c> otherwise.</returns>
	/// <param name="ent">Entity.</param>
	public bool AddEntity(BoardEntity ent)
	{
		return AddEntity(ent, Vector2Int.zero);
	}
}
