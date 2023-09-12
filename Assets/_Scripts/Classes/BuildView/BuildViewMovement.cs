using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildViewMovement : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;
    private Vector3 PlayerMouseInput;
    private float xRot;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController Controller;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;

    void Update() {
        PlayerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer(){
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);

        Controller.Move(MoveVector * Speed * Time.deltaTime);
        Controller.Move(Velocity * Speed * Time.deltaTime);

        Velocity.y = 0f;
    }

    private void MovePlayerCamera(){
        xRot -= PlayerMouseInput.y * Sensitivity;
        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
    }
}
