using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour
{
    public GameObject botaoSair;

    private void Start() 
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
            botaoSair.SetActive(true);
        #endif    
    }

    public void jogar()
    {
        SceneManager.LoadScene("Game");
    }    

    public void sair()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
   
}
