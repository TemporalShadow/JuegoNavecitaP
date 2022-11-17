using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private bool isSinglePlayer=true;

    [SerializeField]
    private GameObject prefabPlayer;

    private SpawnManager spawnManager;

    private bool isGameOver = true;

    private UIManager uimanager;

    private GameObject p1;
    private GameObject p2;



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
        if(isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        /*if (prefabPlayer.IsDestroyed())
        {
            FinishGame();
        }*/

    }

    public bool IsSinglePlayerOn()
    {
        return isSinglePlayer;
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

        if (isSinglePlayer)
        {
            Vector3 posicionPlayer = new Vector3(0, 0, 0);
            p1 = Instantiate(prefabPlayer, posicionPlayer, Quaternion.identity);
        }
        else
        {
            Vector3 posicionPlayer = new Vector3(0, 0, 0);
            p1 = Instantiate(prefabPlayer, posicionPlayer, Quaternion.identity);
            SpriteRenderer rend1 = p1.GetComponent<SpriteRenderer>();
            rend1.color = Color.cyan;


            Vector3 posicionPlayer2 = new Vector3(4, 0, 0);
            p2 = Instantiate(prefabPlayer, posicionPlayer, Quaternion.identity);
            (p2.GetComponent<Player>()).isP1(false);
            SpriteRenderer rend2 = p1.GetComponent<SpriteRenderer>();
            rend1.color = Color.green;
        }

        



        /*Vector3 posicionSpawn = new Vector3(0, 0, 0);
        Instantiate(prefabSpawnManager, posicionSpawn, Quaternion.identity);*/

        //(prefabSpawnManager.GetComponent<SpawnManager>()).setSpawn(true);




    }

    public void playerDied() {
        p1.GetComponentInParent<Player>().PlayerDeath();
        if(!isSinglePlayer)
        p2.GetComponentInParent<Player>().PlayerDeath();

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
