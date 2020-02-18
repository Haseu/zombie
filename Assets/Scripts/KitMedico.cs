using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private int quantidadeVida = 15;
    private int tempoDestruicao = 5;
    
    private void Start() {
        Destroy(gameObject, tempoDestruicao);
    }

    private void OnTriggerEnter(Collider other) 
    {
         if (other.tag == "Jogador")
         {
             other.GetComponent<ControlaJogador>().cura(quantidadeVida);
             Destroy(gameObject);
         }       
    }
}
