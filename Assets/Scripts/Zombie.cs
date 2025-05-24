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

    [SerializeField] GameObject exclamationPrefab;
    GameObject exclamation;
    Exclamation exclamationScript;

    void Start()
    {
        //get each needed components
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();

        //register each states to StateMachine's dictionary
        stateMachine = new EnemyStateMachine();
        stateMachine.Register("Patrol", new ZombiePatrol(this, stateMachine, Animator, "Patrol"));
        stateMachine.Register("Aggro", new ZombieAggro(this, stateMachine, Animator, "Aggro"));
        stateMachine.Register("Attack", new ZombieAttack(this, stateMachine, Animator, "Attack"));

        stateMachine.InitializeStateMachine("Patrol");

        //instantiate exclamation mark prefab to display when the enemy is aggroed
        exclamation = Instantiate(exclamationPrefab, transform);
        exclamation.SetActive(false);
        exclamationScript = exclamation.GetComponent<Exclamation>();

        //get player's game object and layer
        player = GameObject.FindWithTag("Player");
        playerMask = 1 << player.layer;
    }

    void Update()
    {
        playerPosition = player.transform.position;
        isAggro = DetectPlayer();
        stateMachine.CurrentState.LogicUpdate();
        //stateMachine.GetState();
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
}
