﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IDano, ICura
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
            controlaInterface.gameOver();
    }

    public void cura(int quantidadeVida)
    {
        status.vida += quantidadeVida;
        if (status.vida > status.vidaInicial)
        {
            status.vida = status.vidaInicial;
        }
        controlaInterface.atualizarSaude();
    }
}
