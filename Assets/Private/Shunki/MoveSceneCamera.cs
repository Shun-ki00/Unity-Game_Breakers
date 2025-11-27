#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class MoveSceneCamera : EditorWindow
{
    [MenuItem("Tools/Move Scene Camera")]
    public static void MoveCameraToPosition()
    {
        // 移動先の位置と回転を設定（例：原点、正面）
        Vector3 targetPosition = new Vector3(166430.5f, 1952125.0f, -3337518.0f);
        Quaternion targetRotation = Quaternion.Euler(20f, 0f, 0f);

        // 現在のSceneビュー取得
        SceneView sceneView = SceneView.lastActiveSceneView;

        if (sceneView != null)
        {
            // SceneビューカメラのTransformに直接設定
            sceneView.pivot = targetPosition;      // カメラの注視点（pivot）
            sceneView.rotation = targetRotation;   // カメラの回転
            sceneView.size = 10f;                  // ズームレベル（Orthographicビュー用）
            sceneView.Repaint();                   // 表示更新
        }
        else
        {
            Debug.LogWarning("Sceneビューが開いていないため、カメラを移動できません。");
        }
    }
}
#endif
