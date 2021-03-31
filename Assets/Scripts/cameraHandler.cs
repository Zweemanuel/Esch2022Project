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
	public bool frontFacing;


	void Start()
	{
		// Change orientation because of Unity
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		defaultBackground = background.texture;
		WebCamDevice[] devices = WebCamTexture.devices;

		if (devices.Length == 0)
		{
			Debug.Log("no camera detected");
			camAvailable = false;
			return;
		}


		for (int i = 0; i < devices.Length; i++)
		{

			if (devices[i].isFrontFacing == frontFacing) // Front cam could be used later in development
			{
				cameraTexture = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
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


	void Update()
	{
		if (!camAvailable)
			return;

		float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
		fit.aspectRatio = ratio; // Set the aspect ratio

		float scaleY = cameraTexture.videoVerticallyMirrored ? -1f : 1f; // Set the value depending if the cam is mirrored or not
		background.rectTransform.localScale = new Vector3(1f, scaleY, 1f); // swapping the mirrored cam

		int orient = -cameraTexture.videoRotationAngle;
		background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

	}
	void OnDestroy()
	{
		// Change orientation back
		Screen.orientation = ScreenOrientation.Portrait;
	}
}