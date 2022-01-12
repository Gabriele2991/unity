using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed;
    [SerializeField] float moveSpeed;
    private float steerAmount ;
    private float moveAmount ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        steerAmount = Input.GetAxis("Horizontal")*steerSpeed*Time.deltaTime;
        moveAmount = Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0); 
    }
}
