using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MovimentoBehaviour
{
   public void rotacao(LayerMask mascaraChao) 
   {
       Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
         Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

         RaycastHit impacto;

         if (Physics.Raycast(raio, out impacto, 100, mascaraChao))
         {
            Vector3 posicaoMira = impacto.point - transform.position;
            posicaoMira.y = transform.position.y;

           rotacionar(posicaoMira);
         }
   }
}
