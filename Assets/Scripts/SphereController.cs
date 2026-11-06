using UnityEngine;
using UnityEngine.InputSystem;

public class SphereController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float dragGain = 0.02f;

    private Vector2 previousPosition;
    private Vector3 pendingImpulse;

    void Update()
    {
        // Ratón o dedo, lo que esté disponible
        var pointer = Pointer.current;
        if (pointer == null) return;

        Vector2 position = pointer.position.ReadValue();

        if (pointer.press.wasPressedThisFrame)
        {
            // Justo al empezar a pulsar: guardamos dónde empieza el arrastre
            previousPosition = position;
        }
        else if (pointer.press.isPressed)
        {
            // Mientras se mantiene pulsado: cuánto se ha movido desde el frame anterior
            Vector2 delta = position - previousPosition;

            // Movimiento horizontal en pantalla -> eje X; vertical en pantalla -> eje Z
            pendingImpulse += new Vector3(delta.x, 0f, delta.y) * dragGain;
            previousPosition = position;
        }
    }

    void FixedUpdate()
    {
        if (rigidBody == null) return;

        // Aplicamos el impulso acumulado en el paso de física
        rigidBody.AddForce(pendingImpulse, ForceMode.Impulse);
        pendingImpulse = Vector3.zero;
    }
}