using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Vector2IntEvent : UnityEvent<int, int>
{

}

// Extended Math Functions
public static class ExdMath {

	public const float COS60 = 0.5f;
	public const float SIN60 = 0.866f;

	public static float SignedAngle(Vector2 vec1, Vector2 vec2) {
		int signed = 1;
		if (Vector3.Cross(vec1, vec2).z < 0) signed = -1;
		return signed * Vector3.Angle(vec1, vec2);
	}
}
