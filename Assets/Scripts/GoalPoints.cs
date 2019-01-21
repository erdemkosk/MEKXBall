using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoints : MonoBehaviour {
	public bool IsLeftSide;
	private GameObject managers;
	// Use this for initialization
	ScoreManager scoreManager;
	void Start () {
		managers = GameObject.FindGameObjectWithTag("Manager");
		scoreManager = managers.GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag=="Ball")
		{
			if (IsLeftSide==true)
			{
				scoreManager.addGoalForRightSide();
			}
			else
			{
				scoreManager.addGoalForLeftSide();
			}

		}
	}
}
