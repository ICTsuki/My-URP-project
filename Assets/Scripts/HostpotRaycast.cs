using UnityEngine;
using UnityEngine.InputSystem;

public class HostpotRaycast : MonoBehaviour
{
  public InputActionAsset inputActions;
  public InputAction mouseClickAction;
  Camera cam;

  // Variables to track click vs drag
  private Vector2 clickPosition;
  private bool isDragging;

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
    cam = GetComponent<Camera>();
    mouseClickAction = InputSystem.actions.FindAction("UI/Click");
    isDragging = false;
  }

  void Update()
  {
    // 1. Record the position when the mouse button is first pressed
    if (mouseClickAction.WasPressedThisFrame())
    {
      clickPosition = Mouse.current.position.ReadValue();
      isDragging = true;
    }

    // 2. Evaluate the interaction only when the mouse button is released
    if (mouseClickAction.WasReleasedThisFrame())
    {
      isDragging = false;
    }
    // 3. Check if the distance moved is smaller than the drag threshold
    if (!isDragging)
    {
      Ray ray = cam.ScreenPointToRay(clickPosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit))
      {
        Hostpot hostpot = hit.collider.GetComponent<Hostpot>();
        if (hostpot != null)
        {
          hostpot.ChangeSkybox();
          cam.fieldOfView = 90f;
        }
      }
    }
  }
}