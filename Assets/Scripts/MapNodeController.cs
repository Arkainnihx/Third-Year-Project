using System.Collections.Generic;
using UnityEngine;

public class MapNodeController : MonoBehaviour {
    
    private List<GameObject> connectedNodeList = new List<GameObject>();
    private bool isExitNode;
    public bool IsExitNode {
        get { return isExitNode; }
        private set { isExitNode = value; }
    }
    
    private void Start() {
        ExitNodeCheck();
    }

    public void AddEdge(GameObject node) {
        connectedNodeList.Add(node);
//        Debug.DrawLine(gameObject.transform.position, node.transform.position, Color.blue, 60f, false);
    }

    public void AddEdges(IEnumerable<GameObject> nodes) {
        connectedNodeList.AddRange(nodes);
//        foreach (var node in nodes) {
//            Debug.DrawLine(gameObject.transform.position, node.transform.position, Color.blue, 60f, false);
//        }
    }

    public IEnumerable<GameObject> GetConnectedNodeList() {
        return connectedNodeList;
    }

    public GameObject GetNodeDoorPair() {
        return connectedNodeList[0];
    }

    private void ExitNodeCheck() {
        var forward = Physics.Raycast(gameObject.transform.position, gameObject.transform.forward);
        var backward = Physics.Raycast(gameObject.transform.position, gameObject.transform.forward*-1f);
        var right = Physics.Raycast(gameObject.transform.position, gameObject.transform.right);
        var left = Physics.Raycast(gameObject.transform.position, gameObject.transform.right*-1f);
        isExitNode = !(forward && backward && right && left);
    }

}