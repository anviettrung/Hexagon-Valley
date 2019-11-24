using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPoint : MonoBehaviour
{
	public Vector2 boardPosition;
	public Vector2 worldPosition;

	public void SetPoint(int x, int y)
	{
		boardPosition = new Vector2(x, y);
		gameObject.name = "Point [" + x + ", " + y + "]";
	}
}
