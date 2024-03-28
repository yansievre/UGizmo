﻿using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace UGizmo.Extension.Jobs
{
    [BurstCompile]
    internal unsafe struct CreatePrimitiveObjectJob : IJobParallelFor
    {
        [NativeDisableUnsafePtrRestriction]
        [ReadOnly]
        public PrimitiveData* GizmoDataPtr;

        [NativeDisableUnsafePtrRestriction]
        [WriteOnly]
        public RenderData* Result;

        public int MaxInstanceCount;

        private static readonly int stride = UnsafeUtility.SizeOf<float4>();

        [BurstCompile]
        public void Execute([AssumeRange(0, int.MaxValue)] int index)
        {
            PrimitiveData* renderData = GizmoDataPtr + index;

            float4x4 matrix = float4x4.TRS(renderData->Position, renderData->Rotation, renderData->Scale);

            Result[index] = new RenderData(matrix, renderData->Color);
        }
    }
}