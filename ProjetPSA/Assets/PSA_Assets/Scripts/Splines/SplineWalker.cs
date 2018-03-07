using UnityEngine;

public class SplineWalker : MonoBehaviour {
	public BezierSpline splineToFollow;
	public float duration;
	[Range(0f, 5f)]
	public float speed = .15f;

	public bool lookForward;
	public bool isUsingDuration = true;
	private bool goingForward = false;
	public SplineWalkerMode mode;
	[Range(0f, 1f)]
	public float t;	// Spline progress value

	private void Update() {

		Vector3 previousPosition = transform.position;

		if(goingForward){
			if(isUsingDuration){
				t += Time.deltaTime / duration;
			} else{
				t += Time.deltaTime * speed;
			}
			if(t > 1f){
				if(mode == SplineWalkerMode.Once){
					t = 1f;
				} else if (mode == SplineWalkerMode.Loop){
					t -= 1f;
				} else {
					t = 2f - t;
					goingForward = false;
				}
			}
		} else {
			if(isUsingDuration){
				t -= Time.deltaTime / duration;
			} else {
				t -= Time.deltaTime * speed;
			}
			if(t < 0f){
				t -= t;
				goingForward = true;
			}
		}
		Vector3 position = splineToFollow.GetPoint(t);
		transform.localPosition = position;

		if(lookForward){
			transform.LookAt(position + splineToFollow.GetDirection(t));
		}
	}
}

public enum SplineWalkerMode{
	Once,
	Loop,
	PingPong
}
