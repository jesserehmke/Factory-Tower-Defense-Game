using UnityEngine;

public class obstaclegenerator : MonoBehaviour
{
    public GameObject obstacletemplate;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int k = 0; k<200; k++)
        {
            var blockedposition = new Vector3(Mathf.Round(Random.Range(10.0f, -10.0f)*2)/2, 2.25f, Mathf.Round(Random.Range(-8.0f, 8.0f)*2)/2);
            Instantiate(obstacletemplate, blockedposition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
