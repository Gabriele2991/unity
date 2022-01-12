using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject gameObjToFollow;

    //the camera position should be the same of the driver position
    
    //no void Start cause she only need to be yupdated

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = gameObjToFollow.transform.position + new Vector3(0,0,-10);   
    }
}
