using UnityEngine;

public class Hostpot : MonoBehaviour
{
  [SerializeField] private Material skyBox;
  [SerializeField] private GameObject returnHostpot;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }
  // Update is called once per frame
  void Update()
  {

  }

  public void ChangeSkybox()
  {
    RenderSettings.skybox = skyBox;
    returnHostpot.SetActive(true);
    this.gameObject.SetActive(false);
    
  }
}
