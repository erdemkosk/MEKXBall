using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
	public enum GoalKeeperActions { HoGome, MoveUp, MoveDown, DontMove };
	[Range(1f, 5f)]
	public float goalKeeperAwareness;
	[Range(1f, 6f)]
	public float movingSpeed; // goalkeeper speed
	[Range(1f, 2f)]
	public float returningSpeed; // goalkeeper speed
	public TextMesh playerStatus;

	private GoalKeeperActions GoalKeeperAction;

	public Transform ball;
	public GameObject gate; // pos in field

	Vector2 gateArea; // area on which will move goalkeeper
	float distance;
	float move;
	float gateStartPoint;
	float gateSize;
	float goalkeeperSize;


	void Start()
	{
		gateStartPoint = gate.GetComponent<SpriteRenderer>().bounds.min.y;
		gateSize = (gate.GetComponent<SpriteRenderer>().bounds.max.y - gateStartPoint);
		gateArea = new Vector2(gateStartPoint, gateStartPoint + gateSize);
		//GetComponent<Rigidbody2D>().position = new Vector3(gate.GetComponent<Rigidbody2D>().position.x, gateStartPoint + (gateSize * 0.5f), 0); // put goalkepper in middle of the gate
		goalkeeperSize = GetComponent<SpriteRenderer>().bounds.max.y - GetComponent<SpriteRenderer>().bounds.min.y;
	}

	void Update()
	{
		
		distance = Mathf.Abs(ball.position.x - GetComponent<Rigidbody2D>().position.x);

		if (distance < goalKeeperAwareness)
		{
		
			if (ball.position.y > transform.position.y + goalkeeperSize/2) // Topun yukarda olma ihtimali
			{
				GoalKeeperAction = GoalKeeperActions.MoveUp;
			}
			if (ball.position.y < transform.position.y - goalkeeperSize/2) // Topun asagıda olma ihtimali
			{
				GoalKeeperAction = GoalKeeperActions.MoveDown;
			}
			
			

		}
		if (distance > goalKeeperAwareness)
		{
			GoalKeeperAction = GoalKeeperActions.HoGome;
		}

		if (distance < 0)   // when ball is on goalkeeper
		{
			GoalKeeperAction = GoalKeeperActions.DontMove;
			// top kaleyi gecerse
		}

		DoKeeperAction();



	}
	void DoKeeperAction()
	{
		switch (GoalKeeperAction)
		{
			case GoalKeeperActions.HoGome:
				
				Vector2 newPos = new Vector3(gate.transform.position.x, gate.transform.position.y, 0); 
				transform.position = Vector2.Lerp(transform.position, newPos, (Time.deltaTime) * returningSpeed);
				playerStatus.text = "Go Home";
				break;
			case GoalKeeperActions.MoveUp:
				move = Mathf.Clamp(Mathf.Abs(distance), 0, (movingSpeed / 100));
				MovePosition();
				playerStatus.text = "Move Up";
				break;
			case GoalKeeperActions.MoveDown:
				move = Mathf.Clamp(Mathf.Abs(distance), 0, (movingSpeed / 100));
				move = -move;
				MovePosition();
				playerStatus.text = "Move Down";
				break;
			case GoalKeeperActions.DontMove:
				move *= -1;
				MovePosition();
				playerStatus.text = "Dont Move";
				break;
			default:
				break;
		}
		

	}
	void MovePosition()
	{
		Vector3 position = GetComponent<Rigidbody2D>().position;
		position.y = Mathf.Clamp(position.y + move, gateArea.x, gateArea.y);
		GetComponent<Rigidbody2D>().position = position;
	}
}
