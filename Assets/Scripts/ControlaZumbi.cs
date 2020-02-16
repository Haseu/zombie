﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour
{
    public int velocidade = 5; 
    public GameObject jogador;


    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        int gerTipoZumbi = Random.Range(1,28);
        transform.GetChild(gerTipoZumbi).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        float distancia = Vector3.Distance(transform.position, jogador.transform.position);
        Vector3 direcao = jogador.transform.position - transform.position;

        Quaternion rotacao = Quaternion.LookRotation(direcao);
            GetComponent<Rigidbody>().MoveRotation(rotacao);

        if (distancia > 2.5) 
        {            
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (direcao.normalized * velocidade * Time.deltaTime));  
            GetComponent<Animator>().SetBool("atacando", false);                  
        }
        else 
        {
            GetComponent<Animator>().SetBool("atacando", true);
        }
    }

    public void atacaJogador()
    {
        Time.timeScale = 0;
        jogador.GetComponent<ControlaJogador>().gameOver.SetActive(true);
        jogador.GetComponent<ControlaJogador>().vida = false;
    }   
}
