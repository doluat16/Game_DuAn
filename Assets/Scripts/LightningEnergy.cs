using System.Collections;
using UnityEngine;

public class LightningEnergy : MonoBehaviour
{
    [SerializeField] private float damage; // Sát thương gây ra cho Player

    [Header("Lightning Timers")]
    [SerializeField] private float activationDelay; // Thời gian chờ trước khi bẫy kích hoạt
    [SerializeField] private float activeTime;      // Thời gian bẫy hoạt động
    private Animator anim;                         // Điều khiển Animator
    private bool triggered;                        // Xác định xem bẫy đã kích hoạt chưa
    private bool active;                           // Xác định xem bẫy đang hoạt động hay không

    private void Awake()
    {
        // Lấy Animator từ GameObject hiện tại
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu Player đi vào vùng bẫy
        if (collision.tag == "Player")
        {
            // Nếu bẫy chưa kích hoạt, bắt đầu quá trình kích hoạt
            if (!triggered)
                StartCoroutine(ActivateLightningEnergy());

            // Nếu bẫy đang hoạt động, gây sát thương cho Player
            if (active)
            {
                // Lấy component Health của Player và gây sát thương
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActivateLightningEnergy()
    {
        triggered = true; // Đặt trạng thái là bẫy đã được kích hoạt

        // Đợi trong khoảng thời gian kích hoạt
        yield return new WaitForSeconds(activationDelay);

        active = true; // Bẫy bắt đầu hoạt động
        anim.SetBool("activated", true); // Bật animation kích hoạt bẫy

        // Bẫy hoạt động trong khoảng thời gian activeTime
        yield return new WaitForSeconds(activeTime);

        // Tắt bẫy sau khoảng thời gian hoạt động
        active = false;
        triggered = false;
        anim.SetBool("activated", false); // Tắt animation bẫy
    }
}
