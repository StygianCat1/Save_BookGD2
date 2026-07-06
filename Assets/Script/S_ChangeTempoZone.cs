using System;
using UnityEngine;

public class S_ChangeTempoZone : MonoBehaviour
{
    
    private GameObject _player;
    private GameObject _gameManager;
    

    private float _bPMRef;
    
    [SerializeField] private  AudioSource _audio;
    [SerializeField] private float _newBPM;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager");
        _bPMRef = _gameManager.GetComponent<S_BeatChecker>()._bpm;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _audio.pitch = _newBPM / _bPMRef;
            _gameManager.GetComponent<S_BeatChecker>()._bpm = _newBPM;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _audio.pitch = 1;
            _gameManager.GetComponent<S_BeatChecker>()._bpm = _bPMRef;
        }
    }
}
