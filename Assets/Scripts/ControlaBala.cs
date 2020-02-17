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
        if(other.tag == "Inimigo"){
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
