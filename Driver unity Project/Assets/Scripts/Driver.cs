using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{

    [SerializeField]float steerSpeed = 150;
    [SerializeField]float moveSpeed = 15f;
    [SerializeField]float boostSpeed = 25f;
    [SerializeField]float bumpSpeed = 5f;
    private float destroyDelay = 0.5f;

    // Update is called once per frame
    void Update()
    {   
        float steerAmount = Input.GetAxis("Horizontal")*steerSpeed* Time.deltaTime;
        
        float moveAmount = Input.GetAxis("Vertical")*moveSpeed* Time.deltaTime;
        
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveSpeed = bumpSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bump"))
        {
            Debug.Log("Bump hit");
            moveSpeed = bumpSpeed;
        }

        if(collision.CompareTag("Speed Up"))
        {
            Debug.Log("BOOOOOOSTED");
            moveSpeed = boostSpeed;
            Destroy(collision.gameObject, destroyDelay);
        }
    }
}
