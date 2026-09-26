using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        AimAtMouse();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void AimAtMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float distance;

        if (groundPlane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;

            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                transform.forward = direction.normalized;
            }
        }
    }

    private void Shoot()
    {
        Projectile projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );
    }
}