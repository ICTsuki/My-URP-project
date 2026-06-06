using System.Runtime.Serialization;
using UnityEngine;

public class Hostpot : MonoBehaviour
{
  public HotSpotData data;
  public Manager manager;
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
    Material newSkybox = manager.skyboxMaterials[data.transferSpaceId];
    RenderSettings.skybox = newSkybox;
    this.gameObject.SetActive(false);
    foreach(int id in manager.hotspotID)
{
    if(id != data.id)
    {
        manager.hotspotObj[id].SetActive(true);
    }
}
  }
}
