using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Experimental.Build;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour {

    private Vector3 desiredPosition;
    private NavMeshPath desiredPath;
    private float maxSpeed, panicMult, caution;
    private float panic = 0, certainty = 0, awareness = 1, fallChance = 0.0001f;
    private List<GameObject> mapKnowledge = new List<GameObject>();
    
    public bool IsMapKnowledgeRequested { get; set; }

    private void Start() {
        if (mapKnowledge.Count == 0) {
            RequestMapKnowledge();
        }
    }

    private  void FixedUpdate() {
        Move();
    }

    private void Move() {
        var body = gameObject.GetComponent<Rigidbody>();
        if (desiredPosition == null) {
            
        } else {
            
        }
    }

    private bool GetClosestExit(out Vector3 exit) {
        if (mapKnowledge.Count == 0) {
            exit = Vector3.zero;
            return false;
        } else {
            
            return true;
        }
    }
    
    private void RequestMapKnowledge() {
        IsMapKnowledgeRequested = true;
    }

    public void ReceiveMapKnowledge(List<GameObject> mapKnowledge) {
        this.mapKnowledge.AddRange(mapKnowledge);
    }
    
    public float MaxSpeed {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }

    public float PanicMult {
        get { return panicMult; }
        set { panicMult = value; }
    }

    public float Caution {
        get { return caution; }
        set { caution = value; }
    }

    private void ChangePanic(float panicSource) {
        panic += panicSource * panicMult;
    }

}