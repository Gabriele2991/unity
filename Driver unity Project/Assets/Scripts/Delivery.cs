using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    int packageCounter = 0;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Diocane");      
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Package"))
        {
            packageCounter++;
            Debug.Log(packageCounter);
            Debug.Log("Package taken");
        }

        if (collider.CompareTag("Customer"))
        {   
            if(packageCounter != 0)
            {
                Debug.Log(packageCounter);
                Debug.Log("Package Delivered");
            }
            else
            {
                Debug.Log(packageCounter);
                Debug.Log("You need to pick up a Package");
            }
        }
    }
}
