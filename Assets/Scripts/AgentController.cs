using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Build;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour {

	private Vector3 desiredPosition = new Vector3(0, 0, -20);
	
	void Start() {
		NavMeshAgent nav = GetComponent<NavMeshAgent>();
		NavMeshPath path = new NavMeshPath();
		if (nav.CalculatePath(desiredPosition, path)) {
			List<GameObject> pathPointList = new List<GameObject>();
			foreach (Vector3 corner in path.corners) {
				GameObject pathPoint = new GameObject();
				pathPoint.GetComponent<Transform>().position = corner;
				pathPointList.Add(pathPoint);
			}
		}
		
	}
	
	void Update() {
		
	}

	Vector3 CalculateMoveIncrement() {
		
		return Vector3.zero;
	}

	void UpdateDesiredPosition() {
		
	}
}
