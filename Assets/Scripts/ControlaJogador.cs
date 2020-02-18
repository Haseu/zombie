using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public int speed = 10; 
    private Vector3 direcao;
    public LayerMask mascaraChao;
    public GameObject gameOver;
    public int saude = 100;
    public ControlaInterface controlaInterface;
    public AudioClip somDano;
    private PlayerBehaviour playerBehaviour;
    private AnimacaoBehaviour animacaoBehaviour;

    private void Start() {
        Time.timeScale = 1;
        playerBehaviour = GetComponent<PlayerBehaviour>();
        animacaoBehaviour = GetComponent<AnimacaoBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);       

        animacaoBehaviour.movimentar(direcao.magnitude);

        if (saude <= 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("Motel");
            }
        }
    }

    private void FixedUpdate() 
    {
        playerBehaviour.movimentar(direcao, speed);
        playerBehaviour.rotacao(mascaraChao);
    }

    public void causaDano(int dano)
    {
        saude -= dano;
        controlaInterface.atualizarSaude();
        ControlaAudio.instancia.PlayOneShot(somDano);

        if(saude <= 0) 
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }
    }
}
