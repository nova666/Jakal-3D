using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArmyOfOne
{
    public class EnemyBulding : MonoBehaviour
    {
        public HostagesManager _HostagesManager;
        public GameObject[] _Hostages;
        public float _BuildingWalls = 100;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Projectile>())
            {
                if (_BuildingWalls > 0)
                {
                    _BuildingWalls -= 10;
                    if (_BuildingWalls <= 0)
                    {
                        StartCoroutine(CollapseBuilding());
                    }
                }
            }
        }

        IEnumerator CollapseBuilding()
        {
            Debug.Log("Releasing Hostages");
            GetComponent<BuildingDestruction>().ExecuteExplosion();

            yield return new WaitForSeconds(3f);

            _HostagesManager.OnReleaseHostages(_Hostages);
        }
    }
}

