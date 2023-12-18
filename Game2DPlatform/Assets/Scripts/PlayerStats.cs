using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public bool canTakeDamage = true;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        TakeDamage(20);
        TakeDamage(20);
    }

    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            health -= damage;
            //play hurt animation

            if (health <= 0)
            {
                GetComponent<PolygonCollider2D>().enabled = false;
                GetComponentInParent<GatherInput>().DisableControls();
                Debug.Log("The Player is dead");
            }

            StartCoroutine(DamagePrevetion());
        }
    }

    private IEnumerator DamagePrevetion()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.15f);

        if(health > 0)
        {
            canTakeDamage = true;
        }
        else
        {
            //play death animation
        }
    }
}
