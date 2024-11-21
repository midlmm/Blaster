using System;
using UnityEngine;

public class Timer
{
    private Action OnEndTimer;
    private Action<float> OnUpdateTimer;

    private float _time;

    private float _timeLeft = 0f;
    private bool _timerOn = false;

    public Timer(float time, Action actionEndTimer, Action<float> actionUpdateTimer)
    {
        OnEndTimer = actionEndTimer;
        OnUpdateTimer = actionUpdateTimer;

        _time = time;
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
                OnEndTimer?.Invoke();
            }
        }
    }

    public void TimerStart()
    {
        _timeLeft = _time;
        _timerOn = true;
    }

    public void TimerPause()
    {
        _timerOn = false;
    }
}
