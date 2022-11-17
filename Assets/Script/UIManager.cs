using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] livesImages;


    public Image livesDisplayImageP1;
    public Image livesDisplayImageP2;

    public Text scoreCambiante;
    
    private int currentScore = 0;

    private bool isSinglePlayerOn = true;

    [SerializeField]
    private Image MainMenu;

    void Start()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager != null)
            isSinglePlayerOn = gameManager.IsSinglePlayerOn();
    }

    public void UpgradeLifes(int currentLifes,bool player)
    {
        //current lives
         Debug.Log(currentLifes);
        Debug.Log(player);
        if (player)
        {
            if (currentLifes <= livesImages.Length && currentLifes >= 0)
                livesDisplayImageP1.sprite = livesImages[currentLifes];
        }
        else
        {
            if (currentLifes <= livesImages.Length && currentLifes >= 0)
                livesDisplayImageP2.sprite = livesImages[currentLifes];
        }
    }


    public void UpdateScore(int puntos){
        if(currentScore+puntos<0)
            currentScore = 0;
        else
            currentScore+=puntos;

        scoreCambiante.text= currentScore.ToString();
    }

    public void resetScore(){
        currentScore=0;
        scoreCambiante.text= currentScore.ToString();
    }

    public void setMainMenuVisibility(bool visibilidad)
    {
        MainMenu.gameObject.SetActive(visibilidad);
    }

    public void setLifesVisibility(bool visibilidad)
    {
        
        livesDisplayImageP1.gameObject.SetActive(visibilidad);
        if (!isSinglePlayerOn)
            livesDisplayImageP2.gameObject.SetActive(visibilidad);
    }

    public void setScoreVisibility(bool visibilidad)
    {
        scoreCambiante.gameObject.SetActive(visibilidad);
    }
}
