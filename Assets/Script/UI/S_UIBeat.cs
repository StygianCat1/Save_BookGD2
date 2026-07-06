using UnityEngine;
using UnityEngine.UI;


public class S_UIBeat : MonoBehaviour
{
    [SerializeReference] private Image _beatImage;

    private S_BeatChecker _beatChecker;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created + Get ref to the beatChecker
    private void Start()
    {
        _beatChecker = GameObject.FindGameObjectWithTag("GameManager").GetComponent<S_BeatChecker>();
    }

    // Update is called once per frame + Every time beatChecker is true, it show the image otherwise it does not
    private void Update()
    {
        _beatImage.enabled = _beatChecker.CheckInputWindow();
    }
}
