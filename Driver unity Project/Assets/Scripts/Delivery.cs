using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    //private int packageCounter = 0; other way to do the packagePickup processing
    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);
    
    [SerializeField] float destroyDelay = 0.5f;
    private bool hasPackage = false;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Diocane");      
    //}

    void OnTriggerEnter2D(Collider2D collider)
    {



        if (collider.CompareTag("Package"))
        {
            if (hasPackage)
            {
                Debug.Log("Cannot pick up more than 1 package");
                return;
            }
            hasPackage = true;
            Debug.Log("Package picked up");
            spriteRenderer.color = hasPackageColor;
            Destroy(collider.gameObject, destroyDelay);
        }

        if (collider.CompareTag("Customer") && hasPackage)
        {
            //if(packageCounter != 0)
            //Debug.Log(packageCounter);
            Debug.Log("Package Delivered");
            hasPackage = false;
            spriteRenderer.color = noPackageColor;
        }
        else if(collider.CompareTag("Customer") && !hasPackage)
        {
            //Debug.Log(packageCounter);
            Debug.Log("You need to pick up a Package");
        }
    }
}
