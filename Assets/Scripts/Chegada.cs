using Photon.Pun;
using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chegada : MonoBehaviour
{
    // Start is called before the first frame update

    TextMeshProUGUI textoChegada;
    PhotonView photonView;
    TextMeshProUGUI textoMoedas;
    public int vitoria = 0;

    void Start()
    {
        textoChegada = GameObject.FindGameObjectWithTag("TextoChegada").GetComponent<TextMeshProUGUI>();

        textoMoedas = GameObject.FindGameObjectWithTag("TextoMoedas").GetComponent<TextMeshProUGUI>();

        photonView = GetComponent<PhotonView>();

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Chegada")
        {
            StartCoroutine("SalvarDados");
            if (photonView.IsMine)
            {
                vitoria = 1;

                StartCoroutine("FimDeJogoVencedor");

            }
            else
            {
                vitoria = 0;

                StartCoroutine("FimDeJogoPerdedor");
            }

          



        }
    }
    

    IEnumerator FimDeJogoVencedor()
    {
        textoChegada.text = "CHEGOU!!!";
        
        yield return new WaitForSeconds(3f);

        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.DestroyAll();


        Debug.Log("Chegou");
    }

    IEnumerator FimDeJogoPerdedor()
    {
        textoChegada.text = "ALGUEM CHEGOU PRIMEIRO!!!";

        yield return new WaitForSeconds(3f);

        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.DestroyAll();


        Debug.Log("Chegou");
    }

    IEnumerator SalvarDados()
    {
        var usuario = new Usuario();
        usuario.nomeUsuario = ConexaoBanco.nomeUsuario;
        usuario.senhaUsuario = ConexaoBanco.senhaUsuario;

        var linkApi = "https://versao-01.firebaseio.com/" + usuario.nomeUsuario + ".json";

        RestClient.Get<Usuario>(linkApi).Then(response => {

            usuario = response;
            Debug.Log(usuario.pontuacao);

        });

        yield return new WaitForSeconds(3f);

        if (string.IsNullOrEmpty(usuario.pontuacao))
            usuario.pontuacao = "0";

        usuario.pontuacao = (int.Parse(usuario.pontuacao) + int.Parse(textoMoedas.text)).ToString();

        if (string.IsNullOrEmpty(usuario.numeroVitorias))
            usuario.numeroVitorias = "0";

        usuario.numeroVitorias = (int.Parse(usuario.numeroVitorias) +  vitoria).ToString();

        Debug.Log(usuario.nomeUsuario);

        RestClient.Put(linkApi, usuario);


    }
}
