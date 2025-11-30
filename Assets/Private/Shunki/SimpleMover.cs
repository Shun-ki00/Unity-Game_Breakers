using UnityEngine;
using UnityEngine.InputSystem; // 新Input System用

/// <summary>
/// 新Input Systemで上下左右前後に移動できるシンプルなスクリプト
/// </summary>
public class Simple3DInputMover : MonoBehaviour
{
    [Header("移動速度")]
    public float moveSpeed = 5f;

    void Update()
    {
        Vector3 move = Vector3.zero;

        // 上下（Y軸）
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            move += Vector3.up;

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            move += Vector3.down;

        // 左右（X軸）
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            move += Vector3.left;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            move += Vector3.right;

        // 前後（Z軸）
        if (Keyboard.current.eKey.isPressed)
            move += Vector3.forward;

        if (Keyboard.current.qKey.isPressed)
            move += Vector3.back;

        // 移動
        transform.Translate(move.normalized * moveSpeed * Time.deltaTime, Space.World);
    }
}
