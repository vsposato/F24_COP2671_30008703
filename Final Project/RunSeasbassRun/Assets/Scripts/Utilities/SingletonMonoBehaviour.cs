using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// The Singleton pattern restricts the instantiation of a class to one single instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance {
            get
            {
                if (_instance == null)
                    Logging.PrintError($"No instance of {typeof(T)} exists in the scene.");

                return _instance;
            }
        }
        protected SingletonMonoBehaviour() { }
        protected void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;

                InitializeAfterAwake();
            }
            else
            {
                Logging.PrintWarn($"An instance of {typeof(T)} already exists in the scene. Self-destructing.");
                Destroy(gameObject);
            }
        }
        protected void OnDestroy()
        {
            if (this == _instance)
            {
                _instance = null;
            }
        }
        /// <summary>
        /// This overridable method can be used in place of the Awake method.
        /// </summary>
        protected virtual void InitializeAfterAwake() { }
    }
}