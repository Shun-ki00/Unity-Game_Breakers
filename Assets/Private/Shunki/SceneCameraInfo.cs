#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class SceneCameraInfo : EditorWindow
{
    [MenuItem("Tools/Get Scene Camera Info")]
    public static void ShowSceneCameraInfo()
    {
        // 現在のSceneビューを取得
        SceneView sceneView = SceneView.lastActiveSceneView;

        if (sceneView != null)
        {
            // Sceneビューのカメラの位置と回転を取得
            Vector3 camPosition = sceneView.camera.transform.position;
            Quaternion camRotation = sceneView.camera.transform.rotation;

            Debug.Log("Scene Camera Position: " + camPosition);
            Debug.Log("Scene Camera Rotation: " + camRotation.eulerAngles);
        }
        else
        {
            Debug.LogWarning("SceneView が見つかりません。Sceneビューが開いているか確認してください。");
        }
    }
}
#endif
