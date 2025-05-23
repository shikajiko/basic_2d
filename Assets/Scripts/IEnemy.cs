using UnityEngine;

public interface IEnemy
{
    Rigidbody2D Rigidbody2D { get; }
    Animator Animator { get; }
    SpriteRenderer Sprite { get; }
    bool IsPlayerInRange();
}
