using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BoatAnimation : MonoBehaviour
{
    [Header("Поля настройки отоброжения во ViewPort")]
    [SerializeField] Color _gizmosColor;
    [SerializeField] bool _showGizmos = false;
    [Header("Поля контроля скриптом")]
    [SerializeField] Vector3[] _positions;
    [SerializeField] Vector3 _targetPosition;
    [Range(0,1000)][SerializeField] float _rotateSpeed=0.05f;
    [Range(0,1000)][SerializeField] float _moveSpeed=0.5f;
    [SerializeField] bool _isMoved = true;
    private void Start()
    {
        gameObject.transform.position = _positions[0];
        _targetPosition = _positions[1];
        StartCoroutine("Move");
    }
    int _iterator = 0;
    private void Update()
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
        if(_showGizmos)
        for(int i = 0; i < _positions.Length; ++i)
        {
            var position = _positions[i];
            Gizmos.color = _gizmosColor;
            Gizmos.DrawSphere(position, 0.25f);
            Gizmos.DrawWireSphere(position, 3f);
            Gizmos.DrawLine(position, _positions[(i+1)%_positions.Length]);
        }
    }

}
