using UnityEngine;
using System.Collections;
using Kinect = Windows.Kinect;

public class objectscriptcust : MonoBehaviour {
	public GameObject BodySourceManager;
	private BodySourceManager _BodyManager;
	private GameObject rightarm;
	private Quaternion rar;
	private GameObject leftarm;
	private Quaternion lar;
	private GameObject rightfoot;
	private Quaternion rfr;
	private GameObject leftfoot;
	private Quaternion lfr;
	private GameObject belt;
	private Quaternion beltr;
	private GameObject bodyc;
	private Quaternion bodyr;
	private GameObject rightbuster;
	private Quaternion rbr;
	private GameObject leftbuster;
	private Quaternion lbr;
	private GameObject rightshoulder;
	private Quaternion rsr;
	private GameObject leftshoulder;
	private Quaternion lsr;
	private Vector3 m_pos;
	private Vector3 beforebody;
	private int Player;
	// Use this for initialization
	void Start () {
		m_pos = new Vector3 (10, 0, 0);  // ͠гɊӵð֛Ν
		rightarm = GameObject.Find ("armc");
		rar = rightarm.transform.rotation;
		leftarm = GameObject.Find ("armc1");
		lar = leftarm.transform.rotation;
		rightfoot = GameObject.Find ("footc");
		rfr = rightfoot.transform.rotation;
		leftfoot = GameObject.Find ("footc1");
		lfr = leftfoot.transform.rotation;
		belt = GameObject.Find ("beltc");
		beltr = belt.transform.rotation;
		bodyc = GameObject.Find ("bodyc");
		bodyr =bodyc.transform.rotation;
		rightbuster = GameObject.Find ("busterc");
		rbr = rightbuster.transform.rotation;
		leftbuster = GameObject.Find ("busterc1");
		lbr = leftbuster.transform.rotation;
		rightshoulder = GameObject.Find ("shoulderc");
		rsr = rightshoulder.transform.rotation;
		leftshoulder = GameObject.Find ("shoulderc1");
		lsr = leftshoulder.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		_BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
		if (_BodyManager == null)
		{
			return;
		}
		
		Kinect.Body[] data = _BodyManager.GetData();
		if (data == null) {
			return;
		} 
		else{
			for(int i=0;i<data.Length;i++){
				Vector3 nearbody=GetVector3FromJoint(data[i].Joints[Kinect.JointType.Head]);
				if(nearbody.z<beforebody.z)
				{
					beforebody=nearbody;
					Player=i;
				}
			}
		}
		Kinect.Body body = data [Player];
		if (!body.IsTracked) 
			return;
		
		transform.localPosition = GetVector3FromJoint(body.Joints[Kinect.JointType.Head]); 
		// ͠гɊӵðΘі
		Vector3 sb = GetVector3FromJoint (body.Joints [Kinect.JointType.SpineBase]);
		Vector3 hr = GetVector3FromJoint (body.Joints [Kinect.JointType.HipRight]);
		Vector3 hl = GetVector3FromJoint (body.Joints [Kinect.JointType.HipLeft]);
		Vector3 sm = GetVector3FromJoint (body.Joints [Kinect.JointType.SpineMid]);
		Vector3 frontbelt = sb - sm;
		Vector3 backbelt = hl - sm;
		Vector3 yjkbelt = Vector3.Cross(frontbelt, backbelt);

		belt.transform.rotation = Quaternion.LookRotation (frontbelt,yjkbelt) * beltr;
		belt.transform.position = sb;
		
		//
		Vector3 sl = GetVector3FromJoint (body.Joints [Kinect.JointType.ShoulderLeft]);
		Vector3 sr = GetVector3FromJoint (body.Joints [Kinect.JointType.ShoulderRight]);
		//Vector3 sm = GetVector3FromJoint (body.Joints [Kinect.JointType.SpineMid]);
		Vector3 frontbodyc = sl - sr;
		bodyc.transform.rotation = Quaternion.LookRotation (frontbodyc) * bodyr;
		bodyc.transform.position = sm;
		//
		Vector3 centerpointra;
		Vector3 wr = GetVector3FromJoint (body.Joints [Kinect.JointType.WristRight]);//手首を取得
		Vector3 er = GetVector3FromJoint (body.Joints [Kinect.JointType.ElbowRight]);//肘を取得
		centerpointra = (wr + er) / 2;//肘と手首の中点を求める
		Vector3 frontra = wr - er;//肘→手首のベクトルを求める
		Vector3 backra = sr - er;//外積を求めるために肘→肩のベクトルを求める
		Vector3 yjkra = Vector3.Cross(frontra, backra);
		Vector3 gskra = Vector3.Cross (yjkra, frontra);

		rightarm.transform.rotation = Quaternion.LookRotation (frontra,gskra) * rar;
		rightarm.transform.position = centerpointra;
		//
		Vector3 centerpointla;
		Vector3 wl = GetVector3FromJoint (body.Joints [Kinect.JointType.WristLeft]);
		Vector3 el = GetVector3FromJoint (body.Joints [Kinect.JointType.ElbowLeft]);

		centerpointla = (wl + el) / 2;
		Vector3 frontla = wl - el;
		Vector3 backla = sl - el;
		Vector3 yjkla = Vector3.Cross(frontla, backla);
		Vector3 gskla = Vector3.Cross (yjkla, frontla);
		
		leftarm.transform.rotation = Quaternion.LookRotation (frontla,gskla) * lar;
		leftarm.transform.position = centerpointla;
		//
		Vector3 centerpointrf;
		Vector3 kr = GetVector3FromJoint (body.Joints [Kinect.JointType.KneeRight]);
		Vector3 ar = GetVector3FromJoint (body.Joints [Kinect.JointType.AnkleRight]);
		Vector3 hpr = GetVector3FromJoint (body.Joints [Kinect.JointType.HipRight]);
		centerpointrf = (kr + ar) / 2;
		Vector3 frontrf = ar - kr;
		Vector3 backrf = hpr - kr;
		Vector3 yjkrf = Vector3.Cross(frontrf, backrf);
		Vector3 gskrf = Vector3.Cross (yjkrf, frontrf);
		rightfoot.transform.rotation = Quaternion.LookRotation (frontrf,gskrf) * rfr;
		rightfoot.transform.position = centerpointrf;

		//
		Vector3 centerpointlf;
		Vector3 kl = GetVector3FromJoint (body.Joints [Kinect.JointType.KneeLeft]);
		Vector3 al = GetVector3FromJoint (body.Joints [Kinect.JointType.AnkleLeft]);
		Vector3 hpl = GetVector3FromJoint (body.Joints [Kinect.JointType.HipLeft]);
		centerpointlf = (kl + al) / 2;
		Vector3 frontlf = al - kl;
		Vector3 backlf = hpl - kl;
		Vector3 yjklf = Vector3.Cross(frontlf, backlf);
		Vector3 gsklf = Vector3.Cross (yjklf, frontlf);
		leftfoot.transform.rotation = Quaternion.LookRotation (frontlf,gsklf) * lfr;
		leftfoot.transform.position = centerpointlf;
		//
		Vector3 centerpointrb;
		//Vector3 wr = GetVector3FromJoint (body.Joints [Kinect.JointType.WristRight]);
		Vector3 hdr = GetVector3FromJoint (body.Joints [Kinect.JointType.HandRight]);
		centerpointrb = (wr + hdr) / 2;
		Vector3 frontrb = hdr - wr;
		Vector3 backrb = er - wr;
		Vector3 yjkrb = Vector3.Cross(frontrb, backrb);
		Vector3 gskrb = Vector3.Cross (yjkrb, frontrb);
		rightbuster.transform.rotation = Quaternion.LookRotation (frontrb,gskrb) * rbr;
		rightbuster.transform.position = hdr;
		//
		Vector3 centerpointlb;
		//Vector3 wl = GetVector3FromJoint (body.Joints [Kinect.JointType.WristLeft]);
		Vector3 hdl = GetVector3FromJoint (body.Joints [Kinect.JointType.HandLeft]);
		centerpointlb = (wl + hdl) / 2;
		Vector3 frontlb = hdl - wl;
		Vector3 backlb = el - wl;
		Vector3 yjklb = Vector3.Cross(frontlb, backlb);
		Vector3 gsklb = Vector3.Cross (yjklb, frontlb);
		leftbuster.transform.rotation = Quaternion.LookRotation (frontlb,gsklb) * lbr;
		leftbuster.transform.position = hdl;
		//
		Vector3 ss = GetVector3FromJoint (body.Joints [Kinect.JointType.SpineShoulder]);
		Vector3 frontrs = ss - sr;
		Vector3 backrs = er - sr;
		Vector3 yjkrs = Vector3.Cross(frontrs, backrs);
		Vector3 gskrs = Vector3.Cross(yjkrs, backrs);
		rightshoulder.transform.position = sr;
		rightshoulder.transform.Translate (new Vector3 (0, 0, -2));
		rightshoulder.transform.rotation = Quaternion.LookRotation (frontrs,gskrs) * rsr;
	    //
		leftshoulder.transform.position = sl;
		Vector3 frontls = ss - sl;
		Vector3 backls = el - sl;
		Vector3 yjkls = Vector3.Cross(frontls, backls);
		Vector3 gskls = Vector3.Cross(yjkls, backls);
		leftshoulder.transform.position = sl;
		leftshoulder.transform.Translate (new Vector3 (0, 0, -2));
		leftshoulder.transform.rotation = Quaternion.LookRotation (frontls,gskls) * lsr;

	}
	private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
	{
		return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
	}
}