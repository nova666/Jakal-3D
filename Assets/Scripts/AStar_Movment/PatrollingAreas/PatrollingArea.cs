using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ArmyOfOne
{
    public class PatrollingArea : MonoBehaviour
    {
        // This class is used to generate random point;

        public Vector3 RandomPoint()
        {
            return Random.insideUnitSphere * 5;
        }
    }
}

