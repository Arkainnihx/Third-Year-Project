using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Build;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour {

	private Vector3 endPosition = new Vector3(0, 0, -20);
	private Vector3 desiredPosition;
	private Vector2 desiredVelocity = Vector2.zero;
	private NavMeshAgent nav;
	private Vector3[] path;
	
	void Start() {
		nav = GetComponent<NavMeshAgent>();
		GenerateInitialPath();
	}
	
	void Update() {
		
	}

	bool GenerateInitialPath() {
		var newPath = new NavMeshPath();
		if (nav.CalculatePath(endPosition, newPath)) {
			path = newPath.corners;
			var test = new List<Vector3>();
			test.AddRange(path);
		}
		return path.Length > 0;
	}

	Vector2 CalculateMoveIncrement() {
		var moveIncrement = Vector2.zero;
		
		return moveIncrement;
	}
	
	void UpdateDesiredPosition() {
		
	}

	bool isNewPositionDesired() {

		return true;
	}

	Vector2 SocialForce() {
		
		return Vector2.zero;
	}
	
}
