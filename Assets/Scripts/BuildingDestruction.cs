using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestruction : MonoBehaviour
{


    #region Building Componenets
    Transform[] _BuildingComponents;
    public float _ExplosionForce;
    public float _Radius;
    public Transform _OriginOfExplosion;
    public Rigidbody rb;
   #endregion



    // Start is called before the first frame update
    void Start()
    {
        _BuildingComponents = GetComponentsInChildren<Transform>();
        //AssignColliders();
      
    }

    private void AssignRigidBody()
    {
        foreach (var item in _BuildingComponents)
        {
            if (item.tag == "Destroy")
            {
                item.gameObject.AddComponent<Rigidbody>();
            }           
        }
    }


    public void ExecuteExplosion()
    {
        AssignRigidBody();
        //rb.AddExplosionForce(_ExplosionForce, _OriginOfExplosion.position, _Radius);
    }
    // Update is called once per frame

}
