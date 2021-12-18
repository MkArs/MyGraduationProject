using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearController : MonoBehaviour
{
    private GameObject _player;
    private float _travelDistance = 0f;
    private Vector2 _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _travelDistance += Vector2.Distance(_startPosition, gameObject.transform.position);
        _startPosition = gameObject.transform.position;

        if(_travelDistance >= _player.gameObject.GetComponent<PlayerController>().Range)
        {
            Destroy(gameObject);
        }
    }

}
