using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class MapController : MonoBehaviour {

	public GameObject graphSetupPrefab;

	private List<GameObject> nodeList;

	void Start() {
		Instantiate(graphSetupPrefab, gameObject.transform);
	}

	public void SetNodeList(List<GameObject> nodeList) {
		this.nodeList = nodeList;
	}

}
