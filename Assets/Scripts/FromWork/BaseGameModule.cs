using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TGame
{
    public abstract class BaseGameModule : MonoBehaviour
    {
        private void Awake()
        {

        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        private void OnDestroy()
        {

        }

        protected internal virtual void OnModuleInit() { }
        protected internal virtual void OnModuleStart() { }
        protected internal virtual void OnModuleStop() { }
        protected internal virtual void OnModuleUpdate(float daltaTime) { }
        protected internal virtual void OnModuleLateUpdate(float daltaTime) { }
        protected internal virtual void OnModuleFixedUpdate(float daltaTime) { }

    }
}

