using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbi : MonoBehaviour
{
    public GameObject zumbi;
    float contador = 0;
    public float tempoGerar = 1;
    private float distancia = 3;
    public LayerMask layerZumbi;
    private float distanciaDoJogadorParaGeracao = 20;
    private GameObject jogador;
    private int quantidadeMaximaZumbis = 2;
    private int quantidadeZumbisAtual;

    private void Start() {
        jogador = GameObject.FindWithTag("Jogador");
        for(int i = 0; i < quantidadeMaximaZumbis; i++)
        {
            StartCoroutine(this.gerarZumbi());
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool podeGerarZumbisPelaDistancia = Vector3.Distance(transform.position, jogador.transform.position) > distanciaDoJogadorParaGeracao;

        if (podeGerarZumbisPelaDistancia && quantidadeZumbisAtual < quantidadeMaximaZumbis)
        {
            contador += Time.deltaTime;

            if (contador >= tempoGerar) 
            {
            StartCoroutine(this.gerarZumbi());
            contador = 0;
            }
        }        
    }

    // retorno especifico do unity para evitar frezzing em um loop
    private IEnumerator gerarZumbi()
    {
        Vector3 posicaoCriacao = this.aleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoCriacao, 1, layerZumbi); 

        while (colisores.Length > 0)
        {
            posicaoCriacao = this.aleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoCriacao, 1, layerZumbi); 
            yield return null;
        }

        ControlaZumbi cZumbi = Instantiate(zumbi, posicaoCriacao, transform.rotation).GetComponent<ControlaZumbi>();
        cZumbi.gerador = this;
        quantidadeZumbisAtual++;
    }

    private Vector3 aleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distancia;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distancia);
    }

    public void diminuirQuantidadeZumbiTotal ()
    {
        quantidadeZumbisAtual--;
    }
}
