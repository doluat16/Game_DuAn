using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     hit = true;
    //     base.OnTriggerEnter2D(collision); //Execute logic from parent script first
    //     coll.enabled = false;

    //     if (anim != null)
    //         anim.SetTrigger("explode"); //When the object is a fireball explode it
    //     else
    //         gameObject.SetActive(false); //When this hits any object deactivate arrow
    // }
     private void OnTriggerEnter2D(Collider2D collision)
    {
    // Kiểm tra nếu va chạm với Player
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>()?.TakeDamage(damage); // Gây sát thương cho Player
            gameObject.SetActive(false); // Tắt viên đạn
        }

        // Loại trừ Bomb (hoặc bất kỳ đối tượng không mong muốn nào khác)
        if (collision.CompareTag("Bomb"))
        {
            // Logic bỏ qua va chạm với Bomb
            return;
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}