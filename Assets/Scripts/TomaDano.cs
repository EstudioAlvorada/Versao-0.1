using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomaDano : MonoBehaviour
{
    [SerializeField] GameObject jogador;
    [SerializeField] SpriteRenderer jogadorSprite;
    //[SerializeField] PhotonView jogadorView;
    [SerializeField] private CinemachineVirtualCamera cameraJogador;
    PhotonView photonView;

    int contagemDano = 3;
    float tempoCorrido = 0f;
    float tempoFinal = 0f;

    bool invencivel = false;

    Color c;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        jogadorSprite = GetComponent<SpriteRenderer>();

        Debug.Log(jogadorSprite != null ? "NÃO NULO" : "NULO");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Espinhos")
        {
            if(!invencivel)
            {
                Debug.Log("Entrou");
                contagemDano--;
                StartCoroutine("Invencibilidade");
            }
            

            if(contagemDano == 0)
            {
                contagemDano = 3;
                Morre();
            }
        }
    }

    void Morre()
    {

        if (photonView.IsMine)
        {
            //PhotonNetwork.Destroy(jogador);

            jogador.transform.position = new Vector2(Random.Range(-42.49f, -23.87f), Random.Range(8.15f, 4.1f));


            //var player = (GameObject)PhotonNetwork.Instantiate("Jogador", new Vector2(Random.Range(-8f, 8f), transform.position.y), Quaternion.identity);
            //cameraJogador.Follow = player.transform;
        }
       
    }

    IEnumerator Invencibilidade()
    {
        invencivel = true;
       
        c.a = 0.5f;
        jogadorSprite.material.color = c;

        Debug.Log(jogadorSprite.material.color);

        yield return new WaitForSeconds(2f);
        c.a = 1f;
        jogadorSprite.material.color = c;
       
        invencivel = false;
    }
}

      