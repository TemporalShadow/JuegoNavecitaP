using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] livesImages;


    public Image livesDisplayImage;

    public Text scoreCambiante;
    
    private int currentScore = 0;


    [SerializeField]
    private Image MainMenu;

    public void UpgradeLifes(int currentLifes=1)
    {
        //current lives
        Debug.Log(currentLifes);
        if(currentLifes<=livesImages.Length && currentLifes>=0)
            livesDisplayImage.sprite = livesImages[currentLifes];
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
        livesDisplayImage.gameObject.SetActive(visibilidad);
    }

    public void setScoreVisibility(bool visibilidad)
    {
        scoreCambiante.gameObject.SetActive(visibilidad);
    }
}
