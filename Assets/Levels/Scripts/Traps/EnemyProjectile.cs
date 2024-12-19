using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage //Will damage the player every time they touch
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    public void SetDirection(float _direction )
    {

    }

     public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true); // kích hoạt đạn
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); //Excute logic from parent script first
        gameObject.SetActive(false); //when this hits any object deactive arrow
    }
}
