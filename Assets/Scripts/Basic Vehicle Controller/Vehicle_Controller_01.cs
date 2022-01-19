using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArmyOfOne
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Vehicle_Input))]
    public class Vehicle_Controller_01 : MonoBehaviour
    {
        #region Variables
        [Header("Movement_Properties")]
        public float VehicleSpeed = 15;
        public float VehicleTurnSpeed = 20f;
        private Rigidbody rb;
        private Vehicle_Input input;

        // variables
        public GameObject Bullet;
        public Transform muzzle;

        #endregion
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            input = GetComponent<Vehicle_Input>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(rb && input)
            {
                HandleMovement();
                HandleShooting();
            }
        }

        private void HandleShooting()
        {
            if (input.Shoot)
            {
                GameObject bullet = Instantiate(Bullet, muzzle.position, muzzle.rotation) as GameObject;
            }
            
        }

        #region Custom Methods
        protected virtual void HandleMovement()
        {
            //Mova Vehicle Forward
            Vector3 wantedPosition = transform.position + (transform.forward * input.ForwardInput * VehicleSpeed * Time.deltaTime);
            rb.MovePosition(wantedPosition);

            //Rotate Vehicle 
            Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * (VehicleTurnSpeed * input.RotationInput * Time.deltaTime));
            rb.MoveRotation(wantedRotation);
        }
        #endregion
    }

}
