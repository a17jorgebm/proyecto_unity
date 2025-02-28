using UnityEngine;

// conten a funcionalidad de cada movemento do xogador
public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5f;

    //variables para a gravedad
    private Vector3 playerVelocity;
    private float gravity = -20f;
    private bool isGrounded;
    private bool isSprinting;
    private bool isCrouched;

    //saltos
    private float jumpHeight = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //por cada frame comprobamos se o personaje esta tocando o suelo e gardamolo pa poder saltar
        isGrounded = controller.isGrounded;
    }

    //recibe os inputs do InputManager e aplicaos ao personaje
    public void ProccessMove(Vector2 input){
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        //asi paso o movimiento de y a z, para que se mova para os lados non para arriba
        moveDirection.z = input.y;
        //movo o personaje, que porque fai falta facer o TransformDirection? pa que o personaje se mova na dirección na que esta mirando, xa que transforma o vector de local space a global space
        //- se non fixera esto o personaje podería estar mirando pa atras e ao darlle a avanzar iria ao revés
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        //actualizo a gravedad de caída, necesario chamalo aqui e non no update, xa que necesita ejecutarse ao mesmo tempo que o movimiento para que funcione
        UpdateFallingGravity();
    }

    public void Jump(){
        if(isGrounded){
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity); // kinematic equation for motion, para calcular a velocidad inicial
        }
    }

    public void Sprint(){
        if(isSprinting){
            speed/=2;
        }else{
            speed*=2;
        }
        isSprinting = !isSprinting;
    }

    public void Crouch(){
        if(isCrouched){
            controller.height *= 2;
        }else{
            controller.height /= 2;
        }
        isCrouched = !isCrouched;
    }

    //por cada frame comprobase se esta caendo, e se o esta cada vez cae mas rapido, se esta no suelo resetease a forza aplicada
    private void UpdateFallingGravity(){
        //controlar gravedad
        playerVelocity.y += gravity * Time.deltaTime; //ok por cada frame, sumolle a gravedad, multiplicandoa por deltatime para que sea igual en todos os pc. Efectivamente canto mais tempo caendo mais rapido cae
        if(isGrounded && playerVelocity.y < 0){ //se esta no suelo asignolle unha velocidad en y de -2
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime); //aplico a gravedad ao objeto, facendo que se mova esa distancia
    }
}
