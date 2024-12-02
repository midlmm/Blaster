using System;
using UnityEngine;

public class Timer
{
    private Action<float> OnUpdateTimer;
    private Action OnEndTimer;

    private float _timeLeft = 0f;
    private bool _timerOn = false;

    private float _time;

    public Timer(float time, Action<float> updateTimer, Action endTimer)
    {
        _time = time;

        OnUpdateTimer = updateTimer;
        OnEndTimer = endTimer;
    }

    public Timer(float time, Action endTimer)
    {
        _time = time;

        OnEndTimer = endTimer;
    }

    public void Tick()
    {
        if (_timerOn)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                OnUpdateTimer?.Invoke(_timeLeft);
            }
            else
            {
                _timerOn = false;
                OnEndTimer.Invoke();
            }
        }
    }

    public void TimerStart()
    {
        _timeLeft = _time;
        _timerOn = true;
    }

    public void TimerStop()
    {
        _timerOn = false;
    }
}
