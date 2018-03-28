using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AI;

public class MapNodeController : MonoBehaviour {
    
    private List<GameObject> connectedNodeList = new List<GameObject>();

    void Start() {
        
    }

    void Update() {
        
    }

    public void AddEdge(GameObject node) {
        connectedNodeList.Add(node);
        Debug.DrawLine(gameObject.transform.position, node.transform.position, Color.blue, 60f, false);
    }

    public void AddEdges(IEnumerable<GameObject> nodes) {
        connectedNodeList.AddRange(nodes);
        foreach (var node in nodes) {
            Debug.DrawLine(gameObject.transform.position, node.transform.position, Color.green, 60f, false);
        }
    }

    public List<GameObject> GetConnectedNodeList() {
        return connectedNodeList;
    }

    public GameObject GetNodeDoorPair() {
        return connectedNodeList[0];
    } 

}