using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessImage : MonoBehaviour {

	public int width = 960;
	public int height = 600;    
	public static ProcessImage instance = null;
	public RenderTexture renderTexture;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}


	public byte[] captureFrame(Camera camera)
	{
		camera.targetTexture = renderTexture;
		RenderTexture.active = renderTexture;
		Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
		texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
		texture2D.Apply();
		byte[] image = texture2D.EncodeToJPG();
		Object.DestroyImmediate(texture2D);
		camera.targetTexture = null;
		return image;
	}

	private byte[] getBytesImage(Texture2D texture)
	{
		texture = resizeImage(texture);
		byte[] bytes = texture.EncodeToJPG();
		Object.DestroyImmediate(texture);
		return bytes;
	}

	private Texture2D resizeImage(Texture2D source)
	{
		source.filterMode = FilterMode.Point;
		RenderTexture rt = RenderTexture.GetTemporary(width, height);
		rt.filterMode = FilterMode.Point;
		RenderTexture.active = rt;
		Graphics.Blit(source, rt);
		Texture2D nTex = new Texture2D(width, height);
		nTex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		nTex.Apply();
		RenderTexture.active = null;
		return nTex;
	}

	private Texture2D captureTexture(Camera camera)
	{
		RenderTexture currentRT = RenderTexture.active;
		RenderTexture.active = camera.targetTexture;
		camera.targetTexture.width = width;
		camera.targetTexture.width = height;
		camera.Render();
		Texture2D image = new Texture2D(width, height, TextureFormat.RGB24, false);
		image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		image.Apply();
		RenderTexture.active = currentRT;
		return image;
	}

	IEnumerator WaitAndPrint() {
		yield return new WaitForEndOfFrame();
		print("WaitAndPrint " + Time.time);
	}

	public IEnumerator CaptureScreenShot( System.Action<byte[]> Callback){
		yield return new WaitForEndOfFrame();

		Texture2D texture = new Texture2D (Screen.width, Screen.height);
		texture.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0);
		texture.Apply ();
		byte[] image = getBytesImage(texture);

		Callback(image);
	}
		
}
