using UnityEngine;
using System.Collections;

namespace Okky {
	public class TrakingCamera : MonoBehaviour {

		public GameObject obj1;
		public GameObject obj2;

		void Update() {
			var p = gameObject.transform.position;
			var p1 = obj1.transform.position;
			var p2 = obj2.transform.position;
			var center = (p1 + p2) / 2;
			gameObject.transform.position = new Vector3(center.x, center.y, p.z);
		}
	}
}
