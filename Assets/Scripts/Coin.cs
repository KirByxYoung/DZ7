using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    private float _rotateTime = 3;
    private float _degree = -180;
    private Tween _tween;

    private void Start()
    {
        Flip();
    }

    private void Flip()
    {
        Vector3 direction = new Vector3(0, _degree, 0);

        _tween = transform.DORotate(direction, _rotateTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    public void StopFlip()
    {
        _tween.Kill();
    }
}