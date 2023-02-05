using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private Animator _animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
            _sprite.flipX= true;
            _animator.SetFloat("Walk", 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            _sprite.flipX = false;
            _animator.SetFloat("Walk", 1);
        }
        else
        {
            _animator.SetFloat("Walk", 0);
        }
    }
}
