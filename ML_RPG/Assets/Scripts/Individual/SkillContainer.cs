using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[XmlRoot ("Data")]
public class SkillContainer {

	[XmlArray("EnemySkills"), XmlArrayItem("Skill")]
	public Skill[] UsableSkills;

	public static SkillContainer Load(string path){

		var serializer = new XmlSerializer (typeof(SkillContainer));
		using (var stream = new FileStream (path, FileMode.Open)) {

			return serializer.Deserialize (stream) as SkillContainer;
		}
	}
}
