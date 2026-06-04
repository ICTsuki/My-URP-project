using UnityEngine;
using UnityEngine.InputSystem;
public class Zoom : MonoBehaviour
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  [SerializeField] private float zoomSpeed = 200.0f;
  public float minFOV = 20f;
  public float maxFOV = 90f;
  public InputActionAsset inputActions;
  public InputAction scrollAction;

  Camera cam;
  void Start()
  {
    cam = GetComponent<Camera>();
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

    cam.fieldOfView -= scroll.y * zoomSpeed * Time.deltaTime;
    cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFOV, maxFOV);
  }
}