using UnityEngine;

public class Exclamation : MonoBehaviour
{
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        anim = GetComponent<Animator>();
        transform.localPosition = new Vector2(0, 3);
    }
    public void Play()
    {
        anim.Play("ExclamationPopUp", 0, 0f);
    }
}
