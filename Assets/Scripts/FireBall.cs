using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    private bool hasHit = false;
    private float lifetime = 5f; // ✅ Fireball lasts 5 seconds before disappearing

    private void Start()
    {
        Destroy(gameObject, lifetime); // ✅ Destroy fireball if it doesn't hit anything
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        hasHit = true;

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCharacter>()?.Hurt(damage);
        }

        Destroy(gameObject); // ✅ Destroy fireball after hitting something
    }
}
