using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    //vars
    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private bool canTriple=false;

    [SerializeField]
    private bool shieldActive=false;

    //crear variable de speedon y guardar speed base y speed con boost
    
    [SerializeField]
    private GameObject prefabPlayerExplotion;


    //UI
    private UIManager uiManager;



    //OBJ
    [SerializeField]
    private GameObject shieldObj;

    [SerializeField]
    private GameObject HurtObj;

    public GameObject laserPrefab;
    public GameObject laserTriplePrefab;

    public float horizontalInput;
    public float verticalInput;

    //sistema de vida

    [SerializeField]
    private int MAX_LIVES = 3;
    private int currentLifes;

    // Start is called before the first frame update
    void Start()
    {
        currentLifes = MAX_LIVES;

        Debug.Log("Hola");
        Debug.Log(name);
        Debug.Log(transform.position);

        transform.position = new Vector3(0,0,0);


        uiManager=GameObject.Find("Canvas").GetComponent<UIManager>();
        if(uiManager!=null)
            uiManager.UpgradeLifes(currentLifes);

    }

    // Update is called once per frame
    void Update()
    {
        movimiento();

        shoot();
    }

    void movimiento (){
        //horizontal
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right  * speed * horizontalInput * Time.deltaTime);
        
        //vertical
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up  * speed * verticalInput * Time.deltaTime);

        //limite horizontal
        if(transform.position.y > 4.2f){
            transform.position = new Vector3(transform.position.x,4.2f,0);
        }else if(transform.position.y <-4.2f){
            transform.position = new Vector3(transform.position.x, -4.2f,0);
        }

        //transpaso vertical
        if(transform.position.x >8.0f){
            transform.position = new Vector3(-8.0f,transform.position.y,0);
        }else if(transform.position.x < -8.0f){
            transform.position = new Vector3(8.0f,transform.position.y,0);
        }
    }

    void shoot(){
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(laserPrefab,transform.position,Quaternion.identity);
            if(canTriple)
            Instantiate(laserTriplePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            else
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        }
        
    }

    public void CanTripleOn(){
        canTriple=true;
        StartCoroutine(TripleOff());
    }

    public void canTripleOff(){
        canTriple=false;
    }

    public void ShieldOn(){
        shieldActive=true;
        Debug.Log("Shield On");
        shieldObj.SetActive(true);
        //StartCoroutine(ShieldOff());
    }

    public void ShieldOff(){
        Debug.Log("Shield Off");
        shieldActive=false;
        shieldObj.SetActive(false);
    }

    public void SpeedUpOn(){
        speed=10;
        StartCoroutine(SpeedOff());

    }

    public void SpeedUpOff(){
        speed=5;
    }

    IEnumerator TripleOff()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        Debug.Log("Triple off");
        canTripleOff();
    }

    IEnumerator SpeedOff()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        Debug.Log("Speed off");
        SpeedUpOff();
    }


    //SISTEMA DE VIDA
    public void DamagePlayer() {
        //choca con enemigo o laser
        if(shieldActive==true){
            ShieldOff();
        }
        else{
            currentLifes--;
            if (currentLifes < 0)
                PlayerDeath();
            else
                DamageSpaceship();
        }
        if(currentLifes==0){
            HurtObj.SetActive(true);
        }
    }

    private void DamageSpaceship()
    {
        Debug.Log("The quedan " + currentLifes + " vidas.");
        if(uiManager!=null)
            uiManager.UpgradeLifes(currentLifes);
    }

    private void PlayerDeath() {
        //hemos muerto
        Debug.Log("Moriste.");
        Instantiate(prefabPlayerExplotion,transform.position,Quaternion.identity);
        
        GameManager gmanager=GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gmanager != null) { 
            gmanager.FinishGame();
        }

        Destroy(this.gameObject);

    }








}
