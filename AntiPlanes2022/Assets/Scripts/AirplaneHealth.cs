using UnityEngine;

public class AirplaneHealth : MonoBehaviour
{
    
    public int maxHits = 2;
    private int currentHits = 0;
    public bool isShotDown = false;
    public float fallSpeed = 5f;
    public float destroyDelay = 3f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    public void GetHit()
    {
        if (isShotDown) return;

        currentHits++;
        Debug.Log($"Airplane hit! Hits: {currentHits}");

        if (currentHits >= maxHits)
        {
            ShotDown();
        }
    }

    void ShotDown()
    {
        isShotDown = true;
        Debug.Log("Airplane shot down!");

        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        rb.useGravity = true;
        rb.isKinematic = false;
        rb.velocity = Vector3.down * fallSpeed;

        LevelManager.planesShotDown++;
        
        // Удалить самолёт через 4 секунды
        Destroy(gameObject, destroyDelay);
    }
}