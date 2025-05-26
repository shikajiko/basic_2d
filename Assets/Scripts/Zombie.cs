using UnityEngine;

public class Zombie : MonoBehaviour, IEnemy
{
    public Rigidbody2D Rigidbody2D { get; private set; }
    public Animator Animator { get; private set; }
    public SpriteRenderer Sprite { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; } 
    public bool isAggro { get; private set; }
    public Vector2 playerPosition { get; private set; } 

    public GameObject player;
    public float speed;
    public float circleRadius;
    public LayerMask playerMask;


    //variables for managing hit and death
    public int health;
    [HideInInspector] public int lastHitDirection;
    [HideInInspector] public float lastHitForce;

    [SerializeField] GameObject exclamationPrefab;
    GameObject exclamation;
    Exclamation exclamationScript;

    void Start()
    {
        //get each needed components
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        Sprite = GetComponentInChildren<SpriteRenderer>();

        //register each states to StateMachine's dictionary
        stateMachine = new EnemyStateMachine();
        stateMachine.Register("Patrol", new ZombiePatrol(this, stateMachine, Animator, "Patrol"));
        stateMachine.Register("Aggro", new ZombieAggro(this, stateMachine, Animator, "Aggro"));
        stateMachine.Register("Attack", new ZombieAttack(this, stateMachine, Animator, "Attack"));
        stateMachine.Register("Idle", new ZombieIdle(this, stateMachine, Animator, "Idle"));
        stateMachine.Register("Hurt", new ZombieHurt(this, stateMachine, Animator, "Hurt"));
        stateMachine.Register("Death", new ZombieDeath(this, stateMachine, Animator, "Death"));

        stateMachine.InitializeStateMachine("Idle");

        //instantiate exclamation mark prefab to display when the enemy is aggroed
        exclamation = Instantiate(exclamationPrefab, transform);
        exclamation.SetActive(false);
        exclamationScript = exclamation.GetComponent<Exclamation>();

        //get player's game object and layer
        player = GameObject.FindWithTag("Player");
        playerMask = 1 << player.layer;
        health = 5;
    }

    void Update()
    {
        playerPosition = player.transform.position;
        isAggro = DetectPlayer();
        stateMachine.CurrentState.LogicUpdate();
        stateMachine.GetState();

        
    }

    void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
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

    public void ShowAlertIcon()
    {
        exclamation.SetActive(true);
        exclamationScript.Play();
    }

    public void HideAlertIcon()
    {
        exclamation.SetActive(false);
    }

    public void TakeDamage(int damage, float knocback)
    {

        if (!(stateMachine.CurrentState is ZombieHurt) && !(stateMachine.CurrentState is ZombieDeath))
        {
            lastHitDirection = Mathf.FloorToInt(transform.position.x - player.transform.position.x);
            lastHitDirection = lastHitDirection / Mathf.Abs(lastHitDirection);
            lastHitForce = knocback;

            health -= damage;
            stateMachine.ChangeState("Hurt");
        }
       
    }

    public void OnDeathAnimationComplete()
    {
        Destroy(gameObject);
    }
}
