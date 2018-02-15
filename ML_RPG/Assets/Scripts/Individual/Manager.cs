using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class Manager : MonoBehaviour {

	public static Manager manager;

	public SkillContainer skills;
	public List<int> SkillIds;

	void Awake () {
		manager = this;
		DontDestroyOnLoad (gameObject);
		SkillIds = new List<int> ();
		skills = SkillContainer.Load(Path.Combine(Application.dataPath, "Data.xml"));

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Agent")) {
			g.GetComponent<TemplateAgent> ().enabled = true;
		}
	}
}
