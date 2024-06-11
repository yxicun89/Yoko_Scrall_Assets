using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float maxHP = 5;
    float currentHP = 5;
    public Image hpImage;

    public float speed = 5;
    public float jumpPower = 5;
    int jumpCount;

    public AudioClip jumpSound;
    public AudioClip pickUpSound;//音ファイルを扱うときの変数

    AudioSource audioSource;
    Animator animator;

    bool leftMove;
    bool rightMove;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();//楽＆GetComponent重いから処理が速くなる
        animator = this.gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Animation();
    }

    void Move()
    {
        if (Input.GetKey("right") || rightMove == true)
        {
            this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("left") || leftMove == true)
        {
            this.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKeyDown("space")) //ジャンプの処理
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (jumpCount < 2)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
            audioSource.clip = jumpSound;
            audioSource.Play(); //Audionクリップの音を鳴らす
            jumpCount++;
        }
    }

    void Animation()
    {
        if (Input.GetKeyDown("right"))
        {
            animator.SetBool("WalkRight", true);
        }

        if (Input.GetKeyUp("right"))
        {
            animator.SetBool("WalkRight", false);
        }

        if (Input.GetKeyDown("left"))
        {
            animator.SetBool("WalkLeft", true);
        }

        if (Input.GetKeyUp("left"))
        {
            animator.SetBool("WalkLeft", false);
        }
    }

    void OnCollisionEnter(Collision col) //当たり判定の処理
    {
        if(col.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }
        if(col.gameObject.tag == "Coin")
        {
            audioSource.clip = pickUpSound;
            audioSource.Play();
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            currentHP -= 1;
            hpImage.fillAmount = currentHP / maxHP;
            Destroy(col.gameObject);
        }
    }
    
    public void LeftButtonDown()
    {
        leftMove = true;
        animator.SetBool("WalkLeft", true);
    }

    public void LeftButtonUP()
    {
        leftMove = false;
        animator.SetBool("WalkLeft", false);

    }

    public void RightButtonDown()
    {
        rightMove = true;
        animator.SetBool("WalkRight", true);
    }

    public void RightButtonUP()
    {
        rightMove = false;
        animator.SetBool("WalkRight", false);
    }



}
