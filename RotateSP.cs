using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSP : MonoBehaviour
{
    private GameObject player;

    void Start(){
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        var dirX = player.transform.position.x - transform.position.x;
        var dirY = player.transform.position.y - transform.position.y;
        var euler = transform.eulerAngles;
        if (transform.position.x <= player.transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            euler.z = -Mathf.Atan2(dirY, dirX) * Mathf.Rad2Deg ;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            euler.z = Mathf.Atan2(dirY, dirX) * Mathf.Rad2Deg - 180.0f;
        }
        
        transform.eulerAngles = euler;
    }   
}
