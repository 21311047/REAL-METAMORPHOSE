using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System;
public class DepthParticlize_colorchange : MonoBehaviour
{

    // FROM KINECT v2
    ColorFrameReader color_reader;
    public GameObject depthSourceManager;
    DepthSourceManager depthSourceManagerScript;
    ColorSourceManager colorSourceManagerScript;
    FrameDescription depthFrameDesc;
    CameraSpacePoint[] cameraSpacePoints;
    CoordinateMapper mapper;
    public GameObject colorSourceManager;
    FrameDescription colorFrameDesc;
    ColorSpacePoint[] colorSpacePoints;
    private int depthWidth;
    private int depthHeight;
    byte[] color_array;
    //KinectSensor Kinect;
    // PARTICLE SYSTEM
    private ParticleSystem.Particle[] particles;
    FrameDescription colorDesc;
    // DRAW CONTROL
    public Color color = Color.white;
    public float size = 0.2f;
    public float scale = 10f;
    private bool CHECK_GETDATA = false;
    void Start()
    {

        depthFrameDesc = KinectSensor.GetDefault().DepthFrameSource.FrameDescription;
        depthWidth = depthFrameDesc.Width;
        depthHeight = depthFrameDesc.Height;
        // buffer for points mapped to camera space coordinate.
        mapper = KinectSensor.GetDefault().CoordinateMapper;
        cameraSpacePoints = new CameraSpacePoint[depthWidth * depthHeight];
        depthSourceManagerScript = depthSourceManager.GetComponent<DepthSourceManager>();
        colorSourceManagerScript = colorSourceManager.GetComponent<ColorSourceManager>();
        particles = new ParticleSystem.Particle[depthWidth * depthHeight];
        color_reader = KinectSensor.GetDefault().ColorFrameSource.OpenReader();
        colorFrameDesc = KinectSensor.GetDefault().ColorFrameSource.CreateFrameDescription(ColorImageFormat.Rgba);
        colorSpacePoints = new ColorSpacePoint[depthWidth * depthHeight];
        color_array = new byte[colorFrameDesc.BytesPerPixel * colorFrameDesc.LengthInPixels];

    }

    void Update()
    {

        // get new depth data from DepthSourceManager.
        ushort[] rawdata = depthSourceManagerScript.GetData();
        Debug.Log(rawdata.Length);
        if (color_reader == null)
            Debug.Log("error");
        if (color_reader != null)
        {
            var Reference = color_reader.AcquireLatestFrame();
            //Debug.Log(color_reader.AcquireLatestFrame());
           // if (Reference == null)
            //{
            //    Reference = ColorSourceManager_copy.frame;
        //    }
            if (Reference != null)
            {
                try
                {
                    Reference.CopyConvertedFrameDataToArray(color_array, ColorImageFormat.Rgba);
                    CHECK_GETDATA = true;
                    Debug.Log(color_array.Length);
                    Reference.Dispose();
                    Reference = null;
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
        // map to camera space coordinate
        //Debug.Log ("particle");
        mapper.MapDepthFrameToCameraSpace(rawdata, cameraSpacePoints);
        mapper.MapDepthFrameToColorSpace(rawdata, colorSpacePoints);
        Debug.Log(colorSpacePoints.Length);
        for (int i = 0; i < cameraSpacePoints.Length; i++)
        {
            long colorX = float.IsInfinity(colorSpacePoints[i].X) ? 0 : (int)Mathf.Floor(colorSpacePoints[i].X);
            long colorY = float.IsInfinity(colorSpacePoints[i].Y) ? 0 : (int)Mathf.Floor(colorSpacePoints[i].Y);
            if (colorX < 0)
                colorX = 0;
            if (colorY < 0)
                colorY = 0;
            if (i % 1000 == 0)
            {
                Debug.Log("X:" + colorX + " Y:" + colorY);
                Debug.Log(colorSpacePoints[i].X);
            }

            long colorIndex = ((colorY * colorFrameDesc.Width) + colorX) * 4;
            if (CHECK_GETDATA == true)
            {
                if (colorIndex < 0)
                {
                    Debug.Log("error" + colorIndex);
                }
                try
                {
                    byte r = color_array[colorIndex];
                    byte g = color_array[colorIndex + 1];
                    byte b = color_array[colorIndex + 2];
                    byte alpha = 255;
                    particles[i].color = new Color32(r, g, b, alpha);
                }
                catch (Exception e)
                {
                }



            }
            //if (cameraSpacePoints [i].X <= 1 && cameraSpacePoints[i].X >= -1) {//X座標の1~-1を表示


            particles[i].position = new Vector3(cameraSpacePoints[i].X * scale, cameraSpacePoints[i].Y * scale, cameraSpacePoints[i].Z * scale);
            particles[i].size = size;

            //particles[i].color=new Color32(0,(byte)(cameraSpacePoints[i].Z*50),0,255);
            //	if (rawdata [i] == 0)
            //		particles [i].size = 0;//データの値が0ならばサイズを0にする
            //} else if (particles [i].position != null) {//particles[x].positionのデータが空じゃなければサイズを0にする
            //	particles [i].size = 0;
            //}
            if (i % 10000 == 0)
            {
                //Debug.Log (particles [i].color);
            }
        }

        // update particle system
        GetComponent<ParticleSystem>().SetParticles(particles, particles.Length);
    }
}