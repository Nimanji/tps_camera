using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TPSCamera : MonoBehaviour
{
	/// <value>視点基準とするGameObject。Inspectorから指定。</value>
	public GameObject tps_target;
	/// <value>視点基準とカメラ間の距離補正</value>
	Vector3 correct_camera_pos;
	/// <value>視点基準を中心としたときのカメラとの角度(deg)<value>
	double deg_to_camera;

	void Awake ()
	{
		// 距離補正を設定
		correct_camera_pos = new Vector3(0.4f, 1.4f, -0.8f);
		// カメラの位置を現在の視点基準にあわせる
		transform.position = tps_target.transform.position + correct_camera_pos;
		// 視点基準を中心としたカメラの角度
		deg_to_camera = Mathf.Atan2(
			transform.position.z - tps_target.transform.position.z,
			transform.position.x - tps_target.transform.position.z
		) * (180 / Mathf.PI);
	}

	void Update ()
	{
		correctCameraPositionForTPS();
	}

	/// <summary>
	/// 視点基準を中心としてカメラを肩越し位置に自動補正する
	/// </summary>
	void correctCameraPositionForTPS ()
	{
		// 視点対象が向いている方向をdegに反映させる
		double deg_merge = deg_to_camera - tps_target.transform.localEulerAngles.y;
		// 反映させた角度をラジアン(rad)に変更する
		double rad = deg_merge * (Mathf.PI / 180);
		// 角度を元にしたカメラの補正座標を計算する
		Vector3 rad_correct_pos = new Vector3(Mathf.Cos((float)rad) * 1.0f, 1.4f, Mathf.Sin((float)rad) * 1.0f);
		// カメラの座標と角度を再設定する
		transform.position = tps_target.transform.position + rad_correct_pos;
		transform.rotation = tps_target.transform.rotation;
	}
}
