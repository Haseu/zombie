using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour
{
    public int speed = 5; 
    public GameObject jogador;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        

        float distancia = Vector3.Distance(transform.position, jogador.transform.position);

        if (distancia > 2.5) 
        {
            Vector3 direcao = jogador.transform.position - transform.position;
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (direcao.normalized * speed * Time.deltaTime));        

            Quaternion rotacao = Quaternion.LookRotation(direcao);
            GetComponent<Rigidbody>().MoveRotation(rotacao);
        }
    }   
}
