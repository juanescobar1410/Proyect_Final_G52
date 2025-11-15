using UnityEngine;

public class HitBoos : MonoBehaviour
{
    public int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            coll.GetComponent<Player3D>().HP_Min -= damage;
        }
    }
}
