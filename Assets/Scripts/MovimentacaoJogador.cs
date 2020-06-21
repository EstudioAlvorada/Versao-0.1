using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimentacaoJogador : MonoBehaviour
{

    [SerializeField] private float mov;
    [SerializeField] public static float velocMov = 7.5f;
    [SerializeField] public static float velocPulo = 12f;
    [SerializeField] private GameObject checaChao;
    [SerializeField] private LayerMask camadaChao;
    bool noChao;
    private bool olhandoDireita = true;

    [SerializeField] float velocQueda = 2.5f;
    [SerializeField] float multiplicadorPulinho = 2f;

    [SerializeField]  private TextMeshProUGUI textoNome;
    [SerializeField]  private Canvas canvasNome;

    protected Joystick joystick;
    protected Joybutton joybutton;
    protected ButtonDash buttonDash;
    Animator animator;

    protected bool jump;
    protected bool dash;

    Rigidbody2D corpo;

    PhotonView photonView;

    public int nomeObjetoJogador = 0;

    public static bool checkPoint = false;

    [SerializeField] GameObject hitboxAtaque;

    public int qtdeSoco;


    // Start is called before the first frame update
    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();

        photonView = GetComponent<PhotonView>();
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
        buttonDash = FindObjectOfType<ButtonDash>();
        animator = GetComponent<Animator>();
        hitboxAtaque.SetActive(false);

        qtdeSoco = 5;

        nomeObjetoJogador += 1;

        gameObject.name = gameObject.name + nomeObjetoJogador.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        noChao = Physics2D.Linecast(checaChao.transform.position, corpo.transform.position, camadaChao);

        mov = joystick.Horizontal;

        corpo.velocity = new Vector2(mov * velocMov, corpo.velocity.y);

        if(corpo.velocity.x != 0f)
            animator.SetBool("VelocAndando", true);
        else
        {
            animator.SetBool("VelocAndando", false);
        }

        if (transform.position.x > 165f && !checkPoint)
            checkPoint = true;

        Jump();
        Dash();
        Inverte();

        //if(Chegada.acabouPartida == true)
        //{
        //    Photon.Pun.PhotonNetwork.LeaveRoom();
        //    SceneManager.LoadScene("Menu");
        //}
    }

    void Jump()
    {
        photonView.RPC("RPC_ContaJogador", RpcTarget.AllBufferedViaServer, nomeObjetoJogador);


        if (!jump && joybutton.Pressed && noChao)
        {
            jump = true;
            corpo.velocity += Vector2.up * velocPulo;
            animator.SetBool("Pulando", true);
        }

        if (jump && !joybutton.Pressed)
        {
            jump = false;
            animator.SetBool("Pulando", false);
        }

        if(corpo.velocity.y < 0)
        {
            corpo.velocity += Vector2.up * Physics2D.gravity.y * (velocQueda - 1) * Time.deltaTime;
        }
        else if(corpo.velocity.y > 0 && !jump)
        {
            corpo.velocity += Vector2.up * Physics2D.gravity.y * (multiplicadorPulinho - 1) * Time.deltaTime;

        }


    }
    [PunRPC]
    void RPC_ContaJogador(int syncContagem)
    {
        nomeObjetoJogador = syncContagem;
    }

    void Dash()
    {   
        if (buttonDash.Pressed)
        {
            
            
            animator.SetBool("Socando", true);

            if (photonView.IsMine)
            {

                photonView.RPC("RPC_Soco", RpcTarget.AllBufferedViaServer, hitboxAtaque.gameObject.GetPhotonView().ViewID);

            }

          
            

            //Debug.Log("Qtde Soco: " + qtdeSoco);
            //Debug.Log("Qtde Moeda: " + Moedas.contMoeda);

        }            

        
    }



    void Inverte()
    {
        if (mov > 0 && !olhandoDireita || mov < 0 && olhandoDireita)
        {
            olhandoDireita = !olhandoDireita;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            canvasNome.transform.localScale = escala;
            transform.localScale = escala;
        }
    }

    //IEnumerator tempoDash()
    //{
    //    velocMov = 20;
    //    corpo.velocity = new Vector2(corpo.velocity.x, 0f);
    //    yield return new WaitForSeconds(.2f);
    //    velocMov = 6;
    //    yield return new WaitForSeconds(.2f);
    //    dash = false;

    //}


    [PunRPC]
    void RPC_Soco(int idAtaque)
    {
        var ataqueHitBox = PhotonView.Find(idAtaque).gameObject;

        StartCoroutine(Soco(ataqueHitBox));
    }

    IEnumerator Soco(GameObject ataqueHitBox)
    {

        //var outroJogador = GameObject.FindGameObjectsWithTag("Player").Where(p => p.name == nomeJogador).First();

        //var ataqueHitbox = outroJogador.transform.Find("Ataque").gameObject;

        //Debug.Log(ataqueHitbox.name);


        ataqueHitBox.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        ataqueHitBox.SetActive(false);

        animator.SetBool("Socando", false);
      
    }

    public float GetVelocidade()
    {
        return this.corpo.velocity.x;
    }


   
}
