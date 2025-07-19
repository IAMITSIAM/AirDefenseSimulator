/*using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Airplane"))
        {
            Destroy(collision.gameObject); // Уничтожаем самолёт
            Destroy(gameObject);           // Уничтожаем пулю
        }
    }
}*/

using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Airplane"))
        {
            Debug.Log("Airplane hit!");

            Destroy(gameObject); // уничтожаем пулю

            // Сообщаем самолёту, что он сбит
            AirplaneHealth health = collision.gameObject.GetComponent<AirplaneHealth>();
            if (health != null)
            {
                health.GetHit();
            }
        }
    }
}