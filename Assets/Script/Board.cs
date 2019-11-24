using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	public int width;
	public int height;
	public float twoPointDistance;
	public HexPoint hexPointModel;

	protected List<HexPoint> hexMatrix = new List<HexPoint>();

	public const float cos60 = 0.5f;
	public const float sin60 = 0.866f;

	protected void Start()
	{
		NewBoard();
	}

	public void NewBoard(int w, int h)
	{
		hexMatrix.Clear();

		hexMatrix = new List<HexPoint>(w * h);

		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				HexPoint point = Instantiate(hexPointModel.gameObject).GetComponent<HexPoint>();

				point.SetPoint(j, i);
				SetPointPosition(point);

				point.transform.SetParent(this.transform);
				hexMatrix.Add(point);
			}
		}

		hexMatrix.TrimExcess();
	}

	public void NewBoard()
	{
		NewBoard(width, height);
	}

	public void LogMatrix()
	{

	}

	public void SetPointPosition(HexPoint point)
	{
		float d = twoPointDistance;
		Vector2 pos      = point.boardPosition;
		Vector2 boardPos = this.transform.position;
		float deltaX = d * (pos.x - pos.y * cos60);
		float deltaY = -d * pos.y * sin60;

		point.transform.position = new Vector2(boardPos.x + deltaX, boardPos.y + deltaY);
		point.worldPosition = point.transform.position;
	}

	public HexPoint getPoint(int x, int y)
	{
		int index = x + y * width;
		if (index < hexMatrix.Count)
			return hexMatrix[index];

		// Log error
		return null;
	}
}
