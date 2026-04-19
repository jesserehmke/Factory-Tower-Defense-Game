using UnityEngine;

public class laser_behavior : MonoBehaviour
{
    public bool ray_casting = true;
    public Vector3 start_point;
    public Vector3 direction;
    public LineRenderer lineRenderer;
    public GameObject ResourceManager;
    
    public int index = 0;
    public RaycastHit hit;

    void Start()
    {
        start_point = transform.position + transform.up * 0.5f;
        direction = -1*transform.up;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(index, start_point);
    }

    void FixedUpdate()
    {
        resource_manager resource_manager = ResourceManager.GetComponent<resource_manager>();
        lineRenderer.positionCount = 1;
        
        while(ray_casting){

            Physics.Raycast(start_point, direction, out hit, 100f, LayerMask.GetMask("Default", "Mirrors", "Entities"));
            index++;
            
            start_point = hit.point;
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(index, start_point);

            int mirror_layer = 6;
            int entity_layer = 3;

            if(hit.collider.gameObject.layer == mirror_layer){
                direction = Vector3.Reflect(direction, hit.normal);
            }
            else{
                ray_casting = false;
                if(hit.collider.gameObject.layer == entity_layer){
                    hp_manager hp_manager = hit.collider.gameObject.GetComponent<hp_manager>();
                    hp_manager.hp -= 0.6f;
                }
            }
            resource_manager.electricity -= 0.01f;
        }

        if(resource_manager.electricity > 0)
        {
            ray_casting = true;
        }
        start_point = transform.position + transform.up * 0.5f;
        direction = -1*transform.up;
        index = 0;
        lineRenderer.SetPosition(index, start_point);
    }
}
