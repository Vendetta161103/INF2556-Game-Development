using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform[] obj;
    [SerializeField] private GameObject Square;
    [SerializeField] private GameObject Circle;
    [SerializeField] private GameObject Triangle;
    [SerializeField] private float rotation_speed = 4f;
    [SerializeField] private float move_speed = 5f;

    private InputSystem_Actions inputSystem;
    private Vector2 moveInput;

    private void Awake(){
        inputSystem = new InputSystem_Actions();
    }

    private void OnEnable(){
        inputSystem.Enable();
        inputSystem.Player.Square.performed += ctx => Debug.Log("Square Pressed");
        inputSystem.Player.Square.performed += ctx => Square.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        inputSystem.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputSystem.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputSystem.Player.Attack.performed += ctx => {Triangle.transform.Rotate(0, 0, 90f);};
    }

    private void Update(){
        foreach(Transform o in obj){
            o.Rotate(0, rotation_speed * Time.deltaTime, 0);
        }
        Circle.transform.position += new Vector3(moveInput.x, moveInput.y, 0) * Time.deltaTime * move_speed;
    }
}

