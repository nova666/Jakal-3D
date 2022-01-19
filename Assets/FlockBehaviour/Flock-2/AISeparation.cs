using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISeparation : MonoBehaviour
{

    public GameObject[] AI;
    public float Spacebetween = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in AI)
        {
            if(item != gameObject)
            {
                float distance = Vector3.Distance(item.transform.position, this.transform.position);
                if(distance <= Spacebetween)
                {
                    Vector3 direction = transform.position - item.transform.position;
                    transform.Translate(direction * Time.deltaTime);
                }
            }
        }
    }
}
