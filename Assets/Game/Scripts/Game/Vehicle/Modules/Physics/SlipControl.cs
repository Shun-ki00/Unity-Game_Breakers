using UnityEngine;

public class SlipControl
{
    // 横滑りの抑制する強さ
    public float LateralGrip { get; set; }

    private readonly Rigidbody _rb;

    /// <summary> コンストラクタ </summary>
    public SlipControl(Rigidbody rb)
    {
        _rb = rb;
    }

    /// <summary> 更新処理 </summary>
    public void UpdateSlip()
    {
        // 横滑りを抑える処理
        this.ApplyGrip();
    }

    /// <summary> 横滑りを抑える処理 </summary>
    public void ApplyGrip()
    {
        // 現在の速度を取得
        Vector3 velocity = _rb.linearVelocity;
        // 横方向ベクトルの取得
        Vector3 right = _rb.transform.right;

        // 横成分を取り出す
        Vector3 lateralVel = Vector3.Project(velocity, right);

        // 横方向速度を打ち消す力を加える
        _rb.AddForce(-lateralVel * LateralGrip, ForceMode.Acceleration);
    }
}
