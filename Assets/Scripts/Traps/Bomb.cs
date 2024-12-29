using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float damage = 10f;               // Lượng sát thương
    [SerializeField] private float activationDelay = 2f;       // Thời gian trước khi phát nổ
    private Animator anim;                                     // Animator để xử lý hiệu ứng phát nổ
    private bool isExploded = false;                           // Cờ để ngăn việc bomb kích hoạt nhiều lần

    private void Awake()
    {
        anim = GetComponent<Animator>(); // Lấy component Animator của bomb
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Chỉ kích hoạt khi Player chạm vào
        if (collision.CompareTag("Player") && !isExploded)
        {
            isExploded = true; // Đánh dấu bomb đã được kích hoạt
            StartCoroutine(Explode(collision)); // Bắt đầu hiệu ứng phát nổ
        }
    }

    private IEnumerator Explode(Collider2D collision)
    {
        // Bật Animation "activated"
        anim.SetTrigger("activated");

        // Chờ thời gian trước khi phát nổ
        yield return new WaitForSeconds(activationDelay);

        // Gây sát thương cho Player nếu Player vẫn còn ở gần
        if (collision != null && collision.GetComponent<Health>() != null)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }

        // Bật Animation "explode" (phát nổ)
        anim.SetTrigger("explode");

        // Chờ animation phát nổ hoàn tất, sau đó xóa bomb
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
