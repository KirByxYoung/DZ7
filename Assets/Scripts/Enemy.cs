using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 _endPoint;

    private float _drivingTime = 5;

    private void Start()
    {
        Move();
    }

    private void Move()
    {
        transform.DOMove(_endPoint, _drivingTime).SetLoops(-1, LoopType.Yoyo);
    }
}