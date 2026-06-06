using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Manager : MonoBehaviour
{
  public GameObject hotspotPrefab;
  private Skybox6SidedLoader skyboxLoader;
  public TextAsset jsonFile;
  private SpaceViewList myData;
  public Dictionary<int, Material> skyboxMaterials;
  public Dictionary<int, GameObject> hotspotObj;
  public HashSet<int> hotspotID;

  void Awake()
  {
    myData = JsonUtility.FromJson<SpaceViewList>(jsonFile.text);
    skyboxMaterials = new Dictionary<int, Material>();
    hotspotObj = new Dictionary<int, GameObject>();
  }
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    if(myData == null) throw new System.NullReferenceException();
    hotspotID = new HashSet<int>(myData.spaceViews[0].hotpots.Length + 1);
    skyboxLoader = new Skybox6SidedLoader(this);
    CreateHotspots();
    LoadSkybox();
    RenderSettings.skybox = skyboxMaterials[myData.spaceViews[0].id];
    int[] temp = new int[myData.spaceViews[0].hotpots.Length];
    for(int i = 0; i < temp.Length; i++) temp[i] = myData.spaceViews[0].hotpots[i].id;
    hotspotObj[hotspotID.Except(temp).First()].SetActive(false);
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
        Debug.Log(myData.spaceViews[i].src);
        skyboxMaterials[myData.spaceViews[i].id] = skyboxLoader.Load6SidedSkybox(myData.spaceViews[i].src, i+1);
        if(skyboxMaterials[myData.spaceViews[i].id] == null) throw new System.NullReferenceException();
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
          if(hotspotObj.ContainsKey(myData.spaceViews[i].hotpots[j].id)) continue;
          GameObject hotspot = Instantiate(hotspotPrefab);
          Hostpot hotspotComp = hotspot.GetComponent<Hostpot>();
          hotspot.transform.position = ParseCoordinates(myData.spaceViews[i].hotpots[j].coordinates);
          hotspot.transform.localScale = ParseScale(myData.spaceViews[i].hotpots[j].scale);
          hotspot.transform.rotation = ParseRotation(myData.spaceViews[i].hotpots[j].rotation);
          hotspotComp.data = myData.spaceViews[i].hotpots[j];
          hotspotObj[hotspotComp.data.id] = hotspot;
          hotspotComp.manager = this; 
          if(!hotspotID.Contains(hotspotComp.data.id)) hotspotID.Add(hotspotComp.data.id);
        }
      }
    }
  }
  private Vector3 ParseCoordinates(string coordinates)
  {
    if (string.IsNullOrEmpty(coordinates)) return Vector3.zero;

    coordinates = coordinates.Replace("[", "").Replace("]", "");

    string[] parts = coordinates.Split(',');
    if (parts.Length == 3)
    {
      float x = float.Parse(parts[0]);
      float y = float.Parse(parts[1]);
      float z = float.Parse(parts[2]);
      return new Vector3(x, y, z);
    }
    return Vector3.zero;
  }

  private Quaternion ParseRotation(string rotation)
  {
    if (string.IsNullOrEmpty(rotation)) return Quaternion.identity;

    rotation = rotation.Replace("[", "").Replace("]", "");

    string[] parts = rotation.Split(',');
    if (parts.Length == 3)
    {
      float x = float.Parse(parts[0]);
      float y = float.Parse(parts[1]);
      float z = float.Parse(parts[2]);
      return Quaternion.Euler(x, y, z);
    }
    return Quaternion.identity;
  }

  private Vector3 ParseScale(string scale)
  {
    if (string.IsNullOrEmpty(scale)) return Vector3.one;

    scale = scale.Replace("[", "").Replace("]", "");

    string[] parts = scale.Split(',');
    if (parts.Length == 3)
    {
      float x = float.Parse(parts[0]);
      float y = float.Parse(parts[1]);
      float z = float.Parse(parts[2]);
      return new Vector3(x, y, z);
    }
    return Vector3.one;
  }
}
