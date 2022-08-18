using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{
    [SerializeField] private Transform _bladePrefab;

    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _dragPos;
    [SerializeField] private Vector3 _bladeWorldPos;
    private Vector3 _bladeRot;

    [SerializeField] private float _offset;
    [SerializeField] private float _rotTime;
    [SerializeField] private float _posTime;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            _startPos = Input.mousePosition;
            _startPos.z = Camera.main.nearClipPlane + _offset;
        }
        
        if (Input.GetMouseButton(0) || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
        {
            _dragPos = Input.mousePosition;
            _dragPos.z = Camera.main.nearClipPlane + _offset;
            BladeMove();
            BladeRotate();
        }
    }

    private void BladeMove()
    {       
        _bladeWorldPos = Camera.main.ScreenToWorldPoint(_dragPos);

        transform.position = Vector3.Lerp(transform.position, _bladeWorldPos, _posTime);
    }

    private void BladeRotate()
    {
        _bladeRot = (_dragPos - _startPos) - transform.position;
        var _newBladeRot = Quaternion.Euler(0, _bladeRot.y * 3f, 0);

        transform.localRotation= Quaternion.Lerp(transform.localRotation, _newBladeRot, _rotTime * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LegHair")       
            other.gameObject.SetActive(false);
        
    }
}
