using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private bool isPlayer2 = false;
    [SerializeField]private float speed;
    
    
    private Vector2 moveDir;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private RaycastHit2D hit;
    
    [SerializeField]private LayerMask whatIsWall;
    private Animator m_characterAnimator;



    private void Awake()
    {
        moveDir = Vector2.zero;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        m_characterAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        MyInput();
        hit = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), boxCollider2D.size, 0, new Vector2(0, moveDir.y), Mathf.Abs(moveDir.y * Time.deltaTime), LayerMask.GetMask("Entity", "Blocking"));

        if (hit.collider == null)
        {
            if(!GameManager.instance.playerDied)
            transform.Translate(0, moveDir.y * Time.deltaTime, 0);

        }
        hit = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), boxCollider2D.size, 0, new Vector2(moveDir.x, 0), Mathf.Abs(moveDir.x * Time.deltaTime), LayerMask.GetMask("Entity", "Blocking"));

        if (hit.collider == null)
        {
            if (!GameManager.instance.playerDied)
                transform.Translate(moveDir.x * Time.deltaTime, 0, 0);

        }
    }
    private void MyInput()
    {
        float x, y;
        if (isPlayer2)
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            x = Input.GetAxisRaw("Horizontal2");
            y = Input.GetAxisRaw("Vertical2");
        }

        moveDir = new Vector2(x, y).normalized * speed;
        ChangeAnim();
    }

    private void ChangeAnim()
    {
        Vector2 vel = moveDir;
        if (vel.x > 0)
        {
            vel.x = 1;
        }
        else if (vel.x < 0)
        {
            vel.x = -1;
        }
        else
        {
            vel.x = 0;
        }

        if (vel.y > 0)
        {
            vel.y = 1;
        }
        else if (vel.y < 0)
        {
            vel.y = -1;
        }
        else
        {
            vel.y = 0;
        }

        int toSet = 0;
        if (vel == Vector2.zero)
        {
            toSet = 0;
        }
        else if (vel == Vector2.up)
        {
            toSet = 2;
        }
        else if (vel == Vector2.down)
        {
            toSet = 5;

        }
        else if (vel == Vector2.left)
        {
            toSet = 7;
        }
        else if (vel == Vector2.right)
        {
            toSet = 8;
        }
        else if (vel.x > 0 && vel.y > 0)
        {
            toSet = 3;
        }
        else if (vel.x > 0 && vel.y < 0)
        {
            toSet = 4;
        }
        else if (vel.x < 0 && vel.y > 0)
        {
            toSet = 1;
        }
        else
        {
            toSet = 6;
        }
        m_characterAnimator.SetInteger("AnimIdx", toSet);
    }

    public void death()
    {
        Debug.Log("ded");
    }

}
