using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	public GameObject ball;
	public Transform centerPoint;
	public TextMesh scoreBoard;
	public GameObject[] allPlayers;
	int leftGoalCounter=0;
	int rightGoalCounter=0;
	private bool isResetball = false;
	float timeLeft = 2f;

	void Awake()
	{
		

}

// Use this for initialization
void Start () {
		scoreBoard.text = "Score: 0-0";
		allPlayers = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (isResetball==true)
		{
			timeLeft -= Time.deltaTime;
			foreach (var item in allPlayers)
			{
				item.GetComponent<Player>().enabled = false;
			}

			if (timeLeft < 0)
			{
				
				ball.GetComponent<CircleCollider2D>().enabled = false;
				ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
				ball.GetComponent<Rigidbody2D>().rotation = 0;
				ball.GetComponent<Rigidbody2D>().angularVelocity = 0;
				ball.transform.position = Vector3.Lerp(ball.transform.position, centerPoint.position, (Time.deltaTime) * 2);
				if (Mathf.Abs(Vector3.Distance(ball.transform.position, centerPoint.position)) < 0.05f)
				{

					isResetball = false;
					timeLeft = 2f;
					ball.GetComponent<CircleCollider2D>().enabled = true;
					foreach (var item in allPlayers)
					{
						item.GetComponent<Player>().enabled = true;
					}
				}
			}
			
		}
		
	}

	public void addGoalForLeftSide() //sağ taraf gol yemişse
	{
		if(isResetball != true)
		{
			leftGoalCounter++;
			UpdateScoreBoard();
			ResetBall();
		}
	
	}
	public void addGoalForRightSide() //sol taraf gol yemişse
	{
		if (isResetball != true)
		{
			rightGoalCounter++;
			UpdateScoreBoard();
			ResetBall();
		}
		
	}
	private void UpdateScoreBoard()
	{
		scoreBoard.text = "Score: " + leftGoalCounter + "-" + rightGoalCounter;
	}
	public void ResetBall()
	{
		isResetball = true;
	}
}
