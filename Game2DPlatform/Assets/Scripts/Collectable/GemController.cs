using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    public GameObject gemParticle;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.RegisterGem(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCollectables>().GemCollected();

            GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;

            Instantiate(gemParticle, transform.position, transform.rotation);
            GameManager.RemoveGemFromList(this);
        }
    }
}
