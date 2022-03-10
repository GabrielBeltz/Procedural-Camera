using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraVolume))]
public class CameraVolumeEditor : Editor
{
    private static readonly string[] _dontIncludeMe = new string[]{"m_Script"};
    
    public override void OnInspectorGUI()
    {
        CameraVolume script = (CameraVolume)target;
        script.CameraVolumeType = (CameraVolumeType)EditorGUILayout.EnumPopup("Type:", script.CameraVolumeType);

        switch(script.CameraVolumeType)
        {
            case CameraVolumeType.FixatedLookAt:
                EditorGUILayout.BeginFoldoutHeaderGroup(true, "Fixated Angle Follow");
                script.Position = EditorGUILayout.Vector3Field("Position:", script.Position);
                if(GUILayout.Button("Copiar posição do GameObject selecionado."))
                    script.Position = Selection.activeGameObject.transform.position;
                script.Target = EditorGUILayout.ObjectField("Target: ", script.Target, typeof(Transform), true) as Transform;
                if(GUILayout.Button("Pegar ref do GameObject selecionado."))
                    script.Target = Selection.activeGameObject.transform;
                EditorGUILayout.Space(25f);
                EditorGUILayout.EndFoldoutHeaderGroup();
                break;
            case CameraVolumeType.FixatedAngleFollow:
                EditorGUILayout.BeginFoldoutHeaderGroup(true, "Fixated Angle Follow");
                script.Angle = EditorGUILayout.Vector3Field("Angle:", script.Angle);
                if(GUILayout.Button("Copiar ângulo do GameObject selecionado."))
                    script.Angle = Selection.activeGameObject.transform.rotation.eulerAngles;
                script.Target = EditorGUILayout.ObjectField("Target: ", script.Target, typeof(Transform), true) as Transform;
                if(GUILayout.Button("Pegar ref do GameObject selecionado."))
                    script.Target = Selection.activeGameObject.transform;
                EditorGUILayout.Space(25f);
                EditorGUILayout.EndFoldoutHeaderGroup();
                break;
        }

        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, _dontIncludeMe);
        serializedObject.ApplyModifiedProperties();
    }
}
