using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Ball : MonoBehaviour {
	public bool isServer=false;
	public SocketIOComponent socket;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<Rigidbody2D>().velocity.magnitude>0 && isServer==true){
		socket.Emit("moveBall", new JSONObject(Network.VectorToJson(GetComponent<Rigidbody2D>().position)));
		}
	}
	public void NavigateTo(Vector3 position)
	{
		//GetComponent<Rigidbody2D>().MovePosition(position);
		 transform.position = Vector3.Lerp(transform.position, position, 1f);
	}
}
