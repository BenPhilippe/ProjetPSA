using UnityEngine;
using System;

public class BezierSpline : MonoBehaviour {
	[SerializeField]
	public Vector3[] points;
	[SerializeField]
	private BezierControlPointMode[] modes;
	// Is the spline a loop?
	[SerializeField]
	public bool loop;
	public bool Loop{
		get{
			return loop;
		}
		set{
			loop = value;
			if(value == true){
				modes[modes.Length -1] = modes[0];
				SetControlPoint(0, points[0]);
			}
		}
	}
	public int ControlPointCount{
		get{
			return points.Length;
		}
	}

	public Vector3 GetControlPoint (int index){
		return points[index];
	}

	public BezierControlPointMode GetControlPointMode(int index){
		return modes[(index + 1) / 3];
	}

	public void SetControlPoint(int index, Vector3 point){
		// Also moves control points if middle one is selected 
		if(index % 3 == 0){
			Vector3 delta = point - points[index];
			if(index > 0){
				points[index - 1] += delta;
			}
			if(index + 1 < points.Length){
				points[index + 1] += delta;
			}
		}
		points[index] = point;
		EnforceMode(index);
	}

	public void SetControlPointMode(int index, BezierControlPointMode mode){
		modes[(index + 1) / 3] = mode;
		EnforceMode(index);
	}

	public int CurveCount{
		get{
			return (points.Length - 1) / 3;
		}
	}

	public void Reset(){
		points = new Vector3[]{
			new Vector3(1f, 0f, 0f),
			new Vector3(2f, 0f, 0f),
			new Vector3(3f, 0f, 0f),
			new Vector3(4f, 0f, 0f),
		};
		modes = new BezierControlPointMode[]{
			BezierControlPointMode.Free,
			BezierControlPointMode.Free
		};
	}

	public Vector3 GetPoint(float t){
		//quadratic
		//return transform.TransformPoint(Bezier.GetPoint(points[0], points[1], points[2], t));
		
		int i;
		if(t >= 1f){
			t = 1f;
			i = points.Length -4;
		}else{
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}

		//cubic
		return transform.TransformPoint(Bezier.GetPoint(points[i], points[i + 1], points[i + 2], points[i + 3], t));
	}

	public Vector3 GetVelocity (float t){
		//quadratic
		//return transform.TransformPoint(Bezier.GetFirstDerivative(points[0], points[1], points[2], t)) - transform.position;
		
		int i;
		if(t >= 1f){
			t = 1f;
			i = points.Length -4;
		}else{
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}

		//cubic
		return transform.TransformPoint(Bezier.GetFirstDerivative(points[i], points[i + 1], points[i + 2], points[i + 3], t))
				 - transform.position;
	}

	public Vector3 GetDirection(float t){
		return GetVelocity(t).normalized;
	}

	public void AddCurve(){
		Vector3 point = points[points.Length - 1];
		Array.Resize(ref points, points.Length + 3);
		point.x += 1f;
		points[points.Length - 3] = point;
		point.x += 1f;
		points[points.Length - 2] = point;
		point.x += 1f;
		points[points.Length - 1] = point;

		Array.Resize(ref modes, modes.Length +1);
		modes[modes.Length - 1] = modes[modes.Length - 2];
		EnforceMode(points.Length - 4);
	}

	private void EnforceMode(int index){
		int modeIndex = (index + 1) / 3;
		BezierControlPointMode mode = modes[modeIndex];
		//Do nothing in these cases
		if(mode == BezierControlPointMode.Free ||
			modeIndex == 0 ||
			modeIndex == modes.Length -1){
			return;
		}

		int middleIndex = modeIndex * 3;
		int fixedIndex, enforcedIndex;
		if(index <= middleIndex){
			fixedIndex = middleIndex -1;
			enforcedIndex = middleIndex +1;
		}else{
			fixedIndex = middleIndex +1;
			enforcedIndex = middleIndex -1;
		}

		//Mirrored
		Vector3 middle = points[middleIndex];
		Vector3 enforcedTangent = middle - points[fixedIndex];
		//Aligned = same size for each tangents
		if(mode == BezierControlPointMode.Aligned){
			enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, points[enforcedIndex]);
		}
		points[enforcedIndex] = middle + enforcedTangent;
	}
}
