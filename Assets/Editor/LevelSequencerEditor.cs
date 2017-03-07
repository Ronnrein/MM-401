using Assets.Scripts.Utils;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor {

    /// <summary>
    /// Custom editor to allow sequencing by pressing a button
    /// </summary>
    [CustomEditor(typeof(LevelSequencer))]
    public class LevelSequencerEditor : UnityEditor.Editor {

        /// <summary>
        /// Overrides default inspector behaviour
        /// </summary>
        public override void OnInspectorGUI() {
            LevelSequencer sequencer = (LevelSequencer) target;
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Pieces"), true);
            serializedObject.ApplyModifiedProperties();
            if (GUILayout.Button("Sequence")) {
                sequencer.Sequence();
            }
        }
    }
}
