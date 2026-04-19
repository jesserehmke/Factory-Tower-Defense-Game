using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float camspeed;
    public float scale;
    void start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey("w"))
        {
            transform.position += transform.up * Time.deltaTime * camspeed;
        }
        if (Input.GetKey("s"))
        {
            transform.position -= transform.up * Time.deltaTime * camspeed;
        }
        if (Input.GetKey("d"))
        {
            transform.position += transform.right * Time.deltaTime * camspeed;
        }
        if (Input.GetKey("a"))
        {
            transform.position -= transform.right * Time.deltaTime * camspeed;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y - Input.mouseScrollDelta.y * scale, transform.position.z);
    }
}
