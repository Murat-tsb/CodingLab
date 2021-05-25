using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;



public class Hero : MonoBehaviour
{
    [Header("Health and mana")]
    public static float health = 100f;
    public Text healthText;
    public static float mana = 100f;
    public Text manaText;

    [Header("Movement")]
    public float runSpeed = 6f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    [Header("Panel")]
    public GameObject panel;
    public static float scoreTaken = 0;

    [Header("Weapon")]
    public List<GameObject> unlockedWeapon;
    public GameObject[] allWeapons;
    public Image weaponImage;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move();
        checkMH();
        dead();
        SwitchWeapon();
        
    }

    void move(){
        if (health > 0)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput.normalized * runSpeed;
        }
    }

    void checkMH(){
        healthText.text = health.ToString();
        manaText.text = mana.ToString();

        healthText.text = "Health: " + (float)Math.Round(health,0);
        manaText.text = "Mana: " + (float)Math.Round(mana, 0) + "/100";
        if(mana > 100f){
            mana = (float)Math.Round(mana, 0);
            mana = 100f;
        }
        if(health > 100f){
            health = (float)Math.Round(health, 0);
            health = 100f;
        }
    }

    void FixedUpdate()
    {

        if (health > 0f) {
            rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
            rotation();
        }
    }

    void rotation()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (this.transform.position.x < worldPos.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void dead()
    {
        if(health <= 0)
        {
            health = 0;
            panel.SetActive(true);
            scoreTaken = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Weapon")){
            for (int i = 0; i < allWeapons.Length; i++)
            {
                if(other.name == allWeapons[i].name){
                    unlockedWeapon.Add(allWeapons[i]);
                }
            }
            Destroy(other.gameObject);
        }
    }

    void SwitchWeapon(){
        if(Input.GetKeyDown(KeyCode.Q)){
        for (int i = 0; i < unlockedWeapon.Count; i++)
        {
            if(unlockedWeapon[i].activeInHierarchy){
                unlockedWeapon[i].SetActive(false);
                if(i != 0){
                    unlockedWeapon[i-1].SetActive(true);
                    weaponImage.sprite = unlockedWeapon[i-1].GetComponent<SpriteRenderer>().sprite;

                }
                else{
                    unlockedWeapon[unlockedWeapon.Count-1].SetActive(true);
                    weaponImage.sprite = unlockedWeapon[unlockedWeapon.Count-1].GetComponent<SpriteRenderer>().sprite;
                }
                // weaponImage.SetNativeSize();
                break;
            }
        }
        }
    }
}










