using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPlayer;

    [SerializeField]
    private SpawnManager spawnManager;

    private bool isGameOver = true;

    private UIManager uimanager;



    // Start is called before the first frame update
    void Start()
    {
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        
        //enseñar el main menu hasta pulsar la barra espaciadora
        uimanager.setMainMenuVisibility(true);
        uimanager.setLifesVisibility(false);
        uimanager.setScoreVisibility(false);



    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pulse");
            if (isGameOver)
                CreateGame();
        }*/
        if (isGameOver && Input.GetKeyDown(KeyCode.Space)) {
            CreateGame();
        }
        /*if (prefabPlayer.IsDestroyed())
        {
            FinishGame();
        }*/

    }

    private void CreateGame() 
    { 
        //empieza el juego
        isGameOver = false;
        Debug.Log("Entre");
        //quitar el main menu
        if (uimanager != null)
        {
            uimanager.setMainMenuVisibility(false);
            uimanager.resetScore();
            uimanager.setLifesVisibility(true);
            uimanager.setScoreVisibility(true);
        }

        if (spawnManager != null)
            spawnManager.StartCoroutines();

        //crear el jugador y spawn manager

        Vector3 posicionPlayer = new Vector3(0, 0, 0);
        Instantiate(prefabPlayer, posicionPlayer, Quaternion.identity);


        /*Vector3 posicionSpawn = new Vector3(0, 0, 0);
        Instantiate(prefabSpawnManager, posicionSpawn, Quaternion.identity);*/

        //(prefabSpawnManager.GetComponent<SpawnManager>()).setSpawn(true);




    }


    public void FinishGame()
    {
        //terminar el juego
        isGameOver = true;

        uimanager.setMainMenuVisibility(true);
        uimanager.setLifesVisibility(false);
        uimanager.setScoreVisibility(false);
        //volver a enseñar el main menu

        if (spawnManager != null)
            spawnManager.StopCoroutines();

        //Destroy(prefabSpawnManager.gameObject);

        //(prefabSpawnManager.GetComponent<SpawnManager>()).setSpawn(false);

    }
}
