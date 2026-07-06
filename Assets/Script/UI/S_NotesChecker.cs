using System.Collections.Generic;
using Script;
using UnityEngine;

[System.Serializable]
public class NotesRef
{
    public E_Notes notes;
    public S_ArrowChangeSetUp arrowChangeSetUp;
}

public class S_NotesChecker : MonoBehaviour
{
    public List<NotesRef> notes;
    
}
