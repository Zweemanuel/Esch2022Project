using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class albumPreview : MonoBehaviour
{
    [SerializeField]
    GameObject picturePanel;
    [SerializeField]
    Sprite defaultImage;
    string[] files = null;
    int screenShotShown = 0;

    // Start is called before the first frame update
    void Start()
    {
        picturePanel.GetComponent<Image>().sprite = defaultImage;
        files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
        if (files.Length > 0)
        {
            showPicture();
        }
    }

	void showPicture()
	{
		string pathToFile = files[screenShotShown];
		Texture2D texture = GetScreenshotImage(pathToFile);
		Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
			new Vector2(0.5f, 0.5f));
		picturePanel.GetComponent<Image>().sprite = sp;
	}

	Texture2D GetScreenshotImage(string filePath)
	{
		Texture2D texture = null;
		byte[] fileBytes;
		if (File.Exists(filePath))
		{
			fileBytes = File.ReadAllBytes(filePath);
			texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
			texture.LoadImage(fileBytes);
		}
		return texture;
	}

	public void DeleteImage()
	{
		if (files.Length > 0)
		{
			string pathToFile = files[screenShotShown];
			if (File.Exists(pathToFile))
				File.Delete(pathToFile);
			files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
			if (files.Length > 0)
				NextPicture();
			else
				picturePanel.GetComponent<Image>().sprite = defaultImage;
		}
	}

	public void NextPicture()
	{
		if (files.Length > 0)
		{
			screenShotShown += 1;
			if (screenShotShown > files.Length - 1)
				screenShotShown = 0;
			showPicture();
		}
	}

	public void PreviousPicture()
	{
		if (files.Length > 0)
		{
			screenShotShown -= 1;
			if (screenShotShown < 0)
				screenShotShown = files.Length - 1;
			showPicture();
		}
	}
}
