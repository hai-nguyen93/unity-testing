using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    private Animator anim;
    public bool facingRight = true;
    
    // Jump variables
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    public float jumpPower = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Check coi có đang đứng trên đất ko
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Nếu đang ko attack
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Knight_attack"))
        {
            // Đi ngang
            float x_direction = Input.GetAxisRaw("Horizontal"); // lấy hướng x
            anim.SetFloat("xspeed", Mathf.Abs(x_direction)); // gắn giá trị vô cái parameter xspeed trong animator để check animation walk
            if (x_direction > 0 && !facingRight) flip(); // quay đầu nhân vật theo hướng di chuuyển
            if (x_direction < 0 && facingRight) flip();  // như trên
            transform.position = new Vector2(transform.position.x + x_direction * speed * Time.deltaTime, transform.position.y); // di chuyển


            anim.SetFloat("yspeed", Mathf.Abs(rb.velocity.y)); // gắn giá trị vô cái param yspeed trong animator để check animation jump
            // Jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // nếu đang ở trên đất thì nhảy dc
            {
                rb.AddForce(new Vector2(0, jumpPower));              
            }

            // Đánh
            if (Input.GetKeyDown(KeyCode.J) && isGrounded) // nếu đang đứng trên dưới đất thì wánh dc
            {
                anim.SetTrigger("attack"); // chuyển sang animation wánh, chưa làm hitbox j hết nên chỉ cho đẹp thôi wwww
            }
        }
    }

    // quay đầu nhân vật
    void flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * (-1), transform.localScale.y);
        facingRight = !facingRight;
    }
}
