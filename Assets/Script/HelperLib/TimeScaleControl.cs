using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleControl : MonoBehaviour {

	public bool willScaleTime;
	public float timeScale;

	void Update()
	{
		if (Time.timeScale == 1.0f && willScaleTime)
			Time.timeScale = timeScale;
	}

	void OnDisable()
	{
		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
	}
}
