using UnityEngine;

public class SplineWalker : MonoBehaviour {
	public BezierSpline splineToFollow;
	public float duration;
	public bool lookForward;
	private bool goingForward = false;
	public SplineWalkerMode mode;
	[Range(0f, 1f)]
	public float progress;
	private void Update() {
		if(goingForward){
			progress += Time.deltaTime / duration;
			if(progress > 1f){
				if(mode == SplineWalkerMode.Once){
					progress = 1f;
				} else if (mode == SplineWalkerMode.Loop){
					progress -= 1f;
				} else {
					progress = 2f - progress;
					goingForward = false;
				}
			}
		} else {
			progress -= Time.deltaTime / duration;
			if(progress < 0f){
				progress -= progress;
				goingForward = true;
			}
		}
		Vector3 position = splineToFollow.GetPoint(progress);
		transform.localPosition = position;
		if(lookForward){
			transform.LookAt(position + splineToFollow.GetDirection(progress));
		}
	}
}

public enum SplineWalkerMode{
	Once,
	Loop,
	PingPong
}
