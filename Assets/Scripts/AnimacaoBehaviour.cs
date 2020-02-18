using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoBehaviour : MonoBehaviour
{
private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void atacar(bool estado)
    {
        animator.SetBool("atacando", estado);  
    }

    public void movimentar(float valorMovimento)
    {
         animator.SetFloat("Movendo", valorMovimento);
    }

}
