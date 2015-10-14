using UnityEngine;
using System.Collections;

namespace Okky {
	public class Bamboo : MonoBehaviour {
		void OnTriggerEnter2D(Collider2D collider) {
			var ga = collider.gameObject;
			Id id = ga.GetComponent<Id>();
			switch (id.type) {
			case Id.Player1:
			case Id.Player2:
				ga.SendMessage("OnTakeBamboo", gameObject);
				break;
			}
		}
		
		void OnDie() {
			Destroy(gameObject);
		}
	}
}
