using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_HeartChange : MonoBehaviour
{
    //Get Refs to the hearts and to their different sprites
    public List<GameObject> hearts;
    public Sprite heartFullSprites;
    public Sprite heartLessSprites;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created + Set all heart to full
    private void Start()
    {
        foreach (GameObject heart in hearts)
        {
            heart.GetComponent<Image>().sprite = heartFullSprites;
        }
    }

    //Set the life in the UI with a value
    public void SetLife (int value)
    {
        //Create a local variable to keep some data
        List<GameObject> tempHeartList = new List<GameObject>();
        //For the value, add one heart from the heart List to the local variable
        for (int i = 0; i < value; i++)
        {
            {
                tempHeartList.Add(hearts[i]);
            }
        }

        //After that check in the list 
        foreach (GameObject heart in hearts)
        {
            //if the index of the heart is inferior or equal to the total in the local variable 
            if (hearts.IndexOf(heart) <= tempHeartList.Count - 1)
            {
                //Then set the heart to full
                heart.GetComponent<Image>().sprite = heartFullSprites;
            }
            else
            {
                //If it is not set them to the void one
                heart.GetComponent<Image>().sprite = heartLessSprites;
            }
        }
    }
}
