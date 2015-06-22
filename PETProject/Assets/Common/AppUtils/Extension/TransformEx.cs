using UnityEngine;
using System.Collections;


namespace AppUtils
{
	public static class TransformEx
	{
		#region Set position
		/// <summary>
		/// Sets the position.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetPos(this Transform transform, float x, float y, float z)
		{
			transform.position = new Vector3(x, y, z);
		}

		/// <summary>
		/// Sets the position x.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		public static void SetPosX(this Transform transform, float x)
		{
			Vector3 pos = transform.position;
			pos.x = x;
			transform.position = pos;
		}

		/// <summary>
		/// Sets the position y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		public static void SetPosY(this Transform transform, float y)
		{
			Vector3 pos = transform.position;
			pos.y = y;
			transform.position = pos;
		}

		/// <summary>
		/// Sets the position z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetPosZ(this Transform transform, float z)
		{
			Vector3 pos = transform.position;
			pos.z = z;
			transform.position = pos;
		}

		/// <summary>
		/// Sets the position X and Y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static void SetPosXY(this Transform transform, float x, float y)
		{
			Vector3 pos = transform.position;
			pos.x = x;
			pos.y = y;
			transform.position = pos;
		}

		/// <summary>
		/// Sets the position Y and Z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetPosYZ(this Transform transform, float y, float z)
		{
			Vector3 pos = transform.position;
			pos.y = y;
			pos.z = z;
			transform.position = pos;
		}

		/// <summary>
		/// Sets the position X and Z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetPosXZ(this Transform transform, float x, float z)
		{
			Vector3 pos = transform.position;
			pos.x = x;
			pos.z = z;
			transform.position = pos;
		}
		#endregion


		#region Set local position
		public static void SetLocalPos(this Transform transform, float x, float y, float z)
		{
			transform.localPosition = new Vector3(x, y, z);
		}

		/// <summary>
		/// Sets the local position x.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		public static void SetLocalPosX(this Transform transform, float x)
		{
			Vector3 localPos = transform.localPosition;
			localPos.x = x;
			transform.localPosition = localPos;
		}

		/// <summary>
		/// Sets the local position y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		public static void SetLocalPosY(this Transform transform, float y)
		{
			Vector3 localPos = transform.localPosition;
			localPos.y = y;
			transform.localPosition = localPos;
		}

		/// <summary>
		/// Sets the local position z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetLocalPosZ(this Transform transform, float z)
		{
			Vector3 localPos = transform.localPosition;
			localPos.z = z;
			transform.localPosition = localPos;
		}

		/// <summary>
		/// Sets the local position X and Y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static void SetLocalPosXY(this Transform transform, float x, float y)
		{
			Vector3 pos = transform.localPosition;
			pos.x = x;
			pos.y = y;
			transform.localPosition = pos;
		}
		
		/// <summary>
		/// Sets the local position Y and Z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetLocalPosYZ(this Transform transform, float y, float z)
		{
			Vector3 pos = transform.localPosition;
			pos.y = y;
			pos.z = z;
			transform.localPosition = pos;
		}
		
		/// <summary>
		/// Sets the local position X and Z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetLocalPosXZ(this Transform transform, float x, float z)
		{
			Vector3 pos = transform.localPosition;
			pos.x = x;
			pos.z = z;
			transform.localPosition = pos;
		}
		#endregion


		#region Add position
		/// <summary>
		/// Adds the position.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void AddPos(this Transform transform, float x, float y, float z = 0f)
		{
			transform.position += new Vector3(x, y, z);
		}

		/// <summary>
		/// Adds the position x.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		public static void AddPosX(this Transform transform, float x)
		{
			Vector3 pos = new Vector3(x, 0, 0);
			transform.position += pos;
		}

		/// <summary>
		/// Adds the position y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		public static void AddPosY(this Transform transform, float y)
		{
			Vector3 pos = new Vector3(0, y, 0);
			transform.position += pos;
		}

		/// <summary>
		/// Adds the position z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="z">The z coordinate.</param>
		public static void AddPosZ(this Transform transform, float z)
		{
			Vector3 pos = new Vector3(0, 0, z);
			transform.position += pos;
		}
		#endregion


		#region Add local position
		/// <summary>
		/// Adds the local position.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void AddLocalPos(this Transform transform, float x, float y, float z = 0f)
		{
			transform.localPosition += new Vector3(x, y, z);
		}

		/// <summary>
		/// Adds the local position x.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		public static void AddLocalPosX(this Transform transform, float x)
		{
			Vector3 pos = new Vector3(x, 0, 0);
			transform.localPosition += pos;
		}

		/// <summary>
		/// Adds the local position y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		public static void AddLocalPosY(this Transform transform, float y)
		{
			Vector3 pos = new Vector3(0, y, 0);
			transform.localPosition += pos;
		}

		/// <summary>
		/// Adds the local position z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="z">The z coordinate.</param>
		public static void AddLocalPosZ(this Transform transform, float z)
		{
			Vector3 pos = new Vector3(0, 0, z);
			transform.localPosition += pos; 
		}
		#endregion


		#region Set rotation
		/// <summary>
		/// Sets the rotation.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetRot(this Transform transform, float x, float y, float z)
		{
			transform.rotation = Quaternion.Euler(x, y, z);
		}

		/// <summary>
		/// Sets the rotation x.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		public static void SetRotX(this Transform transform, float x)
		{
			Vector3 rot = transform.eulerAngles;
			rot.x = x;
			transform.rotation = Quaternion.Euler(rot);
		}

		/// <summary>
		/// Sets the rotation y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		public static void SetRotY(this Transform transform, float y)
		{
			Vector3 rot = transform.eulerAngles;
			rot.y = y;
			transform.rotation = Quaternion.Euler(rot);
		}

		/// <summary>
		/// Sets the rotation z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetRotZ(this Transform transform, float z)
		{
			Vector3 rot = transform.eulerAngles;
			rot.z = z;
			transform.rotation = Quaternion.Euler(rot);
		}

		/// <summary>
		/// Sets the rotation X and Y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static void SetRotXY(this Transform transform, float x, float y)
		{
			Vector3 rot = transform.eulerAngles;
			rot.x = x;
			rot.y = y;
			transform.rotation = Quaternion.Euler(rot);
		}

		/// <summary>
		/// Sets the rotation Y and Z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="z">The z coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static void SetRotYZ(this Transform transform, float y, float z)
		{
			Vector3 rot = transform.eulerAngles;
			rot.y = y;
			rot.z = z;
			transform.rotation = Quaternion.Euler(rot);
		}

		/// <summary>
		/// Sets the rotation X and Z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetRotXZ(this Transform transform, float x, float z)
		{
			Vector3 rot = transform.eulerAngles;
			rot.x = x;
			rot.z = z;
			transform.rotation = Quaternion.Euler(rot);
		}
		#endregion


		#region Set local rotation
		/// <summary>
		/// Sets the local rotation.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetLocalRot(this Transform transform, float x, float y, float z)
		{
			transform.localRotation = Quaternion.Euler(x, y, z);
		}

		/// <summary>
		/// Sets the local rotation x.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		public static void SetLocalRotX(this Transform transform, float x)
		{
			Vector3 localRot = transform.localEulerAngles;
			localRot.x = x;
			transform.localRotation = Quaternion.Euler(localRot);
		}

		/// <summary>
		/// Sets the local rotation y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		public static void SetLocalRotY(this Transform transform, float y)
		{
			Vector3 localRot = transform.localEulerAngles;
			localRot.y = y;
			transform.localRotation = Quaternion.Euler(localRot);
		}

		/// <summary>
		/// Sets the local rotation z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetLocalRotZ(this Transform transform, float z)
		{
			Vector3 localRot = transform.localEulerAngles;
			localRot.z = z;
			transform.localRotation = Quaternion.Euler(localRot);
		}

		/// <summary>
		/// Sets the local rotation X and Y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static void SetLocalRotXY(this Transform transform, float x, float y)
		{
			Vector3 rot = transform.localEulerAngles;
			rot.x = x;
			rot.y = y;
			transform.localRotation = Quaternion.Euler(rot);
		}
		
		/// <summary>
		/// Sets the local rotation Y and Z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetLocalRotYZ(this Transform transform, float y, float z)
		{
			Vector3 rot = transform.localEulerAngles;
			rot.y = y;
			rot.z = z;
			transform.localRotation = Quaternion.Euler(rot);
		}
		
		/// <summary>
		/// Sets the local rotation X and Z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetLocalRotXZ(this Transform transform, float x, float z)
		{
			Vector3 rot = transform.localEulerAngles;
			rot.x = x;
			rot.z = z;
			transform.localRotation = Quaternion.Euler(rot);
		}
		#endregion


		#region Add rotation
		/// <summary>
		/// Adds the rotation.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void AddRot(this Transform transform, float x, float y, float z)
		{
			transform.eulerAngles += new Vector3(x, y, z);
		}

		/// <summary>
		/// Adds the rotation x.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		public static void AddRotX(this Transform transform, float x)
		{
			Vector3 rot = new Vector3(x, 0, 0);
			transform.eulerAngles += rot;
		}

		/// <summary>
		/// Adds the rotation y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		public static void AddRotY(this Transform transform, float y)
		{
			Vector3 rot = new Vector3(0, y, 0);
			transform.eulerAngles += rot;
		}

		/// <summary>
		/// Adds the rotation z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="z">The z coordinate.</param>
		public static void AddRotZ(this Transform transform, float z)
		{
			Vector3 rot = new Vector3(0, 0, z);
			transform.eulerAngles += rot;
		}
		#endregion


		#region Add local rotation
		/// <summary>
		/// Adds the local rotation.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void AddLocalRot(this Transform transform, float x, float y, float z)
		{
			transform.localEulerAngles += new Vector3(x, y, z);
		}

		/// <summary>
		/// Adds the local rotation x.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		public static void AddLocalRotX(this Transform transform, float x)
		{
			Vector3 rot = new Vector3(x, 0, 0);
			transform.localEulerAngles += rot;
		}

		/// <summary>
		/// Adds the local rotation y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		public static void AddLocalRotY(this Transform transform, float y)
		{
			Vector3 rot = new Vector3(0, y, 0);
			transform.localEulerAngles += rot;
		}

		/// <summary>
		/// Adds the local rotation z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="z">The z coordinate.</param>
		public static void AddLocalRotZ(this Transform transform, float z)
		{
			Vector3 rot = new Vector3(0, 0, z);
			transform.localEulerAngles += rot;
		}
		#endregion


		#region Set scale
		/// <summary>
		/// Sets the scale.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetScale(this Transform transform, float x, float y, float z)
		{
			transform.localScale = new Vector3(x, y, z);
		}

		/// <summary>
		/// Sets the scale x.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		public static void SetScaleX(this Transform transform, float x)
		{
			Vector3 scale = transform.localScale;
			scale.x = x;
			transform.localScale = scale;
		}

		/// <summary>
		/// Sets the scale y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		public static void SetScaleY(this Transform transform, float y)
		{
			Vector3 scale = transform.localScale;
			scale.y = y;
			transform.localScale = scale;
		}

		/// <summary>
		/// Sets the scale z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetScaleZ(this Transform transform, float z)
		{
			Vector3 scale = transform.localScale;
			scale.z = z;
			transform.localScale = scale;
		}

		/// <summary>
		/// Sets the scale X and Y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static void SetScaleXY(this Transform transform, float x, float y)
		{
			Vector3 scale = transform.localScale;
			scale.x = x;
			scale.y = y;
			transform.localScale = scale;
		}

		/// <summary>
		/// Sets the scale Y and Z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetScaleYZ(this Transform transform, float y, float z)
		{
			Vector3 scale = transform.localScale;
			scale.y = y;
			scale.z = z;
			transform.localScale = scale;
		}

		/// <summary>
		/// Sets the scale X and Z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void SetScaleXZ(this Transform transform, float x, float z)
		{
			Vector3 scale = transform.localScale;
			scale.x = x;
			scale.z = z;
			transform.localScale = scale;
		}
		#endregion


		#region Add scale
		/// <summary>
		/// Adds the scale.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void AddScale(this Transform transform, float x, float y, float z = 0f)
		{
			transform.localScale += new Vector3(x, y, z);
		}

		/// <summary>
		/// Adds the scale x.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="x">The x coordinate.</param>
		public static void AddScaleX(this Transform transform, float x)
		{
			Vector3 scale = new Vector3(x, 0, 0);
			transform.localScale += scale;
		}

		/// <summary>
		/// Adds the scale y.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="y">The y coordinate.</param>
		public static void AddScaleY(this Transform transform, float y)
		{
			Vector3 scale = new Vector3(0, y, 0);
			transform.localScale += scale;
		}

		/// <summary>
		/// Adds the scale z.
		/// </summary>
		/// <param name="transform">Transform.</param>
		/// <param name="z">The z coordinate.</param>
		public static void AddScaleZ(this Transform transform, float z)
		{
			Vector3 scale = new Vector3(0, 0, z);
			transform.localScale += scale;
		}
		#endregion
	}
}