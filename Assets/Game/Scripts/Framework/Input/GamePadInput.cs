// ============================================
// 
// ファイル名: GamePadInput.cs
// 概要: ゲームパッドの入力系処理
// 
// 製作者 : 清水駿希
// 
// ============================================
using UnityEngine.InputSystem;

public class GamePadInput : IInputDevice
{
    /// <summary> ゲームパッドの入力値を取得する </summary>
    public GamePlayInputState GetInput { get { return _gamePlayInputSnapshot; } }

    private Gamepad _currentGamepadState = null;
    private GamePlayInputState _gamePlayInputSnapshot;


    /// <summary> 入力状態を更新する </summary>
    public void GamePlayInputUpdate()
    {
        _currentGamepadState = Gamepad.current;

        if (_currentGamepadState == null ) return;

        // 毎フレームで入力を集約して Snapshot を作成
        var snapshot = new GamePlayInputState(
            handle: _currentGamepadState.leftStick.x.ReadValue() * -1.0f,
            accelerator: _currentGamepadState.rightTrigger.ReadValue(),
            brake: _currentGamepadState.leftTrigger.ReadValue(),
            boost: _currentGamepadState.aButton.wasPressedThisFrame,
            cameraView: _currentGamepadState.bButton.wasPressedThisFrame
        );

        _gamePlayInputSnapshot = snapshot;
    }

    /// <summary>
    /// UI用の入力アクションが現在押されているかを判定する
    /// </summary>
    public bool IsPressed(UiInputActionID action)
    {
        bool pressed = false;

        if (_currentGamepadState == null)
            return false;

        switch (action)
        {
            case UiInputActionID.RIGHT:
                pressed = _currentGamepadState.dpad.right.isPressed;
                break;
            case UiInputActionID.LEFT:
                pressed = _currentGamepadState.dpad.left.isPressed;
                break;
            case UiInputActionID.UP:
                pressed = _currentGamepadState.dpad.up.isPressed;
                break;
            case UiInputActionID.DOWN:
                pressed = _currentGamepadState.dpad.down.isPressed;
                break;
            case UiInputActionID.ESC:
                pressed = _currentGamepadState.startButton.isPressed;
                break;
            case UiInputActionID.None:
            default:
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

        if (_currentGamepadState == null)
            return false;

        switch (action)
        {
            case UiInputActionID.RIGHT:
                pressed = _currentGamepadState.dpad.right.wasPressedThisFrame;
                break;
            case UiInputActionID.LEFT:
                pressed = _currentGamepadState.dpad.left.wasPressedThisFrame;
                break;
            case UiInputActionID.UP:
                pressed = _currentGamepadState.dpad.up.wasPressedThisFrame;
                break;
            case UiInputActionID.DOWN:
                pressed = _currentGamepadState.dpad.down.wasPressedThisFrame;
                break;
            case UiInputActionID.ESC:
                pressed = _currentGamepadState.startButton.wasPressedThisFrame;
                break;
            case UiInputActionID.None:
            default:
                break;
        }

        return pressed;
    }
}

