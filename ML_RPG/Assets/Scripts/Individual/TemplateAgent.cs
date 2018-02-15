using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateAgent : Agent {

	[SerializeField]
	private GameObject BattleField;

	private List<int> enemies;
	private float position;

	[SerializeField]
	private int SkillID=0;

	private Skill appliedSkill;
	private List<int> PossiblePositions, PossibleEnemies;

	public override void InitializeAgent ()
	{
		print ("agent");
		PossiblePositions = new List<int> ();
		PossibleEnemies = new List<int> ();

		enemies = new List<int> ();
		for (int i = 0; i < 16; i++) {
			this.enemies.Add (1000);
		}

		if (SkillID != 0) {

			if (!Manager.manager.SkillIds.Contains (SkillID))
				Manager.manager.SkillIds.Add (SkillID);

			foreach (Skill s in Manager.manager.skills.UsableSkills) {
		
				if (s.ID == SkillID) {
					appliedSkill = s;
					break;
				}
			}
			if (appliedSkill != null)
				appliedSkill.Init ();


			List<int> tp_list = new List<int>(appliedSkill.ActivateRow);

			foreach (int i in tp_list.ToArray()) {
				if (i == 0) {
					tp_list.Remove (i);
				}
			}
			for(int i=0;i< tp_list.Count;i++) {
				for(int u=0;u<4;u++){
					PossiblePositions.Add ((u * 4) + (tp_list[i] - 1));
				}
			}

			if (!appliedSkill.postarget) {
				tp_list = new List<int> (appliedSkill.StaticTarget);

				for(int i=0; i<tp_list.Count;i++){
					if (tp_list[i] != 0) {
						PossibleEnemies.Add (i);
					}
				}
			}
		}
	}

	public override List<float> CollectState(){
		List<float> state = new List<float>();		
		for (int i = 0; i < this.enemies.Count; i++) {
			state.Add(this.enemies[i]);
		}
		print (state.Count);
		state.Add (position);
		return state;
	}

	public override void AgentStep(float[] act){

		print ("step");

		//Killed Enemy
		reward = 1.0f;

		//Hit enemy and right skill
		reward = 0.5f;

		//Hit enemy but wrong skill
		reward = 0.1f;


		//did not hit any enemy
		reward = -0.5f;


		//cant use Skill
		reward = -1.0f;

	}

	public override void AgentReset(){

		if (SkillID != 0) {

			int id = Random.Range (0, PossiblePositions.Count);
			this.position = PossiblePositions[id];

			enemies = new List<int> ();
			for (int i = 0; i < 16; i++) {
				this.enemies.Add (1000);
			}
		
			//Static or Positional Target
			if (appliedSkill.postarget) {
		
				List<int> pos = new List<int> (appliedSkill.PositionalTarget);
				int row = Mathf.RoundToInt(this.position) % 3;

				switch (row) {

				case 0:
					pos.RemoveRange (0, 12);
					break;
				case 1:
					pos.RemoveRange (0, 8);
					pos.RemoveRange (16, 4);
					break;
				case 2:
					pos.RemoveRange (0, 4);
					pos.RemoveRange (16, 8);
					break;
				case 3:
					pos.RemoveRange (16, 12);
					break;
				}
				PossibleEnemies = new List<int> ();
				for (int i = 0; i < pos.Count; i++) {
					if (pos [i] != 0)
						PossibleEnemies.Add (i);
				}
				int range = Random.Range (1, 4);
				for (int i = 0; i < range; i++) {
					id = Random.Range (0, PossibleEnemies.Count);
					print (enemies.Count + " - " + PossibleEnemies [id]);
					this.enemies[ PossibleEnemies [id]]= Random.Range(50,401);
				}
		
			} else {
				int range = Random.Range (1, 4);
				for (int i = 0; i < range; i++) {
					id = Random.Range (0, PossibleEnemies.Count);
					this.enemies[ PossibleEnemies [id]]= Random.Range(50,401);
				}
			}

			for (int i=0;i< enemies.Count;i++) {
				if (enemies [i] != 1000) {
					BattleField.transform.GetChild (i).GetComponent<Controller> ().SetValue (enemies [i]);
				} else {
					BattleField.transform.GetChild (i).GetComponent<Controller> ().ResetValue ();
				}
			}


		} else {
		
		
		}


	}
}
