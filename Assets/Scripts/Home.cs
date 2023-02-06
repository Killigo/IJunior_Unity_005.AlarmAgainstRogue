using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Home : MonoBehaviour
{
    private bool _isAlarm;

    public event Action<bool> AlarmChanged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAlarm)
            return;

        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isAlarm = true;
            AlarmChanged?.Invoke(_isAlarm);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isAlarm = false;
            AlarmChanged?.Invoke(_isAlarm);
        }
    }
}
