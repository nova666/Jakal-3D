using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ArmyOfOne
{
    public class Passengers : MonoBehaviour
    {
        public GameObject _UnBoardingPosition; /// this is where the passengers will get off needs to be changed
        public GameObject _PassengerGraphic;
        public int _NumberOfAllowedPassengers;
        public int _NumberOfPassengers;
        public List<GameObject> _UnitsOnBoard = new List<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Civilian" && (other.GetComponentInParent<Unit>()._UnitBehaviour != Unit._UnitState.Hiding))
            {
                if (_NumberOfPassengers < _NumberOfAllowedPassengers)
                {
                    _NumberOfPassengers++;
                    BoardPassenger(other);
                    CheckPassenger(true);
                }
            }
            else
            {
                return;
            }

        }

        public void CheckPassenger(bool state)
        {
            if (state)
            {
                _PassengerGraphic.SetActive(state);
            }
            else
            {
                _PassengerGraphic.SetActive(false);
                _NumberOfPassengers--;
            }

        }

        public void BoardPassenger(Collider other)
        {
            if (other)
            {
                _UnitsOnBoard.Add(other.transform.root.gameObject);
                foreach (var item in _UnitsOnBoard)
                {
                    item.GetComponent<Unit>()._UnitBehaviour = Unit._UnitState.Hiding;
                }
                other.transform.root.gameObject.SetActive(false);

            }

        }

        public void UnBoardUnits()
        {
            foreach (var item in _UnitsOnBoard)
            {
                if (item.GetComponent<Unit>()._UnitBehaviour == Unit._UnitState.Hiding)
                {
                    item.transform.position = _UnBoardingPosition.transform.position;
                    item.transform.root.gameObject.SetActive(true);
                    CheckPassenger(false);
                    item.GetComponent<Unit>().ChangeBehaviour();
                    //_UnitsOnBoard.Clear();
                }
            }

        }
    }
}

