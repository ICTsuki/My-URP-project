using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;


public class Skybox6SidedLoader
{
  private string[] faceNames = { "Front", "Back", "Left", "Right", "Up", "Down" };
  private string[] shaderProperties = { "_FrontTex", "_BackTex", "_LeftTex", "_RightTex", "_UpTex", "_DownTex" };
  private Material skyboxMat;
  private MonoBehaviour runner;

  public Skybox6SidedLoader(MonoBehaviour runner)
  {
    this.runner = runner;
  }

  public Material Load6SidedSkybox(string folderName, int counter)
  {
    skyboxMat = new Material(Shader.Find("Skybox/6 Sided"));
    runner.StartCoroutine(LoadAllFaces(folderName, counter));
    return skyboxMat;
  }

  IEnumerator LoadAllFaces(string folderName, int counter)
  {
    
    string[] faces = new string[6];
    for (int i = 0; i < faceNames.Length; i++) faces[i] = faceNames[i] + counter.ToString() + ".jpg";

    for (int i = 0; i < 6; i++)
    {
      string filePath = Path.Combine(folderName, faces[i]);

      using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(filePath)) // using để tự động giải phóng tài nguyên sau khi xong
      {
        yield return uwr.SendWebRequest(); // Chờ cho đến khi tải xong

        if (uwr.result == UnityWebRequest.Result.Success)
        {
          Texture2D loadedTexture = DownloadHandlerTexture.GetContent(uwr);
          skyboxMat.SetTexture(shaderProperties[i], loadedTexture);

        }
        else
        {
          Debug.LogError("Error when loading faces " + faces[i] + ": " + uwr.error);
        }
      }
    }
  }
}