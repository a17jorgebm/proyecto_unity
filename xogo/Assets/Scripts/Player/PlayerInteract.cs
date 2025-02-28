using System;
using UnityEngine;

/*
Uso Ray, que basicamente crea unnha linea recta dende a camara do personaje, que uso para detectar colisions con objetos interactuables.
- Debug.DrawRay(); para debugear como se ven os ray
*/
public class PlayerInteract : MonoBehaviour
{
    Camera cam;
    [SerializeField] float rayDistance = 3f;

    [SerializeField] LayerMask layerMask; //relacionaremolo coa nova layer que creamos en Unity, Interactable

    PlayerUi playerUi;

    [SerializeField] InputManager inputManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<PlayerLook>().lookCamera;
        playerUi = GetComponent<PlayerUi>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUi.UpdateText(String.Empty);
        //creo un rayo que sale dende a camara e vai dirigido a onde estou mirando, infinitamente
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance);

        RaycastHit hitInfo; //para gardar a información da colision

        /*RayCast devolve un boolean se o Ray tivo unha colision, polo que o podemos meter nun if:
            Pasamoslle:
                - o Ray
                - un RayCastHit, que gracias a anotacion out, a funcion RayCastHit vai gardar a info do hit nesta variable
                - a distancia que terá o Ray
                - a layerMask, para indicar a layer na que se van detectar as colisions, os objetos que estean en outra layer non os vai detectar
        */
        if(Physics.Raycast(ray, out hitInfo, rayDistance, layerMask)){
            Interactable componentHit = hitInfo.collider.GetComponent<Interactable>();
            if(componentHit != null){
                playerUi.UpdateText(componentHit.promptMessage);
                if(inputManager.onFoot.Interact.triggered){
                    componentHit.BaseInteract(); //chamo a función base, que chama a función de interacción da subclase
                }
            }
        }
    }
}
