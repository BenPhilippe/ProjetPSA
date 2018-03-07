using UnityEngine;
using System.Collections;

public class CatmullRomSpline : MonoBehaviour {

	public Transform[] controlPointsList;	// Need at least 4 points
	public bool isLooping = true;

	public float splineResolution = 0.2f;
	public bool showControlLines = false;

	// Display without having to press play
	private void OnDrawGizmos() {

		// Draw the catmull-rom spline between the points
		for(int i = 0; i< controlPointsList.Length; i++){
			Gizmos.color = Color.white;	// Line gizmos color
			// Can't draw between endpoints,
			// neither from the second to the last endpoint
			// and if we are not making a looping line
			if((i == 0 || 
			i == controlPointsList.Length - 2 || i == controlPointsList.Length - 1)
			&& !isLooping){
				continue;
			}

			DisplayCatmullSpline(i);
			if(showControlLines){
				Gizmos.color = Color.gray;
				//DisplayControlLines();
			}
		}
	}

	// Display a spline between 2 points derived with the Catmull-Rom spline algorithm
	void DisplayCatmullSpline(int pos){
		// The 4 points we need to form a spline between p1 and p2
		Vector3 p0 = controlPointsList[ClampListPos(pos - 1)].position;
		Vector3 p1 = controlPointsList[pos].position;
		Vector3 p2 = controlPointsList[ClampListPos(pos + 2)].position;
		Vector3 p3 = controlPointsList[ClampListPos(pos + 1)].position;

		// The start position of the line
		Vector3 lastPos = p1;

		// Spline's resolution
		float resolution = splineResolution;

		// How many times should we loop?
		int loops = Mathf.FloorToInt(1f / resolution);

		for (int i = 1; i <= loops; i++)
		{
			// Which t position are we at?
			float t = i * resolution;

			// Find the coordinate between the end points with a Catmull-Rom spline
			Vector3 newPos = GetCatmullRomPosition(t, p0, p1, p2, p3);

			// Draw this segment
			Gizmos.DrawLine(lastPos, newPos);

			lastPos = newPos;
		}
	}

	/*void DisplayControlLines(){
		Gizmos.DrawLine(p0, p1);
		Gizmos.DrawLine(p2, p3);
	}*/

	// Clamp the list positions to allow looping
	int ClampListPos(int pos){

		if(pos < 0){
			pos = controlPointsList.Length -1;
		}
		if(pos > controlPointsList.Length){
			pos = 1;
		} else if(pos > controlPointsList.Length -1){
			pos = 0;
		}

		return pos;
	}

	Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3){

		// The coeffs of the cubic polynomial
		Vector3 a = 2f * p1;
		Vector3 b = p2 - p0;
		Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
		Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

		// The cubic polynomial: a + b * t + c * t^2 + d * t^3
		Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t *  t * t));
		return pos;
	}
}
