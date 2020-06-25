using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciaSom : MonoBehaviour
{
    public static AudioClip socoSom, moedaSom, espinhoSom, puloSom;

    public static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        socoSom = Resources.Load<AudioClip>("Som de soco (online-audio-converter.com)");
        moedaSom = Resources.Load<AudioClip>("moedaSom");
        espinhoSom = Resources.Load<AudioClip>("espinhoSom");
        puloSom = Resources.Load<AudioClip>("puloSom");

        audioSource = GetComponent<AudioSource>();

        audioSource.volume = MenuPrincipal.volumePrincipal;

    }

    public static void Play(string som)
    {
        switch (som)
        {
            case "socoSom" :
                audioSource.PlayOneShot(socoSom);
                break;
            case "moedaSom" :
                audioSource.PlayOneShot(moedaSom);
                break;
            case "espinhoSom" :
                audioSource.PlayOneShot(espinhoSom);
                break;
            case "puloSom":
                audioSource.PlayOneShot(puloSom);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
