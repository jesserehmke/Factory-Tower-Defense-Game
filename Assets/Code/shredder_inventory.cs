using UnityEngine;

public class shredder_inventory : MonoBehaviour
{
    public int organic_material;
    public GameObject Organic_Material_Template;
    public RaycastHit hit;
    float last_time = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("dt minus lt " );
        Debug.Log(Time.time - last_time);
        //Debug.DrawLine(transform.position + transform.right*0.5f - 0.25f*transform.forward, transform.position + transform.right - 0.25f*transform.up * trans, Color color = Color.white, float duration = 0.0f, bool depthTest = true);
        if(Time.time - last_time > 0.2f)
        {
            Debug.Log("deltzatimeklappt");
            if(organic_material > 0 && Physics.Raycast(transform.position + transform.right*0.5f - 0.25f*transform.forward, Vector3.up, out hit, 1, LayerMask.GetMask("Transport Belt")) )
            {
                Instantiate(Organic_Material_Template, hit.transform.position + new Vector3(0, 0.06f, 0), Quaternion.identity);
                organic_material--;
                last_time = Time.time;
            }
        }
    }
    void OnTriggerEnter(Collider other_collider)
    {
        Debug.Log("colliderenter " + other_collider.gameObject);
        
        switch(other_collider.tag)
        {
            case "organic material":
                organic_material++;
                other_collider.gameObject.SetActive(false);
                break;
            
        }
    }
}