using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour, IKillable
{
    [SerializeField]
    public int health;

    Rigidbody2D rb;

    ParticleSystem ps;
    
    public void ApplyDamage(int damage)
    {
        health-=damage;
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }

    private void ApplyForce(GameObject enemy)
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
        Vector3 forceDirection = this.transform.position - enemy.transform.position;
        rb.velocity = forceDirection * enemy.GetComponent<CanKill>().damageForce;
        

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            ps.Play();
            ApplyDamage(other.gameObject.GetComponent<CanKill>().damage);
            ApplyForce(other.gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Kill();
        }
    }
}
