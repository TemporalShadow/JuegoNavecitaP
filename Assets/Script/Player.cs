using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField]
    private bool inmortal=false;

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

    [SerializeField]
    private int puntosDeDash = 5;

    //UI
    private UIManager uiManager;

    private bool dashDisponible=false;



    //OBJ
    [SerializeField]
    private GameObject shieldObj;

    [SerializeField]
    private GameObject HurtObj;

    public GameObject laserPrefab;
    public GameObject laserTriplePrefab;

    public float horizontalInput;
    public float verticalInput;

    private bool isPlayer1 = true;
    //sistema de vida

    private GameManager gameManager; 

    [SerializeField]
    private int MAX_LIVES = 3;
    private int currentLifes;

    private bool isSinglePlayerOn = true;

    // Start is called before the first frame update
    void Start() 
    {
        currentLifes = MAX_LIVES;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(gameManager != null)
            isSinglePlayerOn = gameManager.IsSinglePlayerOn();

        Debug.Log("Hola");
        Debug.Log(name);
        Debug.Log(transform.position);


        if (isSinglePlayerOn)
        {
            transform.position = new Vector3(0, 0, 0);
        }
        else
        {
            if(isPlayer1)
                transform.position = new Vector3(-3, 0, 0);
            else
                transform.position = new Vector3(3, 0, 0);
        }


        uiManager=GameObject.Find("Canvas").GetComponent<UIManager>();
        if(uiManager!=null)
            uiManager.UpgradeLifes(currentLifes,isPlayer1);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isSinglePlayerOn);
        if (isSinglePlayerOn) {
            if (Input.GetKeyDown(KeyCode.X) && dashDisponible)
            {
                Dashed();
            }
            else
            {
                movimiento();
            }
        }
        else {
            if (isPlayer1)
                movimientoP1();
            else
                movimientoP2();
        }

        

        shoot();
    }


    //movimiento single player - todo
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

    void movimientoP2()
    {
        //horizontal
        horizontalInput = Input.GetAxis("HorizontalP2");
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);

        //vertical
        verticalInput = Input.GetAxis("VerticalP2");
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

        //limite horizontal
        if (transform.position.y > 4.2f)
        {
            transform.position = new Vector3(transform.position.x, 4.2f, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //transpaso vertical
        if (transform.position.x > 8.0f)
        {
            transform.position = new Vector3(-8.0f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.0f)
        {
            transform.position = new Vector3(8.0f, transform.position.y, 0);
        }
    }

    void movimientoP1()
    {
        //horizontal
        horizontalInput = Input.GetAxis("HorizontalP1");
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);

        //vertical
        verticalInput = Input.GetAxis("VerticalP1");
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

        //limite horizontal
        if (transform.position.y > 4.2f)
        {
            transform.position = new Vector3(transform.position.x, 4.2f, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //transpaso vertical
        if (transform.position.x > 8.0f)
        {
            transform.position = new Vector3(-8.0f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.0f)
        {
            transform.position = new Vector3(8.0f, transform.position.y, 0);
        }
    }

    public void isP1(bool b)
    {   
        isPlayer1 = b;
    }
    void shoot(){
        if(Input.GetKeyDown(KeyCode.Space) && isPlayer1)
        {
            //Instantiate(laserPrefab,transform.position,Quaternion.identity);
            if(canTriple)
            Instantiate(laserTriplePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            else
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        }
        else if(Input.GetMouseButtonDown(0) && (!isPlayer1)){
            if (canTriple)
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
        //Debug.Log("Shield On");
        shieldObj.SetActive(true);
        //StartCoroutine(ShieldOff());
    }

    public void ShieldOff(){
        //Debug.Log("Shield Off");
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
        //Debug.Log("Triple off");
        canTripleOff();
    }

    IEnumerator SpeedOff()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        //Debug.Log("Speed off");
        SpeedUpOff();
    }


    //SISTEMA DE VIDA
    public void DamagePlayer() {
        //choca con enemigo o laser
        if(shieldActive==true){
            ShieldOff();
        }
        else{
            if (!inmortal)
            {
                currentLifes--;
                if (currentLifes < 0)
                {
                    GameManager gmanager = GameObject.Find("GameManager").GetComponent<GameManager>();
                    if (gmanager != null)
                    {
                        gmanager.FinishGame();
                        gmanager.playerDied();
                    }
                }
                else
                    DamageSpaceship();
            }
        }
        if(currentLifes==0){
            HurtObj.SetActive(true);
        }
    }

    private void DamageSpaceship()
    {
        //Debug.Log("The quedan " + currentLifes + " vidas.");
        if(uiManager!=null)
            uiManager.UpgradeLifes(currentLifes,isPlayer1);
    }

    public void PlayerDeath() {
        //hemos muerto
        //Debug.Log("Moriste.");
        Instantiate(prefabPlayerExplotion,transform.position,Quaternion.identity);
        

        Destroy(this.gameObject);

    }

    public bool lifeUp() {
        currentLifes++;
        if (currentLifes > MAX_LIVES)
        {
            currentLifes--;
            return false;
        }
        else {
            //Debug.Log("The quedan " + currentLifes + " vidas.");
            if (uiManager != null)
                uiManager.UpgradeLifes(currentLifes, isPlayer1);
        }
        return true;
    }

    public void DashOn()
    {
        Debug.Log("DashOn");
        dashDisponible = true;
    }

    public void DashOff()
    {
        Debug.Log("Dashoff");
        dashDisponible = false;
        StartCoroutine(DashedOff());
    }

    IEnumerator DashedOff()
    {
        //Debug.Log("Entre");
        yield return new WaitForSeconds(1);
        inmortal = false;
        this.gameObject.GetComponent<TrailRenderer>().enabled = false;
        //Debug.Log("Sali");
    }

    public void Dashed()
    {
        inmortal = true;
        this.gameObject.GetComponent<TrailRenderer>().enabled=true;
        

        //horizontal
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
       // transform.Translate(new Vector3(transform.position.x +(horizontalInput *puntosDeDash),transform.position.y +(puntosDeDash*verticalInput),transform.position.z));
        Debug.Log("Antes: "+transform.position); 
        transform.Translate(new Vector3(horizontalInput *puntosDeDash,verticalInput*puntosDeDash ,0));
        Debug.Log("Despues: "+transform.position); 


        //vertical


        

        DashOff();
    }








}
