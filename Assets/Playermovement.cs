using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;  // Velocità del movimento
    private Vector3 targetPosition; // Posizione di destinazione
    private bool isMoving = false;  // Controllo per sapere se il personaggio sta ancora muovendosi
    private Vector3 lastPosition;  // Ultima posizione del personaggio per evitare movimenti strani

    void Start()
    {
        targetPosition = transform.position;  // Impostiamo la posizione iniziale del personaggio
        lastPosition = transform.position;    // Inizializziamo l'ultima posizione
    }

    void Update()
    {
        // Rileviamo se il tasto sinistro del mouse è premuto
        if (Input.GetMouseButton(0)) // 0 corrisponde al tasto sinistro del mouse
        {
            // Otteniamo la posizione del mouse nel mondo
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0; // Rimuoviamo la componente Z, non ci serve in questo caso

            // "Snappiamo" la posizione alla griglia isometrica
            targetPosition = SnapToGrid(mouseWorldPosition);

            // Se il target è diverso dall'ultima posizione, dobbiamo muovere il personaggio
            if (targetPosition != lastPosition)
            {
                isMoving = true;
                lastPosition = targetPosition;  // Aggiorniamo la posizione
            }
        }

        // Se il personaggio sta ancora muovendosi, lo spostiamo verso la destinazione
        if (isMoving)
        {
            // Movimento fluido usando Lerp per una transizione più morbida
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Se il personaggio ha raggiunto la posizione di destinazione, fermiamo il movimento
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f) // Una piccola tolleranza
            {
                isMoving = false; // Fermiamo il movimento
            }
        }
    }

    // Funzione per "snappare" la posizione sulla griglia isometrica
    Vector3 SnapToGrid(Vector3 position)
    {
        // Arrotondiamo la posizione a una griglia isometrica 2D
        // A seconda delle dimensioni della griglia, puoi modificare la costante di arrotondamento
        float gridSize = 1.0f; // Puoi anche cambiare questa costante in base alla tua griglia

        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float y = Mathf.Round(position.y / gridSize) * gridSize;

        return new Vector3(x, y, 0);
    }
}
