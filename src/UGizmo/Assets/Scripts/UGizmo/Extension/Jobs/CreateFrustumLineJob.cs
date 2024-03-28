﻿using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace UGizmo.Extension.Jobs
{
    [BurstCompile]
    internal unsafe struct CreateFrustumLineJob : IJobParallelFor
    {
        [NativeDisableUnsafePtrRestriction]
        [ReadOnly]
        public FrustumLineData* GizmoDataPtr;

        public int MaxInstanceCount;

        [NativeDisableUnsafePtrRestriction]
        [WriteOnly]
        public void* Result;

        private static readonly int stride = UnsafeUtility.SizeOf<float4>();

        [BurstCompile]
        public void Execute([AssumeRange(0, int.MaxValue)] int index)
        {
            FrustumLineData* renderData = GizmoDataPtr + index;

            float3 start = math.rotate(renderData->Rotation, renderData->Start);
            float3 end = math.rotate(renderData->Rotation, renderData->End);

            float3 diff = end - start;
            float3 position = start + diff * 0.5f;
            float lengthE = math.length(diff);

            float cos = (1 + math.clamp(diff.z / lengthE, -1f, 1f)) * 0.5f;
            float3 axis = math.normalize(new float3(-diff.y, diff.x, 0f));
            quaternion rotation = new quaternion(new float4(axis * math.sqrt(1 - cos), math.sqrt(cos)));

            float4x4 matrix = float4x4.TRS(renderData->Center + position, rotation, new float3(0f, 0f, lengthE));
            MathUtil.Pack3x4(matrix, out float3x4 packedMatrix);
            MathUtil.Pack3x4(math.fastinverse(matrix), out float3x4 inversedMatrix);

            UnsafeUtility.WriteArrayElementWithStride(Result, index * 3, stride, packedMatrix);
            UnsafeUtility.WriteArrayElementWithStride(Result, MaxInstanceCount * 3 + index * 3, stride, inversedMatrix);
            UnsafeUtility.WriteArrayElementWithStride(Result, MaxInstanceCount * 6 + index * 1, stride, renderData->Color);
        }
    }
}