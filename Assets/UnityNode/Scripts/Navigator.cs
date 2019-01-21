using UnityEngine;

public class Navigator : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent agent;

	void Awake () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	
	public void NavigateTo (Vector3 position) {
		agent.SetDestination(position);
	}

	void Update (){
		GetComponent<Animator>().SetFloat("Distance", agent.remainingDistance);
	}
}
