// DestroyNotifier.cs
// Tore Knabe
// Copyright 2016 ioccam@ioccam.com

using UnityEngine;


namespace RealisticEyeMovements {

	public class DestroyNotifier : MonoBehaviour
	{

		public event System.Action<DestroyNotifier> OnDestroyedEvent;


		void OnDestroyed()
		{
			if ( OnDestroyedEvent != null )
				OnDestroyedEvent( this );
		}

	}

}