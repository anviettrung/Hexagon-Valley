using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hex map data", menuName = "Hexmap")]
public class HexMapData : ScriptableObject
{
	public int width;
	public int height;
	public float twoPointDistance;

	public List<HexPoint> hexPointModel = new List<HexPoint>();

	public GameObject[] hexMap;
	public List<BoardEntity> entities = new List<BoardEntity>();

	public void Reset()
	{ }

}
