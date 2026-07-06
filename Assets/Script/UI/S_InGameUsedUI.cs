using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class S_InGameUsedUI : MonoBehaviour
{
    private GameObject _gUI;
    private GameObject _gameManager;
    
    [SerializeField] private TextMeshProUGUI _text;
    
    
    
    //Stop time and get Gui Ref
    private void Start()
    {
        _gUI = GameObject.FindGameObjectWithTag("GUI");
        _gameManager = GameObject.FindGameObjectWithTag("GameManager");
        //_musicSpeedSlider = _musicSpeedSliderGameObject.GetComponent<Slider>();
        _gUI.SetActive(false);
        Time.timeScale = 0;
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Pause();
        }
    }

    //Allow to continue the game , show back Gui and set the time scale back to 1
    public void ResumeGame()
    {
        _gUI.SetActive(true);
        Time.timeScale = 1;
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Play();
        }
        Destroy(gameObject);
    }
    
    //relaunch the level
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }

    public void ChangeSpeed(float newSpeed)
    {
        _gameManager.GetComponent<S_BeatChecker>()._bpm = 100 * newSpeed;
        _text.text = (Mathf.Round(newSpeed * 10)/10).ToString();
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.pitch = newSpeed;
        }
    }

    //Open the chosen level (by name)
    public void OpenNewLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    //Just Quit
    public void QuitGame()
    {
        Application.Quit();
    }
}
