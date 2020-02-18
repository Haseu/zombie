using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour
{
    public GameObject jogador;
    private MovimentoBehaviour movimento;
    private AnimacaoBehaviour animacao;
    private Status status;


    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        this.spawnZumbi();
        movimento = GetComponent<MovimentoBehaviour>();
        animacao = GetComponent<AnimacaoBehaviour>();
        status = GetComponent<Status>();
    }

    private void FixedUpdate() 
    {
        float distancia = Vector3.Distance(transform.position, jogador.transform.position);
        Vector3 direcao = jogador.transform.position - transform.position;

        movimento.rotacionar(direcao);

        if (distancia > 2.5) 
        {            
            movimento.movimentar(direcao, status.velocidade);   
            animacao.atacar(false);             
        }
        else 
        {
           animacao.atacar(true);
        }
    }

    public void atacaJogador()
    {
        int dano = Random.Range(1,30);
        jogador.GetComponent<ControlaJogador>().causaDano(dano);
    }   

    private void spawnZumbi()
    {
        int gerTipoZumbi = Random.Range(1,28);
        transform.GetChild(gerTipoZumbi).gameObject.SetActive(true);
    }
}
