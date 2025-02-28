using UnityEngine;

//será a plantilla para todos os objectos interactuables
public abstract class Interactable : MonoBehaviour
{
    //o mensaje que se mostrará ao mirar un iteractuable
    public abstract string promptMessage { get; }

    //function a que chamara o xogador, facendoo así é muito mas facil meter funcionalidades extra despois
    // asi podo separar as funcionalidades que compartirán todos os interactable das funcions especificas de cada un
    public void BaseInteract(){
        Interact();
    }

    //funcion para que a sobreescriban as subclases
    protected virtual void Interact(){
        
    }
}
