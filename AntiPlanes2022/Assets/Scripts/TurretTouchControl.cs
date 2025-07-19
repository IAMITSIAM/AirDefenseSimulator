/*using UnityEngine;

public class TurretTouchControl : MonoBehaviour
{
    
    void Update()
    {
        Vector3? targetPoint = null;

#if UNITY_EDITOR  // Мышь — только в редакторе
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, transform.position);

            if (groundPlane.Raycast(ray, out float enter))
                targetPoint = ray.GetPoint(enter);
        }
#else  // Тач на телефоне
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            Plane groundPlane = new Plane(Vector3.up, transform.position);

            if (groundPlane.Raycast(ray, out float enter))
                targetPoint = ray.GetPoint(enter);
        }
#endif

        if (targetPoint.HasValue)
        {
            Vector3 dir = targetPoint.Value - transform.position;
            //dir.y = 0;

            if (dir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }
}*/

/*using UnityEngine;

public class TurretTouchControl : MonoBehaviour
{
    [SerializeField] float targetYaw;
    [SerializeField] float targetPitch;
    [SerializeField] Vector3 currentEuler;
    [SerializeField] Vector3 direction;
    [SerializeField] Vector3? targetPoint;

    [Header("Огневая система")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireCooldown = 0.5f;
    private float lastFireTime = -10f;

    [Header("Скорость поворота")]
    public float rotationSpeed = 10f;

    void Update()
    {
        targetPoint = null;

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, transform.position);
            if (groundPlane.Raycast(ray, out float enter))
                targetPoint = ray.GetPoint(enter);
        }

        if (Input.GetMouseButton(0) && Time.time - lastFireTime > fireCooldown)
        {
            Fire();
            lastFireTime = Time.time;
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            Plane groundPlane = new Plane(Vector3.up, transform.position);
            if (groundPlane.Raycast(ray, out float enter))
                targetPoint = ray.GetPoint(enter);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && Time.time - lastFireTime > fireCooldown)
            {
                Fire();
                lastFireTime = Time.time;
            }
        }
#endif

        if (targetPoint.HasValue)
        {
            direction = targetPoint.Value - transform.position;

            if (direction.sqrMagnitude > 0.001f)
            {
                // --- Вращение по Y ---
                targetYaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                // Сглаживаем поворот
                currentEuler = transform.rotation.eulerAngles;
                float smoothYaw = Mathf.LerpAngle(currentEuler.y, targetYaw, Time.deltaTime * rotationSpeed);
                float smoothPitch = Mathf.LerpAngle(NormalizeAngle(currentEuler.x), targetPitch, Time.deltaTime * rotationSpeed);

                transform.rotation = Quaternion.Euler(smoothPitch, smoothYaw, 0f);
            }
        }
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f) angle -= 360f;
        return angle;
    }

    void Fire()
    {
        if (projectilePrefab && firePoint)
        {
            Quaternion rotation = firePoint.rotation; // "заморозили" поворот
            Instantiate(projectilePrefab, firePoint.position, rotation);
        }
    }
}*/

using UnityEngine;

public class TurretTouchControl : MonoBehaviour
{
    [Header("Огневая система")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireCooldown = 0.5f;
    private float lastFireTime = -10f;

    [Header("Параметры вращения")]
    public float rotationSpeed = 0.2f; // чувствительность
    public float minPitch = -30f;
    public float maxPitch = 30f;

    private Vector2 startTouchPosition;
    private float currentYaw;
    private float currentPitch;

    private bool isTouching = false;

    void Start()
    {
        currentYaw = transform.eulerAngles.y;
        currentPitch = NormalizeAngle(transform.eulerAngles.x);
    }

    void Update()
    {
#if UNITY_EDITOR
        HandleMouse();
#else
        HandleTouch();
#endif
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isTouching = true;
            startTouchPosition = Input.mousePosition;
            TryFire();
        }
        else if (Input.GetMouseButton(0) && isTouching)
        {
            Vector2 currentPos = Input.mousePosition;
            Vector2 delta = currentPos - startTouchPosition;

            currentYaw += delta.x * rotationSpeed;
            currentPitch -= delta.y * rotationSpeed;
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

            transform.rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);

            startTouchPosition = currentPos; // обновляем позицию для следующего кадра

            TryFire();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isTouching = false;
        }
    }

    void HandleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isTouching = true;
                startTouchPosition = touch.position;
                TryFire();
            }
            else if (touch.phase == TouchPhase.Moved && isTouching)
            {
                Vector2 delta = touch.position - startTouchPosition;

                currentYaw += delta.x * rotationSpeed;
                currentPitch -= delta.y * rotationSpeed;
                currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

                transform.rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);

                startTouchPosition = touch.position; // обновляем

                TryFire();
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
            }
        }
    }

    void TryFire()
    {
        if (Time.time - lastFireTime > fireCooldown)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }

    void Fire()
    {
        if (projectilePrefab && firePoint)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }

    float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}