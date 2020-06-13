using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfiguracaoNome : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cameraJogador = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(cameraJogador.transform);
        //var rot = transform.rotation.eulerAngles;
        //rot.x = 0;
        //rot.y -= 180;
        //rot.z = 0;
        //transform.rotation = Quaternion.Euler(rot);
    }
}
