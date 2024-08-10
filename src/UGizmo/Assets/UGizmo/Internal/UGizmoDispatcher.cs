﻿using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UGizmo.Internal
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif

    internal static class UGizmoDispatcher
    {
        private static readonly ProfilingSampler profilingSampler;
        private static readonly CommandBuffer commandBuffer;
        private static readonly GizmoDrawSystem drawSystem;
        private static bool isFirstRun = true;
        private static bool usingHDRP;
        private static bool usingSRP;

        static UGizmoDispatcher()
        {
            usingSRP = GraphicsSettings.currentRenderPipeline != null;
            usingHDRP = usingSRP && GraphicsSettings.currentRenderPipeline.GetType().ToString().Contains("HighDefinition");

#if UNITY_EDITOR
            EditorApplication.update += Initialize;
            EditorApplication.playModeStateChanged += state =>
            {
                if (state == PlayModeStateChange.ExitingPlayMode || state == PlayModeStateChange.ExitingEditMode)
                {
                    drawSystem?.ClearContinuousGizmo();
                }
            };
#endif
            profilingSampler = new ProfilingSampler("DrawUGizmos");
            commandBuffer = CommandBufferPool.Get();
            drawSystem = new GizmoDrawSystem();

            if (usingSRP)
            {
                RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
            }
            else
            {
                //TODO: BRP Support
                Camera.onPreCull += OnPreCull;
            }
        }

#if !UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod]
#endif
        private static void Initialize()
        {
            if (!isFirstRun)
            {
                return;
            }

            drawSystem.Initialize();
            isFirstRun = false;
        }

        private static int previousFrame;
        internal static bool forceDraw;

        private static void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            if (isFirstRun
#if UNITY_EDITOR
                || !Handles.ShouldRenderGizmos()
#endif
               )
            {
                return;
            }

            if (usingHDRP)
            {
                //Run OnDrawGizmos()
                context.Submit();
            }

            bool updateDrawData = previousFrame != Time.renderedFrameCount;

            //When the frame is updated
            if (updateDrawData || forceDraw)
            {
                forceDraw = false;
                drawSystem.ExecuteCreateJob();
            }

            using (new ProfilingScope(commandBuffer, profilingSampler))
            {
                drawSystem.SetCommandBuffer(commandBuffer);
            }

            context.ExecuteCommandBuffer(commandBuffer);
            context.Submit();

            drawSystem.ClearScheduler();
            commandBuffer.Clear();
            previousFrame = Time.renderedFrameCount;
        }

        private static void OnPreCull(Camera camera)
        {
            if (isFirstRun
#if UNITY_EDITOR
                || !Handles.ShouldRenderGizmos()
#endif
               )
            {
                return;
            }

            bool updateDrawData = previousFrame != Time.renderedFrameCount;

            if (updateDrawData)
            {
                drawSystem.ExecuteCreateJob();
            }

            drawSystem.DrawWithCamera(camera);

            drawSystem.ClearScheduler();
            previousFrame = Time.renderedFrameCount;
        }
    }
}