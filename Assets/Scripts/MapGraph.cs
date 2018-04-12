using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MapGraph : IEnumerable<GameObject> {

    private List<GameObject> nodeList = new List<GameObject>();
    
    public int Count {
        get { return nodeList.Count; }
    }
    
    public IEnumerator<GameObject> GetEnumerator() {
        return nodeList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public void Add(GameObject node) {
        nodeList.Add(node);
    }

    public void AddRange(IEnumerable<GameObject> nodes) {
        nodeList.AddRange(nodes);
    }

    public List<MapNodeController> GetNodeControllers() {
        var controllerList = new List<MapNodeController>();
        foreach (var node in nodeList) {
            controllerList.Add(node.GetComponent<MapNodeController>());
        }
        return controllerList;
    }

    public List<GameObject> GetExitNodes() {
        var exitNodes = new List<GameObject>();
        foreach (var node in nodeList) {
            if (node.GetComponent<MapNodeController>().IsExitNode) {
                exitNodes.Add(node);
            }
        }
        return exitNodes;
    }

}