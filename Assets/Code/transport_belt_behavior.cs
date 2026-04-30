using UnityEngine;

public class transport_belt_behavior : MonoBehaviour
{
    public float speed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
    void OnTriggerStay(Collider other)
    {   
        
        int item_layer = 8;
        if (other.gameObject.layer == item_layer)
        {
            Rigidbody other_rb;
            other_rb = other.gameObject.GetComponent<Rigidbody>();
            other_rb.MovePosition(other.transform.position + transform.forward * speed * -0.01f);
        }
    }
}
