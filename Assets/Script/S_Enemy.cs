using System;
using UnityEngine;
using UnityEngine.UIElements;

public class S_Enemy : MonoBehaviour
{
    public GameObject _playerRef;

    [SerializeField] private float _speedMovementEnemy;

    private float positionXFromPlayer;
    private float positionYFromPlayer;

    public void EnemyAction()
    {
        if (_playerRef == null) {return;}
        getCharacterPosition();
        Debug.Log(transform.position.x);
        if (positionXFromPlayer <= 1 && positionYFromPlayer <= 1)
        {
            _playerRef.GetComponent<S_Health>().TakeDamage(1);
            return;
        }
        if (positionXFromPlayer <= positionYFromPlayer)
        {
            if (Vector3.Distance(new Vector3(0, 0, _playerRef.transform.position.z), new Vector3(0, 0, transform.position.z + 1)) > Vector3.Distance(new Vector3(0, 0, _playerRef.transform.position.z), new Vector3(0, 0, transform.position.z - 1)))
            {
                transform.position = new Vector3(transform.position.x , transform.position.y, transform.position.z - 1);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            }
        }
        else if (positionXFromPlayer > positionYFromPlayer)
        {
            if (Vector3.Distance(new Vector3(_playerRef.transform.position.x, 0, 0), new Vector3(transform.position.x + 1, 0, 0)) > Vector3.Distance(new Vector3(_playerRef.transform.position.x, 0, 0), new Vector3(transform.position.x - 1, 0, 0)))
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            }
        }
    }

    private void getCharacterPosition()
    {
        positionXFromPlayer = Vector3.Distance(new Vector3(_playerRef.transform.position.x, 0, 0), new Vector3(transform.position.x, 0, 0));
        positionYFromPlayer = Vector3.Distance(new Vector3(0, 0, _playerRef.transform.position.z), new Vector3(0, 0, transform.position.z));
    }
}
