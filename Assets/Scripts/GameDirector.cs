using UnityEngine;
using System.Collections;

namespace Okky {
	public class GameDirector : MonoBehaviour {
		public float topScore = 0f;
		public float score = 0f;
		public float koban = 0f;

		void Awake() {
		}

		void Start() {
		}

		void Update() {
			if (!ExistKoban()) {
				AreaClear ();
			}
		}

		void ResetGame() {
			Application.LoadLevel("GameMain");
		}

		public float GetScore() {
			return score;
		}

		public void AddScoreByHealth(Health health) {
			AddScore (health.score);
		}

		void AddScore(float v) {
			score += v;
			if (score > topScore) {
				topScore = score;
			}
		}

		public float GetKoban() {
			return koban;
		}

		void TakeKoban() {
			koban++;
		}

		bool ExistKoban() {
			var objs = GameObject.FindGameObjectsWithTag("Kobans");
			if (objs .Length > 0) {
				return true;
			}
			return false;
		}

		void AreaClear() {
			ResetGame();
		}
    }
}