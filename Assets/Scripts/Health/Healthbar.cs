using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Đảm bảo đã thêm dòng này

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        // Khởi tạo thanh máu tổng ban đầu (nếu cần)
        healthSlider.maxValue = playerHealth.startingHealth;
    }

    private void Update()
    {
        // Cập nhật thanh máu hiện tại dựa trên máu của player
        healthSlider.value = playerHealth.currentHealth;
    }
}
