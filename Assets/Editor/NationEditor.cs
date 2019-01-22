using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Nation))]
public class NationEditor : Editor {

	public float minValue;
	public float maxValue;
	public float scaleMax;

	SerializedProperty economic;
	SerializedProperty environmental;
	SerializedProperty social;

	SerializedProperty socialEconomicInfluencerScale;
	SerializedProperty economicEnvironmentalInfluencerScale;
	SerializedProperty environmentalSocialInfluencerScale;

	void OnEnable() {
		DrawDefaultInspector ();
		economic = serializedObject.FindProperty ("economicLevel");
		environmental = serializedObject.FindProperty ("environmentalLevel");
		social = serializedObject.FindProperty ("socialLevel");
		socialEconomicInfluencerScale = serializedObject.FindProperty ("socialEconomicInfluencerScale");
		economicEnvironmentalInfluencerScale = serializedObject.FindProperty ("economicEnvironmentalInfluencerScale");
		environmentalSocialInfluencerScale = serializedObject.FindProperty ("environmentalSocialInfluencerScale");
	}


	public override void OnInspectorGUI() {
		serializedObject.Update ();
		EditorGUILayout.Slider (economic, minValue, maxValue);
		EditorGUILayout.Slider (environmental, minValue, maxValue);
		EditorGUILayout.Slider (social, minValue, maxValue);

		EditorGUILayout.Slider (economicEnvironmentalInfluencerScale, 0f, scaleMax);
		EditorGUILayout.Slider (environmentalSocialInfluencerScale, 0f, scaleMax);
		EditorGUILayout.Slider (socialEconomicInfluencerScale, 0f, scaleMax);

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.FloatField (minValue);
		EditorGUILayout.FloatField (maxValue);
		EditorGUILayout.FloatField (scaleMax);
		EditorGUILayout.EndHorizontal ();

		serializedObject.ApplyModifiedProperties ();
	}

}
