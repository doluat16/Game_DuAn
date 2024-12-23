using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Đảm bảo đã thêm dòng này

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        // Khởi tạo thanh máu tổng ban đầu (nếu cần)
      totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        // Cập nhật thanh máu hiện tại dựa trên máu của player
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
