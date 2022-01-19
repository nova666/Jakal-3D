using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ArmyOfOne
{
    public class Vehicle_Controller : MonoBehaviour
    {

        // GetInput
        // use input to move sphere
        // set car position to sphere
        float moveInput;
        float turnInput;

        public float fwdSpeed;
        public float revSpeed;
        public float turnSpeed;
        public Rigidbody sphereRB;

        public GameObject _Bullet;
        public Transform _MuzzlePosition;
        public Vehicle_Body _VehicleBody;

        // Start is called before the first frame update
        void Start()
        {
            _VehicleBody = GetComponent<Vehicle_Body>();
            sphereRB.transform.parent = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (_VehicleBody._IsAlive)
            {
                moveInput = Input.GetAxisRaw("Vertical");
                turnInput = Input.GetAxisRaw("Horizontal");

                if (Input.GetAxisRaw("Vertical") > 0)
                    fwdSpeed += 0.3f;

                if (Input.GetAxisRaw("Vertical") <= 0 && fwdSpeed > 50)
                {
                    fwdSpeed -= 0.5f;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject bullet = Instantiate(_Bullet, _MuzzlePosition.position, _MuzzlePosition.rotation) as GameObject;
                }

                moveInput *= fwdSpeed > 0 ? fwdSpeed : revSpeed;

                transform.position = sphereRB.transform.position;

                float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
                transform.Rotate(xAngle: 0, yAngle: newRotation, zAngle: 0, relativeTo: Space.World);
            }
          
        }

        private void FixedUpdate()
        {
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        }
    }
}

