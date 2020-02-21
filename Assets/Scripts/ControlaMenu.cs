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
        StartCoroutine(this.mudarCena("Game"));
    }    

    IEnumerator mudarCena(string name)
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(name);
    }

    public void sair()
    {
       StartCoroutine(this.fechar());
    }

    IEnumerator fechar()
    {
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
   
}
