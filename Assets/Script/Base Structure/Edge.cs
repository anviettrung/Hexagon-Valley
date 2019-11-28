using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EdgeEvent : UnityEvent<Edge>
{

}

public class Edge : MonoBehaviour
{
	public HexPoint[] linkedHexpoint = new HexPoint[2]; // The position is
	public Vector2 worldPosition;
	public Board parentBoard;

	public State state;

	[HideInInspector]
	public EdgeEvent OnTouched = new EdgeEvent();

	private SpriteRenderer spriteRenderer;
	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		OnTouched.RemoveAllListeners();

		ForceChangeStateTo(state);
	}

	public void SetLink(HexPoint a, HexPoint b)
	{
		linkedHexpoint[0] = a;
		linkedHexpoint[1] = b;

		gameObject.name = "Edge between: " + a.name + ", " + b.name;

		SetPositionInWorldCoordinate();

		UpdateLink();
	}

	public void SetParentBoard(Board b)
	{
		parentBoard = b;
		transform.SetParent(b.transform);
	}

	public void SetPositionInWorldCoordinate()
	{
		worldPosition = (linkedHexpoint[0].worldPosition + linkedHexpoint[1].worldPosition) * 0.5f;
		Vector2Int direct = linkedHexpoint[0].positionInBoard - linkedHexpoint[1].positionInBoard;

		int res = ExdMath.FindInDirectionSix(direct);
		if (res == -1)
			Debug.LogError("KJ^%&^$&DFG");

		transform.rotation = Quaternion.Euler(ExdMath.ROTATION_SIX[res]);
		transform.position = worldPosition;
	}

	//public int Distance(Vector2Int targetPoint)
	//{
	//	return parentBoard.Distance(positionInBoard, targetPoint);
	//}

	public void ToggleState()
	{
		if (state == State.NORMAL)
			ChangeStateTo(State.WIREFRAME);
		else if (state == State.WIREFRAME)
			ChangeStateTo(State.CAN_BE_SELECT);
		else
			ChangeStateTo(State.NORMAL);
	}

	private void OnMouseDown()
	{
		OnTouched.Invoke(this);
	}

	public void ChangeStateTo(State s)
	{
		if (state == State.WIREFRAME)
			return;

		ForceChangeStateTo(s);
	}

	public void ForceChangeStateTo(State s)
	{
		state = s;
		anim.SetInteger("StateCode", (int)s);
		UpdateLink();
	}

	public bool IsLinkBetween(HexPoint a, HexPoint b)
	{
		if (a == linkedHexpoint[0] && b == linkedHexpoint[1])
			return true;

		if (a == linkedHexpoint[1] && b == linkedHexpoint[0])
			return true;

		return false;
	}

	public void UpdateLink()
	{
		if (linkedHexpoint[0] == null || linkedHexpoint[1] == null)
			return;

		Vector2Int direct = linkedHexpoint[1].positionInBoard - linkedHexpoint[0].positionInBoard;

		int res = ExdMath.FindInDirectionSix(direct);
		if (res == -1)
			Debug.LogError("KJ^%&^$&DFG");

		byte setVal = state == State.WIREFRAME ? (byte)0 : (byte)1; // if normal then set to 1

		linkedHexpoint[0].edges[res] = setVal;
		linkedHexpoint[1].edges[(res + 3) % 6] = setVal; // opposite direction
	}

	public enum State
	{
		NORMAL,
		WIREFRAME,
		CAN_BE_SELECT,
	};
}
