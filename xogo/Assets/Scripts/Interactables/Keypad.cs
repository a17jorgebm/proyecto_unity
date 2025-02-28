using UnityEngine;

public class Keypad : Interactable
{
    public override string promptMessage => "Presiona E para abrir el cancelo";

    [SerializeField] GameObject door;
    bool isDoorOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        AudioSource audioSourcePuerta = door.GetComponent<BigDoor>().GetAudioSource();
        if(audioSourcePuerta != null){
            audioSourcePuerta.PlayOneShot(door.GetComponent<BigDoor>().GetOpeningSound());
        }
        isDoorOpen=!isDoorOpen;
        door.GetComponent<Animator>().SetBool("isOpen",isDoorOpen);
    }
}
