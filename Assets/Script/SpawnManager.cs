using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float EnemySpawnTime=5;

    [SerializeField]
    private GameObject PUTriplePrefab;

    [SerializeField]
    private GameObject[] PoweUps;

//comentario
    [SerializeField]
    private float PUSpawnTime=5;

    private bool spawnOn = true;

    public bool funciona = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCoroutines()
    {
        StartCoroutine(EnemySpawnRutine());
        StartCoroutine(PUSpawnRoutine());
    }

    public void StopCoroutines() { 
        StopAllCoroutines();
    }

    private IEnumerator EnemySpawnRutine()
    {
        while(true){
            if (spawnOn)
            {
                // suspend execution for 5 seconds
                yield return new WaitForSeconds(EnemySpawnTime);

                float randomX = Random.Range(-7f, 7f);
                Vector3 posicionEnemigo = new Vector3(randomX, 7, 0);
                Instantiate(enemyPrefab, posicionEnemigo, Quaternion.identity);
            }
            else
                break;
         }
        
    }

    private IEnumerator PUSpawnRoutine()
    {
        while(true){
            if (spawnOn)
            {
                // suspend execution for 5 seconds
                yield return new WaitForSeconds(PUSpawnTime);

            float randomX = Random.Range(-7f, 7f);
            Vector3 posicionPU = new Vector3(randomX,7,0);

            //tipo PU
            int tipoPU = Random.Range(0, PoweUps.Length);
            //Debug.Log(tipoPU);
            Instantiate(PoweUps[tipoPU],posicionPU,Quaternion.identity);
            }
            else
                break;
        }
        
    }

    public void setSpawn(bool funcion) {
        spawnOn = funcion;
    }
    
}
