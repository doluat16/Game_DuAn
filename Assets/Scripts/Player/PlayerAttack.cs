using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint; // vt đạn
    [SerializeField] private GameObject[] fireballs; // đặt 10 quả cầu lửa trong này


    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = 0;
    private float fireRate = .3f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > cooldownTimer + fireRate && playerMovement.canAttack())
        {
            cooldownTimer = Time.time;
            anim.SetTrigger("attack");
        }
    }

    public void Attack()
    {
        anim.SetTrigger("attack");
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

}
