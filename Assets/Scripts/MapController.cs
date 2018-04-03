using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class MapController : MonoBehaviour {

	public GameObject graphSetupPrefab;
	public GameObject agentGeneratorPrefab;

	private List<GameObject> nodeList = new List<GameObject>();
	private List<GameObject> agentList = new List<GameObject>();

	private void Start() {
		Instantiate(graphSetupPrefab, gameObject.transform);
		Instantiate(agentGeneratorPrefab, gameObject.transform);
	}

	private void Update() {
		SupplyMapKnowledge();
		if (Input.GetKeyDown(KeyCode.Return)) {
			InitialiseSimulation();
			StartSimulation();
		}
	}

	private void InitialiseSimulation() {
		SetAgentPositions();
	}

	private void StartSimulation() {
		ActivateAgents();
	}

	private void SetAgentPositions() {
		foreach (var agent in agentList) {
			//TODO: Implement proper agent spawning system.
			agent.transform.Translate(new Vector3(2.5f, 0f, -8f));
		}
	}

	private void ActivateAgents() {
		foreach (var agent in agentList) {
			agent.SetActive(true);
		}
	}
	
	public void AddToNodeList(List<GameObject> nodeList) {
		this.nodeList.AddRange(nodeList);
	}

	public void AddToAgentList(List<GameObject> agentList) {
		this.agentList.AddRange(agentList);
	}

	private void SupplyMapKnowledge() {
		if (nodeList.Count == 0 || agentList.Count == 0) return;
		foreach (var agent in agentList) {
			var agentController = agent.GetComponent<AgentController>();
			if (agentController.IsMapKnowledgeRequested) {
				agentController.ReceiveMapKnowledge(nodeList);
				agentController.IsMapKnowledgeRequested = false;
			}
		}
	}

}
