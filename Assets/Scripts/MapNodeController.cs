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
    }

    public void AddEdges(IEnumerable<GameObject> nodes) {
        connectedNodeList.AddRange(nodes);
    }

    public List<GameObject> GetConnectedNodeList() {
        return connectedNodeList;
    }

}