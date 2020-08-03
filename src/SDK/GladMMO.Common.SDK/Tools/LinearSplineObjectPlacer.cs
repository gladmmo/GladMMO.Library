using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class LinearSplineObjectPlacer : GladMMOSDKMonoBehaviour
	{
		[SerializeField]
		private GameObject PlacementPrefab;

		[SerializeField]
		private List<GameObject> PlacedPrefabs;

		[SerializeField]
		private Vector3 Scale;

		[SerializeField]
		private Vector3 Distance;

		[SerializeField]
		private int Count;

		[Button]
		public void PlaceObjects()
		{
			try
			{

				for (int i = 0; i < Count; i++)
				{
					GameObject placedPrefab = (GameObject)GladMMOPrefabUtility.InstantiatePrefab(PlacementPrefab);

					//Set scale and 0 position to the linear spline path.
					placedPrefab.transform.localScale = Scale;
					placedPrefab.transform.position = transform.position + Distance * i;

					//Parent to this gameobject
					placedPrefab.transform.parent = transform;

					PlacedPrefabs.Add(placedPrefab);
				}
			}
			catch (Exception e)
			{
				Debug.LogError($"Failed to place: {PlacementPrefab.name}. Reason: {e}");
				throw;
			}
		}

		[Button]
		public void ClearObjects()
		{
			foreach(var go in PlacedPrefabs)
				if (go != null)
					GameObject.DestroyImmediate(go);

			PlacedPrefabs.Clear();
		}
	}
}
