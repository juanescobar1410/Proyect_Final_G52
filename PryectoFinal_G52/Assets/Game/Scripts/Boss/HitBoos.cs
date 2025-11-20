using UnityEngine;

/// <summary>
/// Gestiona el daño que el jefe inflige al jugador. Cuando el collider del ataque
/// detecta al jugador, reduce su vida aplicando la cantidad de daño configurada.
/// </summary>


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
            coll.GetComponent<PlayerAttack>().currentHP -= damage;
        }
    }
}
