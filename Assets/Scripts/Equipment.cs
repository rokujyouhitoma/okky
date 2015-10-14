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

		public GameObject Fire(Vector3 position, Vector3 dir) {
			var weapon = (GameObject)Instantiate(generator.GenerateById(0), position, Quaternion.identity);
			weapon.transform.parent = layer.transform;
			var projectile = weapon.GetComponent<Projectile>();
			projectile.dir = dir;
			return weapon;
		}
	}
}