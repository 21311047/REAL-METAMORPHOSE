// DepthParticlize.cs

using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class Depth : MonoBehaviour {
	
	// FROM KINECT v2
	public GameObject depthSourceManager;
	DepthSourceManager depthSourceManagerScript;
	FrameDescription depthFrameDesc;
	CameraSpacePoint[] cameraSpacePoints;
	CoordinateMapper mapper;
	
	private int depthWidth;
	private int depthHeight;
	
	// PARTICLE SYSTEM
	private ParticleSystem.Particle[] particles;
	
	// DRAW CONTROL
	public Color color = Color.white;
	public float size = 0.2f;
	public float scale = 10f;
	
	void Start () {
		
		// Get the description of the depth frames.
		depthFrameDesc = KinectSensor.GetDefault().DepthFrameSource.FrameDescription;
		depthWidth = depthFrameDesc.Width;
		depthHeight = depthFrameDesc.Height;
		
		// buffer for points mapped to camera space coordinate.
		cameraSpacePoints = new CameraSpacePoint[depthWidth * depthHeight];
		mapper = KinectSensor.GetDefault ().CoordinateMapper;
		
		// get reference to DepthSourceManager (which is included in the distributed 'Kinect for Windows v2 Unity Plugin zip')
		depthSourceManagerScript = depthSourceManager.GetComponent<DepthSourceManager> ();
		
		// particles to be drawn
		particles = new ParticleSystem.Particle[depthWidth * depthHeight];
	}
	
	void Update () {
		// get new depth data from DepthSourceManager.
		ushort[] rawdata = depthSourceManagerScript.GetData ();
		// map to camera space coordinate
		mapper.MapDepthFrameToCameraSpace (rawdata, cameraSpacePoints);
		
		for (int i = 0; i < cameraSpacePoints.Length; i++) {
			
			particles[i].position = new Vector3(cameraSpacePoints[i].X * scale, cameraSpacePoints[i].Y * scale, cameraSpacePoints[i].Z * scale);
			particles[i].color = color;
			particles[i].size = size;
			if( rawdata[i] == 0 ) particles[i].size = 0;
		}
		
		// update particle system
		GetComponent<ParticleSystem>().SetParticles (particles, particles.Length);
	}
}