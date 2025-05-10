using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocità del movimento
    private Vector3 targetPosition;  // Posizione di destinazione
    private bool isMoving = false;  // Flag per determinare se il player è in movimento

    // Limiti di movimento (da impostare in base alla tua scena)
    public float minX, maxX, minY, maxY;

    void Start()
    {
        targetPosition = transform.position;  // Il player inizia alla sua posizione iniziale
    }

    void Update()
    {
        HandleClickMovement();  // Gestisce il movimento del player al clic del mouse
        MoveToTarget();  // Muove il player verso la destinazione
    }

    void HandleClickMovement()
    {
        // Se il mouse sinistro viene cliccato
        if (Input.GetMouseButtonDown(0))
        {
            // Ottieni la posizione del mouse nel mondo
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;  // Imposta la Z a 0 per evitare spostamenti in profondità

            // Imposta la nuova destinazione del player
            targetPosition = mouseWorldPos;

            // Inizia il movimento
            isMoving = true;
        }
    }

    void MoveToTarget()
    {
        // Se il player è in movimento
        if (isMoving)
        {
            // Muovi il player verso la destinazione
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Se il player ha raggiunto la destinazione
            if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
            {
                transform.position = targetPosition;  // Imposta la posizione finale
                isMoving = false;  // Ferma il movimento
            }
        }

        // Limita il movimento all'interno dei confini definiti
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );
    }
}
