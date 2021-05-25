using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPoint;
    public float reloadTime = 1f;
    public float StartReloadTime;
    public int manacost;

    void FixedUpdate()
    {
        if (Hero.health > 0)
        {
            Rotate();
            Shot();
        }
    }

    void Shot()
    {   
        if(Hero.mana - manacost >= 0 || manacost == 0){
            if (reloadTime <= 0)
            {
               if (Input.GetMouseButton(0))
                {
                  Instantiate(bullet, shotPoint.position, transform.rotation);
                  Hero.mana -= manacost;
                  reloadTime = StartReloadTime;
                }
            }
            else
            {
            reloadTime -= Time.deltaTime;
            }
        }
    }

    void Rotate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, rotZ + 180);
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
            transform.rotation = Quaternion.Euler(180, 0, -rotZ + 180);
    }
}
