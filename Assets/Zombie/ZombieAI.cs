using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class ZombieAI : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private LayerMask layerMask;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private BoxCollider2D _collider;
    private float _currentSpeed;
    private Vector2 _direction = new Vector2(1, 0);
    private bool isAlive = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _currentSpeed = _speed;
        StartCoroutine(Die());
    }

    private void Update()
    {
        if (isAlive == false)
            return;

        if (Physics2D.Raycast(transform.position, _direction, 0.5f))
        {
            if (_currentSpeed != 0)
                StartCoroutine(Turn());

            _currentSpeed = 0;
        }
        _rigidbody.velocity = new Vector2(_currentSpeed * _direction.x, _rigidbody.velocity.y);
        _animator.SetBool("isWalking", _currentSpeed != 0);
        _renderer.flipX = _direction.x < 0;
    }

    private IEnumerator Turn()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 2f));

        _direction = -_direction;
        _currentSpeed = _speed;
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(Random.Range(2f, 30f));

        isAlive = false;
        _currentSpeed = 0;
        _animator.SetBool("isAlive", false);
        _collider.enabled = false;
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }
}
