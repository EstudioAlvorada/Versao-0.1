using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Moedas : MonoBehaviour
{


    TextMeshProUGUI textoMoedas;
    PhotonView photonView;
    public static int contMoeda = 0;

    // Start is called before the first frame update
    void Start()
    {

        textoMoedas = GameObject.FindGameObjectWithTag("TextoMoedas").GetComponent<TextMeshProUGUI>();
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        textoMoedas.text = (contMoeda).ToString();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Moeda")
        {
            if(photonView.IsMine)
            {
                Debug.Log("Pegou a Moeda");
                contMoeda++;
                textoMoedas.text = (contMoeda).ToString();

                photonView.RPC("RPC_DestroiMoeda", RpcTarget.AllBufferedViaServer, other.gameObject.GetPhotonView().ViewID);

            }

        }

    }

    [PunRPC]
    void RPC_DestroiMoeda(int moedaId)
    {
        var moeda = PhotonView.Find(moedaId).gameObject;

        Destroy(moeda.gameObject);

    }
    

}
