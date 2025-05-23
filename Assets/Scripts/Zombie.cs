using UnityEngine;

public class Zombie : MonoBehaviour, IEnemy
{
    private bool isPlayerInRange;

    public Rigidbody2D Rigidbody2D { get; private set; }
    public Animator Animator { get; private set; }
    public SpriteRenderer Sprite { get; private set; }

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }


    public bool IsPlayerInRange()
    {
        return isPlayerInRange;
    }
}
