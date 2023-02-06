using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private Home _home;

    private Coroutine _coroutine;
    private Animator _animator;
    private AudioSource _audio;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _changeRate = 0.5f;
    private int _alarmHash = Animator.StringToHash("Alarm");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
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
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(AlarmState(alarm));
    }

    private IEnumerator AlarmState(bool alarm)
    {
        if (alarm && _audio.volume < _maxVolume)
        {
            if (!_audio.isPlaying)
            {
                _audio.Play();
                _animator.SetBool(_alarmHash, true);
            }

            while (_audio.volume < _maxVolume)
            {
                _audio.volume = Mathf.MoveTowards(_audio.volume, _maxVolume, _changeRate * Time.deltaTime);

                yield return null;
            }
        }
        else if (!alarm && _audio.volume > _minVolume)
        {
            while (_audio.volume > _minVolume)
            {
                _audio.volume = Mathf.MoveTowards(_audio.volume, _minVolume, _changeRate * Time.deltaTime);

                yield return null;
            }

            _animator.SetBool(_alarmHash, false);
            _audio.Stop();
        }
    }
}
