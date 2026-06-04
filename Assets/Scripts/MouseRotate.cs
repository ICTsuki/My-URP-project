using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRotate : MonoBehaviour
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  float turnSpeed = 100.0f;
  public InputActionAsset inputActions;
  public InputAction mouseDeltaAction;
  public InputAction mouseClickAction;

  float horizontal, vertical;

  void OnEnable()
  {
    inputActions.FindActionMap("UI").Enable();
  }

  void OnDisable()
  {
    inputActions.FindActionMap("UI").Disable();
  }
  void Start()
  {
    mouseDeltaAction = InputSystem.actions.FindAction("UI/MouseDelta");
    mouseClickAction = InputSystem.actions.FindAction("UI/Click");
    horizontal = transform.rotation.eulerAngles.y;
    vertical = transform.rotation.eulerAngles.x;
    transform.rotation = Quaternion.Euler(vertical, horizontal, 0);
  }

  // Update is called once per frame
  void Update()
  {
    if (mouseClickAction.IsPressed())
    {
      Vector2 mouseDelta = mouseDeltaAction.ReadValue<Vector2>();
      if (mouseDelta != Vector2.zero)
      {
        Debug.Log("Mouse Delta value: " + mouseDelta);
      }
      horizontal += mouseDelta.x * turnSpeed * Time.deltaTime;
      vertical -= mouseDelta.y * turnSpeed * Time.deltaTime;

      Quaternion rotation = Quaternion.Euler(vertical, horizontal, 0);

      transform.rotation = rotation;
    }
  }
}
