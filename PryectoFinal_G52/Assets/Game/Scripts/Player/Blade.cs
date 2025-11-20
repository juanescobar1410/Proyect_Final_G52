using UnityEngine;

/// <summary>
/// Detecta colisiones del hitbox de la espada:
/// - Si golpea un enemigo, le resta 33 de vida.
/// - Si golpea una FakeWall (objeto padre llamado "FakeWall"), la destruye.
/// - Si golpea un jefe con el tag "Boss", le resta 100 de vida.
/// </summary>
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

        if (coll.CompareTag("Boss"))
        {
            coll.GetComponent<Boss>().Hp_min -= 100;
        }

    }
}