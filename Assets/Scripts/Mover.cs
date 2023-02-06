using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;

    private int _walkHash = Animator.StringToHash("Walk");
    private SpriteRenderer _sprite;
    private Animator _animator;
    private float _idleSpeed = 0f;

    private void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
            _sprite.flipX= true;
            _animator.SetFloat(_walkHash, _speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            _sprite.flipX = false;
            _animator.SetFloat(_walkHash, _speed);
        }
        else
        {
            _animator.SetFloat(_walkHash, _idleSpeed);
        }
    }
}
