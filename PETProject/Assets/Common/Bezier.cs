using UnityEngine;
using System.Collections;

[System.Serializable]
public class Bezier : System.Object
{
	public Vector3 p0;
	public Vector3 p1;
	public Vector3 p2;
	public Vector3 p3;
	
	private Vector3 b0 = Vector3.zero;
	private Vector3 b1 = Vector3.zero;
	private Vector3 b2 = Vector3.zero;
	private Vector3 b3 = Vector3.zero;

	private Vector3 a = Vector3.zero;
	private Vector3 b = Vector3.zero;
	private Vector3 c = Vector3.zero;
	
	// Init function v0 = 1st point, v1 = handle of the 1st point , v2 = handle of the 2nd point, v3 = 2nd point
	// handle1 = v0 + v1
	// handle2 = v3 + v2
	public Bezier( Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3 )
	{
		this.p0 = v0;
		this.p1 = v1;
		this.p2 = v2;
		this.p3 = v3;
	}
	// 0.0 >= t <= 1.0
	public Vector3 GetPointAtTime( float t )
	{
		t = Mathf.Max(0f, Mathf.Min(1f, t));
		this.CheckConstant();
		float t2 = t * t;
		float t3 = t * t * t;
		float x = this.a.x * t3 + this.b.x * t2 + this.c.x * t + p0.x;
		float y = this.a.y * t3 + this.b.y * t2 + this.c.y * t + p0.y;
		float z = this.a.z * t3 + this.b.z * t2 + this.c.z * t + p0.z;
		return new Vector3( x, y, z );
	}
	
	private void SetConstant()
	{
		this.c.x = 3f * ( ( this.p0.x + this.p1.x ) - this.p0.x );
		this.b.x = 3f * ( ( this.p3.x + this.p2.x ) - ( this.p0.x + this.p1.x ) ) - this.c.x;
		this.a.x = this.p3.x - this.p0.x - this.c.x - this.b.x;

		this.c.y = 3f * ( ( this.p0.y + this.p1.y ) - this.p0.y );
		this.b.y = 3f * ( ( this.p3.y + this.p2.y ) - ( this.p0.y + this.p1.y ) ) - this.c.y;
		this.a.y = this.p3.y - this.p0.y - this.c.y - this.b.y;
		
		this.c.z = 3f * ( ( this.p0.z + this.p1.z ) - this.p0.z );
		this.b.z = 3f * ( ( this.p3.z + this.p2.z ) - ( this.p0.z + this.p1.z ) ) - this.c.z;
		this.a.z = this.p3.z - this.p0.z - this.c.z - this.b.z;
	}
	
	// Check if p0, p1, p2 or p3 have change
	private void CheckConstant()
	{
		if( this.p0 != this.b0 || this.p1 != this.b1 || this.p2 != this.b2 || this.p3 != this.b3 )
		{
			this.SetConstant();
			this.b0 = this.p0;
			this.b1 = this.p1;
			this.b2 = this.p2;
			this.b3 = this.p3;
		}
	}
}