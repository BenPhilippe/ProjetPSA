using UnityEngine;

public static class Bezier {
	public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t){
		//line from start to end
		//return Vector3.Lerp(p0, p2, t);

		//Three calls to Lerp()
		//return Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);

		//Quadratic Bezier curve formula
		//B(t) = (1 - t)² P0 + 2 (1 - t) t P1 + t² P2
		t = Mathf.Clamp01(t);
		float invertT = 1f - t;
		return 
			invertT * invertT * p0 +
			2f * invertT * t * p1 +
			t * t * p2;
	}

	public static Vector3 GetFirstDerivative (Vector3 p0, Vector3 p1, Vector3 p2, float t){
		return
			2f * (1f - t) * (p1 - p0) +
			2f * t * (p2 - p1);
	}
}
