using UnityEngine;

public class hp_manager : MonoBehaviour
{
    public float hp;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(hp <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
