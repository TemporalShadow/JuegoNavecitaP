using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float speed=5;

    private UIManager uiManager;

    [SerializeField]
    private int ScorePositive = 10;

    [SerializeField]
    private int ScoreNegative = 50;

    [SerializeField]
    private GameObject prefabExplosionEnemy;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down  * speed  * Time.deltaTime);


        if(transform.position.y <-6){
            //SpawnEnemy();
            EnemyDeath();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ExplosionEnemy();
        switch (other.tag)
        {
            case "Player":
                //Da�ar al jugador
                Player miPlayer = other.GetComponent<Player>();
                if (miPlayer != null) { 
                    miPlayer.DamagePlayer();
                }
                if (uiManager != null)
                    uiManager.UpdateScore(-ScoreNegative);
                //SpawnEnemy(); 
                break;
            case "Laser":
                //Explosi�n y morici�n
                //SpawnEnemy();
                if(uiManager!=null)
                    uiManager.UpdateScore(ScorePositive);
                Destroy(other.gameObject);
                break;
        }
        EnemyDeath();
        
    }

    private void SpawnEnemy() {
        float randomX = Random.Range(-7, 7);
        transform.position = new Vector3(randomX, 7, 0);
    }

    private void ExplosionEnemy() {
        Instantiate(prefabExplosionEnemy,transform.position,Quaternion.identity);
        
    }

    private void EnemyDeath(){
        Destroy(this.gameObject);
    }

}
