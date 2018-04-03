using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GraphSetupController : MonoBehaviour {

	public GameObject mapNode;
	public GameObject obstacle;

	private MapController mapController;
	private bool frameDelay;
	private List<GameObject> mapNodeList = new List<GameObject>();
	private List<GameObject> obstacleList = new List<GameObject>();
	
	private void Start() {
		mapController = gameObject.GetComponentInParent<MapController>();
		GenerateMapNodesAndObstacles();
	}

	private void Update() {
		if (!frameDelay) {
			frameDelay = true;
		} else {
			ConnectMapNodes();
			DestroyObstacles();
			print("Exit Nodes: " + ExitNodeCount());
			mapController.AddToNodeList(mapNodeList);
			Destroy(gameObject);
		}
	}

	private void GenerateMapNodesAndObstacles() {
		foreach (var door in GameObject.FindGameObjectsWithTag("Door")) {
			var frontNode = Instantiate(mapNode, door.transform);
			var backNode = Instantiate(mapNode, door.transform);
			var ob = Instantiate(obstacle, door.transform);
			frontNode.transform.Translate(Vector3.forward*0.6f);
			backNode.transform.Translate(Vector3.back*0.6f);
			frontNode.GetComponent<MapNodeController>().AddEdge(backNode);
			backNode.GetComponent<MapNodeController>().AddEdge(frontNode);
			mapNodeList.Add(frontNode);
			mapNodeList.Add(backNode);
			obstacleList.Add(ob);
		}
	}

	private void ConnectMapNodes() {
		foreach (var node in mapNodeList) {
			var untestedNodes = GetUnconnectedNodeList(node);
			while (untestedNodes.Count > 0) {
				var nodeToTest = untestedNodes[untestedNodes.Count-1];
				var path = new NavMeshPath();
				node.GetComponent<NavMeshAgent>().CalculatePath(nodeToTest.transform.position, path);
				var nodesToCull = new List<GameObject> {nodeToTest};
				if (path.status == NavMeshPathStatus.PathComplete) {
					node.GetComponent<MapNodeController>().AddEdge(nodeToTest);
					var otherConnections = new List<GameObject>(nodeToTest.GetComponent<MapNodeController>().GetConnectedNodeList());
					nodesToCull.AddRange(otherConnections);
					if (otherConnections.Count > 1) {
						otherConnections.RemoveAt(0);
						node.GetComponent<MapNodeController>().AddEdges(otherConnections);
						nodesToCull.AddRange(GetDoorPairedNodes(otherConnections));
					}
					nodeToTest.GetComponent<MapNodeController>().AddEdge(node);
				}
				foreach (var nodeToCull in nodesToCull) {
					untestedNodes.Remove(nodeToCull);
				}
			}
		}
	}
	
	private void DestroyObstacles() {
		foreach (var ob in obstacleList) {
			Destroy(ob);
		}
	}

	private List<GameObject> GetDoorPairedNodes(List<GameObject> nodeList) {
		var pairedNodeList = new List<GameObject>();
		foreach (var node in nodeList) {
			pairedNodeList.Add(node.GetComponent<MapNodeController>().GetNodeDoorPair());
		}
		return pairedNodeList;
	}
	
	private List<GameObject> GetUnconnectedNodeList(GameObject node) {
		var unconnectedNodeList = new List<GameObject>(mapNodeList);
		unconnectedNodeList.Remove(node);
		foreach (var connectedNode in node.GetComponent<MapNodeController>().GetConnectedNodeList()) {
			unconnectedNodeList.Remove(connectedNode);
		}
		return unconnectedNodeList;
	}

	private int ExitNodeCount() {
		var exitNodes = 0;
		foreach (var node in mapNodeList) {
			if (node.GetComponent<MapNodeController>().IsExitNode) {
				exitNodes++;
			}
		}
		return exitNodes;
	}
	
}
