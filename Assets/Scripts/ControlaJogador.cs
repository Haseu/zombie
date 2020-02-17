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
    private Rigidbody cRigidbody;
    private Animator cAnimator;
    public int saude = 100;
    public ControlaInterface controlaInterface;
    public AudioClip somDano;

    private void Start() {
        Time.timeScale = 1;
        cRigidbody = GetComponent<Rigidbody>();
        cAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);       

        if (direcao != Vector3.zero)
        {
            cAnimator.SetBool("Movendo", true);
        }   
        else
        {
            cAnimator.SetBool("Movendo", false);
        }

        if (saude <= 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("Motel");
            }
        }
    }

    private void FixedUpdate() 
    {
         cRigidbody.MovePosition(cRigidbody.position + (direcao * speed * Time.deltaTime));    

         Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
         Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

         RaycastHit impacto;

         if (Physics.Raycast(raio, out impacto, 100, mascaraChao))
         {
            Vector3 posicaoMira = impacto.point - transform.position;
            posicaoMira.y = transform.position.y;

            Quaternion rotacao = Quaternion.LookRotation(posicaoMira);

            cRigidbody.MoveRotation(rotacao);
         }


    }

    public void causaDano(int dano)
    {
        saude -= dano;
        controlaInterface.atualizarSaude();
        ControlaAudio.instancia.PlayOneShot(somDano);

        if(saude <= 0) 
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }
    }
}
