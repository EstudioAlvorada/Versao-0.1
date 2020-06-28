using FullSerializer;
using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingTabela : MonoBehaviour
{
    Transform containerTemplate;
    Transform template;
    string linkApi;
    List<Usuario> listaUsuario = new List<Usuario>();
    public Dictionary<string, Usuario> usuarios;
    public fsSerializer serializer = new fsSerializer();


    // Start is called before the first frame update
    void Awake()
    {

        StartCoroutine("PegaListaUsuario");

        //StartCoroutine("CarregaTabela");

    }

    //IEnumerator CarregaTabela()
    //{
    //    yield return new WaitForSeconds(1.5f);

      
    //}

   

    public void VoltaMenu()
    {
        SceneManager.LoadScene("Menu");

    }

    IEnumerator PegaListaUsuario()
    {
        GetUsers();

        yield return new WaitForSeconds(2f);
        
    }

    public void GetUsers()
    {
        var linkApi = "https://versao-01.firebaseio.com/.json";
        Dictionary<string, Usuario> users = new Dictionary<string, Usuario>();

        RestClient.Get(linkApi).Then(response =>
        {
            var responseJson = response.Text;

            var data = fsJsonParser.Parse(responseJson);

            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, Usuario>), ref deserialized);

            users = deserialized as Dictionary<string, Usuario>;

            PopulaLista(users);

        });

    }

    public void PopulaLista(Dictionary<string, Usuario> users)
    {
        usuarios = users.OrderByDescending(p => p.Value.numeroVitorias).ThenBy(p => p.Value.pontuacao).ToDictionary(p => p.Key, p => p.Value);

        containerTemplate = transform.Find("ContainerTemplate");
        template = containerTemplate.Find("Template");

        template.gameObject.SetActive(false);

        float alturaTemplate = 80f;

        int cont = usuarios.Where(p => !string.IsNullOrWhiteSpace(p.Value.pontuacao) && !string.IsNullOrWhiteSpace(p.Value.numeroVitorias)).ToList().Count <= 10 ? usuarios.Where(p => !string.IsNullOrWhiteSpace(p.Value.pontuacao) || !string.IsNullOrWhiteSpace(p.Value.numeroVitorias)).ToList().Count : 10;

        for (int i = 0; i < cont; i++)
        {
            Transform templateTransform = Instantiate(template, containerTemplate);
            RectTransform templateRectTransform = templateTransform.GetComponent<RectTransform>();
            templateRectTransform.anchoredPosition = new Vector2(0, -alturaTemplate * i);

            templateTransform.Find("TemplateNome").GetComponent<TextMeshProUGUI>().text = usuarios.ElementAt(i).Value.nomeUsuario;
            templateTransform.Find("TemplateVitoria").GetComponent<TextMeshProUGUI>().text = !string.IsNullOrWhiteSpace(usuarios.ElementAt(i).Value.numeroVitorias.ToString()) ? usuarios.ElementAt(i).Value.numeroVitorias.ToString() : "0";
            templateTransform.Find("TemplatePontuacao").GetComponent<TextMeshProUGUI>().text = !string.IsNullOrWhiteSpace(usuarios.ElementAt(i).Value.pontuacao.ToString()) ? usuarios.ElementAt(i).Value.pontuacao.ToString() : "0";

            if (i <= 2)
                templateTransform.Find("TemplateRank").GetComponent<Image>().sprite = Resources.Load<Sprite>("Coroa" + i);
            else
                templateTransform.Find("TemplateRanktxt").GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();

            templateTransform.gameObject.SetActive(true);
        }
    }
}   
