using UnityEngine;


public class Manager : MonoBehaviour
{
  [SerializeField] private GameObject hotspot1;
  [SerializeField] private GameObject hotspot2;
  private Skybox6SidedLoader skyboxLoader;

  public TextAsset jsonFile;
  private SpaceViewList myData;

  void Awake()
  {
    myData = JsonUtility.FromJson<SpaceViewList>(jsonFile.text);
  }
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    skyboxLoader = new Skybox6SidedLoader();
    LoadSkybox();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void LoadSkybox()
  {
    if (myData != null && myData.spaceViews.Length > 0)
    {
      for (int i = 0; i < myData.spaceViews.Length; i++)
      {
        skyboxLoader.Load6SidedSkybox(myData.spaceViews[i].src);
      }
    }
  }

  private void CreateHotspots()
  {
    if (myData != null && myData.spaceViews.Length > 0)
    {
      for (int i = 0; i < myData.spaceViews.Length; i++)
      {
        for (int j = 0; j < myData.spaceViews[i].hotpots.Length; j++)
        {
          GameObject hotspot = Instantiate(hotspot1);
          hotspot.transform.position = new Vector3(0, 0, 0);
          hotspot.transform.localScale = JsonUtility.FromJson<Vector3>(myData.spaceViews[i].hotpots[j].scale);
        }
      }
    }
  }
}
