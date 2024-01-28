using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject cam;
    public float parallaxValue;
    
    private float lenght;
    private float startPos;
    private float startY;

    private void Start()
    {
        startPos = transform.position.x;
        startY = transform.position.y;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxValue));
        float dist = cam.transform.position.x * parallaxValue;
        
        transform.position = new Vector3(startPos + dist, startY, transform.position.z);

        if (temp > startPos + lenght) startPos += lenght;
        else if (temp < startPos - lenght) startPos -= lenght;
    }
}
