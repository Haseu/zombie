﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour, IDano
{
    public GameObject jogador;
    private MovimentoBehaviour movimento;
    private AnimacaoBehaviour animacao;
    private Status status;
    public AudioClip somMorte;
    private Vector3 patrulha;
    public int distancia = 10;
    private Vector3 direcao;
    private float contadorPatrulha;
    private float tempoEntrePatrulha = 12;
    private float porcentagemKitMedico = 0.1f;
    public GameObject kitMedico;
    private ControlaInterface controlaInterface;
    [HideInInspector]
    public GeradorZumbi gerador;
    public GameObject particulaSangueZumbi;

    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        this.spawnZumbi();
        movimento = GetComponent<MovimentoBehaviour>();
        animacao = GetComponent<AnimacaoBehaviour>();
        status = GetComponent<Status>();
        controlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    private void FixedUpdate() 
    {
        float distancia = Vector3.Distance(transform.position, jogador.transform.position);
        movimento.rotacionar(direcao);
        animacao.movimentar(direcao.magnitude);

        if(distancia > 15) 
        {
            this.vagar();    
        }
        else if (distancia > 2.5) 
        {
            this.perseguir();          
        }
        else 
        {
            this.atacar();           
        }
    }
    //Roaming
    private void vagar() 
    {
        contadorPatrulha -= Time.deltaTime;
        if (contadorPatrulha <= 0)
        {
            patrulha = this.aleatorizarPosicao();
            contadorPatrulha += tempoEntrePatrulha + Random.Range(-2f, 2f);
        }

        bool pertoDoPonto = Vector3.Distance(transform.position, patrulha) <= 0.05;
        
        if (!pertoDoPonto)
        {
            direcao = patrulha - transform.position;
            movimento.movimentar(direcao, status.velocidade); 
        }  
    }

    private void perseguir()
    {
        direcao = jogador.transform.position - transform.position;            
        movimento.movimentar(direcao, status.velocidade);   
        animacao.atacar(false);
    }

    private void atacar() 
    {
        direcao = jogador.transform.position - transform.position;
        animacao.atacar(true);
    }

    private Vector3 aleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distancia;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    public void atacaJogador()
    {
        int dano = Random.Range(1,30);
        jogador.GetComponent<ControlaJogador>().dano(dano);
    }   

    private void spawnZumbi()
    {
        int gerTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(gerTipoZumbi).gameObject.SetActive(true);
    }

    public void dano(int dano)
    {
        status.vida -= dano;
        if(status.vida <= 0 && status.vivo)
        {
            this.morrer();
        }
    }

    public void particulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(particulaSangueZumbi, posicao, rotacao);
    }

    public void morrer()
    {
        status.vivo = false;
        animacao.morrer();
        StartCoroutine(movimento.morrer());
        this.enabled = false;
        ControlaAudio.instancia.PlayOneShot(somMorte);
        this.spawnKitMedico(porcentagemKitMedico);
        controlaInterface.AtualizarQuantidadeZumbisMortos();
        gerador.diminuirQuantidadeZumbiTotal();
        Destroy(gameObject, 7);
    }

    private void spawnKitMedico(float porcentagem)
    {
        if(Random.value <= porcentagem)
        {
            Instantiate(kitMedico, transform.position, Quaternion.identity);
        }
    }
}
