using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown; // Thời gian hồi chiêu
    [SerializeField] private float range; // Phạm vi tấn công
    [SerializeField] private int damage; // Sát thương gây ra

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance; // Khoảng cách giữa kẻ địch và người chơi để tấn công
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer; // Lớp của Player
    private float cooldownTimer = Mathf.Infinity; // Bộ đếm thời gian hồi chiêu

    // References
    private Animator anim; // Bộ điều khiển hoạt ảnh
    private Health playerHealth; // Quản lý máu của Player
    private EnemyPatrol enemyPatrol; // Điều khiển di chuyển của Enemy

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        // Tăng bộ đếm thời gian
        cooldownTimer += Time.deltaTime;

        // Ngừng mọi hành động nếu Player đã chết
        if (playerHealth != null && playerHealth.currentHealth <= 0)
        {
            if (enemyPatrol != null)
                enemyPatrol.enabled = false; // Ngừng di chuyển
            return; // Thoát Update
        }

        // Tấn công khi phát hiện Player trong tầm nhìn
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack"); // Kích hoạt hoạt ảnh tấn công
            }
        }

        // Bật/Tắt di chuyển của Enemy tùy vào trạng thái phát hiện Player
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        // Kiểm tra nếu Player nằm trong vùng tấn công
        RaycastHit2D hit =
            Physics2D.BoxCast(
                boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                0,
                Vector2.left,
                0,
                playerLayer
            );

        // Gán Player nếu phát hiện
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
            // Kiểm tra Player còn máu
            if (playerHealth != null && playerHealth.currentHealth > 0)
                return true;
        }

        // Reset nếu không tìm thấy hoặc Player đã chết
        playerHealth = null;
        return false;
    }

    private void OnDrawGizmos()
    {
        // Hiển thị vùng tấn công trên Scene
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
        );
    }

    private void DamagePlayer()
    {
        // Nếu Player trong tầm nhìn và còn sống, gây sát thương
        if (PlayerInSight() && playerHealth != null && playerHealth.currentHealth > 0)
            playerHealth.TakeDamage(damage);
    }
}
