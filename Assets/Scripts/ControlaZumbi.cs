using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour
{
    public int velocidade = 5; 
    public GameObject jogador;
    private Rigidbody cRigidbody;
    private Animator cAnimator;


    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        int gerTipoZumbi = Random.Range(1,28);
        transform.GetChild(gerTipoZumbi).gameObject.SetActive(true);
        cRigidbody = GetComponent<Rigidbody>();
        cAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate() 
    {
        float distancia = Vector3.Distance(transform.position, jogador.transform.position);
        Vector3 direcao = jogador.transform.position - transform.position;

        Quaternion rotacao = Quaternion.LookRotation(direcao);
            cRigidbody.MoveRotation(rotacao);

        if (distancia > 2.5) 
        {            
            cRigidbody.MovePosition(cRigidbody.position + (direcao.normalized * velocidade * Time.deltaTime));  
            cAnimator.SetBool("atacando", false);                  
        }
        else 
        {
           cAnimator.SetBool("atacando", true);
        }
    }

    public void atacaJogador()
    {
        Time.timeScale = 0;
        jogador.GetComponent<ControlaJogador>().gameOver.SetActive(true);
        jogador.GetComponent<ControlaJogador>().vida = false;
    }   
}
