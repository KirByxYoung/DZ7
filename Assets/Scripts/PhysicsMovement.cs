using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private ContactFilter2D _contactFilter;
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    private Vector2 _targetVelocity;
    private Vector2 _groundNormal;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);
    private bool _grounded;
    private float _minMoveDistance = 0.001f;
    private float _shellRadius = 0.01f;
    private float _minGroundNormalY = 0.65f;
    private float _gravityModifier = 0.85f;
    private float _speed = 2.5f;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (Input.GetKey(KeyCode.Space) && _grounded)
        {
            _velocity.y = 5;
        }
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;

        _grounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Move(move, false);

        move = Vector2.up * deltaPosition.y;

        Move(move, true);
    }

    private void Move(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > _minMoveDistance)
        {
            int count = _rigidbody.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currnetNormal = _hitBufferList[i].normal;

                if (currnetNormal.y > _minGroundNormalY)
                {
                    _grounded = true;

                    if (yMovement)
                    {
                        _groundNormal = currnetNormal;
                        currnetNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currnetNormal);

                if (projection < 0)
                {
                    _velocity = _velocity - projection * currnetNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        if (yMovement)
            _rigidbody.position = _rigidbody.position + move.normalized * distance;
        else
            _rigidbody.position = _rigidbody.position + move.normalized * distance * _speed;
    }
}