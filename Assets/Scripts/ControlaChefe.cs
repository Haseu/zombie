using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlaChefe : MonoBehaviour, IDano
{
    private Transform jogador;
    private NavMeshAgent agente;
    private Status status;
    private AnimacaoBehaviour animacao;
    private MovimentoBehaviour movimento;
    public GameObject kitMedico;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador").transform;
        agente = GetComponent<NavMeshAgent>();
        status = GetComponent<Status>();
        animacao = GetComponent<AnimacaoBehaviour>();
        movimento = GetComponent<MovimentoBehaviour>();
        agente.speed = status.velocidade;
    }

    private void Update() 
    {
        agente.SetDestination(jogador.position);
        animacao.movimentar(agente.velocity.magnitude);

        if (agente.hasPath) {
            bool proximidadeJogador = agente.remainingDistance <= agente.stoppingDistance;

            if (proximidadeJogador)
            {
                animacao.atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                movimento.rotacionar(direcao);
            }
            else
            {
                animacao.atacar(false);
            }
        }
    }

    public void atacaJogador()
    {
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControlaJogador>().dano(dano);
    }

    public void dano(int dano)
    {
       status.vida -= dano;
       if (status.vida <= 0 && status.vivo) 
       {
           this.morrer();
       }
    }

    public void morrer()
    {
        status.vivo = false;
        animacao.morrer();
        movimento.morrer();
        this.enabled = false;
        agente.enabled = false;
        Destroy(gameObject, 4);
        Instantiate(kitMedico, transform.position, Quaternion.identity);
    }
}
