
using UnityEngine;
using System.Collections;

namespace Okky {
	public class Cube : MonoBehaviour {

		void OnTriggerEnter2D(Collider2D collider) {
			var ga = collider.gameObject;
			Id id = ga.GetComponent<Id>();
			
			switch (id.type) {
			case Id.Player1:
			case Id.Player2:
			case Id.Ninja:
				ga.SendMessage("OnCube", gameObject);
				break;
			case Id.Kama:
			case Id.Shuriken:
                ga.SendMessage("OnDie", gameObject);
                break;
            }
        }

	}
}