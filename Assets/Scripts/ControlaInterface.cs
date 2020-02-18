using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador jogador;
    public Slider saude;

    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        saude.maxValue = jogador.status.vida;
        atualizarSaude();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void atualizarSaude() 
    {
        saude.value = jogador.status.vida;
    }
}
