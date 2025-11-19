using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    public int keys;
    // Update is called once per frame
    void Update()
    {
        if (keys >= 1)
        {
            Destroy(gameObject, 0.2f);
        } 
    }
}
