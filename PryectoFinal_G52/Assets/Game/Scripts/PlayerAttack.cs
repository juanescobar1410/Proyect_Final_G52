using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ataque")]
    public Animator animator;
    public GameObject swordHitbox;

    [Header("Vida del jugador")]
    public int maxHP = 100;
    public int currentHP;

    private bool isAttacking = false;
    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        if (isDead) return;

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
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
