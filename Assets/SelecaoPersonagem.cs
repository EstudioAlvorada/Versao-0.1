using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecaoPersonagem : MonoBehaviour
{
    public static string personagem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EscolhaMarcao()
    {
        personagem = "Jogador";
        SceneManager.LoadScene("Salas");
    }

    public void EscolhaDiego()
    {
        personagem = "Jogador2";
        SceneManager.LoadScene("Salas");
    }
}
