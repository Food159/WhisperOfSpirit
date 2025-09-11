using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class LungController : MonoBehaviour, IPausable
{
    [Header("FSM")]
    public LungIdleState lidestate;
    public LungWalkState lwalkstate;
    public LungAttackState lattackstate;
    public LungHappyState lhappystate;
    LungState state;

    [Header("Variable")]
    public bool _isFacingRight = false;
    Rigidbody2D  rb2d;
    Animator anim;
    public Transform playerTarget;
    public LayerMask playerLayer;
    private float distance;
    public float yPlayer = 2f;

    [Header("GameObject")]
    [SerializeField] GameObject shadow;

    [Header("Settings")]
    [SerializeField] private float patrolLenght;
    [SerializeField] Transform Area_negX;
    [SerializeField] Transform Area_posX;
    [SerializeField] bool isAleart;
    [SerializeField] public float attackRange;
    public float sentDirection = 1;
    public bool isOutofArea;
    EnemyHealth status;
    
    public void Awake()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            playerTarget = player.transform;
        }
        else
        {
            Debug.Log("player not found");
        }

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        status = GetComponent<EnemyHealth>();
    }
    private void Start()
    {
        lidestate.Setup(rb2d, anim, this);
        lwalkstate.Setup(rb2d, anim, this);
        lattackstate.Setup(rb2d, anim, this);
        lhappystate.Setup(rb2d, anim, this);
        state = lidestate;
    }
    private void Update()
    {
        DistanceCal();
        distance = DistanceCal();
        LookForPlayer();
        SelectState();
        Check();
        state.Do();
        if(playerTarget.position.x > transform.position.x && !_isFacingRight) 
        {
            Flip();
        }
        else if (playerTarget.position.x < transform.position.x && _isFacingRight)
        {
            Flip();
        }
    }
    void SelectState()
    {
        if (status._isDead)
        {
            return;
        }
        if(isAleart)
        {
            if (!isOutofArea)  // if in area
            {
                if(!lattackstate._isAttacking)
                {
                    state = lwalkstate;
                }
                if(transform.position.x <= Area_negX.position.x || transform.position.x >= Area_posX.position.x) // if enemy out of zone
                {
                    isOutofArea = true;
                }
                else
                {
                    if(DistanceCal() <= attackRange) // if in attack range
                    {
                        state = lattackstate;
                    }
                }
            }
            else
            {
                if(playerTarget.position.x <= Area_posX.position.x && playerTarget.position.x >= Area_negX.position.x) // if player in area
                {
                    isOutofArea = false;
                }
                else
                {
                    if(DistanceCal() <= attackRange) // if in attack range
                    {
                        state = lattackstate;
                    }
                    else
                    {
                        state = lidestate;
                    }
                }
            }
        }
        else
        {
            state = lidestate;
        }
        state.Enter();
    }
    public void Flip()
    {
        if (status._isDead)
            return;
        _isFacingRight = !_isFacingRight;
        Vector3 direction = transform.localScale;
        direction.x *= -1;
        transform.localScale = direction;
        sentDirection *= -1;
    }
    void LookForPlayer()
    {
        float distance = DistanceCal();
        float yOffset = Mathf.Abs(playerTarget.position.y - transform.position.y);

        Vector2 rayDirectionLeft = Vector2.left;
        Vector2 rayDirectionRight = Vector2.right;

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, rayDirectionLeft, patrolLenght, playerLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, rayDirectionRight, patrolLenght, playerLayer);

        Debug.DrawRay(transform.position, rayDirectionLeft * patrolLenght, Color.green);
        Debug.DrawRay(transform.position, rayDirectionRight * patrolLenght, Color.green);

        if (distance <= patrolLenght && yOffset < 0.3f)
        {
            isAleart = true;
        }
        else
        {
            isAleart = false;
        }
        //if (hitLeft.collider != null || hitRight.collider != null)
        //{
        //    isAleart = true;
        //}
    }
    void Check()
    {
        if(status._isDead)
        {
            state = lhappystate;
            state.Enter();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().knockback(transform);
        }
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platfrom") || collision.gameObject.CompareTag("firstPlatform"))
        {
            shadow.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Fall"))
        {
            if (status != null)
            {
                status.TakeDamage(5000);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platfrom") || collision.gameObject.CompareTag("firstPlatform"))
        {
            shadow.SetActive(false);
        }
    }
    public float DistanceCal()
    {
        float distance = Mathf.Abs(playerTarget.position.x - transform.position.x);
        return distance;
    }
    public void Pause()
    {
        enabled = false;
    }

    public void Resume()
    {
        enabled = true;
    }
}
