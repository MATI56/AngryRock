using System;
using UnityEngine;

   public abstract class Timer 
    {
        [SerializeField] protected float initialTime;
        [SerializeField] protected float currentTime;
        [SerializeField] protected bool isRunning;
        public float Time { get => currentTime; protected set => currentTime = value; }
        public bool IsRunning { get => isRunning; protected set => isRunning = value; }
        
        public float Progress => Time / initialTime;
        
        public event Action OnTimerStart = delegate { };
        public event Action OnTimerPause = delegate { };
        public event Action OnTimerResume = delegate { };
        public event Action OnTimerStop = delegate { };

        protected Timer(float value)
        {
            initialTime = value;
            IsRunning = false;
        }

        public void Start()
        {
            Time = initialTime;
            if (!IsRunning) {
                IsRunning = true;
                OnTimerStart?.Invoke();
            }
        }

        public void Stop(bool force = false) 
        {
            if (IsRunning || force) 
            {
                IsRunning = false;
                OnTimerStop?.Invoke();
            }
        }
        
        public void Pause()
        { 
            IsRunning = false;
            OnTimerPause?.Invoke();
        }
        
        public void Resume()
        { 
            IsRunning = true;
            OnTimerResume?.Invoke();
        }


        public void ForceSetTime(float time) => Time = time;
        public void SetInitialTime(float time) => initialTime = time;

        public abstract void Tick(float deltaTime);
    }


/// <summary>
/// Counts down timer, starting from value X going to 0.
/// </summary>
[Serializable]
public class CountdownTimer : Timer
{

    public CountdownTimer(float value) : base(value) { }

    public override void Tick(float deltaTime)
    {
        if (IsRunning && Time > 0)
        {
            Time -= deltaTime;
        }

        if (IsRunning && Time <= 0)
        {
            Stop();
        }
    }

    public bool IsFinished => Time <= 0;

    /// <summary>
    /// Resets time back to its initial value and starts the timer
    /// </summary>
    public void Restart()
    {
        ResetTime();
        Start();
    }

    /// <summary>
    /// Resets time to given value and starts the timer
    /// </summary>
    public void Restart(float newTime)
    {
        ResetTime(newTime);
        Start();
    }

    /// <summary>
    /// Resets time back to its initial value
    /// </summary>
    public void ResetTime() => Time = initialTime;

    /// <summary>
    /// Resets time to given value
    /// </summary>
    public void ResetTime(float newTime)
    {
        initialTime = newTime;
        ResetTime();
    }
}

/// <summary>
/// Constantly adds time to the timer. Starts from 0.
/// </summary>
[Serializable]
public class StopwatchTimer : Timer
{
    public StopwatchTimer() : base(0) { }

    public override void Tick(float deltaTime)
    {
        if (IsRunning)
        {
            Time += deltaTime;
        }
    }

    public void Reset() => Time = 0;

    public float GetTime() => Time;
}
