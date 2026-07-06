using UnityEngine;

public class S_OpenPauseMenu : MonoBehaviour
{
    private S_Inputs _inputs;
    [SerializeField] private GameObject _pauseMenu;
    
    private void Start()
    {
        _inputs = GetComponent<S_Inputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputs.pause)
        {
            _inputs.pause = false;
            Instantiate(_pauseMenu);
        }
    }
}
