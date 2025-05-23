using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D playerRb;
    public float offsetX;
    public float lerpSpeedX;
    public float lerpSpeedY;

    private float desiredX;
    private float desiredY;

    private int playerDirection;
    private float halfH;

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        desiredX = playerRb.position.x;
        desiredY = 2;
        transform.position = new Vector3(desiredX, desiredY, -1);
        halfH = Camera.main.orthographicSize;
    }

    void Update()
    {
        if (playerRb.linearVelocityX > 0) playerDirection = 1;
        else if (playerRb.linearVelocityX < 0) playerDirection = -1;
        else playerDirection = 0;

        lerpSpeedX = Mathf.Abs(playerRb.linearVelocityX) > 8.0f ? 5f : 2f;
        lerpSpeedY = 4f;
    }
    private void LateUpdate()
    {
        float cameraTop = halfH + transform.position.y;
        float cameraBottom = transform.position.y - halfH;
        float pad = 4f;

        if (player.transform.position.y > cameraTop - pad && Mathf.Abs(playerRb.linearVelocityY) < 0.01f)
        {
            desiredY = player.transform.position.y;
        }

        if (player.transform.position.y < cameraBottom + 2 * pad && player.transform.position.y > 1)
        {
            desiredY = Mathf.Max(2, player.transform.position.y);
        }


        //Determine desired horizontal camera position
        if (Mathf.Abs(playerRb.linearVelocityX) > 0.5f)
        {
            desiredX = player.transform.position.x + (playerDirection * offsetX);
        }

        else desiredX = player.transform.position.x; 

            Vector3 currentPosition = transform.position;

        //Move camera horizontally based on player's direction
        float newX = Mathf.Abs(desiredX - currentPosition.x) > 0.001f
                  ? Mathf.Lerp(currentPosition.x, desiredX, lerpSpeedX * Time.deltaTime)
                  : desiredX;

        float newY = Mathf.Abs(desiredY - currentPosition.y) > 0.001f
                 ? Mathf.Lerp(currentPosition.y, desiredY, lerpSpeedY * Time.deltaTime)
                 : desiredY;

        transform.position = new Vector3(newX, newY, -1);
    }
}
