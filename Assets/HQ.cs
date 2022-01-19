using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace ArmyOfOne
{
    public class HQ : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Passengers pa = other.GetComponent<Passengers>();
                if (pa._NumberOfPassengers > 0)
                {
                    //other.GetComponent<Passengers>().CheckPassenger(false);
                    other.GetComponent<Passengers>().UnBoardUnits();
                }
            }
        }

    }
}

