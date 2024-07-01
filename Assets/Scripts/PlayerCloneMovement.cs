using System.Collections;
using System.Collections.Generic;
using Presentation.Projectiles;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerCloneMovement : MonoBehaviour
{
    private Vector3 _anchorPosition;
    private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Clone Start");
        _player = GameObject.FindGameObjectWithTag("Player");
        if (!_player)
        {
            Debug.LogError("Please assign player to the clone");
        }

        SetAnchorPosition(transform.position, GetComponent<SummonClone>().range);
        // Debug.Log player position
        Debug.Log("Player's position: " + _player.transform.position);
    }
    
    public void SetAnchorPosition(Vector3 anchorPosition, float range)
    {
        // Direction from _player.transform.position to transform.position is the same as the direction from anchorPosition to _player.transform.position but with a distance of range
        range /= 2;
        _anchorPosition = new Vector3(anchorPosition.x + (anchorPosition.x - _player.transform.position.x) * range, anchorPosition.y, anchorPosition.z + (anchorPosition.z - _player.transform.position.z) * range);
        Debug.Log(_anchorPosition);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
    }

    private void ProcessTranslation()
    {
        // Set transform position by getting the position of the player and mirroring it to the anchor position
        transform.position = new Vector3(_anchorPosition.x - (_player.transform.position.x - _anchorPosition.x), transform.position.y, _anchorPosition.z - (_player.transform.position.z - _anchorPosition.z));
    }
}
