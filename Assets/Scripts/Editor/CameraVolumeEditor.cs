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
            case CameraVolumeType.Fixated:
                EditorGUILayout.Space(5f);
                EditorGUILayout.BeginFoldoutHeaderGroup(true, "Fixated Behaviour");

                script.PositionA = EditorGUILayout.Vector3Field("Position:", script.PositionA);
                if(GUILayout.Button("Copiar posição do GameObject selecionado."))
                    script.PositionA = Selection.activeGameObject.transform.position;

                EditorGUILayout.Space(5f);
                EditorGUILayout.EndFoldoutHeaderGroup();
                break;
            case CameraVolumeType.FollowPlayer:
                EditorGUILayout.Space(5f);
                EditorGUILayout.BeginFoldoutHeaderGroup(true, "Follow Player Behaviour");

                script.Angle = EditorGUILayout.Vector3Field("Angle:", script.Angle);
                if(GUILayout.Button("Copiar ângulo do GameObject selecionado."))
                    script.Angle = Selection.activeGameObject.transform.rotation.eulerAngles;

                script.Target = EditorGUILayout.ObjectField("Target: ", script.Target, typeof(Transform), true) as Transform;
                if(GUILayout.Button("Pegar ref do GameObject selecionado."))
                    script.Target = Selection.activeGameObject.transform;

                EditorGUILayout.Space(5f);
                EditorGUILayout.EndFoldoutHeaderGroup();
                break;
            case CameraVolumeType.Dolly:
                EditorGUILayout.Space(5f);
                EditorGUILayout.BeginFoldoutHeaderGroup(true, "Dolly Behaviour");

                script.PositionA = EditorGUILayout.Vector3Field("Position A:", script.PositionA);
                if (GUILayout.Button("Copiar posição do GameObject selecionado."))
                    script.PositionA = Selection.activeGameObject.transform.position;

                script.PositionB = EditorGUILayout.Vector3Field("Position B:", script.PositionB);
                if (GUILayout.Button("Copiar posição do GameObject selecionado."))
                    script.PositionB = Selection.activeGameObject.transform.position;

                script.Target = EditorGUILayout.ObjectField("Target: ", script.Target, typeof(Transform), true) as Transform;
                if(GUILayout.Button("Pegar ref do GameObject selecionado."))
                    script.Target = Selection.activeGameObject.transform;

                EditorGUILayout.Space(5f);
                EditorGUILayout.EndFoldoutHeaderGroup();
                break;
            case CameraVolumeType.FirstPerson:
                EditorGUILayout.Space(5f);
                EditorGUILayout.BeginFoldoutHeaderGroup(true, "First Person Behaviour");

                script.Target = EditorGUILayout.ObjectField("Head Position: ", script.Target, typeof(Transform), true) as Transform;
                if(GUILayout.Button("Pegar ref do GameObject selecionado."))
                    script.Target = Selection.activeGameObject.transform;

                EditorGUILayout.Space(5f);
                EditorGUILayout.EndFoldoutHeaderGroup();
                break;
        }

        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, _dontIncludeMe);
        serializedObject.ApplyModifiedProperties();
    }
}
