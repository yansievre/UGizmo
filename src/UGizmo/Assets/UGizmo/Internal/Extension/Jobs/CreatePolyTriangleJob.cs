using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace UGizmo.Internal.Extension.Jobs
{
    [BurstCompile]
    internal unsafe struct CreatePolyTriangleJob : IJobParallelFor
    {
        public const int K_TriangleCount = 2;
        [NativeDisableUnsafePtrRestriction]
        [ReadOnly]
        public PolyTriangleData* GizmoDataPtr;

        [NativeDisableUnsafePtrRestriction]
        [WriteOnly]
        public PrimitiveData* TriangleResult;

        [BurstCompile]
        public void Execute([AssumeRange(0, int.MaxValue)] int index)
        {
            PolyTriangleData* triangleData = GizmoDataPtr + index;
            
            float3 p0 = triangleData->p0;
            float3 p1 = triangleData->p1;
            float3 p2 = triangleData->p2;
            
            float l01 = math.length(p1 - p0);
            float l12 = math.length(p2 - p1);
            float l20 = math.length(p0 - p2);
            
            if(l01 > l12 && l01 > l20)
            {
                float3 temp = p0;
                p0 = p1;
                p1 = p2;
                p2 = temp;
            }
            else if(l12 > l01 && l12 > l20)
            {
                float3 temp = p0;
                p0 = p2;
                p2 = p1;
                p1 = temp;
            }
            
            float3 to2 = p2 - p0;
            float3 to1 = p1 - p0;
            float3 proj = math.project(to1, to2);
            float3 height = to1 - proj;

            float3 cross = math.normalizesafe(math.cross(proj, height));
            var rot = quaternion.LookRotation(cross, height);

            TriangleResult[K_TriangleCount * index + 0] = new PrimitiveData(p0 + proj, rot, new float3(math.length(to2-proj) ,math.length(height),1), triangleData->color);
            TriangleResult[K_TriangleCount * index + 1] = new PrimitiveData(p0 + proj, rot, new float3(-math.length(proj),math.length(height),1), triangleData->color);
        }
    }

    public readonly struct PolyTriangleData
    {
        public readonly float3 p0;
        public readonly float3 p1;
        public readonly float3 p2;
        public readonly Color color;

        public PolyTriangleData(float3 p0, float3 p1, float3 p2, Color color)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
            this.color = color;
        }
    }
}