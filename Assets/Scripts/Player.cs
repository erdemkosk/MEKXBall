using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
public class Player : MonoBehaviour {
	[Range(1,4)]
	public float movingSpeed;
	public bool DisableMovement;
	public SocketIOComponent socket;
	public TextMesh playerStatus;
	public bool isAI = false;
	GameObject enemyGate;
	public Teams Team;
	[Range(5, 12)]
	public float shootErrorFactor;
	public Sprite SpriteRed;
	float firstSpeed;
	private PlayerActions PlayerAction;
	private float AıShootDistance = 5;
	
   

	//Define Enum
	public enum Teams { Left,Right };
	public enum PlayerActions { HoGome, ChasingABall, Shot, DoPassABall, WaitPassABall, MovingEnemyGate };

	//This is what you need to show in the inspector.
	GameObject managers;

	private GameObject ball;
	Vector3 tempVect;

	

	void Awake()
	{
		firstSpeed = movingSpeed;
		;
	
		managers = GameObject.FindGameObjectWithTag("Manager");
		if (Team == Teams.Right)
		{
			enemyGate = GameObject.FindGameObjectWithTag("Gate-Left");
		}
		else
		{
			enemyGate = GameObject.FindGameObjectWithTag("Gate-Right");
		}
	}
	public void Update()
	{

       

        if (isAI == true)
		{
			PlayerAction = PlayerActions.MovingEnemyGate;
					if (Mathf.Abs(Vector3.Distance(transform.position, enemyGate.transform.position)) < AıShootDistance)
					{
						if (Mathf.Abs(transform.position.y - enemyGate.transform.position.y) < 1f)
						{
							PlayerAction = PlayerActions.Shot;
						}
					}
			
			
		}
	
		else if(DisableMovement!=true)
		{
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			float total =Mathf.Abs(h + v);

			tempVect = new Vector3(h, v, 0);
			tempVect = tempVect.normalized * movingSpeed * Time.deltaTime;
			GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().transform.position + tempVect);
            if (h != 0 || v != 0)
            {

                if (socket != null)
                {
                    socket.Emit("move", new JSONObject(Network.VectorToJson(GetComponent<Rigidbody2D>().position)));

                }
                else
                {
                    Debug.Log("socket baglanamadı");
                }
            }





            if (total>0)
			{
				
				//transform.rotation = Quaternion.LookRotation(Vector3.forward, tempVect);

				float angle = Mathf.Atan2(tempVect.x, tempVect.y) * Mathf.Rad2Deg;
				Quaternion q = Quaternion.AngleAxis(-angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * movingSpeed);
				
			}
			
			if (Input.GetKeyDown(KeyCode.G)) //Shoot
			{
				
	            MakeShot(tempVect);

			}
			
			if (Input.GetKeyDown(KeyCode.F)) //Speed
			{
				if (movingSpeed<15)
				{
					movingSpeed += 1f;
				}
				
			}
			if (Input.GetKeyUp(KeyCode.F)) //Speed
			{
				movingSpeed = firstSpeed;
			}
			playerStatus.text = "Controlled By User";

		}
		DoPlayerAction();

	}
	void DoPlayerAction()
	{
		switch (PlayerAction)
		{
			case PlayerActions.HoGome:
				break;
			case PlayerActions.ChasingABall:
				transform.position = Vector2.MoveTowards(transform.position, ball.transform.position, movingSpeed * Time.deltaTime);
				LookAt(ball.transform.position);
				playerStatus.text = "Chasing Ball";
				break;
			case PlayerActions.Shot:
				Vector3 randomVector = new Vector3(enemyGate.transform.position.x + UnityEngine.Random.Range(-shootErrorFactor, shootErrorFactor), enemyGate.transform.position.y + UnityEngine.Random.Range(-shootErrorFactor, shootErrorFactor), enemyGate.transform.position.z);
				MakeShot(randomVector);
				AıShootDistance = UnityEngine.Random.Range(4f, 12f);
				break;
		
			case PlayerActions.MovingEnemyGate:
				
					transform.position = Vector2.MoveTowards(transform.position, enemyGate.transform.position, movingSpeed * Time.deltaTime);
					playerStatus.text = "Moving Enemy Gate";
					LookAt(enemyGate.transform.position);
				break;
			default:
				break;
		}


	}

	void MakeShot(Vector3 position)
	{
        if (ball!=null)
        {
            ball.GetComponent<Rigidbody2D>().simulated = true;
            ball.GetComponent<Rigidbody2D>().AddForce(position.normalized * 250f);
        }
	
	}

	
	void LookAt(Vector3 lookatPosition)
	{
		Vector3 diff = lookatPosition - transform.position;
		diff.Normalize();
		float angle = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(-angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * movingSpeed);
		
	}
	
	public void NavigateTo(Vector3 position)
	{
		Debug.Log("saas");
        GetComponent<Rigidbody2D>().MovePosition(position);
		// transform.position = Vector3.Lerp(transform.position, position, 1f);
	}
	public void SetBlueTeam(){
	//GetComponent<SpriteRenderer>().sprite = SpriteRed;
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "ball")
        {
            ball = col.transform.gameObject;
           
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name=="ball")
        {
            ball = null;
        }
    }

}
