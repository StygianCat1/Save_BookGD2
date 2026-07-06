using System;
using Script;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class S_CombatZone : MonoBehaviour
{
    
    private S_Sequences _sequences;

    [SerializeField] private S_SequenceOfNotes _sequencesOfNotesUI; 
    [SerializeField] private GameObject _enemyRef;

    private Transform _enemyTransform;
    private int _indexSequencesOfNotes = 0;

    private void Start()
    {
        _sequences = GameObject.FindGameObjectWithTag("GameManager").GetComponent<S_Sequences>();
        _enemyTransform = _enemyRef.transform;
    }
    
    
    public void CheckNoteGiven(E_Notes note)
    {
        if (_sequencesOfNotesUI.sequenceOfNotes[_indexSequencesOfNotes].notes == note)
        {
            
            _sequencesOfNotesUI.sequenceOfNotes[_indexSequencesOfNotes].notesImage.color = Color.grey;
            _indexSequencesOfNotes++;
        }


        if (_indexSequencesOfNotes == _sequencesOfNotesUI.sequenceOfNotes.Count )
        {
            foreach (SequenceOfNotes _sequence in _sequencesOfNotesUI.sequenceOfNotes)
            {
                _sequence.notesImage.color = Color.white;
            }
            _indexSequencesOfNotes = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _sequences._combatZoneRef = this;
            _enemyRef.GetComponent<S_Enemy>()._playerRef = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _sequences._combatZoneRef = null;
            _enemyRef.transform.position = _enemyTransform.position;
            _enemyRef.GetComponent<S_Enemy>()._playerRef = null;
        }
    }
}
