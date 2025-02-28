using UnityEngine;

//TODO
// - facer o de poder agacharse
// -esprintar
public class InputManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;

    //uso Awake porque é mellor que Start() para inicializar objetos, sobretodo se dependen uns de outros
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        //cada vez que se ejecuta unha accion de salto, correr... chamamos a funcion correspondiente do playermotor mediante un callback
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Crouch.performed += ctx => motor.Crouch();
    }

    // Update is called once per frame
    void Update(){
        motor.ProccessMove(onFoot.Movement.ReadValue<Vector2>());
    }


    //fixedUpdate ejecutase xunto co motor de fisicas, nun tempo constante, polo que non depende dos frames como si o fai Update()
    //  asi que para cousas que afectan as fisicas mellor facelo aquí, pero o movimiento do xogador é mais fluído se se fai no Update()
    void FixedUpdate(){
        //decimoslle ao PlayerMotor que se mova usando os valores do input
        //motor.ProccessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    // ejecutase despois do update, recomendase facer o reposicionamento da camara aqui e o reposicionamiento do personaje no update, asi cando chegue aqui o personaje xa se acabou de mover
    void LateUpdate(){
        look.ProcessCameraRotation(onFoot.Look.ReadValue<Vector2>());
    }

    //fan que solo se activen os controles cando o GameObject esta activo
    private void OnEnable(){
        onFoot.Enable();
    }

    private void OnDisable(){
        onFoot.Disable();
    }
}
