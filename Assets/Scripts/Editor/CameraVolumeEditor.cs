using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraVolume))]
public class CameraVolumeEditor : Editor
{
    private static readonly string[] _dontIncludeMe = new string[]{"m_Script", ""};
    
    public override void OnInspectorGUI()
    {
        CameraVolume script = (CameraVolume)target;
        script.CameraVolumeType = (CameraVolumeType)EditorGUILayout.EnumPopup("Type:", script.CameraVolumeType);

        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, _dontIncludeMe);
        script.TargetTag = EditorGUILayout.TagField("Target Tag", script.TargetTag);
        serializedObject.ApplyModifiedProperties();
    }
}
