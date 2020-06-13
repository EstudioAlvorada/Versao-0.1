using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NomeJogador : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI textoNome;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera cameraFixa;
    Vector3 escala;
    // Start is called before the first frame update
    void Start()
    {
        textoNome.text = photonView.Owner.NickName;
    }


}
