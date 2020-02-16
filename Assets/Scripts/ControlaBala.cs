using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaBala : MonoBehaviour
{
    public float velocidade = 20;
    private void FixedUpdate() {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (transform.forward * velocidade * Time.deltaTime));
    }
}
