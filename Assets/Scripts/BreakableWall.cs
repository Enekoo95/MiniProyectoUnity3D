using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public void BreakWall()
    {
        Destroy(gameObject);  // Destruye la pared
    }
}
