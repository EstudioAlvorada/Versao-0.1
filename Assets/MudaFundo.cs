using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MudaFundo : MonoBehaviour
{
    [SerializeField] Sprite fundoDia;
    [SerializeField] Sprite fundoNoite;
    Image fundo;
    // Start is called before the first frame update
    void Start()
    {
        fundo = GetComponent<Image>();

        if (MenuPrincipal.sala == 1)
            fundo.sprite = fundoDia;
        else if (MenuPrincipal.sala == 2)
            fundo.sprite = fundoNoite;


    }

    // Update is called once per frame
    
}
