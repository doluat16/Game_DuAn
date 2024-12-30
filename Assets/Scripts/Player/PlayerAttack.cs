using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private GameObject[] spongeBullet;
    [SerializeField] private GameObject muzzleFlash;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = 0;
    private float fireRate = .3f;
    public Inventory inventory;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > cooldownTimer + fireRate)
        {
            cooldownTimer = Time.time;
            anim.SetTrigger("attack");
        }

        if (Input.GetMouseButtonDown(1))
        {
            inventory.UseItem();
        }
    }

    public IEnumerator Attack()
    {
        muzzleFlash.SetActive(true);
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        spongeBullet[FindSpongeBullet()].SetActive(true);
        yield return new WaitForSeconds(.02f);
        muzzleFlash.SetActive(false);
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

    private int FindSpongeBullet()
    {
        for (int i = 0; i < spongeBullet.Length; i++)
        {
            if (!spongeBullet[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
