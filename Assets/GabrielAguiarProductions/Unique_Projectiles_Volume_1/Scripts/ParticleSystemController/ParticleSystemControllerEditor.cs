using UnityEngine;

namespace GAP_ParticleSystemController
{
#if UNITY_EDITOR
    using UnityEditor;

    [CustomEditor(typeof(ParticleSystemController))]
    public class ParticleSystemControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var psCtrl = (ParticleSystemController)target;

            if (GUILayout.Button("Fill Lists")) psCtrl.FillLists();
            if (GUILayout.Button("Empty Lists")) psCtrl.EmptyLists();
            if (GUILayout.Button("Apply")) psCtrl.UpdateParticleSystem();
            if (GUILayout.Button("Reset")) psCtrl.ResetParticleSystem();
        }
    }
#endif
}