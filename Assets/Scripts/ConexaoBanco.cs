using EncryptStringSample;
using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using FullSerializer;

public class ConexaoBanco : MonoBehaviour
{
    [SerializeField]
    TMP_InputField nome;

    [SerializeField]
    TMP_InputField senha;


    [SerializeField]
    TextMeshProUGUI textoErro;

    public static string nomeUsuario;
    public static string senhaUsuario;
    public static string pontuacaoUsuario;

    private bool valido = true;

    List<Usuario> listaUsuario = new List<Usuario>();
    private static fsSerializer serializer = new fsSerializer();

    public delegate void GetUsersCallback(Dictionary<string, Usuario> users);


    Usuario verificacao = new Usuario();

    string linkApi;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    public void AoEntrar()
    {
        if (!string.IsNullOrEmpty(nome.text) && !string.IsNullOrEmpty(senha.text))
        {
            foreach (var item in nome.text)
            {
                if (item == ';')
                {
                    valido = false;
                    break;
                }
            }
            if (valido)
                StartCoroutine("Conecta");
            else
            {
                textoErro.text = "Caractere inválido.";

                valido = true;

            }
        }
        else
        {
            textoErro.text = "Algum campo inválido.";
        }

    }
    public void AoCadastrar()
    {
        if (!string.IsNullOrEmpty(nome.text) && !string.IsNullOrEmpty(senha.text))
        {
            foreach (var item in nome.text)
            {
                if (item == ';')
                {
                    valido = false;
                    break;
                }
            }
            if (valido)
                StartCoroutine("Cria");
            else
            {
                textoErro.text = "Caractere inválido.";

                valido = true;

            }

        }
        else
        {
            textoErro.text = "Algum campo inválido.";
        }
    }

    private void VerificaUsuario(Usuario usuario)
    {
        RestClient.Get<Usuario>(linkApi).Then(response => {

            Debug.Log(response.nomeUsuario);
            SetaUsuario(response);

        });
    }

    IEnumerator Conecta()
    {
        var usuario = new Usuario();
        usuario.nomeUsuario = nome.text;
        usuario.senhaUsuario = senha.text;

        nomeUsuario = nome.text;
        senhaUsuario = senha.text;



        linkApi = "https://versao-01.firebaseio.com/" + nome.text + ".json";

        VerificaUsuario(usuario);
        textoErro.text = "CARREGANDO...";

        yield return new WaitForSeconds(3f);

        if (string.IsNullOrEmpty(verificacao.nomeUsuario))
        {
            textoErro.text = "USUÁRIO NÃO EXISTE.";
            verificacao = new Usuario();

            //EnviaDados(usuario);
            //Debug.Log("Criou");
        }
        else if (verificacao.senhaUsuario == senhaUsuario)
        {
            Debug.Log("Já existe");
            verificacao = new Usuario();

            SceneManager.LoadScene("Menu");

        }
        else
        {

            textoErro.text = "SENHA INCORRETA.";
            verificacao = new Usuario();
        }
    }
    IEnumerator Cria()
    {
        var usuario = new Usuario();
        usuario.nomeUsuario = nome.text;
        usuario.senhaUsuario = senha.text;

        nomeUsuario = nome.text;
        senhaUsuario = senha.text;

        Debug.Log(usuario.nomeUsuario);

        linkApi = "https://versao-01.firebaseio.com/" + nome.text + ".json";

        VerificaUsuario(usuario);
        textoErro.text = "CARREGANDO...";

        yield return new WaitForSeconds(3f);

        if (string.IsNullOrEmpty(verificacao.nomeUsuario))
        {
            EnviaDados(usuario);
            textoErro.text = "USUÁRIO CRIADO.   ";

            verificacao = new Usuario();
        }
        else
        {
            textoErro.text = "USUÁRIO JÁ ESTÁ SENDO USADO.";
            verificacao = new Usuario();

        }
    }

    private void EnviaDados(Usuario usuario)
    {
        RestClient.Put(linkApi, usuario);
    }

    private void SetaUsuario(Usuario usuario)
    {
        verificacao = usuario;
    }


    
}
