using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArmyOfOne
{
    public class UnitState : MonoBehaviour
    {
        public GameObject _HQ;
        protected string _UnitNameType;
        public enum _UnitState
        {
            Patrol,
            Attacking,
            Chasing,
            Hiding,
            Saved,
        };

        public _UnitState _UnitBehaviour = _UnitState.Patrol;

        public Transform _pointTarget;
        Vector3 _NextTarget; // Waypooint
        Transform _TargetPosition;

        public string UnitType
        {
            get { return _UnitNameType; }
            set { _UnitNameType = value; }
        }

        #region Unit AI
        void Patrol() // Randomly Wander Around
        {
            /// methods called after the target is found
            Vector3 _RandomPoint = _pointTarget.GetComponent<PatrollingArea>().RandomPoint();
            _RandomPoint.y = 0;
            _RandomPoint += transform.position;
            _NextTarget = _RandomPoint;
        }

        void Attack()
        {
            _NextTarget = _TargetPosition.position;
            Debug.Log("Enemy Detected");
        }

        public Vector3 ReturnNextTarget()
        {
            switch (_UnitBehaviour)
            {
                case _UnitState.Patrol:
                    Patrol();
                    break;
                case _UnitState.Chasing:
                    Chase();
                    break;
                case _UnitState.Attacking:
                    Attack();
                    break;
                case _UnitState.Hiding:
                    Go_ToHQ();
                    break;
            }

            return _NextTarget;
        }

        private void Go_ToHQ()
        {
            _NextTarget = _HQ.transform.position;
        }

        private void Chase()
        {
            _NextTarget = _TargetPosition.position;
        }

        public void NextState(int state)
        {
            _UnitBehaviour = (_UnitState)state;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Target")
            {
                if (UnitType == "Soldier" || UnitType == "Turret")
                {
                    _UnitBehaviour = _UnitState.Attacking;
                }
                else if (UnitType == "Civilian" && _UnitBehaviour != _UnitState.Hiding)
                {
                    _UnitBehaviour = _UnitState.Chasing;
                }
                // set mark target that 
                // has entered the collider bounds
                _TargetPosition = other.transform;
            }
        } /// when enemies enters determinmed ares

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "Target" && _UnitBehaviour != _UnitState.Hiding)
            {
                _UnitBehaviour = _UnitState.Patrol;

            }
        }

        #endregion


    }
}

