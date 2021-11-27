using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Controls : MonoBehaviour
{
    public Transform characterModel;
    private CharacterController character;
    [Range(0,10)]
    public float walkingSpeed = 1;


    private Vector2 inputSpeed = Vector2.zero;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    { 
        controller = GetComponent<CharacterController>();
    }


    
    public void MoveDirection(InputAction.CallbackContext context)
    {
        inputSpeed = context.ReadValue<Vector2>();
        Debug.Log($"{context.control.name} Move ");
    }
    
    
    void Update()
    {
        // Move forward / backward
        controller.SimpleMove(new Vector3(inputSpeed.x, 0, inputSpeed.y) * walkingSpeed);
        if(inputSpeed != Vector2.zero)
        characterModel.LookAt(characterModel.position + new Vector3(inputSpeed.x, 0, inputSpeed.y));
    }
}
