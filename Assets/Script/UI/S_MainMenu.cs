using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MainMenu : MonoBehaviour
{

    //Launch the scene chose(by name)
    public void PlayGame(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }

    //Open a chosen menu 
    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    
    // Just Quit
    public void QuitGame()
    {
        Application.Quit();
    }

}
