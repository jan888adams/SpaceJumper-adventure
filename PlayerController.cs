using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Constant State
    private const string STATE_ALIVE = "isALive";
    private const string STATE_ON_THE_GROUND = "isOnTheGround";
    private const string STATE_IS_MOVING = "isMoving";
    private const string STATE_IS_FACING_RIGHT = "isFacingRight";
    //Var Unity
    private Animator animator;
    public float JumpRaycastDistance = 1.5f;
    public LayerMask groundMask;
    private Vector3 startPosition; // need to be Vector 3 -> 
    // Var for the movement
    public float jumpForce = 8f;
    private Rigidbody2D rigidBody;
    public float runnigSpeed = 20f;
    // Var for State
  //  [SerializeField] // to show a private instants vars
    private int _healthPoints;
    private int _mannaPoints;
    public const int InitialHealth = 200;
    public const int InitialManna = 30;
    public const int MaxHealth = 200;
    public const int MinHealth = 10;
    public const int MaxManna = 30;
    public const int MinManna = 0;
    //var for super jump
    public const int SuperJumpCost = 5;
    public const float SuperJumpForce = 1.5f;
        

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayerPrefs.SetFloat("MaxScoreText", 0.0f);
        PlayerPrefs.SetInt("CoinText", 0);
    }

    // Start is called before the first frame update
    private void Start()
    {
        startPosition = this.transform.position;
        animator.SetBool(STATE_IS_FACING_RIGHT, true);

    }

    public void StartGame()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        animator.SetBool(STATE_IS_MOVING, false);

        _healthPoints = InitialHealth;
        _mannaPoints = InitialManna;

        Invoke("RestartPosition", 0.4f); // Delay restart
        
    }

    void RestartPosition()
    {
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump(false);
        }
        if (Input.GetButtonDown("SuperJump"))
        {
            Jump(true);
        }

        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        Debug.DrawRay(transform.position, Vector2.down * JumpRaycastDistance, Color.red);
    }

    // Update is set up in a certain period of time
    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.CurrentGameState == GameState.Menu) // In game
        {
            // NOT in the game
            rigidBody.Sleep(); // not moving
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                Move();
                animator.SetBool(STATE_IS_MOVING, true);
            }
            else
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                animator.SetBool(STATE_IS_MOVING, false);
            }

            if (Input.GetKey(KeyCode.LeftArrow) && animator.GetBool(STATE_IS_FACING_RIGHT))
            {
                Flip();
                animator.SetBool(STATE_IS_FACING_RIGHT, false);
            }

            if (Input.GetKey(KeyCode.RightArrow) && !animator.GetBool(STATE_IS_FACING_RIGHT))
            {
                Flip();
                animator.SetBool(STATE_IS_FACING_RIGHT, true);
            }

            if (Input.GetAxis("Horizontal") > 0 && !animator.GetBool(STATE_IS_FACING_RIGHT))
            {
                Flip();
            }
            else if (hideFlags < 0 && animator.GetBool(STATE_IS_FACING_RIGHT))
            {
                Flip();
            }
            
        }
    }

    private void Jump(Boolean superJump)
    {
        float jumpForceFactor = jumpForce;

        if (superJump && _mannaPoints >= SuperJumpCost )
        {
            _mannaPoints -= SuperJumpCost;
            jumpForceFactor *= SuperJumpForce;
        }
        if (IsTouchingTheGround() &&
            GameManager.sharedInstance.CurrentGameState == GameState.InGame)
        {
            GetComponent<AudioSource>().Play();
            rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
        }
         
    }

    private void Move()
    {
        if (animator.GetBool(STATE_IS_FACING_RIGHT)) // Right 
            rigidBody.velocity = new Vector2(runnigSpeed, rigidBody.velocity.y);
        else if (!animator.GetBool(STATE_IS_FACING_RIGHT)) // Left
            rigidBody.velocity = new Vector2(runnigSpeed * -1, rigidBody.velocity.y);
    }

    private void Flip()
    {
        animator.SetBool(STATE_IS_FACING_RIGHT, false);
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    private bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(transform.position,
            Vector2.down, 4.5f,
            groundMask))
            return true;
        return false;
    }

    public void Die()
    {
        float travelledDistance = GetTravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("MaxScoreText", 0f);
        int currentCoins = GetCollectedCoins();
        int maxCoins = PlayerPrefs.GetInt("CoinText");
        if (travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("MaxScoreText", travelledDistance);
        }

        if (currentCoins > maxCoins)
        {
            PlayerPrefs.SetInt("CoinText", currentCoins);
        }


        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }

    public void CollectHealth(int points)
    {
        this._healthPoints += points;
        if (this._healthPoints >= MaxHealth)
        {
            this._healthPoints = MaxHealth;
        }

    }

    public void SufferDamage(int damage)
    {
        this._healthPoints += damage;
        if (this._healthPoints <= 0)
        {
            Die();
        }
       
    }

    public void CollectManna(int points)
    {
        this._mannaPoints += points;
        if (_mannaPoints >= MaxManna)
        {
            this._mannaPoints = MaxManna;
        }

    }

    public int GetHealth()
    {
        return _healthPoints;
    }

    public int GetManna()
    {
        return _mannaPoints;
    }

    public float GetTravelledDistance()
    {
        return this.transform.position.x - startPosition.x;
    }

    public int GetCollectedCoins()
    {
        return GameManager.sharedInstance.CollectedObject;
    }
}