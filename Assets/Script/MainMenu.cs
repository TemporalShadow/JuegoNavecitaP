using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadSinglePlayerGame()
    {
        Debug.Log("SinglePlayer is Loading...");

        //aqui va el codigo para transportarnos a la escena de singleplayer
        SceneManager.LoadScene("Single");
    }

    public void LoadMultiPlayerGame() 
    {
        Debug.Log("MultiPlayer is Loading...");

        //aqui va el codigo para transportarnos a la escena de multiplayer
        SceneManager.LoadScene(2);
    }
}
