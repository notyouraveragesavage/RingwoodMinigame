using UnityEngine;

public class PlayerDebug : MonoBehaviour
{
    void Update()
    {
        Debug.Log(
            "Player position: " + transform.position
        );
    }

    void OnDestroy()
    {
        Debug.LogError("PLAYER CAR WAS DESTROYED");
    }
}
