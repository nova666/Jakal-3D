using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ArmyOfOne
{
    public class TurretGuarding : UnitState
    {
        // Start is called before the first frame update
        #region Properites
        //UnitState _TurretUnit;
        Vector3 _Target;
        public GameObject _Bullet;
        public Transform _Muzzle;
        public float _FiringRate;
        public float _RotatingSpeed = 0.5f;
        #endregion

        private void Awake()
        {
            UnitType = "Turret";
        }

        private void Start()
        {
            ChangeBehaviour();
        }

        public void ChangeBehaviour()
        {
            StopAllCoroutines();
            if (_UnitBehaviour == UnitState._UnitState.Patrol)
            {
                StartCoroutine(Patrol()); // --> looking for path // this random point on map
            }
            else if (_UnitBehaviour == UnitState._UnitState.Attacking)
            {
                // do something else
                // attack
                StartCoroutine(Shoot());
            }
        }

        IEnumerator Patrol()
        {
            Quaternion targetRotation = Quaternion.identity;
            Vector3 _TargetPosition = ReturnNextTarget();

            do
            {
                Debug.Log("do rotation");
                Vector3 targetDirection = (_TargetPosition - transform.position).normalized;
                targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _RotatingSpeed);

                if (Quaternion.Angle(transform.rotation, targetRotation) <= 0)
                {
                    _TargetPosition = ReturnNextTarget();
                    yield return new WaitForSeconds(1f);
                }
                yield return null;
            } while ((_UnitBehaviour == UnitState._UnitState.Patrol));

            ChangeBehaviour();
        }

        IEnumerator Shoot()
        {
            Quaternion targetRotation = Quaternion.identity;
            Vector3 _TargetPosition = ReturnNextTarget();

            do
            {
                Debug.Log("do rotation");
                Vector3 targetDirection = (_TargetPosition - transform.position).normalized;
                targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _RotatingSpeed);
                if (Quaternion.Angle(transform.rotation, targetRotation) <= 0)
                {
                    GameObject bullet = Instantiate(_Bullet, _Muzzle.position, _Muzzle.rotation) as GameObject; //3
                    yield return new WaitForSeconds(1f);
                    _TargetPosition = ReturnNextTarget();
                }
                yield return null;
            } while ((_UnitBehaviour == UnitState._UnitState.Attacking));

            Debug.Log(Quaternion.Angle(transform.rotation, targetRotation));
            ChangeBehaviour();
        }
    }
}

