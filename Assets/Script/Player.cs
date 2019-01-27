using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 1;
    bool isDie = false;
    int health = 1;
    
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;
  
    private bool facingRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int extraJumps;
    public int extraJumpsValue;

    public bool inputLeft = false;
    public bool inputRight = false;
    public bool inputJump = false;



    // Use this for initialization
    void Start()
    {
        
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        UIButtonManager ui = GameObject.FindGameObjectWithTag("Managers").GetComponent<UIButtonManager>();
        ui.Init();
    }
    void FixedUpdate()
    {
        Move();
        if (health == 0)
            return;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

      
    }
    void Update()
    {
        
        if (health==0)
        {
            if (!isDie)
                Die();

            return;
        }
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
            
        }
        if (inputJump && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            inputJump = false;
            Debug.Log("위에이프");
        }
        else if (inputJump && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            inputJump = false;
            Debug.Log("밑에이프");
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;


            if (inputLeft)
            {
                moveVelocity = Vector3.left;

                transform.localScale = new Vector3(-1, 1, 1);


            }
            else if (inputRight)
            {
                moveVelocity = Vector3.right;

                transform.localScale = new Vector3(1, 1, 1);


            }
            transform.position += moveVelocity * speed * Time.deltaTime;
    }
    
        void OnTriggerEnter2D(Collider2D other)
    {
        
        //일정속도로 몬스터를 밟게되면
        if (other.gameObject.tag == "Creature"&& !other.isTrigger&&rb.velocity.y<-10f)
        {
            
            Monster creature = other.gameObject.GetComponent<Monster>();
            //몬스터의 Die함수 호출 
            creature.Die();

            //바운스
            Vector2 killVelocity = new Vector2(0, 30f);
            rb.AddForce(killVelocity, ForceMode2D.Impulse);

            //스코어 매니저에 몬스터의 점수 저장
           

        }
        else if (other.gameObject.tag == "Creature" && !other.isTrigger && !(rb.velocity.y < -10f))
        {
           
            health=0;
            
        }
        if(other.gameObject.tag=="Bottom")
        {
            health = 0;
        }
        

        if (other.gameObject.tag == "End")
        {
            other.enabled = false;
            GameManager.EndGame();
            
        }
    }

    void Die()
    {
        isDie = true;
        rb.velocity = Vector2.zero;

        BoxCollider2D[] colls = gameObject.GetComponents<BoxCollider2D>();
        colls[0].enabled = false;
        colls[1].enabled = false;

        Vector2 dieVelocity = new Vector2(0, 20f);
        rb.AddForce(dieVelocity, ForceMode2D.Impulse);

        Invoke("RestartStage", 2f);

    }

    void RestartStage()
    {
        GameManager.RestartStage();
    }
}