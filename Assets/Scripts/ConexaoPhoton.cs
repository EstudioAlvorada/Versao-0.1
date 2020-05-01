using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;


public class ConexaoPhoton : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Entrou Conecta");

        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings();

        Debug.Log("Está conectado: " + PhotonNetwork.IsConnected);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        Debug.Log("Está no Lobby");
    } 

    // Update is called once per frame
    //void Update()
    //{

    //}
}
