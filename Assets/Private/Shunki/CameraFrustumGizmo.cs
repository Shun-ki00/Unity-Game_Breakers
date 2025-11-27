#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class CameraFrustumGizmo : MonoBehaviour
{
    public Color gizmoColor = Color.cyan;

    private void OnDrawGizmos()
    {
        DrawFrustum();
    }

    private void OnDrawGizmosSelected()
    {
        // オブジェクト選択時にも描画（色を変えるなど可能）
        DrawFrustum();
    }

    private void DrawFrustum()
    {
        Camera cam = GetComponent<Camera>();
        if (cam == null) return;

        Gizmos.color = gizmoColor;
        Matrix4x4 temp = Gizmos.matrix;

        Gizmos.matrix = cam.transform.localToWorldMatrix;
        Gizmos.DrawFrustum(
            Vector3.zero,
            cam.fieldOfView,
            cam.farClipPlane,
            cam.nearClipPlane,
            cam.aspect
        );

        Gizmos.matrix = temp;
    }
}
#endif
