using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimentacaoJogador : MonoBehaviour
{

    [SerializeField] private float mov;
    [SerializeField] private float velocMov = 0f;
    [SerializeField] private float velocPulo = 0f;
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

    public static bool checkPoint = false;





    // Start is called before the first frame update
    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();


        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
        buttonDash = FindObjectOfType<ButtonDash>();
        animator = GetComponent<Animator>();
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

    void Dash()
    {
        if(buttonDash.Pressed && !dash)
        {
            dash = true;
            StartCoroutine("tempoDash");
            
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

    IEnumerator tempoDash()
    {
        velocMov = 20;
        corpo.velocity = new Vector2(corpo.velocity.x, 0f);
        yield return new WaitForSeconds(.2f);
        velocMov = 6;
        yield return new WaitForSeconds(.2f);
        dash = false;

    }

    public float GetVelocidade()
    {
        return this.corpo.velocity.x;
    }

   
    
}
