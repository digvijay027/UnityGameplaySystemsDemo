using System.Collections;
using UnityEngine;

/// <summary>
/// Handles enemy hit flash and death effects.
/// Add to enemy prefab. Hooks into EnemyHealth events without modifying core gameplay code.
/// </summary>
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyVisuals : MonoBehaviour
{
    private Renderer enemyRenderer;
    private Color originalColor;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyRenderer = GetComponent<Renderer>();
        enemyHealth   = GetComponent<EnemyHealth>();
        originalColor = enemyRenderer.material.color;
    }

    private void OnEnable()
    {
        enemyHealth.OnHit  += HandleHit;
        enemyHealth.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        enemyHealth.OnHit  -= HandleHit;
        enemyHealth.OnDeath -= HandleDeath;
    }

    private void HandleHit()
    {
        if (EffectsManager.Instance != null)
            EffectsManager.Instance.PlayHitFlash(enemyRenderer, originalColor);
    }

    private void HandleDeath()
    {
        if (EffectsManager.Instance != null)
            EffectsManager.Instance.PlayDeathBurst(transform.position);
    }
}
