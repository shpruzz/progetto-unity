using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Riferimento al player
    public float smoothSpeed = 0.125f;  // Velocit� di follow
    public Vector3 offset;  // Distanza tra la camera e il player

    void FixedUpdate()
    {
        // Calcola la posizione desiderata della camera
        Vector3 desiredPosition = player.position + offset;

        // Applica un movimento pi� fluido della camera verso la posizione desiderata
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Imposta la posizione della camera
        transform.position = smoothedPosition;
    }
}
