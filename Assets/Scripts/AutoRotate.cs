using UnityEngine;

public class AutoRotate : MonoBehaviour
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  float turnSpeed = 20.0f;

  void Start()
  {
    transform.position = new Vector3(0, 0, 0);
  }

  // Update is called once per frame
  void Update()
  {
    transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
  }
}
