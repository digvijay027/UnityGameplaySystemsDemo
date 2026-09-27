using System.Collections;
using UnityEngine;

/// <summary>
/// Centralised lightweight effects manager.
/// Spawns transient particle bursts for hit, death, and muzzle flash events.
/// All effects use primitive Unity particles — no asset dependencies.
/// </summary>
public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance { get; private set; }

    [Header("Effect Durations")]
    [SerializeField] private float hitFlashDuration  = 0.08f;
    [SerializeField] private float deathBurstDuration = 0.4f;
    [SerializeField] private float muzzleFlashDuration = 0.06f;

    [Header("Colors")]
    [SerializeField] private Color hitFlashColor    = new Color(1f, 0.3f, 0.1f);
    [SerializeField] private Color deathBurstColor  = new Color(1f, 0.4f, 0f);
    [SerializeField] private Color muzzleColor      = new Color(1f, 0.95f, 0.3f);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // ── Public API ─────────────────────────────────────────────────────────

    /// <summary>Flash an enemy renderer on hit.</summary>
    public void PlayHitFlash(Renderer renderer, Color originalColor)
    {
        if (renderer == null) return;
        StartCoroutine(HitFlashRoutine(renderer, originalColor));
    }

    /// <summary>Spawn a small burst particle at position to signal death.</summary>
    public void PlayDeathBurst(Vector3 position)
    {
        StartCoroutine(SpawnBurst(position, deathBurstColor, 12, 4f, deathBurstDuration));
    }

    /// <summary>Spawn a quick muzzle flash at a fire point.</summary>
    public void PlayMuzzleFlash(Vector3 position)
    {
        StartCoroutine(SpawnBurst(position, muzzleColor, 5, 2.5f, muzzleFlashDuration));
    }

    /// <summary>Spawn a small impact flash at a hit point.</summary>
    public void PlayImpact(Vector3 position)
    {
        StartCoroutine(SpawnBurst(position, hitFlashColor, 6, 2f, 0.15f));
    }

    // ── Private Coroutines ─────────────────────────────────────────────────

    private IEnumerator HitFlashRoutine(Renderer renderer, Color originalColor)
    {
        if (renderer == null) yield break;
        renderer.material.color = hitFlashColor;
        yield return new WaitForSeconds(hitFlashDuration);
        if (renderer != null)
            renderer.material.color = originalColor;
    }

    private IEnumerator SpawnBurst(Vector3 pos, Color color, int count, float speed, float lifetime)
    {
        // Use a temporary particle system for a zero-dependency burst
        GameObject go = new GameObject("_Burst");
        go.transform.position = pos;

        ParticleSystem ps = go.AddComponent<ParticleSystem>();

        // Stop auto-play so we can configure first
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        var main = ps.main;
        main.startLifetime     = lifetime;
        main.startSpeed        = speed;
        main.startSize         = 0.18f;
        main.startColor        = color;
        main.maxParticles      = count + 2;
        main.loop              = false;
        main.playOnAwake       = false;
        main.gravityModifier   = 0.5f;

        var emission = ps.emission;
        emission.enabled = false;

        var shape = ps.shape;
        shape.enabled      = true;
        shape.shapeType    = ParticleSystemShapeType.Sphere;
        shape.radius       = 0.3f;

        ps.Emit(count);

        Destroy(go, lifetime + 0.1f);
        yield break;
    }
}
