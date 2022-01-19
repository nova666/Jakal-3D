using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    //public GameObject _Item;
    public int _numberofItems;
    public GameObject[] _AllItems;
    public Vector3 _ActionLimits = new Vector3(5, 5, 5);

    [Header("Item Settings")]
   // [Range(0.0f, 5.0f)]
  //  public float minSpeed;
  //  [Range(0.0f, 5.0f)]
   // public float maxSpeed;
    [Range(0.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        //_AllItems = new GameObject[_numberofItems];
        //for (int i = 0; i < _numberofItems; i++)
        //{
        //    Vector3 _pos = this.transform.position = new Vector3(Random.Range(-_ActionLimits.x, _ActionLimits.x),
        //        Random.Range(-_ActionLimits.y, _ActionLimits.y),
        //        Random.Range(-_ActionLimits.z, _ActionLimits.z));
        //    _AllItems[i] = (GameObject)Instantiate(_Item, _pos, Quaternion.identity);
        //    _AllItems[i].GetComponent<Flock>().myManager = this;
        //}
        
    }
}
