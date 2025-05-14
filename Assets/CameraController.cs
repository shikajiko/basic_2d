using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D playerRb;
    public float offsetX;
    public float offsetY;
    private float desiredX;
    private float desiredY;
    private float lerpSpeed;
    private Vector3 defaultOffset = new Vector3(0, 2, -5); 
    private int playerDirection;
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
    }

   void Update()
    {
        if (playerRb.linearVelocityX > 0)
        {
            playerDirection = 1;
        }
        else if (playerRb.linearVelocityX < 0)
        {
            playerDirection = -1;
        }
        else {
            playerDirection = 0;
        }

        if (Mathf.Abs(playerRb.linearVelocityX) > 8.0f)
        {
            lerpSpeed = 5;
        }
        else lerpSpeed = 2;
    }
    private void LateUpdate()
    {
       
        desiredX = player.transform.position.x + (playerDirection * offsetX);

        if(transform.position.x != desiredX)
        {
            transform.position = Vector3.Lerp(transform.position, 
                new Vector3(desiredX,
                player.transform.position.y + defaultOffset.y,
                -1), lerpSpeed * Time.deltaTime); 
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, 0, 0) + defaultOffset;
        }
    }
}
