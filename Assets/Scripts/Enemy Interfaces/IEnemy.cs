using UnityEngine;

public interface IEnemy
{
    Rigidbody2D Rigidbody2D { get; }
    Animator Animator { get; }
    SpriteRenderer Sprite { get; }
    EnemyStateMachine stateMachine { get; }

    public Vector2 playerPosition { get; }
    public bool isAggro { get; }

    public void TakeDamage(int damage, float knockback);
}
