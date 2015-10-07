using UnityEngine;
using System.Collections;

namespace Okky {
	public class Item : MonoBehaviour {
		Id id;

		GameObject background; 
		GameDirector gameDirector;

		void Awake() {
			var gd = GameObject.Find("/GameDirector");
			gameDirector = gd.GetComponent<GameDirector>();
			id = GetComponent<Id>();
		}
 
		void Start() {
		}

		void Update() {
		}

		void Die() {
			Destroy(gameObject);
		}

	}
}