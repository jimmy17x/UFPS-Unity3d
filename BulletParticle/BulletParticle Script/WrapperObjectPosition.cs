
// this class can be used to send position across scripts  as object parameter
using UnityEngine;
using System.Collections;

	public class WrapperObjectPosition
	{
		private Vector3 position;	
		private Quaternion rotation;

		public WrapperObjectPosition (Vector3 position , Quaternion rotation)
		{
			this.position = position;
			this.rotation = rotation;
		}
		
		public Vector3 getPosition()
		{
			return this.position;
		}
		public Quaternion getRotation()
		{
			return this.rotation;
		}

		
		
	}


