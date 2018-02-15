using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill{

	#region xml
	public int ID;
	public string Name;
	public float Modifier;
	public float CritModifier;
	public int SkillIdentifier; //0=Offensive 1=Deffensive 2=Utility
	public string DamageType;//	Phys, Fire, Ice, Light
	public int TargetMode; // 0=Single 1=Multi 2=AOE 3=FreeAOE 4=DOT //0=Mov+Skill 1=OptionalMove 2=ForcedMove
	public int SkillType; // 0=Dmg 1=Heal 2=ApplyBuff 3=ApplyDot 4=Movement+ 5=ApplyDebuff
	public string Target_Activate;
	public string Target;
	public string Target_Individual;
	public int Image;
	public bool TargetEnemy; // 0=false 1=true
	public int TargetCount; //How many Targets -1 = all
	public int TurnCount; // how long skill should last
	public bool Dodgeable; // 0=fase 1=true
	public int SkillUse;
	public int BuffCount; // 0=NoBuff 1-*= Buffs
	public string Buffstring;
	public string DescriptionText;
	public string Upgrade; //FirstRow|SecondRow|ThirdRow|...|(UltimateAvailable from Row)1-2-3
	public string UpgradeTree; //UpgradeType/Value/Count/StartCost-Increase|...|...
	#endregion

	public int[] ActivateRow{ get; set;} // 1/2/3/4
	public int[] StaticTarget{ get; set;} // 1234/1 columns/x x=0 only 1 row 
	public int[] PositionalTarget{ get; set;}
	public bool postarget;

	public Skill(){
	}

	public void Init(){
		postarget = false;
		string[] tp_array;
		tp_array = Target.Split ("/" [0]);
		MakeStaticTarget (tp_array);

		tp_array = Target_Activate.Split ("/"[0]);
		MakeActivateRow (int.Parse (tp_array [0]), int.Parse (tp_array [1]), int.Parse (tp_array [2]), int.Parse (tp_array [3]));

		tp_array = Target_Individual.Split ("/"[0]);
		PositionalTargets (tp_array);
	}

	#region makeRow
	public void MakeStaticTarget(string[] tp_array){

		StaticTarget = new int[tp_array.Length];
		List<int> tp_List = new List<int> ();

		for (int i = 0; i < tp_array.Length; i++) {
			tp_List.Add (int.Parse(tp_array[i]));
			if (int.Parse (tp_array [i])!= 0)
				postarget = false;
		}

		StaticTarget = tp_List.ToArray ();
	}

	public void MakeActivateRow(int o, int j, int k, int l){

		ActivateRow = new int[4];
		List<int> tp_List = new List<int> ();

		tp_List.Add (o);
		tp_List.Add (j);
		tp_List.Add (k);
		tp_List.Add (l);

		ActivateRow = tp_List.ToArray ();
	}

	public void PositionalTargets(string[] tp_array){

		PositionalTarget = new int[tp_array.Length];
		List<int> tp_List = new List<int> ();

		for (int i = 0; i < tp_array.Length; i++) {
			tp_List.Add (int.Parse(tp_array[i]));
			if (int.Parse (tp_array [i]) != 0)
				postarget = true;
		}

		PositionalTarget = tp_List.ToArray ();
	}
	#endregion
}
