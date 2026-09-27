using System.Collections;
using UnityEngine;

/// <summary>
/// Attach to the Player. Triggers a muzzle flash effect at the fire point on each shot.
/// Listens for shoot events via PlayerShooter without modifying PlayerShooter.
/// </summary>
public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    private PlayerShooter shooter;

    private void Awake()
    {
        shooter = GetComponent<PlayerShooter>();
    }

    private void OnEnable()
    {
        if (shooter != null)
            shooter.OnShoot += HandleShoot;
    }

    private void OnDisable()
    {
        if (shooter != null)
            shooter.OnShoot -= HandleShoot;
    }

    private void HandleShoot()
    {
        if (EffectsManager.Instance == null || firePoint == null) return;
        EffectsManager.Instance.PlayMuzzleFlash(firePoint.position);
    }
}
