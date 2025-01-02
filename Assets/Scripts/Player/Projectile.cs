using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask[] target;
    private float direction;
    private bool hit;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float damage;

    private List<int> layerValues = new List<int>();
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        foreach (LayerMask _target in target)
        {
            int layerValue = (int)Mathf.Log(_target.value, 2);
            layerValues.Add(layerValue);
        }
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false); // chỉnh time đạn biến mất
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        if (anim != null)
            anim.SetTrigger("explode");
        else Deactivate();

        Debug.Log(collision.gameObject.layer);
        if (layerValues.Contains(collision.gameObject.layer) && collision.GetComponent<Health>() != null)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0; // chỉnh time đạn biến mất
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
}
// using UnityEngine;

// public class Projectile : MonoBehaviour
// {
//     [SerializeField] private float speed;
//     private float direction;
//     private bool hit;
//     private float lifetime;

//     private Animator anim;
//     private BoxCollider2D boxCollider;

//     private void Awake()
//     {
//         anim = GetComponent<Animator>();
//         boxCollider = GetComponent<BoxCollider2D>();
//     }
//     private void Update()
//     {
//         if (hit) return;
//         float movementSpeed = speed * Time.deltaTime * direction;
//         transform.Translate(movementSpeed, 0, 0);

//         lifetime += Time.deltaTime;
//         if (lifetime > 5) gameObject.SetActive(false);
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         hit = true;
//         boxCollider.enabled = false;
//         anim.SetTrigger("explode");

//         if (collision.tag == "Enemy")
//             collision.GetComponent<Health>().TakeDamage(1);
//     }
//     public void SetDirection(float _direction)
//     {
//         lifetime = 0;
//         direction = _direction;
//         gameObject.SetActive(true);
//         hit = false;
//         boxCollider.enabled = true;

//         float localScaleX = transform.localScale.x;
//         if (Mathf.Sign(localScaleX) != _direction)
//             localScaleX = -localScaleX;

//         transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
//     }
//     private void Deactivate()
//     {
//         gameObject.SetActive(false);
//     }
// }