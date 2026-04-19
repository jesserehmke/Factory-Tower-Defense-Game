using UnityEngine;

public class shredderblades : MonoBehaviour
{
    public float rotation = 0;
    public GameObject shredder;
    public int direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotation += 3f;
        transform.localRotation =  Quaternion.Euler(0 , 0, rotation*direction);
        if(rotation == 360f)
        {
            rotation = 0f;
        }
    }
}
