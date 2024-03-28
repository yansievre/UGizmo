﻿using UnityEditor;
using UnityEngine;

namespace UGizmo
{
    public class UGizmosStarter : ScriptableSingleton<UGizmosStarter>
    {
        [SerializeField] private GizmoDispatcher dispatcher;

        private static void Init()
        {
            if (instance.dispatcher == null)
            {
                GameObject gameObj = new GameObject
                {
                    name = nameof(GizmoDispatcher),
                    hideFlags = HideFlags.HideAndDontSave
                };

                GameObject childObj = new GameObject
                {
                    name = nameof(GizmoDispatcher)
                };

                instance.dispatcher = childObj.AddComponent<GizmoDispatcher>();
                childObj.transform.SetParent(gameObj.transform);
            }
        }

        private void OnDisable()
        {
            dispatcher.Dispose();
        }
    }
}