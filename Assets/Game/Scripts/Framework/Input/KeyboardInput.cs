// ============================================
// 
// ファイル名: KeyboardInput.cs
// 概要: キーボードの入力系処理
// 
// 製作者 : 清水駿希
// 
// ============================================
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : IInputDevice
{
    public GamePlayInputState GetInput { get { return _gamePlayInputSnapshot; } }


    private GamePlayInputState _gamePlayInputSnapshot;

    // 現在キーボードステート
    private Keyboard _currentKeyboardState = null;


    /// <summary>
    /// 入力状態を更新する
    /// </summary>
    public void GamePlayInputUpdate()
    {
        // 現在のキーボード情報を更新する
        _currentKeyboardState = Keyboard.current;

        // 毎フレームで入力を集約して Snapshot を作成
        var snapshot = new GamePlayInputState(
            handle: this.GetHandleAxis(),
            accelerator: this.GetAcceleratorAxis(),
            brake: this.GetBrakeAxis(),
            boost: _currentKeyboardState.spaceKey.wasPressedThisFrame,
            cameraView: _currentKeyboardState.cKey.wasPressedThisFrame
        );

        _gamePlayInputSnapshot = snapshot;
    }

    /// <summary>
    /// UI用の入力アクションが現在押されているかを判定する
    /// </summary>
    public bool IsPressed(UiInputActionID action)
    {
        bool pressed = false;

        if (_currentKeyboardState == null)
            return false;

        switch (action)
        {
            case UiInputActionID.None:
                break;
            case UiInputActionID.RIGHT:
                if (_currentKeyboardState.rightArrowKey.isPressed || _currentKeyboardState.dKey.isPressed)
                    pressed = true;
                break;
            case UiInputActionID.LEFT:
                if (_currentKeyboardState.leftArrowKey.isPressed || _currentKeyboardState.aKey.isPressed)
                    pressed = true;
                break;
            case UiInputActionID.UP:
                if (_currentKeyboardState.upArrowKey.isPressed || _currentKeyboardState.wKey.isPressed)
                    pressed = true;
                break;
            case UiInputActionID.DOWN:
                if (_currentKeyboardState.downArrowKey.isPressed || _currentKeyboardState.sKey.isPressed)
                    pressed = true;
                break;
            case UiInputActionID.ESC:
                if (_currentKeyboardState.escapeKey.isPressed)
                    pressed = true;
                break;
            default:
                pressed = false;
                break;

        }

        return pressed;
    }

    /// <summary>
    /// UI用の入力アクションが今フレームで押されたかを判定する
    /// </summary>
    public bool WasPressedThisFrame(UiInputActionID action)
    {
        bool pressed = false;

        if (_currentKeyboardState == null)
            return false;

        switch (action)
        {
            case UiInputActionID.None:
                break;
            case UiInputActionID.RIGHT:
                if (_currentKeyboardState.rightArrowKey.wasPressedThisFrame || _currentKeyboardState.dKey.wasPressedThisFrame)
                    pressed = true;
                break;
            case UiInputActionID.LEFT:
                if (_currentKeyboardState.leftArrowKey.wasPressedThisFrame || _currentKeyboardState.aKey.wasPressedThisFrame)
                    pressed = true;
                break;
            case UiInputActionID.UP:
                if (_currentKeyboardState.upArrowKey.wasPressedThisFrame || _currentKeyboardState.wKey.wasPressedThisFrame)
                    pressed = true;
                break;
            case UiInputActionID.DOWN:
                if (_currentKeyboardState.downArrowKey.wasPressedThisFrame || _currentKeyboardState.sKey.wasPressedThisFrame)
                    pressed = true;
                break;
            case UiInputActionID.ESC:
                if (_currentKeyboardState.escapeKey.wasPressedThisFrame)
                    pressed = true;
                break;
            default:
                pressed = false;
                break;

        }

        return pressed;
    }

    /// <summary>
    /// ハンドルの入力値を取得する
    /// </summary>
    private float GetHandleAxis()
    {
        float handle = 0f;
        // 右
        if(_currentKeyboardState.rightArrowKey.isPressed) handle = -1f;
        // 左
        if(_currentKeyboardState.leftArrowKey.isPressed) handle = 1f;

        return handle;
    }
    /// <summary>
    /// アクセルの入力値を取得する
    /// </summary>
    private float GetAcceleratorAxis()
    {
        float accelerator = 0f;
        // 上キー
        if(_currentKeyboardState.upArrowKey.isPressed) accelerator = 1f;

        return accelerator; 
    }
    /// <summary>
    /// ブレーキの入力値を取得する
    /// </summary>
    private float GetBrakeAxis()
    {
        float brake = 0f;
        // 上キー
        if (_currentKeyboardState.downArrowKey.isPressed) brake = 1f;

        return brake;
    }

}
