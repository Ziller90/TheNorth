using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAnimation : MonoBehaviour
{
    [SerializeField] Vector3 _startPosition, _delta;
    [SerializeField] float _rotSpeed;
    [SerializeField] float _upSpeed;
    int _direction = 1;
    void Update()
    {
        gameObject.transform.Rotate(0,0,_rotSpeed*Time.deltaTime);
        transform.position += Vector3.up * Time.deltaTime * _upSpeed * _direction;
        if (Vector3.Distance(gameObject.transform.position, _startPosition + _delta)<1)
            _direction = -1;
        if (Vector3.Distance(gameObject.transform.position, _startPosition - _delta) < 1)
            _direction = 1;
    }
}
