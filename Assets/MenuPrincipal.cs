using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Photon.Pun;
using Photon.Realtime;
using System.Threading;
using TMPro;
using System.Linq;
using Proyecto26;

public class MenuPrincipal : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public TextMeshProUGUI campoNome;
    List<Usuario> listaUsuario = new List<Usuario>();
    string linkApi;


    string nome = "Não está logado.";
    

    private void Start()
    {
        //nome = ConexaoBanco.nomeUsuario;

        //if (string.IsNullOrEmpty(campoNome.text))
        //{
        //    campoNome.text = "Nome: " + nome;
        //}

        
    }

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

    public void Ranking()
    {
        SceneManager.LoadScene("Ranking");

    }



}
