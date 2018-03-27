using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class MapController : MonoBehaviour {

	public GameObject mapNode; 

	private List<GameObject> mapNodeList = new List<GameObject>();
	
	void Start() {
		GenerateInitialMapNodes();
		ConnectMapNodes();
	}

	void GenerateInitialMapNodes() {
		foreach (var door in GameObject.FindGameObjectsWithTag("Door")) {
			var frontNode = Instantiate(mapNode, door.transform);
			var backNode = Instantiate(mapNode, door.transform);
			frontNode.transform.Translate(Vector3.forward*0.5f);
			backNode.transform.Translate(Vector3.back*0.5f);
			frontNode.GetComponent<MapNodeController>().AddEdge(backNode);
			backNode.GetComponent<MapNodeController>().AddEdge(frontNode);
			mapNodeList.Add(frontNode);
			mapNodeList.Add(backNode);
		}
	}

	void ConnectMapNodes() {
		foreach (var node in mapNodeList) {
			node.GetComponent<NavMeshAgent>().enabled = true;
			node.GetComponent<NavMeshObstacle>().enabled = false;
			ConnectMapNode(node);
			node.GetComponent<NavMeshAgent>().enabled = false;
			node.GetComponent<NavMeshObstacle>().enabled = true;
		}
	}

	void ConnectMapNode(GameObject node) {
		foreach (var unconnectedNode in GetUnconnectedNodeList(node)) {
			unconnectedNode.GetComponent<NavMeshObstacle>().enabled = false;
			var path = new NavMeshPath();
			node.GetComponent<NavMeshAgent>().CalculatePath(unconnectedNode.transform.position, path);
			if (path.status == NavMeshPathStatus.PathComplete) {
				node.GetComponent<MapNodeController>().AddEdge(unconnectedNode);
				var otherConnections = new List<GameObject>(unconnectedNode.GetComponent<MapNodeController>().GetConnectedNodeList());
				if (otherConnections.Count > 1) {
					otherConnections.RemoveAt(0);
					node.GetComponent<MapNodeController>().AddEdges(otherConnections);
				}
				unconnectedNode.GetComponent<MapNodeController>().AddEdge(node);
			}
			unconnectedNode.GetComponent<NavMeshObstacle>().enabled = true;
		}
	}

	//TODO: Start here.
//	void ConnectMapNodeRecursive(GameObject startNode, List<GameObject> untestedNodes) {
//		var nodeToTest = untestedNodes[]
//	}
	
	private List<GameObject> GetUnconnectedNodeList(GameObject node) {
		var unconnectedNodeList = new List<GameObject>(mapNodeList);
		foreach (var connectedNode in node.GetComponent<MapNodeController>().GetConnectedNodeList()) {
			unconnectedNodeList.Remove(connectedNode);
		}
		return unconnectedNodeList;
	}
	
}
