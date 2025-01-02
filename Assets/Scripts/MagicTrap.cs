using System.Collections;
using UnityEngine;

public class MagicTrap : MonoBehaviour
{
    [SerializeField] private float slowDuration = 3f; // Thời gian làm chậm
    [SerializeField] private float slowMultiplier = 0.5f; // Hệ số làm chậm (50% tốc độ)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            // Kiểm tra Player có script PlayerMovement không
            if (playerMovement != null)
            {
                StartCoroutine(SlowPlayer(playerMovement));
            }
        }
    }

    private IEnumerator SlowPlayer(PlayerMovement playerMovement)
    {
        // Giảm tốc độ Player
        float originalSpeed = playerMovement.speed;
        playerMovement.speed *= slowMultiplier;

        // Chờ trong thời gian slowDuration
        yield return new WaitForSeconds(slowDuration);

        // Phục hồi tốc độ ban đầu
        playerMovement.speed = originalSpeed;
    }
}
