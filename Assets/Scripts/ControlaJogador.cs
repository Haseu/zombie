using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour, IDano
{
    private Vector3 direcao;
    public LayerMask mascaraChao;
    public GameObject gameOver;
    public ControlaInterface controlaInterface;
    public AudioClip somDano;
    private PlayerBehaviour playerBehaviour;
    private AnimacaoBehaviour animacaoBehaviour;
    public Status status;

    private void Start() {
        Time.timeScale = 1;
        playerBehaviour = GetComponent<PlayerBehaviour>();
        animacaoBehaviour = GetComponent<AnimacaoBehaviour>();
        status = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);       

        animacaoBehaviour.movimentar(direcao.magnitude);

        if (status.vida <= 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("Motel");
            }
        }
    }

    private void FixedUpdate() 
    {
        playerBehaviour.movimentar(direcao, status.velocidade);
        playerBehaviour.rotacao(mascaraChao);
    }

    public void dano(int dano)
    {
        status.vida -= dano;
        controlaInterface.atualizarSaude();
        ControlaAudio.instancia.PlayOneShot(somDano);

        if(status.vida <= 0) 
        {
           this.morrer();
        }
    }

    public void morrer()
    {
            Time.timeScale = 0;
            gameOver.SetActive(true);
    }
}
