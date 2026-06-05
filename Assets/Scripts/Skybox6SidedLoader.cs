using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class Skybox6SidedLoader : MonoBehaviour
{
    private string[] faceNames = { "Front.jpg", "Back.jpg", "Left.jpg", "Right.jpg", "Up.jpg", "Down.jpg" };
    private string[] shaderProperties = { "_FrontTex", "_BackTex", "_LeftTex", "_RightTex", "_UpTex", "_DownTex" };

    public void Load6SidedSkybox(string folderName)
    {
        StartCoroutine(LoadAllFaces(folderName));
    }

    IEnumerator LoadAllFaces(string folderName)
    {
        // 1. Tạo một Material mới dùng shader 6 mặt
        Material skyboxMat = new Material(Shader.Find("Skybox/6 Sided"));
        
        // 2. Lặp qua 6 mặt để tải từng ảnh
        for (int i = 0; i < 6; i++)
        {
            string filePath = Path.Combine(folderName, faceNames[i]);
            if (!filePath.Contains("://")) filePath = "file://" + filePath;

            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(filePath)) // using để tự động giải phóng tài nguyên sau khi xong
            {
                yield return uwr.SendWebRequest(); // Chờ cho đến khi tải xong

                if (uwr.result == UnityWebRequest.Result.Success)
                {
                    Texture2D loadedTexture = DownloadHandlerTexture.GetContent(uwr);
                    
                    // 3. Gán ảnh vừa tải vào đúng mặt của Material
                    skyboxMat.SetTexture(shaderProperties[i], loadedTexture);
                }
                else
                {
                    Debug.LogError("Lỗi tải mặt " + faceNames[i] + ": " + uwr.error);
                }
            }
        }
    }
}