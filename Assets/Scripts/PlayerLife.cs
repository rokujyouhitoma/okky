using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Okky {
	public class PlayerLife : MonoBehaviour {
		public GameDirector gameDirector;
		public int playerId;

		Text textUI;

		void Start () {
			gameDirector = gameDirector.GetComponent<GameDirector>();
			textUI = GetComponent<Text>();
		}
		
		void Update () {
			textUI.text = gameDirector.GetPlayerLives(playerId).ToString() ;
		}
	}
}