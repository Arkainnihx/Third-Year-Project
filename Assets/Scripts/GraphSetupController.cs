using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.AI;

public class GraphSetupController : MonoBehaviour {

	public GameObject mapNode;

	private MapController mapController;
	private bool isMapGraphComplete = false;
	private int nodeIndex = 0;
	private GameObject currentNode;
	private GameObject nodeToTest;
	private bool isNodeReadyToTest = false;
	private int state = 0;
	private List<GameObject> mapNodeList = new List<GameObject>();
	private List<GameObject> untestedNodeList = new List<GameObject>();
	
	void Start() {
		mapController = gameObject.GetComponentInParent<MapController>();
		GenerateInitialMapNodes();
	}
	//TODO: Sort this crap out tomorrow.
//	void Update() {
//		switch (state) {
//			case 0:
//				currentNode = mapNodeList[nodeIndex];
//				untestedNodeList = GetUnconnectedNodeList(currentNode);
//				nodeToTest = untestedNodeList[untestedNodeList.Count - 1];
//				currentNode.GetComponent<NavMeshObstacle>().enabled = false;
//				currentNode.GetComponent<NavMeshAgent>().enabled = true;
//				nodeToTest.GetComponent<NavMeshObstacle>().enabled = false;
//				state = 1;
//				break;
//			case 1:
//				TestNode();
//				nodeToTest.GetComponent<NavMeshObstacle>().enabled = true;
//				nodeToTest = untestedNodeList[untestedNodeList.Count - 1];
//				nodeToTest.GetComponent<NavMeshObstacle>().enabled = false;
//				break;
//			case 2:
//				
//				break;
//			case 3:
//				
//				break;
//			default:
//				break;
//		}
////		if (untestedNodeList.Count > 0) {
////			if (isNodeReadyToTest) {
////				TestNode();
////			} else {
////				SetupNewNodeToTest();
////			}
////		} else {
////			NextNode();
////		}
////		if (isMapGraphComplete) {
////			mapController.SetNodeList(mapNodeList);
////			Destroy(gameObject);
////		}
//	}

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

//	void NextNode() {
//		if (currentNode != null) {
//			currentNode.GetComponent<NavMeshAgent>().enabled = false;
//			currentNode.GetComponent<NavMeshObstacle>().enabled = true;
//		}
//		if (nodeIndex < mapNodeList.Count - 1) {
//			nodeIndex++;
//			currentNode = mapNodeList[nodeIndex];
//			currentNode.GetComponent<NavMeshObstacle>().enabled = false;
//			untestedNodeList = GetUnconnectedNodeList(currentNode);
//			currentNode.GetComponent<NavMeshAgent>().enabled = true;
//			print("New current node.");
//		} else {
//			isMapGraphComplete = true;
//		}
//	}
//
//	void SetupNewNodeToTest() {
//		nodeToTest = untestedNodeList[untestedNodeList.Count-1];
//		nodeToTest.GetComponent<NavMeshObstacle>().enabled = false;
//		isNodeReadyToTest = true;
//		print("New test node.");
//	}
//
//	void TestNode() {
//		var path = new NavMeshPath();
//		var nodesToCull = new List<GameObject> {nodeToTest};
//		if (path.status == NavMeshPathStatus.PathComplete) {
//			print("Path found.");
//			currentNode.GetComponent<MapNodeController>().AddEdge(nodeToTest);
//			var otherConnections = new List<GameObject>(nodeToTest.GetComponent<MapNodeController>().GetConnectedNodeList());
//			nodesToCull.AddRange(otherConnections);
//			if (otherConnections.Count > 1) {
//				print("Secondhand connections found.");
//				otherConnections.RemoveAt(0);
//				currentNode.GetComponent<MapNodeController>().AddEdges(otherConnections);
//				nodesToCull.AddRange(GetDoorPairedNodes(otherConnections));
//			}
//			nodeToTest.GetComponent<MapNodeController>().AddEdge(currentNode);
//		} else {
//			print("Path not found.");
//		}
//		foreach (var nodeToCull in nodesToCull) {
//			untestedNodeList.Remove(nodeToCull);
//		}
//		if (untestedNodeList.Count == 0) {
//			nodeIndex++;
//			state = 0;
//		}
//	}

	void ConnectMapNode(GameObject node) {
		var untestedNodes = GetUnconnectedNodeList(node);
		while (untestedNodes.Count > 0) {
			var nodeToTest = untestedNodes[untestedNodes.Count-1];
			nodeToTest.GetComponent<NavMeshObstacle>().enabled = false;
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
			} else {
				print("Path not found.");
			}
			foreach (var nodeToCull in nodesToCull) {
				untestedNodes.Remove(nodeToCull);
			}
			nodeToTest.GetComponent<NavMeshObstacle>().enabled = true;
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
	
}
