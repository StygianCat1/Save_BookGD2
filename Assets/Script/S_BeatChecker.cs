using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Intervals
{
    [SerializeField] private float _steps;
    [SerializeField] private UnityEvent _trigger;
    
    private int _lastInterval;

    //Calculate Interval length with the bpm (given in editor) and the steps (said below, change depending on how the
    //editor want it to be played (will mostly be 1 as want to be played every beat)) to allow CheckForNewInterval to work
    public float GetIntervalLength(float bpm)
    {
        return 60f / (bpm * _steps);
    }

    // Check with the update below the time before the next beat and when it's time, it play the unity event and reset until next beat 
    public void CheckForNewinterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != _lastInterval)
        {
            _lastInterval = Mathf.FloorToInt(interval);
            _trigger.Invoke();
        }
    }
}

public class S_BeatChecker : MonoBehaviour
{
    //Basic information to be added for the beat to be always right on time
    public float _bpm;
    [SerializeField] private AudioSource _audioSource;
    [Tooltip("Will be converted in ms for practical usage")][SerializeField] private float _playerInputWindow;
    [Tooltip("Only to be used on enemy / UI, used to make them follow the beat (1 = every beat, 0.5 = every 2 beat, etc...")][SerializeField] private Intervals[] _intervals;

    private float _sampleTime;
    
    //Change constantly the CheckForNewInterval function, interval to allow its event to be played
    private void Update()
    {
        foreach (Intervals i in _intervals)
        {
            float sampleTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * i.GetIntervalLength(_bpm)));
            i.CheckForNewinterval(sampleTime);
        }
    }

    //Test to check for input window of charater movement
    public bool CheckInputWindow()
    {
        _sampleTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * 60 / _bpm));
        if (Mathf.RoundToInt(_sampleTime) - (_playerInputWindow / 1000) <= _sampleTime && Mathf.RoundToInt(_sampleTime) + (_playerInputWindow / 1000) >= _sampleTime)
        {
            return true;
        }
        return false;
    }
}

