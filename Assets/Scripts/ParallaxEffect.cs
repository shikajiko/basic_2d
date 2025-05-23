using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;
    public bool isRepeating;

    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        
        if(isRepeating)
        {
            float temp = (cam.transform.position.x * (1 - parallaxEffect));
            if (temp > startpos + length) startpos += length;
            else if (temp < startpos - length) startpos -= length;
        }


    }
}
