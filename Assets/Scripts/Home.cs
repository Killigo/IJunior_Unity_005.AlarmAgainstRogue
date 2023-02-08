using UnityEngine;
using UnityEngine.Events;

public class Home : MonoBehaviour
{
    private bool _isAlarm;
    private readonly float _minVolue = 0f;
    private readonly float _maxVolue = 1f;

    public UnityAction<bool, float> AlarmChanged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAlarm)
            return;

        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isAlarm = true;
            AlarmChanged?.Invoke(_isAlarm, _maxVolue);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isAlarm = false;
            AlarmChanged?.Invoke(_isAlarm, _minVolue);
        }
    }
}
