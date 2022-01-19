using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ArmyOfOne
{
    public class Vehicle_Body : MonoBehaviour
    {
        public int _VehicleHealth = 100;
        public bool _IsAlive = true;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Projectile>())
            {
                if(_VehicleHealth > 0)
                {
                    _VehicleHealth -= 10;
                }
                else
                {
                    _IsAlive = false;
                    ExplodeVehicle();
                }
                
                Debug.Log("Been Shot !!!");
            }
           
        }

        private void ExplodeVehicle()
        {
            Debug.Log("BOOM");
        }
    }
}

