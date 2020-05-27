using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Moedas : MonoBehaviour
{


    TextMeshProUGUI textoMoedas;
    PhotonView photonView;
    private int contMoeda = 0;

    // Start is called before the first frame update
    void Start()
    {

        textoMoedas = GameObject.FindGameObjectWithTag("TextoMoedas").GetComponent<TextMeshProUGUI>();
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "Moeda")
            {
                Debug.Log("Pegou a Moeda");
                contMoeda++;
                textoMoedas.text = (contMoeda).ToString();

                Destroy(other.gameObject);
            }
        }
       
    }
}
