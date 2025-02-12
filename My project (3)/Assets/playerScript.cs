using UnityEngine;

public class playerScript : MonoBehaviour
{
    public Animator animator;//parametro per le animazioni del personaggio

    private float movement;
    public float moveSpeed = 5f;
    private bool direzione=true;//se il parametro e true il giocatore sta andando verso destra

    public Rigidbody2D rb;
    public float jumpHeight = 5f;// valore per poter controlare da console il valore del salto
    private bool isGround=true; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        if (movement < 0f && direzione)
        {
            transform.eulerAngles = new Vector3 (0f,-180f,0f);
            direzione = false;

        }else if(movement > 0f && !direzione)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            direzione = true;

        }

        if (Input.GetKey(KeyCode.Space) && isGround)// quando viene rilevato il pulsante del salto
        {
            jump();
            isGround= false;   
            animator.SetBool("jump",true);// fa partire l'animazione del salto
        }
        
        if (Mathf.Abs(movement) > 0.1f)//movement puo essere -1 o 1 ci dice se andiamo a destra o sinistra la funzione mathf.Abs serve per rendere il valore positivo
        {
            animator.SetFloat("Run", 1f); //in caso ci stiamo muovendo facciamo partire l'animazione


        }
        else if (movement < 0.1f)
        {
            animator.SetFloat("Run", 0f);
        }

        if(Input.GetMouseButtonDown(0))//se il tasto sinistro viene premuto parte l'attaco
        {
            animator.SetTrigger("attack");

        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime*moveSpeed;

    }

    void jump()
    {
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGround= true;
            animator.SetBool("jump", false);// fa finire l'animazione del salto
        }
    }
}

