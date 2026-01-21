using UnityEngine;
using UnityEngine.InputSystem;

namespace Car
{
    public class ArcadeyCarController : CarController
    {
        [SerializeField] private float mgripDuringLateralMovement = 1.0f;
        [SerializeField] private float mgripDuringSidewaysMovement = 4.0f;
        void Update()
        {
            base.Update();
        }

        void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}

