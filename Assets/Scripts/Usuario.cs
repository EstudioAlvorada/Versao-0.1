using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Usuario 
{
    public string nomeUsuario;
    public string senhaUsuario;
    public string pontuacao;
    public string numeroVitorias;
}

[SerializeField]
public class UsuarioJson
{
   
   public string Key;
   public Usuario usuario;

}
