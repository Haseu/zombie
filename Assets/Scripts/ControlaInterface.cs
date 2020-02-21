using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador jogador;
    public Slider saude;
    public GameObject painelGameOver;
    public Text textoDadosGamePlay;
    public Text textoMelhorPontuacao;
    private float tempoMaximoDeJogo;
    private int quantidadeZumbiMortos;
    public Text textoQuantidadeZumbisMortos;
    public Text textoChefeAparece;

    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        saude.maxValue = jogador.status.vida;
        atualizarSaude();
        Time.timeScale = 1;
        tempoMaximoDeJogo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void atualizarSaude() 
    {
        saude.value = jogador.status.vida;
    }

    public void gameOver() 
    {
        painelGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int) (Time.timeSinceLevelLoad / 60);
        int segundos = (int) (Time.timeSinceLevelLoad % 60);
        textoDadosGamePlay.text = "Você sobreviveu por "+minutos+"min e "+segundos+"s";
        this.atualizarPontuacao(minutos, segundos);
    }

    public void reiniciar()
    {
        SceneManager.LoadScene("Game");
    }

    public void atualizarPontuacao(int min, int seg)
    {
        if (Time.timeSinceLevelLoad > tempoMaximoDeJogo)
        {
            tempoMaximoDeJogo = Time.timeSinceLevelLoad;
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoMaximoDeJogo);
        }

        min= (int)tempoMaximoDeJogo / 60;
        seg = (int)tempoMaximoDeJogo % 60;
        textoMelhorPontuacao.text = string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);
    }

    public void AtualizarQuantidadeZumbisMortos()
    {
        quantidadeZumbiMortos++;
        textoQuantidadeZumbisMortos.text = string.Format("x {0}", quantidadeZumbiMortos);
    }

    public void mostraTextoChefe()
    {
        StartCoroutine(removerTexto(2, textoChefeAparece));
    }

    
    IEnumerator removerTexto (float tempoRemocao, Text texto)
    {
        texto.gameObject.SetActive(true);
        Color corTexto = texto.color;
        corTexto.a = 1;
        texto.color = corTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;
        while(texto.color.a > 0)
        {
            contador += Time.deltaTime / tempoRemocao;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            texto.color = corTexto;
            if (texto.color.a <= 0)
            {
                texto.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
