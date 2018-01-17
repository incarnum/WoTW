using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothCamScript : MonoBehaviour {
	public Transform target;
	public float camDistance;
	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
	void Update() {
		Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z + camDistance);
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
	}
}
