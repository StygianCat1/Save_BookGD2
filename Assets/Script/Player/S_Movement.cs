using System.Collections.Generic;
using Script;
using UnityEngine;

public class S_Movement : MonoBehaviour
{
    [SerializeField] private float speedMovement;
    [Tooltip("An empty GameObject en character position to allow grid movement")][SerializeField] private Transform movePoint;
    
    [Tooltip("Ref to the Layer the character can walk on")]public LayerMask groundLayer;
    
    //Ref to where the adventurer moved in the last beat
    public List<E_MoveDirection> _moveDirections;
    
    //bunch of private ref for link to other script
    private S_Inputs _inputs;
    private GameObject _gameManager;
    private S_BeatChecker _beatChecker;
    private S_Sequences _sequences;
    private S_Health _health;
        
    //Dictionary to know where the player move depending on his movement input
    Dictionary<Vector2, E_MoveDirection> _moveDirectionDictionary = new Dictionary<Vector2, E_MoveDirection>() 
    { 
        { Vector2.up, E_MoveDirection.Up },
        { Vector2.left, E_MoveDirection.Left },
        { Vector2.down, E_MoveDirection.Down },
        { Vector2.right, E_MoveDirection.Right }
    };
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //Ref to other GameObject / Script
        _inputs = GetComponent<S_Inputs>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager");
        _beatChecker = _gameManager.GetComponent<S_BeatChecker>();
        _sequences = _gameManager.GetComponent<S_Sequences>();
        _health = GetComponent<S_Health>();
        
        //Cut the parent movePoint (cut it from the character) so that it does not move with it
        movePoint.parent = null;
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //move the adventurer to the move Point position at chosen speed
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoint.position.x, transform.position.y, movePoint.position.z), speedMovement * Time.deltaTime);
        
        //Verify if player is pressing input in the rhythm input window
        if (!_beatChecker.CheckInputWindow())
        {
            //if not stop movement & skip the rest of the code
            _inputs.moveDirection = new Vector2(0,0);
            return;
        }
        
        if (Vector3.Distance(transform.position, new Vector3(movePoint.position.x, transform.position.y, movePoint.position.z)) <= 0.1f)

        //Verify that player pressed a touch on x axis
        if (_inputs.moveDirection.x != 0)
        {
            // create a collider that check if character can walk where he want to go
            Collider[] Checkcollider = (Physics.OverlapSphere(movePoint.position + new Vector3(_inputs.moveDirection.x, 0, 0), 0.1f, groundLayer));
            if (Checkcollider.Length > 0)
            {
                //if he can, he moves and register the button that was pressed
                movePoint.position += new Vector3(_inputs.moveDirection.x, 0, 0);
                GetPressedButton();
            }
        }
        //Or if he pressed a touch on y axis (block possibility to move diagonally 
        else if (_inputs.moveDirection.y != 0)
        {
            // create a collider that check if character can walk where he want to go
            Collider[] Checkcollider = (Physics.OverlapSphere(movePoint.position + new Vector3(0, 0, _inputs.moveDirection.y), 0.1f, groundLayer));
            if (Checkcollider.Length > 0)
            {
                //if he can, he moves and register the button that was pressed
                movePoint.position += new Vector3(0, 0, _inputs.moveDirection.y);
                GetPressedButton();
            }
        }
        //Used to constantly stop movement, to allow for taping and not sliding
        _inputs.moveDirection = new Vector2(0,0);
    }

    private void GetPressedButton()
    {
        //Use the dictionary to convert the Vector 2 into a direction in an enumeration and add it to the "_moveDirections" List 
        _moveDirections.Add(_moveDirectionDictionary[_inputs.moveDirection]);
        //Part that verify the list to see if the movement followed a certain sequences of movement
        _sequences.CheckSequences(_moveDirections);
    }
}
