using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Build;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour {

	public Transform goal;
	private NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(goal.position);
	}
	
	void Update () {
		if (agent.remainingDistance < 2) {
			agent.ResetPath();
		}
	}
}
