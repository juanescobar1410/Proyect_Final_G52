using UnityEngine;

/// <summary>
/// Detecta cuando el jugador entra en el rango del jefe y selecciona aleatoriamente
/// un ataque cuerpo a cuerpo o habilidad según la fase del Boss. Configura la animación,
/// el tipo de golpe a ejecutar y activa el estado de ataque deshabilitando el collider.
/// </summary>


public class RangoBoss : MonoBehaviour
{
    public Animator ani;
    public Boss boss;
    public int melee;

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("Player"))
        {
           melee = Random.Range(0, 4);
           switch (melee)
            {
                case 0:
                    //Golpe 1//
                    ani.SetFloat("skills", 0f);
                    boss.hit_select = 0;
                    break;

                case 1:
                    //Golpe 2//
                    ani.SetFloat("skills", 0.2f);
                    boss.hit_select = 1;
                    break;

                case 2:
                    //jump//
                    ani.SetFloat("skills", 0.8f);
                    boss.hit_select = 2;
                    break;

                case 3:
                    //Fireball//
                    if (boss.fase == 2)
                    {
                        ani.SetFloat("skills", 0.6f);
                    }
                    else
                    {
                        melee = 0;
                    }
                    
                    break;
            }
            ani.SetBool("walk", false);
            ani.SetBool("run", false);
            ani.SetBool("attack", true);
            boss.atacando = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }

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
