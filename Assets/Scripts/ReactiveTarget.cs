using System.Collections;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    public float moveDistance = 3f;
    public float moveSpeed = 2f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // ✅ Move only if it's a SNOWMAN (Avoid affecting enemies)
        if (gameObject.CompareTag("Snowman"))
        {
            float pingPongValue = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
            transform.position = new Vector3(startPosition.x + pingPongValue, startPosition.y, startPosition.z);
        }
    }

    public IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.5f);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.TargetDestroyed();
        }
        else
        {
            Debug.LogError("🚨 GameManager is missing! Target counter won't update.");
        }

        Destroy(gameObject);
    }



    public void ReactToHit()
    {
        WanderingAI behavior = GetComponent<WanderingAI>(); // ✅ Stop enemy AI when hit
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }
        StartCoroutine(Die());
    }
}
