using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        NavMeshAgent agente = gameObject.GetComponent<NavMeshAgent>();
        if(agente)
        {
            agente.isStopped = true;
        }
        rigidbody.isKinematic = false;
        rigidbody.detectCollisions = false;
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = false;
        yield return new WaitForSeconds(2f);        
        GetComponent<Collider>().enabled = false;
        rigidbody.useGravity = true;
    }
}
