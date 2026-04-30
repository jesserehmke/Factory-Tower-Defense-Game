using UnityEngine;
public class hp_manager : MonoBehaviour
{
    public GameObject Organic_Material_Template;
    public float hp;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(hp <= 0)
        {
            for(int i = 0; i < 11; i++)
            {
                Instantiate(Organic_Material_Template, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f)), Quaternion.identity);
            }

            this.gameObject.SetActive(false);
        }
    }
}
