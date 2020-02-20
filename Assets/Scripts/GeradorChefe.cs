using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoGeracao;
    public float tempoEntreGeracao = 60;
    public GameObject chefe;

    private void Start() {
        tempoGeracao = tempoEntreGeracao;
    }

    private void Update() {
        if (Time.timeSinceLevelLoad > tempoEntreGeracao)
        {
            Instantiate(chefe, transform.position, Quaternion.identity);
            tempoEntreGeracao = Time.timeSinceLevelLoad + tempoEntreGeracao;
        }
    }
}
