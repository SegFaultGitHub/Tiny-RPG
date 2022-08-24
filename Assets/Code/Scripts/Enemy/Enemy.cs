using System.Collections;
using UnityEngine;

namespace Assets.Code.Scripts.Enemy {
    public class Enemy : MonoBehaviour {
        public void OnCollisionEnter(Collision collision) {
            Debug.Log("COucou");
        }
    }
}