using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionComponent : MonoBehaviour
{
    [SerializeField] Player target = null;

    [SerializeField] bool showDetectionRange = true;
    [SerializeField,Range(5.0f,10.0f)] float detectionRange = 0.0f;

    public Player Target => target;

    // Start is called before the first frame update
    void Start()
    {

    }

    public bool IsClose()
    {
        List<PlayerGameInfo> _allPlayers = GameManager.Instance.AllPlayers;
        foreach (PlayerGameInfo _data in _allPlayers)
        {
            Player _player = _data.objectReference;

            if (!_player) continue;

            if (Vector3.Distance(transform.position, _player.transform.position) <= detectionRange)
            {
                target = _player;
                return true;
            }
        }
        target = null;
        return false;        
    }

    private void OnDrawGizmos()
    {
        if (!showDetectionRange) return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position,detectionRange);

        Gizmos.color = Color.white;
    }
}
