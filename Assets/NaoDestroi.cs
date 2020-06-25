using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NaoDestroi : MonoBehaviour
{
    List<AudioSource> menuAudio;

    [SerializeField]
    Slider volume;


    // Start is called before the first frame update
    private void Awake()
    {
        menuAudio = FindObjectsOfType<AudioSource>().Where(p => p.gameObject.name == "AudioManager").ToList();
        volume = FindObjectOfType<Slider>();
        if(menuAudio.Count() > 1)
        {
            Destroy(menuAudio.Last());
        }

        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update()
    {
        if(volume != null)
            menuAudio.FirstOrDefault().volume = volume.value;
    }
}
