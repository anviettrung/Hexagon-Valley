using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Vector2IntEvent : UnityEvent<int, int> {}

[System.Serializable]
public class BoardEntityEvent : UnityEvent<BoardEntity> {}

// Extended Math Functions
public static class ExdMath {

	public const float COS60 = 0.5f;
	public const float SIN60 = 0.866f;

	public static readonly Vector2Int[] DIRECTION_SIX = {
		new Vector2Int(0, -1),
		new Vector2Int(1, 0),
		new Vector2Int(1, 1),
		new Vector2Int(0, 1),
		new Vector2Int(-1, 0),
		new Vector2Int(-1, -1)
	};

	public static readonly Vector2[] DIRECTION_SIX_WORLD_COORD = {
		new Vector2(COS60, SIN60),
		new Vector2(1, 0),
		new Vector2(COS60, -SIN60),
		new Vector2(-COS60, -SIN60),
		new Vector2(-1, 0),
		new Vector2(-1, SIN60)
	};

	public static readonly Vector3[] ROTATION_SIX = {
		new Vector3(0, 0, 60),
		new Vector3(0, 0, 0),
		new Vector3(0, 0, -60),
		new Vector3(0, 0, -120),
		new Vector3(0, 0, 180),
		new Vector3(0, 0, 120)
	};

	public static float SignedAngle(Vector2 vec1, Vector2 vec2) {
		int signed = 1;
		if (Vector3.Cross(vec1, vec2).z < 0) signed = -1;
		return signed * Vector3.Angle(vec1, vec2);
	}

	public static int FindInDirectionSix(Vector2Int v)
	{
		for (int i = 0; i < 6; i++)
			if (v == ExdMath.DIRECTION_SIX[i])
				return i;
	

		return -1; // not found
	}
}
