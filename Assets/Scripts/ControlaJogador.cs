using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public int speed = 10; 
    private Vector3 direcao;
    public LayerMask mascaraChao;
    public GameObject gameOver;
    public bool vida = true;

    private void Start() {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);       

        if (direcao != Vector3.zero)
        {
            GetComponent<Animator>().SetBool("Movendo", true);
        }   
        else
        {
            GetComponent<Animator>().SetBool("Movendo", false);
        }

        if (vida == false)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("Motel");
            }
        }
    }

    private void FixedUpdate() 
    {
         GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (direcao * speed * Time.deltaTime));    

         Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
         Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

         RaycastHit impacto;

         if (Physics.Raycast(raio, out impacto, 100, mascaraChao))
         {
            Vector3 posicaoMira = impacto.point - transform.position;
            posicaoMira.y = transform.position.y;

            Quaternion rotacao = Quaternion.LookRotation(posicaoMira);

            GetComponent<Rigidbody>().MoveRotation(rotacao);
         }


    }
}
