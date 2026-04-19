using UnityEngine;

public class kinectic_flooring : MonoBehaviour
{
    public GameObject ResourceManager;
    public resource_manager resource_manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resource_manager = ResourceManager.GetComponent<resource_manager>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerStay(Collider other_collider)
    {
        Rigidbody other_rb = other_collider.gameObject.GetComponent<Rigidbody>();
        resource_manager.electricity += other_rb.mass*0.01f;
    }
}
