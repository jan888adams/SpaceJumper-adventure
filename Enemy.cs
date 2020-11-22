using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float RunningSpeed = 1.5f;
    public int EnemyDamage = 10;
    private Rigidbody2D RigidBody;
    public bool facingRight = false;
    private Vector3 StartPosition;
    private AudioSource music;
    

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        StartPosition = this.transform.position;
        music = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
       StartPosition = this.transform.position;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentRunnigSpeed = RunningSpeed;

        if (facingRight)
        {
            currentRunnigSpeed = RunningSpeed;
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            currentRunnigSpeed = -RunningSpeed;
            this.transform.eulerAngles = Vector3.zero;
        }

        if (GameManager.sharedInstance.CurrentGameState == GameState.InGame)
        {
            RigidBody.velocity = new Vector2(currentRunnigSpeed, RigidBody.velocity.y);
        }
    }

    void RespawnPosition()
    {
        this.transform.position = StartPosition;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            return;
        }

        if (collision.tag == "Player")
        {
            StartCoroutine(WaitForSeconds());
            collision.gameObject.GetComponent<PlayerController>().SufferDamage(-EnemyDamage);
            return;
        }

        Wait();
        RespawnPosition();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" || collision.tag != "Coin" || collision.tag != "Enemy")
        {
            facingRight = !facingRight;
        }

        if (collision.tag == "Player" && GameManager.sharedInstance.CurrentGameState == GameState.InGame)
        {
            music.Play();
        }
        
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSecondsRealtime(5);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3000);
    }

}
