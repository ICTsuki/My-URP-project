using UnityEngine;

public class AutoRotate : MonoBehaviour
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  float turnSpeed = 20.0f;

  private InputActionAsset inputActions;
  private InputAction mouseClickAction;

  void OnEnable()
  {
    inputActions.FindActionMap("UI").Enable();
  }

  void Start()
  {
    mouseClickAction = InputSystem.actions.FindAction("UI/Click");
    transform.position = new Vector3(0, 0, 0);
    transform.rotation = Quaternion.Euler(0, 0, 0);
  }

  // Update is called once per frame
  void Update()
  {
    if(!mouseClickAction.IsPressed())
    {
      transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
    }
  }
}
