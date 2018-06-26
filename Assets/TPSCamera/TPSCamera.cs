using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TPSCamera : MonoBehaviour
{
	/// <value>視点基準とするGameObject。Inspectorから指定。</value>
	public GameObject tps_target;

	double deg_diff;

	void Awake ()
	{
		// 視点対象を基準にしてカメラの位置を決定する
		transform.position = tps_target.transform.position + new Vector3(1.0f, 5.0f, -1.0f);
		// カメラの位置と視点基準の角度を割り出す
		deg_diff = Mathf.Atan2(transform.position.z-tps_target.transform.position.z, transform.position.x-tps_target.transform.position.x) * (180/Mathf.PI);
	}

	void Start ()
	{
		Observable.EveryUpdate().Subscribe(_ => calcTpsPosition());
	}

	void calcTpsPosition ()
	{
		// 調整したdegからオブジェクトの座標を修正する
		float rad = (float)(deg_diff - tps_target.transform.localEulerAngles.y * (Mathf.PI/180));
		transform.position = tps_target.transform.position + (new Vector3(Mathf.Cos(rad)*1.0f, 0.0f, Mathf.Sin(rad)*1.0f) + new Vector3(0.0f, 1.4f, 0.0f));
		// カメラの方向を視点基準と同期させる
		transform.rotation = tps_target.transform.rotation;
	}
}
