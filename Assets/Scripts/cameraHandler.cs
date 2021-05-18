using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cameraHandler : MonoBehaviour
{

	private bool camAvailable;
	private WebCamTexture cameraTexture;
	private Texture defaultBackground;

	public RawImage background;
	public AspectRatioFitter fit;
	public bool frontFacing=false;
	WebCamDevice back, front;
	void Start()
	{
		// Change orientation because of Unity
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		defaultBackground = background.texture;
		WebCamDevice[] devices = WebCamTexture.devices;
		back = devices[0];front = devices[1];

		if (devices.Length == 0)
		{
			Debug.Log("no camera detected");
			camAvailable = false;
			return;
		}


		for (int i = 0; i < devices.Length; i++)
		{

			if (devices[i].isFrontFacing == frontFacing) // Front cam and rotation switched with this later
			{
				cameraTexture = new WebCamTexture(devices[0].name, Screen.width, Screen.height);
				break;
			}
		}

		if (cameraTexture == null)
		{
			Debug.Log("no camera available");
			return;
		}


		cameraTexture.Play(); // starting the camera
		background.texture = cameraTexture; // setting the texture

		camAvailable = true;
	}

	public void switchCam()// switches from front to back and back again and rotation because of Unity...
    {
		cameraTexture.Stop();
		if (!frontFacing)
        {
			frontFacing = !frontFacing;
			cameraTexture = new WebCamTexture(front.name, Screen.width, Screen.height);
		}
        else
        {
			frontFacing = !frontFacing;
			cameraTexture = new WebCamTexture(back.name, Screen.width, Screen.height);
		}
        
		cameraTexture.Play();
		background.texture = cameraTexture;

	}


	void Update()
	{
		
		float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
		fit.aspectRatio = ratio; // Set the aspect ratio

		float scaleY = cameraTexture.videoVerticallyMirrored ? -1f : 1f; // Set the value depending if the cam is mirrored or not
		float scaleX = frontFacing ? -1f : 1f;// flips the image if the cam is front facing.. thx Unity

		background.rectTransform.localScale = new Vector3(scaleX, scaleY, 1f); // swapping the mirrored cam

		int orient = -cameraTexture.videoRotationAngle;
		background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

	}
	void OnDestroy()
	{
		// Change orientation back
		Screen.orientation = ScreenOrientation.Portrait;
	}
}