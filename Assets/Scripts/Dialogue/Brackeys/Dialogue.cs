using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "New Dialogue/Dialogue")]
public class Dialogue : ScriptableObject
{

	//public string Name;

	[TextArea(3, 10)]
	public string[] sentences;

	public Relation relation;

	public Sprite sprite;

	public enum Relation
    {
		Enemy,
		Neutral,
		ally
    }
}
