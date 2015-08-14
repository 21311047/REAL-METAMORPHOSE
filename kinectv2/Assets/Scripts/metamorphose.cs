using UnityEngine;
using System.Collections;
using Kinect = Windows.Kinect;

public class metamorphose : MonoBehaviour {
	public GameObject BodySourceManager;
	private BodySourceManager _BodyManager;

	//Head
	private GameObject mask;
	private Quaternion maskr;

	//Neck
	private GameObject neckc;
	private Quaternion neckcr;

	//RightArm
	private GameObject rightarm;
	private Quaternion rar;

	//RightUpperarm
	private GameObject rightupperarm;
	private Quaternion ruar;

	//LeftArm
	private GameObject leftarm;
	private Quaternion lar;

	//leftUpperarm
	private GameObject leftupperarm;
	private Quaternion luar;

	//RightFoot
	private GameObject rightfoot;
	private Quaternion rfr;

	//rightthigh
	private GameObject rightthigh;
	private Quaternion rtr;

	//LeftFoot
	private GameObject leftfoot;
	private Quaternion lfr;

	//leftthigh
	private GameObject leftthigh;
	private Quaternion ltr;

	//RightLeg
	private GameObject rightleg;
	private Quaternion rlgr;

	//LefttLeg
	private GameObject leftleg;
	private Quaternion llgr;

	//RightKnee
	private GameObject rightknee;
	private Quaternion rkr;

	//LeftKnee
	private GameObject leftknee;
	private Quaternion lkr;

	//Belt
	private GameObject belt;
	private Quaternion beltr;

	//Body
	private GameObject bodyc;
	private Quaternion bodyr;

	//back
	private GameObject back;
	private Quaternion backr;

	//RightBuster
	private GameObject rightbuster;
	private Quaternion rbr;

	//LeftBuster
	private GameObject leftbuster;
	private Quaternion lbr;

	//RightShoulder
	private GameObject rightshoulder;
	private Quaternion rsr;

	//LeftShoulder
	private GameObject leftshoulder;
	private Quaternion lsr;

	//Hip
	private GameObject hip;
	private Quaternion hipr;

	//Not Use?
	private Vector3 m_pos;

	//Player
	private Vector3 beforebody;
	private int Player=0;

	//Change effect
	private GameObject changeeffect;

	//Charge effect
	private GameObject chargeeffect;

	//Attack effectとその位置
	private GameObject bulleteffect;
	
	private Vector3 attackV;

	//Change Counter
	private int henshin = 0;

	//Change Timer
	private float htimer = 0;

	//Delete Timer
	private float ktimer = 0;

	//Effect Timer
	private float etimer = 0;

	//Attack Timar
	private float attacktimer = 0;

	//SE
	private AudioSource change1_se;
	private AudioSource change3_se;

	// Use this for initialization
	void Start () {
		m_pos = new Vector3 (10, 0, 0);  // ͠гɊӵð֛Ν

		//オブジェクトを探してくる
		mask = GameObject.Find ("Head");
		maskr = mask.transform.rotation;

		neckc= GameObject.Find ("Neck");
		neckcr = neckc.transform.rotation;

		rightarm = GameObject.Find ("R_Arm");
		rar = rightarm.transform.rotation;

		rightupperarm = GameObject.Find ("R_Upper_arm");
		ruar = rightupperarm.transform.rotation;

		leftarm = GameObject.Find ("L_Arm");
		lar = leftarm.transform.rotation;

		leftupperarm = GameObject.Find ("L_Upper_arm");
		luar = leftupperarm.transform.rotation;

		rightfoot = GameObject.Find ("R_Shin");
		rfr = rightfoot.transform.rotation;

		rightthigh = GameObject.Find ("R_Thigh");
		rtr = rightthigh.transform.rotation;

		leftfoot = GameObject.Find ("L_Shin");
		lfr = leftfoot.transform.rotation;

		leftthigh = GameObject.Find ("L_Thigh");
		ltr = leftthigh.transform.rotation;

		rightleg = GameObject.Find ("R_leg");
		rlgr = rightleg.transform.rotation;

		leftleg = GameObject.Find ("L_leg");
		llgr = rightleg.transform.rotation;

		rightknee = GameObject.Find ("R_Knee");
		rkr = rightknee.transform.rotation;

		leftknee = GameObject.Find ("L_Knee");
		lkr = leftknee.transform.rotation;

		belt = GameObject.Find ("Belt");
		beltr = belt.transform.rotation;

		bodyc = GameObject.Find ("Breast");
		bodyr = bodyc.transform.rotation;

		back = GameObject.Find ("Back");
		backr = back.transform.rotation;

		rightbuster = GameObject.Find ("R_Hand");
		rbr = rightbuster.transform.rotation;

		leftbuster = GameObject.Find ("L_Hand");
		lbr = leftbuster.transform.rotation;

		rightshoulder = GameObject.Find ("R_Shoulder");
		rsr = rightshoulder.transform.rotation;

		leftshoulder = GameObject.Find ("L_Shoulder");
		lsr = leftshoulder.transform.rotation;

		hip = GameObject.Find ("Hip");
		hipr = hip.transform.rotation;

		changeeffect = GameObject.Find ("change_particle");
		chargeeffect = GameObject.Find ("charge_particle");
		bulleteffect = GameObject.Find ("Bullet");

		//変身用項目(検索用)
		//メッシュレンダラーをオフにする
		mask.GetComponent<MeshRenderer>().enabled = false;
		neckc.GetComponent<MeshRenderer>().enabled = false;
		rightarm.GetComponent<MeshRenderer>().enabled = false;
		rightupperarm.GetComponent<MeshRenderer>().enabled = false;
		//meshrendererを探してenabledをfalseにする
        leftarm.GetComponent<MeshRenderer>().enabled = false;
		leftupperarm.GetComponent<MeshRenderer>().enabled = false;
        rightfoot.GetComponent<MeshRenderer>().enabled = false;
		rightthigh.GetComponent<MeshRenderer>().enabled = false;
        leftfoot.GetComponent<MeshRenderer>().enabled = false;
		leftthigh.GetComponent<MeshRenderer>().enabled = false;
		rightleg.GetComponent<MeshRenderer>().enabled = false;
		leftleg.GetComponent<MeshRenderer>().enabled = false;
		rightknee.GetComponent<MeshRenderer>().enabled = false;
		leftknee.GetComponent<MeshRenderer>().enabled = false;
        belt.GetComponent<MeshRenderer>().enabled = false;
        //GameObject.Find ("default_MeshPart0").GetComponent<MeshRenderer>().enabled=false;
		//GameObject.Find ("default_MeshPart1").GetComponent<MeshRenderer>().enabled=false;
		//GameObject.Find ("default_MeshPart2").GetComponent<MeshRenderer>().enabled=false;
		bodyc.GetComponent<Renderer>().enabled=false;
		back.GetComponent<Renderer>().enabled=false;
		rightbuster.GetComponent<MeshRenderer>().enabled = false;
        leftbuster.GetComponent<MeshRenderer>().enabled = false;
        rightshoulder.GetComponent<MeshRenderer>().enabled=false;
		leftshoulder.GetComponent<MeshRenderer>().enabled=false;
		hip.GetComponent<MeshRenderer>().enabled=false;
		//this.transform.FindChild("default").gameObject.GetComponent<MeshRenderer>().enabled=false;
		changeeffect.GetComponent<Renderer>().enabled=false;
		changeeffect.transform.FindChild("Particle System 1").gameObject.GetComponent<Renderer>().enabled = false;
		//changeeffectの子オブジェクトdefaultのmeshrendererをfalseにする
		chargeeffect.GetComponent<Renderer> ().enabled = false;
		chargeeffect.transform.FindChild("Particle System 1").gameObject.GetComponent<Renderer>().enabled = false;
		bulleteffect.GetComponent<Renderer> ().enabled = false;

		//SEを指定する
		AudioSource[] audioSources = GetComponents<AudioSource>();
		change1_se = audioSources[1];
		change3_se = audioSources[0];
	}

	
	// Update is called once per frame
	void Update () {
		_BodyManager = BodySourceManager.GetComponent<BodySourceManager> ();
		if (_BodyManager == null) {
			return;
		}
		
		Kinect.Body[] data = _BodyManager.GetData ();
		if (data == null) {
			return;
		} else {
			beforebody = new Vector3 (0, 0, 1000);
			for (int i=0; i<data.Length; i++) {
				Vector3 nearbody = GetVector3FromJoint (data [i].Joints [Kinect.JointType.Head]);
				if (nearbody.z < beforebody.z && nearbody.z != 0) {
					beforebody = nearbody;
					Player = i;
				}
			}
		}
		Kinect.Body body = data [Player];
		//トラッキング出来てなかったら・・・メッシュレンダラーを切る
		if (!body.IsTracked) 
			return;
		
		//transform.localPosition = GetVector3FromJoint (body.Joints [Kinect.JointType.Head]); 
		Vector3 Head = GetVector3FromJoint (body.Joints [Kinect.JointType.Head]);
		Vector3 Neck = GetVector3FromJoint (body.Joints [Kinect.JointType.Neck]); 
		Vector3 ss = GetVector3FromJoint (body.Joints [Kinect.JointType.SpineShoulder]);
		Vector3 sr = GetVector3FromJoint (body.Joints [Kinect.JointType.ShoulderRight]);
		Vector3 fronthead = Head - ss;
		Vector3 backhead = sr - ss;
        Vector3 yjkhead = Vector3.Cross (fronthead, backhead);

		mask.transform.rotation = Quaternion.LookRotation (fronthead, yjkhead) * maskr;
		mask.transform.position = Head;
		// マスクを移動

		Vector3 frontneckc = ss - Neck;
		neckc.transform.rotation = Quaternion.LookRotation (frontneckc) * maskr;
		neckc.transform.position = Neck;
		//首を移動
		Vector3 centerpointbelt;
		Vector3 sb = GetVector3FromJoint (body.Joints [Kinect.JointType.SpineBase]);
		Vector3 hr = GetVector3FromJoint (body.Joints [Kinect.JointType.HipRight]);
		Vector3 hl = GetVector3FromJoint (body.Joints [Kinect.JointType.HipLeft]);
		Vector3 sm = GetVector3FromJoint (body.Joints [Kinect.JointType.SpineMid]);
		centerpointbelt = (sb + sm) / 2;
		Vector3 frontbelt = sb - sm;
		Vector3 backbelt = hl - sm;
		Vector3 yjkbelt = Vector3.Cross (frontbelt, backbelt);
		
		belt.transform.rotation = Quaternion.LookRotation (frontbelt, yjkbelt) * beltr;
		belt.transform.position = centerpointbelt;
		//ベルトを移動

		Vector3 centerpointbodyc;
		Vector3 sl = GetVector3FromJoint (body.Joints [Kinect.JointType.ShoulderLeft]);
		//Vector3 sm = GetVector3FromJoint (body.Joints [Kinect.JointType.SpineMid]);
		centerpointbodyc = (sm + ss) / 2;
		Vector3 frontbodyc = sm - ss;
		Vector3 backbodyc = sl - ss;
		Vector3 yjkbodyc = Vector3.Cross (frontbodyc, backbodyc);
		bodyc.transform.rotation = Quaternion.LookRotation (frontbodyc,yjkbodyc) * bodyr;
		bodyc.transform.position = centerpointbodyc;
		//ボディーを移動

		back.transform.position = sm;
		back.transform.rotation = Quaternion.LookRotation (frontbodyc) * backr;
		//

		Vector3 centerpointra;
		Vector3 wr = GetVector3FromJoint (body.Joints [Kinect.JointType.WristRight]);//手首を取得
		Vector3 er = GetVector3FromJoint (body.Joints [Kinect.JointType.ElbowRight]);//肘を取得
		centerpointra = (wr + er) / 2;//肘と手首の中点を求める
		Vector3 frontra = wr - er;//肘→手首のベクトルを求める
		Vector3 backra = sr - er;//外積を求めるために肘→肩のベクトルを求める
		Vector3 yjkra = Vector3.Cross (frontra, backra);
		Vector3 gskra = Vector3.Cross (yjkra, frontra);
		
		rightarm.transform.rotation = Quaternion.LookRotation (frontra,gskra) * rar;
		rightarm.transform.position = centerpointra;
		//右腕を移動

		Vector3 centerpointrua;
		Vector3 el = GetVector3FromJoint (body.Joints [Kinect.JointType.ElbowLeft]);
		centerpointrua = (sr + er) / 2;
		Vector3 frontrua = er - sr;
		rightupperarm.transform.rotation = Quaternion.LookRotation (frontrua) * ruar;
		rightupperarm.transform.position = centerpointrua;
		//右二の腕を移動

		Vector3 centerpointla;
		Vector3 wl = GetVector3FromJoint (body.Joints [Kinect.JointType.WristLeft]);
		
		centerpointla = (wl + el) / 2;
		Vector3 frontla = wl - el;
		Vector3 backla = sl - el;
		Vector3 yjkla = Vector3.Cross (frontla, backla);
		Vector3 gskla = Vector3.Cross (yjkla, frontla);
		leftarm.transform.rotation = Quaternion.LookRotation (frontla, gskla) * lar;
		leftarm.transform.position = centerpointla;
		//左腕を移動

		Vector3 centerpointlua;
		centerpointlua = (sl + el) / 2;
		Vector3 frontlua = el - sl;
		leftupperarm.transform.rotation = Quaternion.LookRotation (frontlua) * luar;
		leftupperarm.transform.position = centerpointlua;
		//左二の腕を移動

		Vector3 centerpointrf;
		Vector3 kr = GetVector3FromJoint (body.Joints [Kinect.JointType.KneeRight]);
		Vector3 ar = GetVector3FromJoint (body.Joints [Kinect.JointType.AnkleRight]);
		Vector3 hpr = GetVector3FromJoint (body.Joints [Kinect.JointType.HipRight]);
		centerpointrf = (kr + ar) / 2;
		Vector3 frontrf = ar - kr;
		Vector3 backrf = hpr - kr;
		Vector3 yjkrf = Vector3.Cross (frontrf, backrf);
		Vector3 gskrf = Vector3.Cross (yjkrf, frontrf);
		rightfoot.transform.rotation = Quaternion.LookRotation (frontrf, gskrf) * rfr;
		rightfoot.transform.position = centerpointrf;
		//右膝下を移動

		Vector3 centerpointrt;
		centerpointrt = (hpr + kr) / 2;
		Vector3 frontrt = kr - hpr;
		rightthigh.transform.rotation = Quaternion.LookRotation (frontrt) * rtr;
		rightthigh.transform.position = centerpointrt;
		//右膝上を移動

		Vector3 centerpointlf;
		Vector3 kl = GetVector3FromJoint (body.Joints [Kinect.JointType.KneeLeft]);
		Vector3 al = GetVector3FromJoint (body.Joints [Kinect.JointType.AnkleLeft]);
		Vector3 hpl = GetVector3FromJoint (body.Joints [Kinect.JointType.HipLeft]);
		centerpointlf = (kl + al) / 2;
		Vector3 frontlf = al - kl;
		Vector3 backlf = hpl - kl;
		Vector3 yjklf = Vector3.Cross (frontlf, backlf);
		Vector3 gsklf = Vector3.Cross (yjklf, frontlf);
		leftfoot.transform.rotation = Quaternion.LookRotation (frontlf, gsklf) * lfr;
		leftfoot.transform.position = centerpointlf;
		//左膝下を移動

		Vector3 centerpointlt;
		centerpointlt = (hpl + kl) / 2;
		Vector3 frontlt = kl - hpl;
		leftthigh.transform.rotation = Quaternion.LookRotation (frontlt) * ltr;
		leftthigh.transform.position = centerpointlt;
		//左膝上を移動

		Vector3 fr = GetVector3FromJoint (body.Joints [Kinect.JointType.FootRight]);
		Vector3 centerpointrlg;
		centerpointrlg = (fr + ar) / 2;
		Vector3 frontrlg = fr - ar;
		Vector3 backrlg = kr - ar;
		rightleg.transform.rotation = Quaternion.LookRotation (frontrlg,backrlg) * rlgr;
		rightleg.transform.position = centerpointrlg;
		//右靴部分を移動

		Vector3 fl = GetVector3FromJoint (body.Joints [Kinect.JointType.FootLeft]);
		Vector3 centerpointllg;
		centerpointllg = (fl + al) / 2;
		Vector3 frontllg = fl - al;
		Vector3 backllg = kl - al;
		leftleg.transform.rotation = Quaternion.LookRotation (frontllg,backllg) * llgr;
		leftleg.transform.position = centerpointllg;
		//左靴部分を移動

		rightknee.transform.position = kr;
		//右ひざ部分を移動

		leftknee.transform.position = kl;
		//左ひざ部分を移動

		Vector3 centerpointrb;
		//Vector3 wr = GetVector3FromJoint (body.Joints [Kinect.JointType.WristRight]);
		Vector3 hdr = GetVector3FromJoint (body.Joints [Kinect.JointType.HandRight]);
		centerpointrb = (wr + hdr) / 2;
		Vector3 frontrb = hdr - wr;
		Vector3 backrb = er - wr;
		Vector3 yjkrb = Vector3.Cross (frontrb, backrb);
		Vector3 gskrb = Vector3.Cross (yjkrb, frontrb);
		rightbuster.transform.rotation = Quaternion.LookRotation (frontrb, gskrb) * rbr;
		rightbuster.transform.position = hdr;
		//右バスターを移動

		Vector3 centerpointlb;
		//Vector3 wl = GetVector3FromJoint (body.Joints [Kinect.JointType.WristLeft]);
		Vector3 hdl = GetVector3FromJoint (body.Joints [Kinect.JointType.HandLeft]);
		centerpointlb = (wl + hdl) / 2;
		Vector3 frontlb = hdl - wl;
		Vector3 backlb = el - wl;
		Vector3 yjklb = Vector3.Cross (frontlb, backlb);
		Vector3 gsklb = Vector3.Cross (yjklb, frontlb);
		leftbuster.transform.rotation = Quaternion.LookRotation (frontlb, gsklb) * lbr;
		leftbuster.transform.position = hdl;
		//左バスターを移動
		
		Vector3 frontrs = ss - sr;
		Vector3 backrs = er - sr;
		Vector3 yjkrs = Vector3.Cross (frontrs, backrs);
		Vector3 gskrs = Vector3.Cross (yjkrs, backrs);
		rightshoulder.transform.position = sr;
		//rightshoulder.transform.Translate (new Vector3 (0, 0, -2));
		rightshoulder.transform.rotation = Quaternion.LookRotation (frontrs, yjkrs) * rsr;
		//右肩を移動
		
		Vector3 frontls = ss - sl;
		Vector3 backls = el - sl;
		Vector3 yjkls = Vector3.Cross (frontls, backls);
		Vector3 gskls = Vector3.Cross (yjkls, backls);
		leftshoulder.transform.position = sl;
		//leftshoulder.transform.Translate (new Vector3 (0, 0, -2));
		leftshoulder.transform.rotation = Quaternion.LookRotation (frontls, yjkls) * lsr;
		//左肩を移動

		Vector3 fronthip = sb - sm;
		hip.transform.position = centerpointbelt;
		hip.transform.rotation = Quaternion.LookRotation (fronthip) * hipr;
		//腎部を移動

		changeeffect.transform.position = sb;
		changeeffect.transform.Translate (new Vector3 (0, 1, -4));
		chargeeffect.transform.position = hdr;
		//エフェクトを移動

		attackV.x=(hdr.x-sr.x)*1.2f;
		attackV.y=(hdr.y-sr.y)*1.2f;
		attackV.z=(hdr.z-sr.z);
		//attackに肩の座標から手の座標に向かう方向を求めて入れる
		bulleteffect.transform.position=this.transform.position+attackV;
		bulleteffect.transform.rotation=Quaternion.LookRotation(attackV);
		//現在の位置にattackVを足して肘から手に向かう延長線上に弾が飛んでいくようにする
		
		//henshinpose
		//Head head
		//SpineShoulder ss
		//ShoulderLeft sl
		//ShoulderRight sr
		//ElbowRight er
		//HandRight hdr
		//Neck Neck


		switch (henshin) {
	    //変身一段階目
		case 0:
			if (sl.x <= hdr.x && ss.x >= hdr.x &&//左肩から肩中央に右手がある状態
			    //sm.x <= er.x && 
				Head.y >= hdr.y && ss.y <= hdr.y && //頭から肩中央に右手がある状態
			    sr.y >= er.y) //右肘が右肩より下にある状態
	  	 	{
				htimer += Time.deltaTime;
                    Debug.Log("変身認識開始");
                }
			if (htimer >= 0.3){
				change1_se.Play();
				belt.GetComponent<MeshRenderer>().enabled = true;
				Debug.Log ("変身1段階目！");
				henshin = 1;
				htimer = 0;			
			}
			break;
		//変身二段階目
		case 1:
			ktimer += Time.deltaTime;
			if (ktimer >= 10)
			{
				belt.GetComponent<MeshRenderer>().enabled = false;
				Debug.Log ("変身解除");
				henshin = 0;
				ktimer = 0;
				//一定時間放置してたら変身解除
			}
			if (sr.x <= hdr.x && sr.x <= er.x && hdr.x <= er.x + 1 && //右手が右肩と右肘の間にある状態
				    er.y <= sr.y && er.y <= hdr.y) //肘が手と右肩より下にある状態
			{
				htimer += Time.deltaTime;
			}
			if (htimer >= 1)
			{
				change1_se.Play();
				Debug.Log ("変身2段階目！！");	
				henshin = 2;
				htimer = 0;
				ktimer = 0;
			}
			break;
		//変身三段階目
		case 2:
			ktimer += Time.deltaTime;
			if (ktimer >= 10)
			{
				belt.GetComponent<MeshRenderer>().enabled = true;
				Debug.Log ("変身解除");
				henshin = 0;
				ktimer = 0;
				//一定時間放置してたら変身解除
			}
			if(sr.x<=hdl.x&&//右肩より左手が外側にある状態
			   Neck.y<=hdl.y&&sm.y<=el.y//左手が首と右肩より上にある状態
			   ){
				change3_se.Play();
				//変身用項目(検索用)
				//falce(off)になっていたオブジェクトの描画をtrue(on)にして表示
				changeeffect.GetComponent<Renderer>().enabled=true;
				changeeffect.transform.FindChild("Particle System 1").gameObject.GetComponent<Renderer>().enabled=true;
				mask.GetComponent<MeshRenderer>().enabled = true;
				neckc.GetComponent<MeshRenderer>().enabled = true;
				rightarm.GetComponent<MeshRenderer>().enabled = true;
				rightupperarm.GetComponent<MeshRenderer>().enabled = true;
                leftarm.GetComponent<MeshRenderer>().enabled = true;
				leftupperarm.GetComponent<MeshRenderer>().enabled = true;
                rightfoot.GetComponent<MeshRenderer>().enabled = true;
				rightthigh.GetComponent<MeshRenderer>().enabled = true;
                leftfoot.GetComponent<MeshRenderer>().enabled = true;
				leftthigh.GetComponent<MeshRenderer>().enabled = true;
				rightleg.GetComponent<MeshRenderer>().enabled = true;
				leftleg.GetComponent<MeshRenderer>().enabled = true;
				rightknee.GetComponent<MeshRenderer>().enabled = true;
				leftknee.GetComponent<MeshRenderer>().enabled = true;
				bodyc.GetComponent<Renderer>().enabled=true;
				back.GetComponent<Renderer>().enabled=true;
				rightbuster.GetComponent<MeshRenderer>().enabled = true;
                leftbuster.GetComponent<MeshRenderer>().enabled = true;
                rightshoulder.GetComponent<MeshRenderer>().enabled=true;
				leftshoulder.GetComponent<MeshRenderer>().enabled=true;
				hip.GetComponent<MeshRenderer>().enabled=true;
				//this.transform.FindChild("default").gameObject.GetComponent<MeshRenderer>().enabled=true;
				//3段階目のポーズを検知したら変身
				Debug.Log("変身完了ッ！！！");	
				henshin = 3;
				htimer = 0;
				ktimer = 0;
				attacktimer = 0;
			}
			break;
		case 3:
			//変身完了してたら・・・
			etimer += Time.deltaTime;//timer変数にdeltaTimeを足していく
			ktimer += Time.deltaTime;
			//タイマーが2以上なら・・・
			if (etimer >= 2) 
			{
				changeeffect.GetComponent<Renderer> ().enabled = false;
				changeeffect.transform.FindChild ("Particle System 1").gameObject.GetComponent<Renderer> ().enabled = false;
				//変身して2秒経ったら変身を解除
			}
			if (hdr.y>sb.y&&sm.y>=hdr.y&&sl.x <= hdr.x ) 
			{
				Debug.Log("攻撃チャージ開始！");
				attacktimer += Time.deltaTime;
				//貯めポーズをしたらチャージ開始
			}
			else
			{
				//attacktimer = 0;
			}

			if (attacktimer >= 4)
			{
				chargeeffect.GetComponent<Renderer>().enabled=true;
				chargeeffect.transform.FindChild("Particle System 1").gameObject.GetComponent<Renderer>().enabled=true;
				Debug.Log("チャージ完了！");
				//チャージ完了したらエフェクト発生
				if (hdr.y>=sr.y)
				{
					Debug.Log("攻撃！");
					attacktimer = 0;
					chargeeffect.GetComponent<Renderer>().enabled = false;
					chargeeffect.transform.FindChild("Particle System 1").gameObject.GetComponent<Renderer>().enabled = false;
					bulleteffect.GetComponent<Renderer>().enabled = true;
					//チャージ完了後に攻撃ポーズをしたら攻撃
				}
			}
			if (hdr.y<=sr.y&&attacktimer == 0)
			{
				bulleteffect.GetComponent<Renderer>().enabled = false;
				Debug.Log("攻撃終了");
				//攻撃ポーズをやめたらエフェクト消滅
			}

			if (ktimer >= 160) {
				attacktimer = 0;
				//変身用項目(検索用)
				mask.GetComponent<MeshRenderer>().enabled = false;
				neckc.GetComponent<MeshRenderer>().enabled = false;
				rightarm.GetComponent<MeshRenderer>().enabled = false;
				rightupperarm.GetComponent<MeshRenderer>().enabled = false;
                leftarm.GetComponent<MeshRenderer>().enabled = false;
				leftupperarm.GetComponent<MeshRenderer>().enabled = false;
                rightfoot.GetComponent<MeshRenderer>().enabled = false;
				rightthigh.GetComponent<MeshRenderer>().enabled = false;
                leftfoot.GetComponent<MeshRenderer>().enabled = false;
				leftthigh.GetComponent<MeshRenderer>().enabled = false;
				rightleg.GetComponent<MeshRenderer>().enabled = false;
				leftleg.GetComponent<MeshRenderer>().enabled = false;
				rightknee.GetComponent<MeshRenderer>().enabled = false;
				leftknee.GetComponent<MeshRenderer>().enabled = false;
                belt.GetComponent<MeshRenderer>().enabled = false;
				bodyc.GetComponent<Renderer>().enabled=false;
				back.GetComponent<Renderer>().enabled=false;
				rightbuster.GetComponent<MeshRenderer>().enabled = false;
                leftbuster.GetComponent<MeshRenderer>().enabled = false;
                rightshoulder.GetComponent<MeshRenderer>().enabled=false;
				leftshoulder.GetComponent<MeshRenderer>().enabled=false;
				hip.GetComponent<MeshRenderer>().enabled=false;
				//this.transform.FindChild("default").gameObject.GetComponent<MeshRenderer>().enabled=false;
				changeeffect.GetComponent<Renderer>().enabled=false;
				changeeffect.transform.FindChild("Particle System 1").gameObject.GetComponent<Renderer>().enabled = false;
				chargeeffect.GetComponent<Renderer>().enabled=false;
				chargeeffect.transform.FindChild("Particle System 1").gameObject.GetComponent<Renderer>().enabled = false;
				bulleteffect.GetComponent<Renderer>().enabled = false;
				Debug.Log ("変身解除");
				henshin = 0;
				htimer = 0;
				ktimer = 0;
				//変身してしばらく経ったら全て初期状態に
			}
			break;
		}
	}		
	private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
	{
		return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
	}
}