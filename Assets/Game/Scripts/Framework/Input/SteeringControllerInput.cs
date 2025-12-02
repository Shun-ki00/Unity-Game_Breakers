// ============================================
// 
// ファイル名: SteeringControllerInput.cs
// 概要: ステアリングコントローラーの入力系処理
// 
// 製作者 : 清水駿希
// 
// ============================================

public class SteeringControllerInput : IInputDevice
{
    public GamePlayInputState GetInput { get { return _gamePlayInputSnapshot; } }

    private GamePlayInputState _gamePlayInputSnapshot;
    private SteeringController _steeringController;

    /// <summary>
    /// 入力状態を更新する
    /// </summary>
    public void GamePlayInputUpdate()
    {
        // ステアリングコントローラー
        _steeringController = SteeringController.Instance;

        _steeringController.Update();

        if (_steeringController.GetState() == false) return;

        // 毎フレームで入力を集約して Snapshot を作成
        var snapshot = new GamePlayInputState(
            handle: _steeringController.GetSteeringPosition() * -1.0f,
            accelerator: _steeringController.GetAcceleratorPosition(),
            brake: _steeringController.GetBrakePosition(),
            boost: _steeringController.GetButtonWasPressedThisFrame(SteeringController.ButtonID.A),
            cameraView: _steeringController.GetButtonWasPressedThisFrame(SteeringController.ButtonID.B)
        );

        _gamePlayInputSnapshot = snapshot;
    }

    /// <summary>
    /// UI用の入力アクションが現在押されているかを判定する
    /// </summary>
    public bool IsPressed(UiInputActionID action)
    {
        bool pressed = false;

        switch (action)
        {
            case UiInputActionID.RIGHT:
                pressed = _steeringController.GetPOVIsPressed(SteeringController.POVDirection.RIGHT);
                break;
            case UiInputActionID.LEFT:
                pressed = _steeringController.GetPOVIsPressed(SteeringController.POVDirection.LEFT);
                break;
            case UiInputActionID.UP:
                pressed = _steeringController.GetPOVIsPressed(SteeringController.POVDirection.UP);
                break;
            case UiInputActionID.DOWN:
                pressed = _steeringController.GetPOVIsPressed(SteeringController.POVDirection.DOWN);
                break;
            case UiInputActionID.ESC:
                pressed = _steeringController.GetButtonIsPressed(SteeringController.ButtonID.OPTIONS);
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

        switch (action)
        {
            case UiInputActionID.RIGHT:
                pressed = _steeringController.GetPOVWasPressedThisFrame(SteeringController.POVDirection.RIGHT);
                break;
            case UiInputActionID.LEFT:
                pressed = _steeringController.GetPOVWasPressedThisFrame(SteeringController.POVDirection.LEFT);
                break;
            case UiInputActionID.UP:
                pressed = _steeringController.GetPOVWasPressedThisFrame(SteeringController.POVDirection.UP);
                break;
            case UiInputActionID.DOWN:
                pressed = _steeringController.GetPOVWasPressedThisFrame(SteeringController.POVDirection.DOWN);
                break;
            case UiInputActionID.ESC:
                pressed = _steeringController.GetButtonWasPressedThisFrame(SteeringController.ButtonID.OPTIONS);
                break;
            case UiInputActionID.None:
            default:
                break;
        }

        return pressed;
    }
}

