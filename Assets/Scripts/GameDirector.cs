using UnityEngine;
using System.Collections;

namespace Okky {
	public class GameDirector : MonoBehaviour {
		public float topScore = 0;
		public float score = 0;

		void Awake() {
		}

		void Start() {
		}

		void Update() {
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
    }
}