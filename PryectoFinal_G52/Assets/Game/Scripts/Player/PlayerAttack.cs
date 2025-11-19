using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ataque")]
    public Animator animator;
    public GameObject swordHitbox;

    [Header("Vida del jugador")]
    public int maxHP = 100;
    public int currentHP;
    public Image barra;

    [Header("Monedas")]
    public int monedaTotal = 0;

    private bool isAttacking = false;
    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;

        // Verificar que la barra esté asignada
        if (barra == null)
        {
            Debug.LogError("¡La barra de vida NO está asignada en el Inspector!");

            // Intentar buscarla automáticamente
            barra = GameObject.Find("Jugador")?.GetComponent<Image>();

            if (barra != null)
            {
                Debug.Log("Barra encontrada automáticamente!");
            }
        }
        else
        {
            Debug.Log("Barra asignada correctamente: " + barra.name);
        }
    }

    void Update()
    {
        if (isDead) return;

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
        }

        // Verificar muerte en cada frame
        if (currentHP <= 0 && !isDead)
        {
            Die();
        }

        // CORRECCIÓN: Convertir a float y clampearlo entre 0 y 1
        if (barra != null)
        {
            barra.fillAmount = Mathf.Clamp01((float)currentHP / (float)maxHP);
        }
    }

    public void EnableHitbox()
    {
        swordHitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        swordHitbox.SetActive(false);
        isAttacking = false;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHP -= amount;
        currentHP = Mathf.Max(0, currentHP); // Evitar que sea negativo
        Debug.Log("HP actual: " + currentHP + "/" + maxHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        swordHitbox.SetActive(false);
        animator.SetTrigger("deadArissa");
        Invoke("Respawn", 5f);
    }

    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Puño"))
        {
            print("Daño recibido");
            TakeDamage(20);
        }
    }
}