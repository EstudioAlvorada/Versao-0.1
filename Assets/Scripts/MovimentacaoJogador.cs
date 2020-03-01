using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoJogador : MonoBehaviour
{

    [SerializeField] private float mov;
    [SerializeField] private float velocMov = 0f;
    [SerializeField] private float velocPulo = 0f;
    [SerializeField] private GameObject checaChao;
    [SerializeField] private LayerMask camadaChao;
    bool noChao;

    [SerializeField] float velocQueda = 2.5f;
    [SerializeField] float multiplicadorPulinho = 2f;


    protected Joystick joystick;
    protected Joybutton joybutton;
    protected ButtonDash buttonDash;

    protected bool jump;
    Rigidbody2D corpo;

    // Start is called before the first frame update
    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();

        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
        buttonDash = FindObjectOfType<ButtonDash>();


    }

    // Update is called once per frame
    void Update()
    {
        noChao = Physics2D.Linecast(checaChao.transform.position, corpo.transform.position, camadaChao);

        mov = joystick.Horizontal;

        corpo.velocity = new Vector2(mov * velocMov, corpo.velocity.y);

        Jump();
        Dash();
    }

    void Jump()
    {
        if (!jump && joybutton.Pressed && noChao)
        {
            jump = true;
            corpo.velocity += Vector2.up * velocPulo;
        }

        if (jump && !joybutton.Pressed)
        {
            jump = false;
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
        if(buttonDash.Pressed)
        {
            StartCoroutine("tempoDash");
        }
    }

    IEnumerator tempoDash()
    {
        velocMov = 10;
        yield return new WaitForSeconds(.5f);
        velocMov = 6;

    }
}
