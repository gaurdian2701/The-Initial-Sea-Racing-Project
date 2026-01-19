using System;
using System.Collections.Generic;
using System.Linq;
using Car;
using UnityEngine;
using UnityEngine.UIElements;

public class OutOfBoundsRetriever : MonoBehaviour
{
    [SerializeField]
    private BezierCurve _tracedPath;
    [SerializeField]
    private GameObject _car;
    [SerializeField]
    private int _amountOfCheckpoints = 1;
    [SerializeField]
    private int _checkpointInterval = 1;
    [SerializeField]
    private float _heightOffset = 8;
    [SerializeField] 
    private float _speed = 1.0f;
    private float _timer;
    private float _startTime;
    private bool _rewind = false;

    private bool _areVectorsClose(Vector3 a, Vector3 b, float tolerance = 1f)
    {
        return Vector3.Distance(a, b) <= tolerance;
    }

    private float _lerpTimer = 0;
    private float _distance;
    private int _counter;
    private float shitasstimer = 0;
    private bool shitassbool = true;
    
    public void Start()
    {
        
    }

    public void OutOfBounds()
    {
        _rewind = true;
        Debug.Log("OutOfBounds");
        /*
        for (int i = AmountOfCheckpoints; i < 0; i--)
        {
            while (TracedPath[i] != Car.transform.position)
            {
                Debug.Log("Rewind");
                Car.transform.position = Vector3.Lerp(Car.transform.position, TracedPath[i], 2);
            }
        }
        */

        Rigidbody rb = _car.GetComponent<Rigidbody>();
        
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        ResetRotation();
        
        BezierCurve.ControlPoint NewPosition = new BezierCurve.ControlPoint();
        NewPosition.m_vPosition = new Vector3(_car.transform.position.x, _car.transform.position.y + _heightOffset, _car.transform.position.z);
                
        _tracedPath.m_points.Add(NewPosition);
        
        _counter = _tracedPath.Points.Count()-1;
        //_distance = Vector3.Distance(_car.transform.position, _tracedPath.m_points[_counter].m_vPosition);
        _startTime = Time.time;
        
        _tracedPath.UpdateDistances();
        _distance = _tracedPath.TotalDistance;
    }

    private void InBounds()
    {
        Rigidbody rb = _car.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        
        _rewind = false;
    }

    private void ResetRotation()
    {
        _car.transform.rotation.SetFromToRotation(_car.transform.eulerAngles, Vector3.zero);
    }
    
    void Update()
    {
        
        shitasstimer += Time.deltaTime;
        if (!_rewind)
        {
            _timer += Time.deltaTime;
            if (_timer >= _checkpointInterval)
            {
                _timer = 0;
                if (_tracedPath.m_points.Count > _amountOfCheckpoints)
                {
                    _tracedPath.m_points.RemoveAt(0);
                }
                
                BezierCurve.ControlPoint NewPosition = new BezierCurve.ControlPoint();
                NewPosition.m_vPosition = new Vector3(_car.transform.position.x, _car.transform.position.y + _heightOffset, _car.transform.position.z);
                
                _tracedPath.m_points.Add(NewPosition);
            }

            if (shitasstimer >= 4)
            {
                if (shitassbool)
                {
                    shitassbool = false;
                    OutOfBounds();
                }
            }
        }
        
        else
        {
            _lerpTimer += Time.fixedDeltaTime * _speed;
            _distance = Mathf.Lerp(_tracedPath.TotalDistance, 0, _lerpTimer);
            Debug.LogError("Position on Track: " + _distance);
            //Debug.Log("Rewind");
            _car.transform.position = _tracedPath.GetPose(_distance).position;
            //Debug.Log(_tracedPath.GetPose(Mathf.Lerp(_tracedPath.TotalDistance, 0, Time.deltaTime * _speed)).position);
            /*
            float distanceCovered = (Time.time - _startTime) * _speed;
            float fractionOfDistance = distanceCovered / _distance;

            _car.transform.position = Vector3.Lerp(_car.transform.position, _tracedPath.m_points[_counter].m_vPosition, fractionOfDistance);
            if (_areVectorsClose(_car.transform.position, _tracedPath.m_points[_counter].m_vPosition))
            {
                Debug.Log("Reached Checkpoint " + _counter);
                _counter--;
                if (_counter < 0)
                {
                    Debug.Log("InBounds");
                    InBounds();
                }
            }
            */
        }
    }
}
