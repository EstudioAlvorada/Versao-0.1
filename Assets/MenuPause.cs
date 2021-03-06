﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public static bool JogoPausado = false;

    public GameObject pausarMenu;
    GameObject jogador;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (JogoPausado)
            {
                Retomar();
            }
            else
            {
                Pausar();
            }
        }
    }

    void Retomar()
    {
        pausarMenu.SetActive(false);
        Time.timeScale = 1f;
        JogoPausado = false;
    }

    void Pausar()
    {
        pausarMenu.SetActive(true);
        Time.timeScale = 0f;
        JogoPausado = true;
    }

    public void IrParaMenu()
    {
        Time.timeScale = 1f;

        Photon.Pun.PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Menu");

    }

    public void Sair()
    {
        Debug.Log("Saindo");
        Application.Quit();
    }
}
