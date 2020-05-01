using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Photon.Pun;
using Photon.Realtime;
using System.Threading;

public class MenuPrincipal : MonoBehaviourPunCallbacks
{
    #region Iniciar e Sair
    public void IniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EntraSala(string sala)
    {
        Time.timeScale = 1f;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();

        }

        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinOrCreateRoom(sala, new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }
    }

    public void EscolherSala1()
    {
        EntraSala("Sala01");
    }

    public void EscolherSala2()
    {
        EntraSala("Sala02");

    }

    public void Sair()
    {
        Debug.Log("Saindo");
        Application.Quit();
    }
    #endregion

    #region Volume
    public AudioMixer audioMixer;

    public void AlterarVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    #endregion

    
}
