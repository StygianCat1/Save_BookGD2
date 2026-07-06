using System.Collections.Generic;
using Script;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Class that can be used in the script (used to know the different sequences to create / play a note)
[System.Serializable]
public class SequencesType
{
    public E_Notes notes;
    public List <E_MoveDirection> sequences = new List<E_MoveDirection>();
}


public class S_Sequences : MonoBehaviour
{
    //The basic sequence list, filled in editor to know all possibility
    public List<SequencesType> _sequencesList = new List<SequencesType>();
    
    //List that will be used to check the notes 
    private List<SequencesType> _sequencesListChecker = new List<SequencesType>();

    //the ref to other GameObject / Script
    private GameObject _characterRef;
    private S_Movement _movement;
    private GameObject _gUI;
    private S_NotesChecker _noteschecker;

    public S_CombatZone _combatZoneRef;
        
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //Get the refs
        _characterRef = GameObject.FindGameObjectWithTag("Player");
        _movement = _characterRef.GetComponent<S_Movement>();
        _gUI = GameObject.FindGameObjectWithTag("GUI");
        _noteschecker = _gUI.GetComponent<S_NotesChecker>();
        
        //Sort of reset of the _sequencesListChecker, allow it to be then filled by the full List
        RefillSequences();
    }

    public void CheckSequences(List<E_MoveDirection> moveDirections)
    {
        //Verify if the sequences can be used in the movement made 
        List<SequencesType> sequencesToRemove = new List<SequencesType>();
        
        foreach (SequencesType sequences in _sequencesListChecker)
        {
            for (int i = 0; i < moveDirections.Count; i++)
            {
                //If they are not, they get added to the local variable that wiil soon after allow them to be cleared from the List
                if (moveDirections[i] != sequences.sequences[i])
                {
                    sequencesToRemove.Add(sequences);
                }
                
                //If they are, then change the color of the arrow played to yellow for a sign 
                else
                {
                    foreach (NotesRef notes in _noteschecker.notes)
                    {
                        if (notes.notes == sequences.notes)
                        {
                            notes.arrowChangeSetUp.arrows[i].GetComponent<Image>().color = Color.yellow;
                        }
                    }
                }
            }
        }
        
        //Clear any sequence that isn't used + reset their arrow for better understanding
        foreach (SequencesType sequences in sequencesToRemove)
        {
            foreach (NotesRef notesCheck in _noteschecker.notes)
            {
                if (notesCheck.notes == sequences.notes)
                {
                    notesCheck.arrowChangeSetUp.ResetAllArrows();  
                }
            }
            _sequencesListChecker.Remove(sequences);
        }


        //If the number stiil in the list is higher than one, do nothing
        if (_sequencesListChecker.Count > 1)
        {
            return;
        }
        //If it is equal to one, check if it is the full sequence
        if (_sequencesListChecker.Count == 1)
        {
            if (_sequencesListChecker[0].sequences.Count == moveDirections.Count)
            {
                // if it is signal it to another function
                GiveSequenceInformation(_sequencesListChecker[0].notes);
            }
        }
        // If it is equal to 0, then reset all
        else if (_sequencesListChecker.Count == 0)
        {
            Debug.Log("No sequences");
            RefillSequences();
        }
    }

    private void GiveSequenceInformation(E_Notes notes)
    {
        //Add here script to make it visible that sequence worked
        if (_combatZoneRef != null)
        {
            _combatZoneRef.CheckNoteGiven(notes);
        }
        foreach (NotesRef notesCheck in _noteschecker.notes)
        {
            if (notesCheck.notes == notes)
            {
                notesCheck.arrowChangeSetUp.ResetAllArrows();
            }
        }
        //Debug.Log(notes);
        RefillSequences();
    }

    //Sort of reset of the Lists used
    private void RefillSequences()
    {
        _movement._moveDirections.Clear();
        _sequencesListChecker.Clear();
        _sequencesListChecker.AddRange(_sequencesList);
    }
    
}
