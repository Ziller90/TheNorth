using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BoatAnimation : MonoBehaviour
{
    [SerializeField] Color _gizmosColor;
    [SerializeField] Vector3[] _positions;
    [SerializeField] Vector3 _targetPosition;
    [SerializeField] float _rotateSpeed=0.05f;
    [SerializeField] float _moveSpeed=0.5f;
    [SerializeField] bool showGizmos;
    int _iterator = 0;

    private void Start()
    {
        gameObject.transform.position = _positions[0];
        _targetPosition = _positions[1];
    }

    private void FixedUpdate()
    {
        RotateTo(_targetPosition);
        MovedTo(_targetPosition);
    }

    void RotateTo(Vector3 targetPosition)
    {
        var LookDirrection = Quaternion.LookRotation(targetPosition- gameObject.transform.position);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, LookDirrection, _rotateSpeed);
    }

    void MovedTo(Vector3 targetPosition)
    {
        var LookDirrection = targetPosition - gameObject.transform.position;
        if (Vector3.Distance(gameObject.transform.position, targetPosition) > 1f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed);
        }
        else
        {
            _iterator = (_iterator + 1) % _positions.Length;
            _targetPosition = _positions[_iterator];
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            for (int i = 0; i < _positions.Length; ++i)
            {
                var position = _positions[i];
                Gizmos.color = _gizmosColor;
                Gizmos.DrawSphere(position, 0.4f);
                Gizmos.DrawLine(position, _positions[(i + 1) % _positions.Length]);
            }
        }
    }

}
