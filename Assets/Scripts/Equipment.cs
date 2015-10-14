using UnityEngine;
using System.Collections;

namespace Okky {
	public class Equipment : MonoBehaviour {
		Generator generator;
		GameObject layer;

		void Awake() {
			generator = GetComponent<Generator>();
			layer = GameObject.Find("/Layer");
		}

		public GameObject Fire(int weaponId, Vector3 position) {
			var weapon = (GameObject)Instantiate(generator.GenerateById(weaponId), position, Quaternion.identity);
			weapon.transform.parent = layer.transform;
			return weapon;
		}
	}
}