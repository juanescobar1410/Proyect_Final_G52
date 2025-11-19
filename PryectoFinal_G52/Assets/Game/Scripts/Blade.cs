using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            coll.GetComponent<Enemigo1>().HP_Min -= 33;
        }

        if (coll.transform.parent != null && coll.transform.parent.name == "FakeWall")
        {
            Destroy(coll.transform.parent.gameObject);
        }

    }
}