using UnityEngine;
using Unity.Cinemachine;  // Cinemachine 3 では名前空間が Unity.Cinemachine

[RequireComponent(typeof(CinemachineSplineCart))]
public class SplineCartFollower : MonoBehaviour
{
    [SerializeField] private Transform _Target;
    [SerializeField] private float _offset;

    [SerializeField] private int _stepsPerSegment = 5;

    private CinemachineSplineCart _splineCart;

    // 旧: m_Position の代わりに SplineCart では Position プロパティ
    // 旧: DollyCart.m_Path → SplineCart.track または Spline (depending on setup)

    void Start()
    {
        _splineCart = GetComponent<CinemachineSplineCart>();
    }

    void FixedUpdate()
    {
        //if (_Target == null || _splineCart.TrackingTarget == null)
        //    return;

        //Vector3 targetPosition = _Target.position;
        //Vector3 direction = -_Target.forward.normalized;
        //targetPosition += direction * _offset;

        //// 現在の Cart の位置（Spline 上の t）を取得
        //float currentT = _splineCart.SplinePosition;

        //// t 周辺を調べる分割数
        //int startStep = Mathf.FloorToInt(currentT) - 3;
        //int endStep = Mathf.CeilToInt(currentT) + 3;

        //// ClosestPoint を探す
        //float closestT = _splineCart.TrackingTarget
        //    .FindClosestPoint(targetPosition, startStep, endStep, _stepsPerSegment);

        //// Cart の位置を更新
        //_splineCart.SplinePosition = closestT;
    }
}
