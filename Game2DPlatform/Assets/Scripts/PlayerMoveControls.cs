using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private GatherInput gatherInput;
    private Rigidbody2D rb;
    private Animator anim;

    private int direction = 1;

    public float rayLength;
    public LayerMask groundLayer;
    public Transform leftPoint;
    private bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorValeus();
    }

    private void FixedUpdate()
    {
        Move();
        JumpPlayer();
        CheckStatus();
    }

    private void Move()
    {
        Flip();
        rb.velocity = new Vector2(speed * gatherInput.valueX, rb.velocity.y);
    }

    private void JumpPlayer()
    {
        if (gatherInput.jumpInput)
        {
            if(grounded)
            {
                rb.velocity = new Vector2(gatherInput.valueX * speed, jumpForce);
            }
        }

        gatherInput.jumpInput = false;
    }

    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        if (leftCheckHit)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        SeeRays(leftCheckHit);
    }

    private void SeeRays(RaycastHit2D leftCheckHit)
    {
        Color color1 = leftCheckHit ? Color.red : Color.green;
        Debug.DrawRay(leftPoint.position, Vector2.down * rayLength, color1);
    }

    private void Flip()
    {
        if(gatherInput.valueX * direction < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            direction *= -1;
        }
    }

    private void SetAnimatorValeus()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
}
