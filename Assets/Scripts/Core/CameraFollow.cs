using UnityEngine;

/// <summary>
/// Smoothly follows a target (the Player) while preserving a fixed world-space offset.
/// The camera angle and framing are never modified at runtime — only position is interpolated.
///
/// Usage:
///   Attach to Main Camera. Assign Target. The offset is captured automatically from
///   the camera's position relative to the target at startup, so the existing scene
///   framing is always preserved.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("The transform to follow. Assign the Player.")]
    [SerializeField] private Transform target;

    [Header("Follow Settings")]
    [Tooltip("How quickly the camera catches up to the player. Higher = snappier. Recommended: 4–10.")]
    [SerializeField] [Range(1f, 20f)] private float followSmoothness = 6f;

    [Tooltip("World-space offset from the target. Captured automatically from the scene on Start. " +
             "Override only if you need a manual offset after placement.")]
    [SerializeField] private Vector3 positionOffset;

    [Tooltip("If true, offset is captured from the initial camera/target positions at runtime " +
             "instead of using the serialized positionOffset value.")]
    [SerializeField] private bool captureOffsetOnStart = true;

    // ── Private state ──────────────────────────────────────────────────────
    private bool hasTarget;

    // ── Unity lifecycle ────────────────────────────────────────────────────

    private void Start()
    {
        if (target == null)
        {
            // Auto-find Player by tag as a fallback.
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
                target = playerGO.transform;
        }

        hasTarget = target != null;

        if (!hasTarget)
        {
            Debug.LogWarning("[CameraFollow] No target assigned and no GameObject with tag 'Player' found.");
            return;
        }

        if (captureOffsetOnStart)
        {
            // Derive offset from existing scene placement so the framing is preserved exactly.
            positionOffset = transform.position - target.position;
        }
    }

    /// <summary>
    /// LateUpdate ensures the camera moves after all player/physics updates this frame,
    /// eliminating jitter caused by update-order conflicts.
    /// </summary>
    private void LateUpdate()
    {
        if (!hasTarget) return;

        Vector3 desiredPosition = target.position + positionOffset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSmoothness * Time.deltaTime
        );
    }

#if UNITY_EDITOR
    /// <summary>Visualise the follow target and offset in the Scene view.</summary>
    private void OnDrawGizmosSelected()
    {
        if (target == null) return;
        Gizmos.color = new Color(0f, 0.85f, 1f, 0.6f);
        Gizmos.DrawLine(transform.position, target.position);
        Gizmos.DrawWireSphere(target.position, 0.4f);
    }
#endif
}
