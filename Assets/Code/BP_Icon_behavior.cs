using UnityEngine;

public class BP_Icon_behavior : MonoBehaviour
{
    public GameObject Bp;

    public void OpenBlueprint()
    {
        Bp.SetActive(true);
        this.gameObject.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
