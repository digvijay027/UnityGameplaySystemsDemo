using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Displays a brief centered "WAVE X" announcement when a new wave begins.
/// Requires a full-screen TextMeshProUGUI element passed in via Inspector.
/// </summary>
public class WaveAnnouncer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI announcementText;
    [SerializeField] private float displayDuration = 1.8f;
    [SerializeField] private EnemySpawner enemySpawner;

    private int lastKnownWave;

    private void Start()
    {
        if (announcementText != null)
            announcementText.gameObject.SetActive(false);

        lastKnownWave = 0;
    }

    private void Update()
    {
        if (enemySpawner == null) return;

        int currentWave = enemySpawner.CurrentWave;
        if (currentWave > lastKnownWave)
        {
            lastKnownWave = currentWave;
            ShowWaveAnnouncement(currentWave);
        }
    }

    private void ShowWaveAnnouncement(int wave)
    {
        StopAllCoroutines();
        StartCoroutine(AnnounceRoutine(wave));
    }

    private IEnumerator AnnounceRoutine(int wave)
    {
        if (announcementText == null) yield break;

        announcementText.text = $"WAVE {wave}";
        announcementText.gameObject.SetActive(true);

        // Fade in
        float elapsed = 0f;
        float fadeIn = 0.25f;
        while (elapsed < fadeIn)
        {
            elapsed += Time.unscaledDeltaTime;
            announcementText.alpha = Mathf.Clamp01(elapsed / fadeIn);
            yield return null;
        }

        announcementText.alpha = 1f;
        yield return new WaitForSecondsRealtime(displayDuration - fadeIn * 2f);

        // Fade out
        elapsed = 0f;
        while (elapsed < fadeIn)
        {
            elapsed += Time.unscaledDeltaTime;
            announcementText.alpha = Mathf.Clamp01(1f - elapsed / fadeIn);
            yield return null;
        }

        announcementText.gameObject.SetActive(false);
    }
}
