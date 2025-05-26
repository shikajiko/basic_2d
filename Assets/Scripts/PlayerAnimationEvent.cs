using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour {
    [SerializeField] private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void AttackEvent()
    {
        player.Attack();
    }


}
