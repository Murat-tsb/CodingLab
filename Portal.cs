using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

    public class Portal : MonoBehaviour
    {
        
        public Transform spawnPoint;
        public GameObject enemy;
        private float timer = 1f;

        public GameObject portal;
        public int scoreToActive;

        void Start(){
            
        }

        void FixedUpdate()
        {
            PortalSwitchOn();   
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                RandomEnemy();
                timer = 7;
            }
        }

        void RandomEnemy()
        {
            // Random x = new Random();
            // int n = x.Next(1,3);
            // if (n == 1)
            // {
                if (scoreToActive <= Hero.scoreTaken){
                Instantiate(enemy, spawnPoint.position, transform.rotation);
                timer = 3;
                }
            // }
            // if (n == 2)
            // {
            //     Instantiate(enemy2, spawnPoint.position, transform.rotation);
            //     timer = 3;
            // }
        }

        void PortalSwitchOn(){
            if (scoreToActive > Hero.scoreTaken){
            GetComponent<SpriteRenderer>().enabled = false;
            }
            if (scoreToActive <= Hero.scoreTaken){
            GetComponent<SpriteRenderer>().enabled = true;
            }
        }

    }