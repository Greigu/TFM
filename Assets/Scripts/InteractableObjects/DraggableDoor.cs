using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableDoor : MonoBehaviour
{
    public Transform door; // Objeto que representa la puerta.
    public Transform playerCamera; // Cámara del jugador en primera persona.
    public float minHeight = 0f; // Altura mínima de la puerta (cerrada).
    public float maxHeight = 3f; // Altura máxima de la puerta (abierta).
    public float sensitivity = 1f; // Sensibilidad del movimiento de la puerta.

    private bool isDragging = false; // Si el jugador está arrastrando la puerta.
    private float initialCameraRotationY; // Rotación inicial de la cámara en el eje Y.
    private float initialDoorHeight; // Altura inicial de la puerta.

    private void Start()
    {
        initialDoorHeight = door.position.y;
    }
    void Update()
    {
        // Detectar si el jugador presiona el botón para "agarrar" la puerta.
        if (Input.GetKeyDown(KeyCode.E)) // Cambia "E" si necesitas otro botón.
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == door)
                {
                    isDragging = true;
                    initialCameraRotationY = playerCamera.eulerAngles.x;
                    
                }
            }
        }

        // Si el jugador está arrastrando la puerta.
        if (isDragging)
        {
            // Calcular el cambio en la rotación de la cámara.
            float cameraRotationY = playerCamera.eulerAngles.x;

            // Manejar la transición de ángulos entre 0 y 360 grados.
            if (cameraRotationY > 180) cameraRotationY -= 360;
            if (initialCameraRotationY > 180) initialCameraRotationY -= 360;

            float rotationDelta = initialCameraRotationY - cameraRotationY;

            // Actualizar la altura de la puerta basándose en la rotación.
            float newHeight = initialDoorHeight + (rotationDelta * sensitivity);
            newHeight = Mathf.Clamp(newHeight, minHeight + initialDoorHeight, maxHeight + initialDoorHeight);

            door.position = new Vector3(door.position.x, newHeight, door.position.z);
        }

        // Soltar el botón para dejar de arrastrar.
        if (Input.GetKeyUp(KeyCode.E))
        {
            isDragging = false;
            if(door.position.y < initialDoorHeight + maxHeight)
            {

            }
        }
    }
}
