using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomaSoco : MonoBehaviour
{
    [SerializeField] GameObject jogador;
    [SerializeField] PhotonAnimatorView jogadorSprite;
    //[SerializeField] PhotonView jogadorView;
    [SerializeField] private CinemachineVirtualCamera cameraJogador;
    PhotonView photonView;

    Transform[] children;

    int contagemDano = 3;
    float tempoCorrido = 0f;
    float tempoFinal = 0f;

    bool invencivel = false;

    Color c;

    private void Start()
    {
        children = GetComponentsInChildren<Transform>();
        photonView = GetComponent<PhotonView>();

    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Soco")
        {
            if (photonView.IsMine)
            {
                GerenciaSom.Play("espinhoSoco");

                StartCoroutine("Inconsciente");
            }
           
        }
    }

    IEnumerator Inconsciente()
    {
        if (photonView.IsMine)
        {
            MovimentacaoJogador.velocMov = 0;
            MovimentacaoJogador.velocPulo = 0;

            Color c = children[2].GetComponent<SpriteRenderer>().material.color;

            foreach (var item in children)
            {
                if (item.GetComponent<SpriteRenderer>() != null)
                {
                    c.a = 0.5f;
                    item.GetComponent<SpriteRenderer>().material.color = c;
                }


            }
            yield return new WaitForSeconds(2f);

            foreach (var item in children)
            {
                if (item.GetComponent<SpriteRenderer>() != null)
                {
                    c.a = 1f;
                    item.GetComponent<SpriteRenderer>().material.color = c;
                }
            }

            MovimentacaoJogador.velocMov = 3;
            MovimentacaoJogador.velocPulo = 3;

            yield return new WaitForSeconds(4f);


            MovimentacaoJogador.velocMov = 7.5f;
            MovimentacaoJogador.velocPulo = 12f;

        }

    }
}
