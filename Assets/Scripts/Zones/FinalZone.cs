using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalZone : MonoBehaviour
{
    public string finalZone = "FinalScene";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(finalZone);
        }
    }
}