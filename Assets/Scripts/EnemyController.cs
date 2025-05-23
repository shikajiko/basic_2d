using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float circleRadius;
    public LayerMask playerMask;
    private float distanceToPlayer;
    private float distanceY;
    private float targetX;
    private int direction;
    private Rigidbody2D rb;
    private bool isAggro;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool canMove;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        canMove = true;
    }

    private void Update()
    {
        targetX = player.transform.position.x;
        distanceToPlayer = Mathf.Abs(targetX - transform.position.x);
        distanceY = Mathf.Abs(player.transform.position.y - transform.position.y);

        if (canMove)
        {
            if (targetX > transform.position.x)
            {
                sprite.flipX = false;
                direction = 1;
            }
            else if (targetX < transform.position.x)
            {
                sprite.flipX = true;
                direction = -1;
            }
            else direction = 0;

            if (!isAggro)
            {
                isAggro = DetectPlayer();
            }

            if (distanceToPlayer > 20f)
            {
                isAggro = false;
            }
            else if (distanceY > 3f && distanceToPlayer < 2f)
            {
                isAggro = false;
            }

            anim.SetBool("chase", isAggro);
        }
        
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            if (distanceToPlayer > 2f && isAggro)
            {
                rb.linearVelocity = new Vector2(direction * speed * Time.fixedDeltaTime, transform.position.y);
            }
            if (distanceToPlayer < 2f && distanceY < 1f)
            {
                anim.SetTrigger("attack");
                StartCoroutine(attackDelay());
            }
        }
   
     }
       
    

    private bool DetectPlayer()
    {
        if (Physics2D.CircleCast(transform.position, circleRadius, -transform.up, 0f, playerMask))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

    private IEnumerator attackDelay()
    {
        canMove = false;
        anim.SetBool("chase", false);
        yield return new WaitForSeconds(0.3f);
        canMove = true;
    }
}
