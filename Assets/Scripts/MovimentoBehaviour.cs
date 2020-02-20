using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBehaviour : MonoBehaviour
{
    private Rigidbody rigidbody;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void movimentar(Vector3 direcao, float velocidade)
    {
        rigidbody.MovePosition(rigidbody.position + (direcao.normalized * velocidade * Time.deltaTime));  
    }

    public void rotacionar(Vector3 direcao)
    {
        Quaternion rotacao = Quaternion.LookRotation(direcao);
            rigidbody.MoveRotation(rotacao);
    }

    public IEnumerator morrer()
    {
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(1.5f);
        GetComponent<Collider>().enabled = false;
    }
}
