using System;
using Car;
using UnityEngine;

public class DriftSmoke : MonoBehaviour
{
    [SerializeField] private ArcadeyCarController _carController;
    [SerializeField] private ParticleSystem _driftSmokeA;
    [SerializeField] private ParticleSystem _driftSmokeB;
    [SerializeField] private Rigidbody _carRigidBody;

    private void Start()
    {
        _carController.onDrift += EnableDriftSmoke;
        _driftSmokeA.Play();
        _driftSmokeB.Play();
        DisableDriftSmoke();
    }

    private void EnableDriftSmoke(bool isdrifting)
    {
        if (isdrifting && _carRigidBody.linearVelocity.magnitude > 30f)
        {
            _driftSmokeA.enableEmission = true;
            _driftSmokeB.enableEmission = true;
        }
        else
        {
            DisableDriftSmoke();
        }
    }

    private void DisableDriftSmoke()
    {
        _driftSmokeA.enableEmission = false;
        _driftSmokeB.enableEmission = false;
    }
}
