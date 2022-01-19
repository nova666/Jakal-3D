using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;
    public float speed;

    private void Update()
    {
        ApplyRules();
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    private void ApplyRules()
    {
        GameObject[] gos;
        gos = myManager._AllItems;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        //Vector3 goalPos = new Vector3; // this os the target

        float nDistance;
        int groupSize = 0;

        foreach (var item in gos)
        {
            if(item != this.gameObject)
            {
                nDistance = Vector3.Distance(item.transform.position, this.transform.position);
                if(nDistance <= myManager.neighbourDistance)
                {
                    vcentre += item.transform.position;
                    groupSize++;

                    if(nDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - item.transform.position);
                    }

                    Flock anotherFlock = item.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        //if(groupSize > 0)
        //{
        //    vcentre = vcentre / groupSize + (goalPos - this.transform.position);
        //    speed = gSpeed / groupSize;

        //    Vector3 direction = (vcentre + vavoid) - transform.position;
        //    if (direction != Vector3.zero)
        //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
        //            myManager.rotationSpeed * Time.deltaTime);
        //}

    }
}
