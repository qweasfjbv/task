using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rigid;

    public float jumpPower;
    public float maxSpeed;

    private bool isJumping;

    private void Awake()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        maxSpeed = 3.0f;
        jumpPower = 8.0f;
        isJumping = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");


        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * -1)
            rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("platform"));

        if (rayHit.collider != null)
        {
            if (rayHit.distance < 0.5f)
            {
                isJumping = false;
            }   
        }
    }
}
