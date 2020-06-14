using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingTabela : MonoBehaviour
{
    Transform containerTemplate;
    Transform template;
    string linkApi;
    List<Usuario> listaUsuario = new List<Usuario>();

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine("Conecta");


        StartCoroutine("CarregaTabela");
       
    }

    IEnumerator CarregaTabela()
    {
        yield return new WaitForSeconds(1.5f);

        containerTemplate = transform.Find("ContainerTemplate");
        template = containerTemplate.Find("Template");

        template.gameObject.SetActive(false);

        float alturaTemplate = 80f;

        int cont = listaUsuario.Where(p => !string.IsNullOrWhiteSpace(p.pontuacao) && !string.IsNullOrWhiteSpace(p.numeroVitorias)).ToList().Count <= 10 ? listaUsuario.Where(p => !string.IsNullOrWhiteSpace(p.pontuacao) || !string.IsNullOrWhiteSpace(p.numeroVitorias)).ToList().Count : 10;

        for (int i = 0; i < cont; i++)
        {
            Transform templateTransform = Instantiate(template, containerTemplate);
            RectTransform templateRectTransform = templateTransform.GetComponent<RectTransform>();
            templateRectTransform.anchoredPosition = new Vector2(0, -alturaTemplate * i);

            templateTransform.Find("TemplateNome").GetComponent<TextMeshProUGUI>().text = listaUsuario[i].nomeUsuario;
            templateTransform.Find("TemplateVitoria").GetComponent<TextMeshProUGUI>().text = !string.IsNullOrWhiteSpace(listaUsuario[i].numeroVitorias.ToString()) ? listaUsuario[i].numeroVitorias.ToString() : "0";
            templateTransform.Find("TemplatePontuacao").GetComponent<TextMeshProUGUI>().text = !string.IsNullOrWhiteSpace(listaUsuario[i].pontuacao.ToString()) ? listaUsuario[i].pontuacao.ToString() : "0";

            templateTransform.gameObject.SetActive(true);
        }
    }

    IEnumerator Conecta()
    {

        linkApi = "https://versao-01.firebaseio.com/.json";

        VerificaUsuario();

        yield return new WaitForSeconds(3f);

        
    }

    private void VerificaUsuario()
    {
        RestClient.Get(linkApi).Then(allUsers => {
            Debug.Log(allUsers.Text.ToString());
            if (allUsers != null)
            {
                string stringUsuarios = allUsers.Text.ToString();

                Splita(stringUsuarios);
            }

            else
                Debug.Log("Nulo");
        });
    }

    void Splita(string stringUsuarios)
    {
        string[] split1 = stringUsuarios.Replace("},", "@").Split('@');
        foreach (var item in split1)
        {
            var listaUsuarioAdd = new Usuario();

            var dadosUsuario = item.Replace(":{", "@").Split('@')[1].Split(',');

            listaUsuarioAdd.nomeUsuario = dadosUsuario[0].Split(':')[1].Replace('"', ' ').Replace("}", "");
            listaUsuarioAdd.numeroVitorias = !string.IsNullOrEmpty(dadosUsuario[1].Split(':')[1].Replace('"', ' ').Replace("}", "")) ? dadosUsuario[1].Split(':')[1].Replace('"', ' ').Replace("}", "") : "0";
            listaUsuarioAdd.pontuacao = !string.IsNullOrEmpty(dadosUsuario[2].Split(':')[1].Replace('"', ' ').Replace("}", "")) ? dadosUsuario[2].Split(':')[1].Replace('"', ' ').Replace("}", "") : "0";
            listaUsuarioAdd.senhaUsuario = dadosUsuario[3].Split(':')[1].Replace('"', ' ').Replace("}", "");

            listaUsuario.Add(listaUsuarioAdd);
        }
        listaUsuario = listaUsuario.OrderByDescending(p => p.numeroVitorias).ThenByDescending(p => p.pontuacao).ToList();
        Debug.Log(listaUsuario[6].pontuacao.ToString());
    }

    public void VoltaMenu()
    {
        SceneManager.LoadScene("Menu");

    }

}   
