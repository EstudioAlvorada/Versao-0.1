using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroiSomMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        var menuAudio = FindObjectsOfType<AudioSource>().Where(p => p.gameObject.name == "AudioManager").First();

        if (menuAudio != null)
            Destroy(menuAudio);
    }
}
