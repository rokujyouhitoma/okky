using UnityEngine;
using System.Collections;


namespace Okky {
	public class Generator : MonoBehaviour {
		public GameObject[] objects;

		public GameObject GenerateRandom() {
			int rand = Random.Range (0, objects.Length);
			var obj = objects[rand];
			return obj;
		}
	}
}