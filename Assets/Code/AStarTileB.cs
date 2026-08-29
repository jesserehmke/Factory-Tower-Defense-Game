using UnityEngine;

public class AStarTileB : MonoBehaviour
{
    public int live = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            live--;
            if(live < 1)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
