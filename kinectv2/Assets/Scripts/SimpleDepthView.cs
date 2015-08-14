using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class SimpleDepthView : MonoBehaviour {
	
	public GameObject depthSourceManager;
	private DepthSourceManager depthSourceManagerScript;
	
	Texture2D texture;
	byte[] depthBitmapBuffer;
	FrameDescription depthFrameDesc;
	
	public float scale = 1.0f;
	
	void Start () {
		// Get the description of the depth frames.
		depthFrameDesc = KinectSensor.GetDefault().DepthFrameSource.FrameDescription;
		
		// get reference to DepthSourceManager (which is included in the distributed 'Kinect for Windows v2 Unity Plugin zip')
		depthSourceManagerScript = depthSourceManager.GetComponent<DepthSourceManager> ();
		
		// allocate.
		depthBitmapBuffer = new byte[depthFrameDesc.LengthInPixels * 3];
		texture = new Texture2D(depthFrameDesc.Width, depthFrameDesc.Height, TextureFormat.RGB24, false);
		
		// arrange size of gameObject to be drawn
		gameObject.transform.localScale = new Vector3 (scale * depthFrameDesc.Width / depthFrameDesc.Height, scale, 1.0f);
	}
	
	void Update () {
		updateTexture ();
		gameObject.GetComponent<Renderer>().material.mainTexture = texture;
	}
	
	// referred the code below. thx kaorun55-san
	// https://github.com/kaorun55/Kinect-for-Windows-SDK-v2.0-Samples/blob/master/C%23(WinRT)/02_Depth/KinectV2-Depth-01/KinectV2/MainPage.xaml.cs
	
	void updateTexture() {
		// get new depth data from DepthSourceManager.
		ushort[] rawdata = depthSourceManagerScript.GetData ();
		
		// convert to byte data (
		for ( int i = 0; i < rawdata.Length; i++ ) {
			// 0-8000を0-256に変換する
			byte value = (byte)(rawdata[i] * 255 / 8000);
			
			int colorindex = i * 3;
			depthBitmapBuffer[colorindex + 0] = value;
			depthBitmapBuffer[colorindex + 1] = value;
			depthBitmapBuffer[colorindex + 2] = value;
		}
		
		// make texture from byte array
		texture.LoadRawTextureData (depthBitmapBuffer);
		texture.Apply ();
	}
}