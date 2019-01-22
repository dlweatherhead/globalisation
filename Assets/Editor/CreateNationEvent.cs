using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateNationEvent {

	[MenuItem("Game Utilities/Create Nation Event")]
	public static void CreateNewNationEvent() {
		NationEvent nationEvent = ScriptableObject.CreateInstance<NationEvent> ();

		AssetDatabase.CreateAsset (nationEvent, "Assets/NationEvents/NewNationEvent.asset");
		AssetDatabase.SaveAssets ();

		EditorUtility.FocusProjectWindow ();

		Selection.activeObject = nationEvent;
	}
}
