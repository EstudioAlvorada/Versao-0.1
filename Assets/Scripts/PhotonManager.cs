using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private CinemachineVirtualCamera cameraJogador = null;
    //[SerializeField] private CinemachineVirtualCamera cameraRespawn = null;


    // Start is called before the first frame update
    //void Start()
    //{
    //    if (PhotonNetwork.IsConnected)
    //        PhotonNetwork.Disconnect();
    //    PhotonNetwork.ConnectUsingSettings();
    //}

    //public override void OnConnectedToMaster()
    //{
    //    PhotonNetwork.JoinLobby();
    //}

    //public override void OnJoinedLobby()
    //{
    //    PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    //}

    public override void OnJoinedRoom()
    {
        var player = PhotonNetwork.Instantiate("Jogador", new Vector2(-32.96581f, 13.20467f), Quaternion.identity);
        cameraJogador.Follow = player.transform;
        player.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        //cameraJogador.LookAt = player.transform;
    }
}