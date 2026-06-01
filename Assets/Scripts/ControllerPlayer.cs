using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movimiento del personaje")]
    public float velocidadMovimiento = 5f;
    public float velocidadCorrer = 10f;
    public float fuerzaSalto = 8f;
    public float gravedad = 9.8f;

    [Header("Control de la cámara")]
    public float sensibilidadMouse = 100f;

    private CharacterController controlador;
    private float rotacionX = 0f;
    private Vector3 direccionMovimiento;
    private float velocidadVertical = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controlador = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movimiento();
        RotacionCamara();
    }

    void Movimiento()
    {
        float speedMovement = Input.GetKey(KeyCode.LeftShift) ? velocidadCorrer : velocidadMovimiento;

        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = transform.right * movimientoHorizontal + transform.forward * movimientoVertical;
        direccionMovimiento = movimiento * speedMovement;

        if (controlador.isGrounded)
        {
            velocidadVertical = -gravedad * Time.deltaTime;

            if (Input.GetButtonDown("Jump"))
            {
                velocidadVertical = fuerzaSalto;
            }
        }
        else
        {
            velocidadVertical -= gravedad * Time.deltaTime;
        }

        direccionMovimiento.y = velocidadVertical;

        controlador.Move(direccionMovimiento * Time.deltaTime);
    }

    void RotacionCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f); 
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
    }
}
