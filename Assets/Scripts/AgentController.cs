using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEditor.Experimental.Build;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour {

    private int pathIndex = 0;
    private NavMeshPath desiredPath;
    private float panic = 0, certainty = 0, awareness = 1, fallChance = 0.0001f;
    private MapGraph mapKnowledge = new MapGraph();
    private NavMeshAgent nav;
    private Vector3 desiredPosition {
        get { return desiredPath.corners[pathIndex]; }
    }

    public bool IsMapKnowledgeRequested { get; set; }
    public float MaxSpeed { get; set; }
    public float PanicMult { get; set; }
    public float Caution { get; set; }

    private void Start() {
        nav = gameObject.GetComponent<NavMeshAgent>();
        InvokeRepeating("UpdateDesiredPoint", 0.1f, 0.2f);
    }

    private  void FixedUpdate() {
        if (mapKnowledge.Count == 0) {
            RequestMapKnowledge();
        }
        if (desiredPath != null && desiredPath.corners.Length > 0) {
            Move();
        } else {
            CalculateExitPath();
        }
    }

//    private void LateUpdate() {
//        Mathf.Clamp(gameObject.GetComponent<Rigidbody>().velocity);
//    }

    private void Move() {
        var body = gameObject.GetComponent<Rigidbody>();
        var heading = desiredPosition - gameObject.transform.position;
        var desiredDirection = heading / heading.magnitude;
        body.AddForce(desiredDirection * (MaxSpeed - body.velocity.magnitude), ForceMode.VelocityChange);
        Debug.DrawRay(gameObject.transform.position, desiredDirection, Color.green, 0f, false);
        Debug.DrawRay(gameObject.transform.position, body.velocity, Color.red, 0f, false);
    }

    private bool CalculateExitPath() {
        if (mapKnowledge.Count > 0) {
            var path = new NavMeshPath();
            nav.enabled = true;
            nav.CalculatePath(mapKnowledge.GetExitNodes()[0].transform.position, path);
            nav.enabled = false;
            desiredPath = path;
            Debug.DrawLine(gameObject.transform.position, path.corners[0], Color.blue, 60f, false);
            for (var pointCount = 1; pointCount < path.corners.Length; pointCount++) {
                Debug.DrawLine(path.corners[pointCount - 1], path.corners[pointCount], Color.blue, 60f, false);
            }
            return true;
        }
        return false;
    }

    private void UpdateDesiredPoint() {
        var hit = new NavMeshHit();
        nav.enabled = true;
        if (pathIndex < desiredPath.corners.Length - 1) {
            if (!nav.Raycast(desiredPath.corners[pathIndex + 1], out hit)) {
                Debug.DrawLine(gameObject.transform.position, desiredPath.corners[pathIndex + 1], Color.magenta, 1f, false);
                pathIndex++;
            }
            if (nav.Raycast(desiredPath.corners[pathIndex], out hit)) {
                Debug.DrawLine(gameObject.transform.position, desiredPath.corners[pathIndex], Color.cyan, 1f, false);
                pathIndex--;
            }
        }
        nav.enabled = false;
        Debug.Log(desiredPosition);
    }
    
    private void RequestMapKnowledge() {
        IsMapKnowledgeRequested = true;
    }

    public void ReceiveMapKnowledge(MapGraph mapKnowledge) {
        this.mapKnowledge.AddRange(mapKnowledge);
        IsMapKnowledgeRequested = false;
    }

    private void AddPanic(float panicSource) {
        panic += panicSource * PanicMult;
    }

}