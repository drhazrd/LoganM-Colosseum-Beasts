using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartMenu : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

   public void GameStart(int sceneID){
        SceneManager.LoadScene(sceneID);
    }
}
