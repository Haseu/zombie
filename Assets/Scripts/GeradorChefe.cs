using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoGeracao;
    public float tempoEntreGeracao = 60;
    public GameObject chefe;
    private ControlaInterface controlaInterface;
    public Transform[] posicoesPossiveisGeracao;
    private Transform jogador;

    private void Start() {
        tempoGeracao = tempoEntreGeracao;
        controlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag("Jogador").transform;
    }

    private void Update() {
        if (Time.timeSinceLevelLoad > tempoEntreGeracao)
        {
            Vector3 posicaoCriacao = this.calcularPosicaoMaisDistanteJogador();
            Instantiate(chefe, posicaoCriacao, Quaternion.identity);
            controlaInterface.mostraTextoChefe();
            tempoEntreGeracao = Time.timeSinceLevelLoad + tempoEntreGeracao;
        }
    }

    private Vector3 calcularPosicaoMaisDistanteJogador()
    {
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;
        foreach (Transform posicao in posicoesPossiveisGeracao)
        {
            float distanciaEntreJogador = Vector3.Distance(posicao.position, jogador.position);
            if(distanciaEntreJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEntreJogador;
                posicaoDeMaiorDistancia = posicao.position;
            }
        }

        return posicaoDeMaiorDistancia;
    }
}
