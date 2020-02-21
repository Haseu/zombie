using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaBala : MonoBehaviour
{
    public float velocidade = 20;
    private Rigidbody cRigidbody;

    private void Start() {
        cRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        cRigidbody.MovePosition(cRigidbody.position + (transform.forward * velocidade * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other) 
    {
        Quaternion rotacaoOpostaBala = Quaternion.LookRotation(-transform.forward);
        switch(other.tag)
        {
            case "Inimigo":
                ControlaZumbi inimigo = other.GetComponent<ControlaZumbi>();
                inimigo.dano(1);
                inimigo.particulaSangue(transform.position, rotacaoOpostaBala);
                break;
            case "Chefe":
                ControlaChefe chefe = other.GetComponent<ControlaChefe>();
                chefe.dano(1);
                chefe.particulaSangue(transform.position, rotacaoOpostaBala);
                break;
        }

        Destroy(gameObject);
    }
}
