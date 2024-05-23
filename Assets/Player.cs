using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private GameInput gameinput;
    //public ParticleSystem DUST;

    [SerializeField] private Rigidbody2D rbPlayer;
    //[SerializeField] public Rigidbody2D ItemCheck;
    [SerializeField] private Animator ani;

    [SerializeField] public bool isLifepPlayer = true;
    [SerializeField] private float SpeedPlayer;
    [SerializeField] private float JumpForce;

    [SerializeField] private bool IsRightFace = true;
    [SerializeField] public bool isJumped = false;
    [SerializeField] public bool isMove = false;
    [SerializeField] public bool isOnGround = true;
    void Start()
    {
        gameinput = GetComponent<GameInput>();
        rbPlayer = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
       JumpCode();
    }
    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        if (isLifepPlayer == false) { return; } // kiểm tra player còn sống không
        #region move

        float moveInput = gameinput.HorizontalInput; // lấy inputd

        if (moveInput != 0)
        {
            //wallSound.Play();
            isMove = true;
            rbPlayer.velocity = new Vector2(moveInput * SpeedPlayer, rbPlayer.velocity.y);
        }
        else
        {
            //wallSound.Stop();
            isMove = false;
        }
        //if (isLifepPlayer == true && isMove == true && isOnGround == true)
        //{
        //    int directionDust = (int)transform.localScale.x;
        //    // lay rotation cua x
        //    Quaternion rotateDust = DUST.transform.localRotation;
        //    if (directionDust == 1)
        //    {
        //        rotateDust.y = 180;

        //    }
        //    else if (directionDust == -1)
        //    {
        //        rotateDust.y = 0;
        //    }
        //    DUST.transform.localRotation = rotateDust; // cap nhat
        //    DUST.Play();
        //}
        //ani.SetFloat("walk", Mathf.Abs(moveInput)); // lấy nimation từ input
        #endregion

        #region right face
        // A= -1, 0, D=1
        if (IsRightFace == true && moveInput == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            IsRightFace = false;
        }
        if (IsRightFace == false && moveInput == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            IsRightFace = true;
        }
        #endregion
    }


    private void JumpCode()
    {
        if (isLifepPlayer == false) { return; }
        if ((Input.GetKeyDown(KeyCode.Space) && isOnGround))
        {
            isJumped = true;
            Debug.Log("space true");

            rbPlayer.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);// lực nhảy của player
            Debug.Log("nhay");
            isOnGround = false; // Đặt biến isOnGround thành false khi jump
            //ataC.isAttack = false;
        }
        //ani.SetBool("jump", isJumped && isOnGround == false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region check ground
        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("check ground true");
            isOnGround = true; // Đặt biến isOnGround thành true khi va chạm với ground
        }
        else
        {
            Debug.Log("check ground false");
            isOnGround = false;
        }
        #endregion

    }

}
