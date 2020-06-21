using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomaDano : MonoBehaviour
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
        photonView = GetComponent<PhotonView>();
        jogadorSprite = GetComponent<PhotonAnimatorView>();


        children = GetComponentsInChildren<Transform>();


        if (photonView.IsMine)
        {
            photonView.Owner.NickName = ConexaoBanco.nomeUsuario;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Espinhos")
        {           
            Morre();
        }
    }

    void Morre()
    {

        if (photonView.IsMine)
        {
            //PhotonNetwork.Destroy(jogador);

            if(MovimentacaoJogador.checkPoint)
                jogador.transform.position = new Vector2(165f, 17.29f);
            else
                jogador.transform.position = new Vector2(Random.Range(-42.49f, -23.87f), Random.Range(8.15f, 4.1f));

            //var player = (GameObject)PhotonNetwork.Instantiate("Jogador", new Vector2(Random.Range(-8f, 8f), transform.position.y), Quaternion.identity);
            //cameraJogador.Follow = player.transform;
        }
       
    }

    IEnumerator Invencibilidade()
    {
        if (photonView.IsMine)
        {
            invencivel = true;

            Color c = children[2].GetComponent<SpriteRenderer>().material.color;

            foreach (var item in children)
            {
                if(item.GetComponent<SpriteRenderer>() != null)
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


            invencivel = false;
        }
      
    }
}

      