using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoGeracao;
    public float tempoEntreGeracao = 60;
    public GameObject chefe;
    private ControlaInterface controlaInterface;

    private void Start() {
        tempoGeracao = tempoEntreGeracao;
        controlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    private void Update() {
        if (Time.timeSinceLevelLoad > tempoEntreGeracao)
        {
            Instantiate(chefe, transform.position, Quaternion.identity);
            controlaInterface.mostraTextoChefe();
            tempoEntreGeracao = Time.timeSinceLevelLoad + tempoEntreGeracao;
        }
    }
}
