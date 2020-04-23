using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuPrincipal : MonoBehaviour
{
    #region Iniciar e Sair
    public void IniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EscolherSala()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Sair()
    {
        Debug.Log("Saindo");
        Application.Quit();
    }
    #endregion

    #region Volume
    public AudioMixer audioMixer;

    public void AlterarVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    #endregion
}
