using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ManagersInstantiator))]
public class ManagersInstantiatorEditor : Editor
{

	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI ();

		ManagersInstantiator mgr = this.target as ManagersInstantiator;

		for (int i = 0; i < mgr.managers.Count; i++) 
		{
			Color c = GUI.color;

			EditorGUILayout.BeginHorizontal ();

			mgr.managers [i] = EditorGUILayout.ObjectField (mgr.managers [i], typeof(GameObject)) as GameObject;

			if (GUILayout.Button ("Up", GUILayout.MaxWidth (25))) 
			{
				if (i > 0) {
					GameObject go = mgr.managers [i-1];
					mgr.managers [i-1] = mgr.managers [i];
					mgr.managers [i] = go;
				}
			}

			if (GUILayout.Button ("Down", GUILayout.MaxWidth(25)))
			{
				if (i < mgr.managers.Count - 1) {
					GameObject go = mgr.managers [i+1];
					mgr.managers [i+1] = mgr.managers [i];
					mgr.managers [i] = go;
				}
			}

			GUI.color = Color.red;
			if (GUILayout.Button ("Remove", GUILayout.MaxWidth (40)))
				mgr.managers.RemoveAt (i);

			GUI.color = c;

			EditorGUILayout.EndHorizontal ();
		}

		if (GUILayout.Button ("Add Manager"))
			mgr.managers.Add(null);
	}
}
