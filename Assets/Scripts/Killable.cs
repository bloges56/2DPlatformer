using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour, IKillable
{
    [SerializeField]
    int health;
    
    public void ApplyDamage(int damage)
    {
        health-=damage;
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            
            ApplyDamage(other.gameObject.GetComponent<CanKill>().damage);
        }
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
