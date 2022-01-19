using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    float Speed = 500f;

    private void Awake()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * Speed);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
