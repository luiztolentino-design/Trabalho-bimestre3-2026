using System;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform interactPosition;
    
    private Animator anim;
    private bool isOpen;
    private bool _isInteractable;
    private bool isInteractable
    {
        get => _isInteractable; 
        set
        {
            _isInteractable = value; 
            InteractOM.Interactable(_isInteractable);
        }
    }
    

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInteractable)
        {
            isInteractable = true;
           // InteractOM.Interactable(isInteractable);
            
            InteractOM.OnInteract += AbreFechaPorta;
            InteractOM.InteractPosition(interactPosition.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && isInteractable)
        {
            isInteractable = false;
            //InteractOM.Interactable(isInteractable);
            
            InteractOM.OnInteract -= AbreFechaPorta;
        }
    }

    private void AbreFechaPorta()
    {
        if (!isOpen)
        {
            anim.StopPlayback();
            anim.Play("PortaAbrindo");
            isOpen = true;
        }
        else
        {
            anim.StopPlayback();
            anim.Play("PortaFechando");
            isOpen = false;
        }
    }
}
