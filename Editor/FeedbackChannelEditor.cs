using System.Collections.Generic;
using Bomome.UI.Feedbacks;
using UnityEditor;
using UnityEngine;

namespace Bomome.EditorTools
{
    [CustomEditor(typeof(FeedbackChannel))]
    public sealed class FeedbackChannelEditor : UnityEditor.Editor
    {
        private readonly List<string> usages = new List<string>();
        private readonly List<Component> loadedMappings = new List<Component>();
        private bool searched;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Find Usages (Scenes and Prefabs)")) FindUsages();
            if (!searched) return;
            EditorGUILayout.HelpBox("Includes indirect references through nested prefabs. Select a result to inspect its asset. Search runs only when requested.", MessageType.Info);
            foreach (string path in usages)
            {
                if (!GUILayout.Button(path, EditorStyles.linkLabel)) continue;
                var asset = AssetDatabase.LoadMainAssetAtPath(path);
                Selection.activeObject = asset;
                EditorGUIUtility.PingObject(asset);
            }
            if (usages.Count == 0) EditorGUILayout.LabelField("No saved scene/prefab references found.");
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Loaded Channel Mappings", EditorStyles.boldLabel);
            foreach (Component component in loadedMappings)
            {
                if (component == null) continue;
                string hierarchy = component.name;
                for (Transform parent = component.transform.parent; parent != null; parent = parent.parent)
                    hierarchy = parent.name + "/" + hierarchy;
                if (GUILayout.Button($"{component.GetType().Name}: {component.gameObject.scene.name}/{hierarchy}", EditorStyles.linkLabel))
                {
                    Selection.activeGameObject = component.gameObject;
                    EditorGUIUtility.PingObject(component);
                }
            }
        }

        private void FindUsages()
        {
            usages.Clear();
            searched = true;
            loadedMappings.Clear();
            // Inspect serialized references so optional receivers need no editor assembly dependency.
            FindLoadedMappings(Resources.FindObjectsOfTypeAll<MonoBehaviour>());
            string channelPath = AssetDatabase.GetAssetPath(target);
            string[] guids = AssetDatabase.FindAssets("t:Scene t:Prefab", new[] { "Assets" });
            try
            {
                for (int i = 0; i < guids.Length; i++)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                    if (EditorUtility.DisplayCancelableProgressBar("Finding feedback channel usages", path, (float)i / guids.Length)) break;
                    foreach (string dependency in AssetDatabase.GetDependencies(path, true))
                    {
                        if (dependency != channelPath) continue;
                        usages.Add(path);
                        break;
                    }
                }
                usages.Sort(System.StringComparer.OrdinalIgnoreCase);
            }
            finally { EditorUtility.ClearProgressBar(); }
        }

        private void FindLoadedMappings<T>(T[] components) where T : Component
        {
            foreach (T component in components)
            {
                if (EditorUtility.IsPersistent(component) || !component.gameObject.scene.IsValid()) continue;
                var serialized = new SerializedObject(component);
                SerializedProperty property = serialized.GetIterator();
                while (property.Next(true))
                {
                    if (property.propertyType != SerializedPropertyType.ObjectReference || property.objectReferenceValue != target) continue;
                    loadedMappings.Add(component);
                    break;
                }
            }
        }
    }
}
