using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MovementAnimation : MonoBehaviour
{
    private Animator _animator;

    private int _moveLeftHash;
    private int _moveRightHash;
    private int _idleHash;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _moveLeftHash = Animator.StringToHash("MoveLeft");
        _moveRightHash = Animator.StringToHash("MoveRight");
        _idleHash = Animator.StringToHash("Idle");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _animator.SetTrigger(_moveLeftHash);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _animator.SetTrigger(_moveRightHash);
        }

        if (Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false)
        {
            _animator.SetTrigger(_idleHash);
        }
    }
}