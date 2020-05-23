using Photon.Pun;
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


    void Start()
    {
        textoChegada = GameObject.FindGameObjectWithTag("TextoChegada").GetComponent<TextMeshProUGUI>();
        photonView = GetComponent<PhotonView>();

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Chegada")
        {
            if (photonView.IsMine)
            {
                StartCoroutine("FimDeJogoVencedor");

            }
            else
            {
                StartCoroutine("FimDeJogoPerdedor");
            }

        }
    }

    IEnumerator FimDeJogoVencedor()
    {
        textoChegada.text = "CHEGOU!!!";
        
        yield return new WaitForSeconds(3f);

        PhotonNetwork.DestroyAll();

        Debug.Log("Chegou");
    }

    IEnumerator FimDeJogoPerdedor()
    {
        textoChegada.text = "ALGUEM CHEGOU PRIMEIRO!!!";

        yield return new WaitForSeconds(3f);

        PhotonNetwork.DestroyAll();

        Debug.Log("Chegou");
    }
}
