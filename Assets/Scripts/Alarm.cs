using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private Home _home;

    private Coroutine _coroutine;
    private Animator _animator;
    private AudioSource _audio;
    private float _changeRate = 0.5f;
    private int _alarmHash = Animator.StringToHash("Alarm");

    private void OnEnable() => _home.AlarmChanged += OnAlarmChanged;

    private void OnDisable() => _home.AlarmChanged -= OnAlarmChanged;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    public void OnAlarmChanged(bool isAlarm, float targetVolume)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeValue(isAlarm, targetVolume));
    }

    private IEnumerator ChangeValue(bool isAlarm, float targetVolume)
    {
        if (isAlarm)
            _animator.SetBool(_alarmHash, isAlarm);

        while (_audio.volume != targetVolume)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, targetVolume, _changeRate * Time.deltaTime);

            yield return null;
        }

        _animator.SetBool(_alarmHash, isAlarm);
    }
}
