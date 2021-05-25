using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Enemy : MonoBehaviour
{

    [Header("Points")] 
    public int scoreGiven;
    public int manaGiven;
    
    [Header("Characteristics")]
    public int health;
    public int enemyDamage;
    public float speed;
    public int ExplosionDamage;
        
    [Header("AttackRange")]
    public float attackRange;
    public float explosionRadius;

    [Header("gameObjects")]
    private GameObject player;
    public GameObject effect;
    public GameObject blowEffect;
    public GameObject bulletEnemy;
    public GameObject shotPoint;

    [Header("Reload")]
    public float reloadTime;
    public float StartReloadTime;

    [Header("Type")]
    public bool isRange;
    public bool isSuicide;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        if (Hero.health > 0)
        {
            if(isRange == true){
                moveToHeroRange();
            }
            else{
                moveToHero();    
            }
            
        }
        if(health > 0 && Hero.health > 0){
            if(isRange == true){
                AttackRange();
            }
            else if(isSuicide == true){
                AttackSuicide();
            }
            else{
               Attack(); 
            }
        }
        death();
        rotation();
    }

    public async Task TakeDamage(int damage)
    {
        health -= damage;
        if (health > 0)
        {
            GetComponent<Renderer>().material.color = Color.red;
            await Task.Delay(200);
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public async Task death()
    {
        if (health <= 0)
        {
            if (this.transform.position.x <= player.transform.position.x)
            {
                transform.localRotation = Quaternion.Euler(0, 180, -90);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, -90);
            }
            
            this.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Renderer>().material.color = Color.red;

            ScoreManager.score += scoreGiven;
            Hero.scoreTaken += scoreGiven;
            Hero.mana += manaGiven;
            if(isSuicide == true){
                explosion();
                Destroy(gameObject);
            }
            else{
                //await Task.Delay(700);
                Destroy(gameObject);
            }
        }
    }

    public async Task explosion(){
        float blowRadiusX = this.transform.position.x - player.transform.position.x;
        float blowRadiusY = this.transform.position.y - player.transform.position.y;
        GameObject effect = Instantiate(blowEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(effect, 0.4f);
        if((blowRadiusX >= -explosionRadius && blowRadiusX <= explosionRadius) && (blowRadiusY >= -explosionRadius && blowRadiusY <= explosionRadius)){
            Hero.health -= ExplosionDamage;
            player.GetComponent<Renderer>().material.color = Color.red;
            await Task.Delay(500);
            player.GetComponent<Renderer>().material.color = Color.white;
                
        }
        
        
    }

    void moveToHero()
    {
        if (health > 0)
        {
            float attackRangeX = this.transform.position.x - player.transform.position.x;
            float attackRangeY = this.transform.position.y - player.transform.position.y;
            if ((attackRangeX >= -attackRange && attackRangeX <= attackRange) && (attackRangeY >= -attackRange && attackRangeY <= attackRange))
            {
                
            }
            else{
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            
            
        }
    }

    void moveToHeroRange()
    {
        float attackRangeX = this.transform.position.x - player.transform.position.x;
        float attackRangeY = this.transform.position.y - player.transform.position.y;
        if (health > 0)
        {   
            if ((attackRangeX >= -attackRange && attackRangeX <= attackRange) && (attackRangeY >= -attackRange && attackRangeY <= attackRange)){
                
            }
            else{
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            
        }
    }

    void rotation(){
        if (health > 0)
        { 
            if (transform.position.x <= player.transform.position.x){
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    async Task Attack()
    {
        // 
        float attackRangeX = this.transform.position.x - player.transform.position.x;
        float attackRangeY = this.transform.position.y - player.transform.position.y;
        //if(Math.Sqrt(Math.Pow(attackRangeX,2) + Math.Pow(attackRangeY,2)) <= attackRange)
        if ((attackRangeX >= -attackRange && attackRangeX <= attackRange) && (attackRangeY >= -attackRange && attackRangeY <= attackRange))
        {
            if (reloadTime <= 0)
            {
                Hero.health -= enemyDamage;
                Instantiate(effect, player.transform.position, Quaternion.identity);
                reloadTime = StartReloadTime;
                player.GetComponent<Renderer>().material.color = Color.red;
                await Task.Delay(500);
                player.GetComponent<Renderer>().material.color = Color.white;
                
            }
            else
            {
                reloadTime -= Time.deltaTime;
            }
        }
    }



    async Task AttackRange()
    {
        float attackRangeX = this.transform.position.x - player.transform.position.x;
        float attackRangeY = this.transform.position.y - player.transform.position.y;

        if ((attackRangeX >= -attackRange && attackRangeX <= attackRange) && (attackRangeY >= -attackRange && attackRangeY <= attackRange))
        {
            if (reloadTime <= 0)
            {
                Instantiate(bulletEnemy, shotPoint.transform.position, shotPoint.transform.rotation);
                reloadTime = StartReloadTime;
            }
            else
            {
                reloadTime -= Time.deltaTime;
            }
        }
    }

    async Task AttackSuicide()
    {
        float attackRangeX = this.transform.position.x - player.transform.position.x;
        float attackRangeY = this.transform.position.y - player.transform.position.y;

        if ((attackRangeX >= -attackRange && attackRangeX <= attackRange) && (attackRangeY >= -attackRange && attackRangeY <= attackRange))
        {
                
                Instantiate(effect, player.transform.position, Quaternion.identity);
                explosion();
                Destroy(gameObject);
                player.GetComponent<Renderer>().material.color = Color.red;
                await Task.Delay(500);
                player.GetComponent<Renderer>().material.color = Color.white;
                
        }
    }

}
            
        // if (this.transform.position.x >= 3 + player.transform.position.x)
        //     transform.Translate(Vector2.left * speed * Time.deltaTime);
        //     transform.localRotation = Quaternion.Euler(0, 0, 0);
        // if (this.transform.position.x < player.transform.position.x)
        //     transform.localRotation = Quaternion.Euler(0, 180, 0);
        //     transform.Translate(Vector2.left * speed * Time.deltaTime);
        // if (this.transform.position.y > player.transform.position.y)
        //     transform.Translate(Vector2.down * speed * Time.deltaTime);
        // if (this.transform.position.y < player.transform.position.y)
        //     transform.Translate(Vector2.up * speed * Time.deltaTime);