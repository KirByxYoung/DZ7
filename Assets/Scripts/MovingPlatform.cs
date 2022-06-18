using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector2 _endPoint;
    [SerializeField] private float _drivingTime = 10;

    private float _minDrivingTime = 10;

    private void Start()
    {
        _drivingTime = _drivingTime < _minDrivingTime ? _minDrivingTime : _drivingTime;

        Move();
    }

    private void Move()
    {
        transform.DOMove(_endPoint, _drivingTime).SetLoops(-1, LoopType.Yoyo);
    }
}