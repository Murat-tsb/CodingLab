using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public Rigidbody2D rb;
    public GameObject effect;
    public bool isSelfDestroyable;

    void Start()
    {
        rb.velocity = -transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(hitInfo.gameObject.tag == "Enemy")
        {
            GameObject effectObject = Instantiate(effect, transform.position, Quaternion.identity);
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            if(isSelfDestroyable){
                Destroy(effectObject);
            }
            else{
                Destroy(effectObject, 0.4f);
            }
        }
        if(hitInfo.gameObject.tag == "Wall")
        {
            GameObject effectObject = Instantiate(effect, transform.position, Quaternion.identity);
            if(isSelfDestroyable){
                Destroy(effectObject);
            }
            else{
                Destroy(effectObject, 0.4f);
            }
            
        }
    }
}