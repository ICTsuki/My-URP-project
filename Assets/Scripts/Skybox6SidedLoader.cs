using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class Skybox6SidedLoader
{
  private string[] faceNames = { "Front", "Back", "Left", "Right", "Up", "Down" };
  private string[] shaderProperties = { "_FrontTex", "_BackTex", "_LeftTex", "_RightTex", "_UpTex", "_DownTex" };
  string dataPath = Application.dataPath;
  private MonoBehaviour runner;

  public Skybox6SidedLoader(MonoBehaviour runner)
  {
    this.runner = runner;
  }

  public Material Load6SidedSkybox(string folderName, int counter)
  {
    Material newSkyboxMat = new Material(Shader.Find("Skybox/6 Sided"));
    runner.StartCoroutine(LoadAllFaces(folderName, counter, newSkyboxMat));
    return newSkyboxMat;
  }

  IEnumerator LoadAllFaces(string folderName, int counter, Material targetMat)
  {
    string[] faces = new string[6];
    for (int i = 0; i < faceNames.Length; i++)
    {
      faces[i] = faceNames[i] + counter.ToString() + ".jpg";
    }

    for (int i = 0; i < 6; i++)
    {
      string filePath = "file:///" + Path.Combine(dataPath, folderName, faces[i]).Replace("\\", "/");

      using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(filePath))
      {
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.Success)
        {
          Texture2D loadedTexture = DownloadHandlerTexture.GetContent(uwr);
          loadedTexture.wrapMode = TextureWrapMode.Clamp;
          targetMat.SetTexture(shaderProperties[i], loadedTexture);
        }
        else
        {
          Debug.LogError("Error when loading faces " + faces[i] + " at " + filePath + ": " + uwr.error);
        }
      }
    }
  }
}