using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassManager : MonoBehaviour {
	GameObject playerHasABall; // Suan topa sahip olan kişi
	GameObject playerWaitForPass; //Pas bekleyen kişi


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public GameObject GetPlayerForHasingingABall()
	{
		return playerHasABall ;
	}
	public GameObject GetPlayerForWaitAPass()
	{
		return playerWaitForPass ;
	}
}
