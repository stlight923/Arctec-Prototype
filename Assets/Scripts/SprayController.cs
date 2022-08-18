using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayController : MonoBehaviour
{
    [SerializeField] private Vector3 _dragPos;
    [SerializeField] private Vector3 _sprayWorldPos;
    [SerializeField] private float _posTime;
    [SerializeField] private float _offset;

    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Transform _particleHitPoint;

    private void Update()
    {
        SprayMove();
        ParticleShapeMove();

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)                   
            _particleSystem.Play();
        
        if (Input.GetMouseButtonUp(0) || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)        
            _particleSystem.Stop();
              
    }

    private void SprayMove()
    {
        _dragPos = Input.mousePosition;
        _dragPos.z = Camera.main.nearClipPlane + _offset;

        _sprayWorldPos = Camera.main.ScreenToWorldPoint(_dragPos);

        transform.position = Vector3.Lerp(transform.position, _sprayWorldPos, _posTime);
    }

    private void ParticleShapeMove()
    {
        ParticleSystem.ShapeModule _editableShape = _particleSystem.shape;
        _editableShape.position = _particleHitPoint.position;
    }
}
