using UnityEngine;
using System.Collections;
using Kinect = Windows.Kinect;

public class armscript : MonoBehaviour {
	public GameObject BodySourceManager;
	private BodySourceManager _BodyManager;
	private Vector3 m_pos;
	// Use this for initialization
	void Start () {
		m_pos = new Vector3 (10, 0, 0);  // 形状位置を保持
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
		
		transform.localPosition = GetVector3FromJoint(body.Joints[Kinect.JointType.ElbowLeft ]);  // 形状位置を更新
		
	}
	private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
	{
		return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
	}
}