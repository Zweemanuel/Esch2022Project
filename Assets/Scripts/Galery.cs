//THIS CODE IS MAINLY TAKEN WITH PERMISSION FROM THE YOUTUBER "www.rosspctraining.co.uk"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;

public class Galery : MonoBehaviour
{

	public Image image1;
	private Sprite[] SpriteArray;
	private int currentImage = 0;
	public float fadeTime = 2f;
	public bool fadefinished = false;
	private float fadetimer = 2f;


	//private float deltaTime = 0.0f;

	public float timer1 = 5.0f;
	private float timer1Remaining = 5.0f;
	public bool timer1IsRunning = true;

	// Start is called before the first frame update
	void Start()
	{
		SpriteArray = Galery.loadSprites();
		image1.canvasRenderer.SetAlpha(0.0f);
		image1.sprite = SpriteArray[currentImage];

		image1.CrossFadeAlpha(1, fadeTime, false);

		//bool timer1IsRunning = false;
		// timer1 should be bigger than fade time 
		timer1Remaining = timer1;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
#if UNITY_EDITOR
				if (EditorApplication.isPlaying)
				{
					EditorApplication.isPlaying = false;
				}

#else
			Application.Quit();
#endif
		}

		if (timer1IsRunning)
		{
			if (timer1Remaining > 0)
			{
				timer1Remaining -= Time.deltaTime;

				image1.CrossFadeAlpha(1, fadeTime, false);

			}

			else
			{
				UnityEngine.Debug.Log("Timer1 has finished!");
				timer1Remaining = timer1;
				fadefinished = true;
				image1.sprite = SpriteArray[currentImage];
				timer1IsRunning = !timer1IsRunning;
				fadetimer = fadeTime;

				image1.CrossFadeAlpha(0, fadeTime, false);
			}
		}
		else
		{
			if (fadetimer > 0)
			{
				fadetimer -= Time.deltaTime;
			}
			else
			{
				currentImage++;

				if (currentImage >= SpriteArray.Length)
					currentImage = 0;


				fade();
			}

		}


	}

	public void PreviousImage()
	{
		UnityEngine.Debug.Log("Pressed Left.");
		--currentImage;

		if (currentImage < 0)
			currentImage = SpriteArray.Length - 1;

		fade();
	}

	public void NextImage()
	{
		UnityEngine.Debug.Log("Pressed Right.");
		currentImage++;

		if (currentImage >= SpriteArray.Length)
			currentImage = 0;
		fade();
	}


	public void fade()
	{
		image1.canvasRenderer.SetAlpha(0.0f);
		image1.sprite = SpriteArray[currentImage];
		timer1Remaining = timer1;
		timer1IsRunning = true;
	}

	private static Sprite[] loadSprites()
    {
		List<Sprite> spriteList = new List<Sprite>();
		UnityEngine.Debug.Log(Application.persistentDataPath);
		var fileNames = Directory.GetFiles(Application.persistentDataPath);
		UnityEngine.Debug.Log(fileNames.Length);
		foreach (var fileName in fileNames)
        {
			var bytes = File.ReadAllBytes(fileName);
			Texture2D texture = new Texture2D(1, 1);
			texture.LoadImage(bytes);
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			spriteList.Add(sprite);
		}
		Sprite[] spriteArray = spriteList.ToArray();
		return spriteArray;
    }

}
