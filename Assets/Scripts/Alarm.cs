using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private Home _home;

    private Coroutine _alarmStart;
    private Coroutine _alarmStop;
    private Animator _animator;
    private AudioSource _audio;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _changeRate = 0.5f;
    private float _currentVolume;
    private int _alarmHash = Animator.StringToHash("Alarm");


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _currentVolume = _audio.volume;
    }

    private void OnEnable()
    {
        _home.AlarmChanged += Alarmed;
    }

    private void OnDisable()
    {
        _home.AlarmChanged -= Alarmed;
    }

    public void Alarmed(bool alarm)
    {
        if (alarm && _currentVolume < _maxVolume)
        {
            _alarmStart = StartCoroutine(AlarmUp());
        }

        if (!alarm && _currentVolume > _minVolume)
        {
            _alarmStop = StartCoroutine(AlarmDown());
        }
    }

    private IEnumerator AlarmUp()
    {
        if (_alarmStop != null)
        {
            StopCoroutine(_alarmStop);
        }

        if (!_audio.isPlaying)
        {
            _audio.Play();
            _animator.SetBool(_alarmHash, true);
        }

        while (_currentVolume < _maxVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _maxVolume, _changeRate * Time.deltaTime);
            _audio.volume = _currentVolume;

            yield return null;
        }
    }

    private IEnumerator AlarmDown()
    {
        StopCoroutine(_alarmStart);

        while (_currentVolume > _minVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _minVolume, _changeRate * Time.deltaTime);
            _audio.volume = _currentVolume;

            yield return null;
        }

        _animator.SetBool(_alarmHash, false);
        _audio.Stop();
    }

    //private void Update()
    //{
    //    if (_isAlarm && _currentVolume < _maxVolume)
    //    {
    //        _currentVolume = Mathf.MoveTowards(_currentVolume, _maxVolume, _changeRate * Time.deltaTime);
    //        _audio.volume = _currentVolume;
    //    }

    //    if (!_isAlarm && _currentVolume > _minVolume)
    //    {
    //        _currentVolume = Mathf.MoveTowards(_currentVolume, _minVolume, _changeRate * Time.deltaTime);
    //        _audio.volume = _currentVolume;
    //    }

    //    if (_audio.volume == _minVolume)
    //    {
    //        _animator.SetBool(_alarmHash, false);
    //        _audio.Stop();
    //    }
    //}
}
