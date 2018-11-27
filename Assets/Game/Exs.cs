using UnityEngine;

namespace Assets.Game {
    public static class Exs {

        public static T GetAddComponent<T>(this MonoBehaviour monoBehaviour) where T : Component {
            return monoBehaviour.GetComponent<T>() ?? monoBehaviour.gameObject.AddComponent<T>();
        }


    }
}
