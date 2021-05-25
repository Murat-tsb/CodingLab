using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class BulletEnemy : MonoBehaviour
{
    public float speed;
    public int damage;
    public Rigidbody2D rb;
    public GameObject effect;
    private GameObject player;

    void Start()
    {
        rb.velocity = -transform.right * speed;
        player = GameObject.FindWithTag("Player");
    }

    async Task OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Hero.health -= damage;
            GameObject effectBlow = Instantiate(effect, player.transform.position, Quaternion.identity);
            Destroy(effectBlow, 0.4f);
            player.GetComponent<Renderer>().material.color = Color.red;
            await Task.Delay(500);
            player.GetComponent<Renderer>().material.color = Color.white;
        }
        if(hitInfo.gameObject.tag == "Wall")
        {
            GameObject effectObject = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(effectObject, 0.4f);
            Destroy(gameObject);
        }
    }
}