using UnityEngine;
using UnityEngine.Rendering;

public class VolumeTrigger : MonoBehaviour
{
    private Volume volume;

    private void Start()
    {
        volume = GetComponent<Volume>();
        if (volume != null)
            volume.weight = 0f; // Comienza apagado
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeVolumeWeight(1f, 0.5f)); // Aumentar peso a 1 en 0.5s
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeVolumeWeight(0f, 0.5f)); // Bajar peso a 0 en 0.5s
        }
    }

    private System.Collections.IEnumerator FadeVolumeWeight(float targetWeight, float duration)
    {
        float startWeight = volume.weight;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            volume.weight = Mathf.Lerp(startWeight, targetWeight, elapsed / duration);
            yield return null;
        }
        volume.weight = targetWeight;
    }
}
