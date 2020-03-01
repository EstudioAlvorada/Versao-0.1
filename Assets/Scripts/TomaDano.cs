using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomaDano : MonoBehaviour
{
    [SerializeField] GameObject jogador;
    //[SerializeField] PhotonView jogadorView;
    [SerializeField] private CinemachineVirtualCamera cameraJogador;
    PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
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

            jogador.transform.position = new Vector2(Random.Range(-42.49f, -23.87f), Random.Range(8.15f, 4.1f));

            //var player = (GameObject)PhotonNetwork.Instantiate("Jogador", new Vector2(Random.Range(-8f, 8f), transform.position.y), Quaternion.identity);
            //cameraJogador.Follow = player.transform;
        }
       
    }
}

      