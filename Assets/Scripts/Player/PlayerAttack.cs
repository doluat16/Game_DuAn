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
    private float fireRate = .5f;
    public Inventory inventory;

    private float attackDam = 25;
    private int level;
    private readonly int maxLevel = 3;

    private float expToLevelUp;
    private float exp;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        SetupLevel();
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
        fireballs[FindFireball()].GetComponent<Projectile>().SetDamage(attackDam);
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

    public void UpFirerate(float newFirate)
    {
        fireRate = newFirate;
    }

    public void UpDamage(float newDamage)
    {
        attackDam = newDamage;
    }

    public void UpLevel()
    {
        if (level < maxLevel)
        {
            level++;
            PlayerPrefs.SetInt("PlayerLevel", level);
            switch (level)
            {
                case 2:
                    {
                        playerMovement.UpSpeed(playerMovement.speed + playerMovement.speed * .1f);
                        UpFirerate(fireRate + fireRate * .1f);
                        expToLevelUp = 1000f;
                    };
                    break;
                case 3:
                    {
                        UpDamage(attackDam + attackDam * .1f);
                    };
                    break;
            }
        }
    }

    public void GainExp(float _exp)
    {
        if (level == maxLevel) return;
        exp += _exp;
        if (exp >= expToLevelUp)
        {
            exp -= expToLevelUp;
            UpLevel();
        }
    }

    private void SetupLevel()
    {
        exp = PlayerPrefs.GetFloat("PlayerExp");
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {
            level = PlayerPrefs.GetInt("PlayerLevel");
        }
        else
        {
            level = 1;
            PlayerPrefs.SetInt("PlayerLevel", level);
        }

        switch (level)
        {
            case 1:
                {
                    expToLevelUp = 500;
                };
                break;
            case 2:
                {
                    playerMovement.UpSpeed(playerMovement.speed + playerMovement.speed * .1f);
                    UpFirerate(fireRate + fireRate * .1f);
                    expToLevelUp = 1000f;
                };
                break;
            case 3:
                {
                    playerMovement.UpSpeed(playerMovement.speed + playerMovement.speed * .1f);
                    UpFirerate(fireRate + fireRate * .1f);
                    UpDamage(attackDam + attackDam * .1f);
                };
                break;
        }
    }
}
