using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class WASDMoved : MonoBehaviour
{
	const float MOVE_SPEED = 0.1f;
	const float ROTATE_SPEED = 1.0f;

	void Start ()
	{
		// キャラクターの移動
		Observable.EveryUpdate().Where(_ => Input.GetKey(KeyCode.W)).Subscribe(_ => movedByKeyCode(KeyCode.W));
		Observable.EveryUpdate().Where(_ => Input.GetKey(KeyCode.A)).Subscribe(_ => movedByKeyCode(KeyCode.A));
		Observable.EveryUpdate().Where(_ => Input.GetKey(KeyCode.S)).Subscribe(_ => movedByKeyCode(KeyCode.S));
		Observable.EveryUpdate().Where(_ => Input.GetKey(KeyCode.D)).Subscribe(_ => movedByKeyCode(KeyCode.D));
		// キャラクターの向きを変える
		Observable.EveryUpdate().Where(_ => Input.GetKey(KeyCode.LeftArrow)).Subscribe(_ => rotateByKeyCode(KeyCode.LeftArrow));
		Observable.EveryUpdate().Where(_ => Input.GetKey(KeyCode.RightArrow)).Subscribe(_ => rotateByKeyCode(KeyCode.RightArrow));
	}

	/// <summary>
	/// 入力のあった方向にこのGameObjectを移動させる
	/// </summary>
	void movedByKeyCode(KeyCode code)
	{
		float mv_x = 0.0f;
		float mv_z = 0.0f;

		switch (code) {
			case KeyCode.W:
				mv_z = MOVE_SPEED;
				break;
			case KeyCode.A:
				mv_x = MOVE_SPEED * -1;
				break;
			case KeyCode.S:
				mv_z = MOVE_SPEED * -1;
				break;
			case KeyCode.D:
				mv_x = MOVE_SPEED;
				break;
		}

		Vector3 move_vec = new Vector3(mv_x, 0.0f, mv_z);

		this.transform.position += move_vec;
	}

	/// <summary>
	/// 入力のあった方向にこのGameObjectの角度を変える
	/// </summary>
	void rotateByKeyCode(KeyCode code)
	{
		switch (code) {
			case KeyCode.LeftArrow:
				transform.Rotate(new Vector3(0.0f, ROTATE_SPEED * -1, 0.0f));
				break;
			case KeyCode.RightArrow:
				transform.Rotate(new Vector3(0.0f, ROTATE_SPEED, 0.0f));
				break;
		}
	}
}
