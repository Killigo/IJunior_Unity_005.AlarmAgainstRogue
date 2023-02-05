using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private UnityEvent _alarmed;

    private Animator _animator;
    private AudioSource _audio;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _changeRate = 0.5f;
    private float _currentVolume;
    private bool _isAlarm;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _currentVolume = _audio.volume;
    }

    private void Update()
    {
        if (_isAlarm && _currentVolume < _maxVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _maxVolume, _changeRate * Time.deltaTime);
            _audio.volume = _currentVolume;
        }

        if (!_isAlarm && _currentVolume > _minVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _minVolume, _changeRate * Time.deltaTime);
            _audio.volume = _currentVolume;
        }

        if (_audio.volume == _minVolume)
        {
            _animator.SetBool("Alarm", false);
            _audio.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isAlarm = true;

        if (collision.TryGetComponent<Player>(out Player player))
        {
            if (!_audio.isPlaying)
            {
                _audio.Play();
            }

            _animator.SetBool("Alarm", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isAlarm = false;
        }
    }
}
