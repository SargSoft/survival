using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math3d : MonoBehaviour {
	
	//Projects a vector onto a plane. The output is not normalized.
	public static Vector3 ProjectVectorOnPlane(Vector3 planeNormal, Vector3 vector) {
		return vector - (Vector3.Dot(vector, planeNormal) * planeNormal);
	}

	//This function returns a point which is a projection from a point to a line.
	//The line is regarded infinite. If the line is finite, use ProjectPointOnLineSegment() instead.
	public static Vector3 ProjectPointOnLine(Vector3 linePoint, Vector3 lineVec, Vector3 point) {        
		
		//get vector from point on line to point in space
		Vector3 linePointToPoint = point - linePoint;
		
		float t = Vector3.Dot(linePointToPoint, lineVec);
		
		return linePoint + lineVec * t;
	}
}
