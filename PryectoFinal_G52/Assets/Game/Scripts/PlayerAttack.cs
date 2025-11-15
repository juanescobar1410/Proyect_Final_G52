using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public GameObject swordHitbox; 

    private bool isAttacking = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    // Llamado desde un Animation Event
    public void EnableHitbox()
    {
        swordHitbox.SetActive(true);
    }

    // Llamado desde un Animation Event
    public void DisableHitbox()
    {
        swordHitbox.SetActive(false);
        isAttacking = false;
    }
}