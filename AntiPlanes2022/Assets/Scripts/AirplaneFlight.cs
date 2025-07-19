using UnityEngine;

public class AirplaneFlight : MonoBehaviour
{
    [Header("Z-координаты старта и финиша")]
    public float startZ = -50f;
    public float endZ = 50f;

    [Header("Диапазон случайного X и Y")]
    public Vector2 xRange = new Vector2(-10f, 10f);
    public Vector2 yRange = new Vector2(5f, 15f);

    [Header("Скорость полёта")]
    public float speed = 10f;

    private Vector3 targetPosition;

    void Start()
    {
        // Выбираем случайные координаты X и Y
        float randomX = Random.Range(xRange.x, xRange.y);
        float randomY = Random.Range(yRange.x, yRange.y);

        // Устанавливаем начальную позицию
        transform.position = new Vector3(randomX, randomY, startZ);

        // Задаём конечную точку
        targetPosition = new Vector3(randomX, randomY, endZ);
    }

    void Update()
    {
        // Движение к конечной точке
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Если достигли цели — удаляем объект
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            Destroy(gameObject);
            LevelManager.gameOver = true;
        }
    }
}

/*using UnityEngine;

public class AirplaneFlight : MonoBehaviour
{
    public float startZ;
    public float endZ;
    public Vector2 xRange;
    public Vector2 yRange;
    public float speed = 10f;

    public GameObject canvasGameOver;

    private Vector3 direction;

    void Start()
    {
        float x = Random.Range(xRange.x, xRange.y);
        float y = Random.Range(yRange.x, yRange.y);
        transform.position = new Vector3(x, y, startZ);

        direction = new Vector3(0, 0, 1); // движение по оси Z
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Проверка: достиг конца и выше 10 по Y
        if (transform.position.z >= endZ && transform.position.y > 10f)
        {
            if (canvasGameOver != null)
            {
                canvasGameOver.SetActive(true);
            }

            // Можно уничтожить самолёт или остановить
            Destroy(gameObject);
        }
    }
}*/