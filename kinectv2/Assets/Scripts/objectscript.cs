using UnityEngine;
using System.Collections;
using Kinect = Windows.Kinect;

public class objectscript : MonoBehaviour {
	public GameObject BodySourceManager;
	private BodySourceManager _BodyManager;
	private GameObject rightarm;
	private Quaternion r;
	private Vector3 m_pos;
	// Use this for initialization
	void Start () {
		m_pos = new Vector3 (10, 0, 0);  // 形状位置を保持
		rightarm = GameObject.Find ("armc");
		r = rightarm.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		_BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
		if (_BodyManager == null)
		{
			return;
		}
		
		Kinect.Body[] data = _BodyManager.GetData();
		if (data == null)
		{
			return;
		}
		Kinect.Body body = data [0];
		if (!body.IsTracked) 
			return;

		transform.localPosition = GetVector3FromJoint(body.Joints[Kinect.JointType.Head]); // 形状位置を更新
		Vector3 centerpoint;
		Vector3 wr = GetVector3FromJoint (body.Joints [Kinect.JointType.WristRight]);
		Vector3 er = GetVector3FromJoint (body.Joints [Kinect.JointType.ElbowRight]);
		centerpoint = (wr + er) / 2;
		Vector3 front = wr - er;

		rightarm.transform.rotation = Quaternion.LookRotation (front) * r;

		rightarm.transform.position = centerpoint;
	}
	private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
	{
		return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
	}
}