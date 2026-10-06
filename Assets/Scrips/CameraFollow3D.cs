using UnityEngine;

public class CameraFollow3D : MonoBehaviour
{
    public Transform player;

    // Distance from the player.
    public Vector3 offset = new Vector3(-8f, 3.5f, 58.1f);

    void LateUpdate()
    {
        if (player == null)
            return;

        transform.position = player.position + offset;

        // Keep the exact side-view rotation.
        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }
}
