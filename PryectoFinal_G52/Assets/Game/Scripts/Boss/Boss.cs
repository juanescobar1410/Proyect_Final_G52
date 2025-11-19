using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // Codigo enemigo base
    public int rutina;
    public float cronometro;
    public float tiempoRutina;
    public Animator anim;
    public quaternion angulo;
    public float grado;
    public GameObject target;
    public bool atacando;
    public RangoBoss rango;
    public float speed;
    public GameObject[] hit;
    public int hit_select;

    //----EscupeFuego----//
    public bool lanza_fuego;
    public List<GameObject> pool = new List<GameObject>();
    public GameObject fire;
    public GameObject cabeza;
    private float cronometro_fuego;

    //---JumpAttack---//
    public float jump_distance;
    public bool direction_skill;

    //---fireball---//
    public GameObject fire_ball;
    public GameObject point;
    public List<GameObject> pool2 = new List<GameObject>();

    //-------//--------//-------//
    public int fase = 1;
    public float Hp_min;
    public float Hp_max;
    public Image barra;
    public AudioSource musica;
    public bool muerto;

    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        barra.fillAmount = Hp_min / Hp_max;
        if (Hp_min > 0)
        {
            vivo();
        }
        else
        {
            if (!muerto)
            {
                anim.SetTrigger("dead");
                musica.enabled = false;
                muerto = true;
            }
        }
    }

    public void comportamientoBoss()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 15)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            point.transform.LookAt(target.transform.position);
            musica.enabled = true;

            if (Vector3.Distance(transform.position,target.transform.position) > 1 && !atacando)
            {
                switch (rutina)
                {
                    case 0:
                        // caminar//
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        anim.SetBool("walk", true);
                        anim.SetBool("run", false);
                        
                        if (transform.rotation == rotation)
                        {
                           transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        }

                        anim.SetBool("attack", false);

                        cronometro += 1 * Time.deltaTime;

                        if (cronometro > tiempoRutina)
                        {
                            rutina = UnityEngine.Random.Range(0, 5);
                            cronometro = 0;
                        }
                        break;

                    case 1:
                        // correr//
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        anim.SetBool("run", true);
                        anim.SetBool("walk", false);
                        
                        if (transform.rotation == rotation)
                        {
                           transform.Translate(Vector3.forward * speed * 2 * Time.deltaTime);
                        }
                        anim.SetBool("attack", false);
                        break;

                    case 2:
                        //--Lanzar fuego--//
                        anim.SetBool("walk", false);
                        anim.SetBool("run", false);
                        anim.SetBool("attack", true);
                        anim.SetFloat("skills", 0.4f);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        rango.GetComponent<CapsuleCollider>().enabled = false;
                        break;

                    case 3:
                       //--Jump Attack--//
                        if (fase == 2)
                        {
                            jump_distance += 1 * Time.deltaTime;
                            anim.SetBool("walk", false);
                            anim.SetBool("run", false);
                            anim.SetBool("attack", true);
                            anim.SetFloat("skills", 1f);
                            hit_select = 3;
                            rango.GetComponent<CapsuleCollider>().enabled = false;

                            if(direction_skill)
                            {
                                if (jump_distance < 1f)
                                {
                                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                                }

                                transform.Translate(Vector3.forward * 8 * Time.deltaTime);
                            }
                        }
                        else
                        {
                            rutina = 0;
                            cronometro = 0;
                        }
                            break;

                        case 4:
                        //--Fireball--//
                        if (fase == 2)
                        {
                            anim.SetBool("walk", false);
                            anim.SetBool("run", false);
                            anim.SetBool("attack", true);
                            anim.SetFloat("skills", 0.6f);
                            rango.GetComponent<CapsuleCollider>().enabled = false;
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 0.5f);
                        }
                        else
                        {
                            rutina = 0;
                            cronometro = 0;
                        }


                        break;
                }
            }
    
        }
    }

    public void final_ani()
    {
        rutina = 0;
        anim.SetBool("attack", false);
        atacando = false;
        rango.GetComponent<CapsuleCollider>().enabled = true;
        lanza_fuego = false;
        jump_distance = 0;
        direction_skill = false;
    }

    public void Direction_Attack_Start()
    {
               direction_skill = true;
    }

    public void Direction_Attack_Final()
    {
               direction_skill = false;
    }

    ///-----Melee-----///
    
    public void ColliderWeaponTrue()
    {
               hit[hit_select].GetComponent<SphereCollider>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
               hit[hit_select].GetComponent<SphereCollider>().enabled = false;
    }

    //--lanzar fuego--//
    public GameObject GetBala()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        GameObject obj = Instantiate(fire,cabeza.transform.position,cabeza.transform.rotation) as GameObject;
        pool.Add(obj);
        return obj;
    }

    public void LanzaFuego_Skill()
    {
        cronometro_fuego += 1 * Time.deltaTime;
        if (cronometro > 0.1f)
        {
            GameObject obj = GetBala();
            obj.transform.position = cabeza.transform.position;
            obj.transform.rotation = cabeza.transform.rotation;
            cronometro_fuego = 0;
        }
    }

    public void Start_Fire()
    {
        lanza_fuego = true;
    }

    public void Stop_Fire()
    {
        lanza_fuego = false;
    }

    //--Fireball--//
    public GameObject GetFireball()
    {
        for (int i = 0; i < pool2.Count; i++)
        {
            if (!pool2[i].activeInHierarchy)
            {
                pool2[i].SetActive(true);
                return pool2[i];
            }
        }
        GameObject obj = Instantiate(fire_ball,point.transform.position,point.transform.rotation) as GameObject;
        pool2.Add(obj);
        return obj;
    }

    public void Fireball_Skill()
    {
            GameObject obj = GetFireball();
            obj.transform.position = point.transform.position;
            obj.transform.rotation = point.transform.rotation;
    }

    public void vivo()
    {
        if (Hp_min < 500)
        {
            fase = 2;
            tiempoRutina = 1;
        }

        comportamientoBoss();

        if(lanza_fuego)
        {
            LanzaFuego_Skill();
        }
    }
}
