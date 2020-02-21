using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int vidaInicial = 100;
    [HideInInspector]
    public int vida;
    public float velocidade = 5;
    public bool vivo = true;

    private void Awake() {
        vida = vidaInicial;
    }
}
