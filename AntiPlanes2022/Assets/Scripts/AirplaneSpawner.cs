/*using UnityEngine;

public class AirplaneSpawner : MonoBehaviour
{
    [Header("Префаб самолёта")]
    public GameObject airplanePrefab;

    [Header("Интервал спавна")]
    public float spawnInterval = 1f;

    [Header("Настройки диапазона и координат")]
    public float startZ = -50f;
    public float endZ = 50f;
    public Vector2 xRange = new Vector2(-10f, 10f);
    public Vector2 yRange = new Vector2(5f, 15f);
    public float speed = 10f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnAirplane();
            timer = 0f;
        }
    }

    void SpawnAirplane()
    {
        GameObject airplane = Instantiate(airplanePrefab);

        // Передаём параметры скрипту самолёта
        AirplaneFlight flight = airplane.GetComponent<AirplaneFlight>();
        if (flight != null)
        {
            flight.startZ = startZ;
            flight.endZ = endZ;
            flight.xRange = xRange;
            flight.yRange = yRange;
            flight.speed = speed;
        }
    }
}*/

using UnityEngine;

public class AirplaneSpawner : MonoBehaviour
{
    [Header("Префаб самолёта")]
    public GameObject airplanePrefab;

    [Header("Интервал спавна")]
    public float spawnInterval = 1f;

    [Header("Настройки диапазона и координат")]
    public float startZ = -50f;
    public float endZ = 50f;
    public Vector2 xRange = new Vector2(-10f, 10f);
    public Vector2 yRange = new Vector2(15f, 25f);
    public float speed = 10f;

    [Header("Ограничение количества самолётов")]
    public int maxAirplanes = 10;
    private int spawnedCount = 0;

    private float timer;

    void Update()
    {
        if (spawnedCount >= maxAirplanes)
            return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnAirplane();
            timer = 0f;
        }
    }

    void SpawnAirplane()
    {
        GameObject airplane = Instantiate(airplanePrefab);

        // Передаём параметры скрипту самолёта
        AirplaneFlight flight = airplane.GetComponent<AirplaneFlight>();
        if (flight != null)
        {
            flight.startZ = startZ;
            flight.endZ = endZ;
            flight.xRange = xRange;
            flight.yRange = yRange;
            flight.speed = speed;
            
        }
        spawnedCount++;
    }
}