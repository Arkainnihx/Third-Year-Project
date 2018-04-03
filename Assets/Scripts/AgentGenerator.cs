using System.Collections.Generic;
using UnityEngine;

public class AgentGenerator : MonoBehaviour {

	public GameObject agentPrefab;

	private MapController mapController;
	private List<GameObject> agentList = new List<GameObject>();
	private List<PopulationSpec> populationsToGenerate = new List<PopulationSpec>();

	private void Start () {
		mapController = gameObject.GetComponentInParent<MapController>();
		populationsToGenerate.Add(new PopulationSpec(1));
		GeneratePopulations();
		mapController.AddToAgentList(agentList);
		Destroy(gameObject);
	}

	private void GeneratePopulations() {
		foreach (var pop in populationsToGenerate) {
			for (int agentCount = 0; agentCount < pop.PopSize; agentCount++) {
				var agent = Instantiate(agentPrefab);
				var controller = agent.GetComponent<AgentController>();
				controller.MaxSpeed = Random.Range(pop.MaxSpeedMin, pop.MaxSpeedMax);
				controller.PanicMult = Random.Range(pop.PanicMultMin, pop.PanicMultMax);
				controller.Caution = Random.Range(pop.CautionMin, pop.CautionMax);
				agentList.Add(agent);
			}
		}
	}
	
}
