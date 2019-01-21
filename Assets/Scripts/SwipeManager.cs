using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour {
	Vector2 starPos, endPos, direction;
	float touchTimeStart, touchTimeFinish, timeInterval;
	[Range(0.05f,0.5f)]
	public float throwForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			touchTimeStart = Time.time;
			starPos = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp(0))
		{
			touchTimeFinish = Time.time;
			timeInterval = touchTimeFinish - touchTimeStart;
			endPos = Input.mousePosition;
			direction = starPos - endPos;
			GetComponent<Rigidbody2D>().AddForce(-direction/timeInterval*throwForce);
		}
	}
}
