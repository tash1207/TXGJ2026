using System.Collections;
using UnityEngine;

public class Detonate : MonoBehaviour
{
    public float explosionRadius = 2.5f;
    public Sprite[] explosionFrames;
    public float frameDuration = 0.08f;
    public string explosionSound = "Audio/explosion";
    public bool wasDropped = false;
    
    private bool hasExploded = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D bombCollider;
    private Rigidbody2D bombRigidbody;

    private void Awake()
    {
        bombCollider = GetComponent<Collider2D>();
        bombRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //if (bombCollider == null) bombCollider = gameObject.GetComponentInChildren<Collider2D>();
        //if (bombRigidbody == null) bombRigidbody = gameObject.GetComponentInChildren<Rigidbody2D>();
        //if (spriteRenderer == null) spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (wasDropped && !hasExploded)
        {
            Explode();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (wasDropped && !hasExploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (hasExploded) return;

        PlayExplosionSound();

        hasExploded = true;

        Debug.Log("Bomb exploded!");

        if (bombCollider != null) bombCollider.enabled = false;
        if (bombRigidbody != null) bombRigidbody.bodyType = RigidbodyType2D.Kinematic;

        Collider2D[] hitPackages = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hitPackages)
        {
            if (hit.gameObject == gameObject) continue;
            
            if(hit.CompareTag("Package"))
            {
                Destroy(hit.gameObject);
                Debug.Log("Package hit!");
            }
        }

        if (explosionFrames != null && explosionFrames.Length > 0 && spriteRenderer != null)
        {
            StartCoroutine(PlayExplosionAnimation());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PlayExplosionSound()
    {
        AudioClip explosion = Resources.Load<AudioClip>(explosionSound);

        if (explosion != null)
        {
            AudioSource.PlayClipAtPoint(explosion, transform.position, 1.0f);
            Debug.Log("Explosion sound played!");
        }
        else
        {
            Debug.Log("Failed to load explosion sound!");
        }
    }

    private IEnumerator PlayExplosionAnimation()
    {
        for (int i = 0; i < explosionFrames.Length; i++)
        {
            if(explosionFrames[i] != null)
            {
                spriteRenderer.sprite = explosionFrames[i];
            }

            yield return new WaitForSeconds(frameDuration);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }


}
