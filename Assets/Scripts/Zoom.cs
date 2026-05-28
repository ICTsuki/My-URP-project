using UnityEngine;
using UnityEngine.InputSystem;
public class Zoom : MonoBehaviour
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  float zoomSpeed = 10.0f;
  public float minZoom = 5.0f;
  public float maxZoom = 20.0f;
  public InputActionAsset inputActions;
  public InputAction scrollAction;

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
    scrollAction = InputSystem.actions.FindAction("UI/ScrollWheel");
  }

  // Update is called once per frame
  void Update()
  {
    Vector2 scroll = scrollAction.ReadValue<Vector2>();
    if (scroll != Vector2.zero)
    {
      Debug.Log("Scroll value: " + scroll);
    }
    Vector3 pos = transform.position;

    pos.z += -scroll.y * zoomSpeed * Time.deltaTime;

    pos.z = Mathf.Clamp(pos.z, minZoom, maxZoom);

    transform.position = pos;
  }
}