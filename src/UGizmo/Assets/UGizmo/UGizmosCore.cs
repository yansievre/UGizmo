﻿using System.Runtime.CompilerServices;
using UGizmo.Internal;
using UGizmo.Internal.Extension;
using UGizmo.Internal.Extension.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Plane = UGizmo.Internal.Extension.Plane;

namespace UGizmo
{
    public static partial class UGizmos
    {
        private static readonly LineData[] wireLineBuffer = new LineData[2049];
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawPlaneCore(float3 position, quaternion rotation, float2 size, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, new float3(size.x, 1f, size.y), color);
            Gizmo<Plane, PrimitiveData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawWirePlaneCore(float3 position, quaternion rotation, float2 size, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, new Vector3(size.x, 1f, size.y), color);
            PreparableGizmo<WirePlane, PrimitiveData>.AddData(data, duration);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawTriangleCore(float3 p0, float3 p1, float3 p2, Color color, float duration = 0f)
        {
            var data = new PolyTriangleData(p0,p1,p2, color);
            PreparableGizmo<PolyTriangle, PolyTriangleData>.AddData(data, duration);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawTriangleCore(float3 position, quaternion rotation, float2 size, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, new Vector3(size.x, size.y, 1f), color);
            Gizmo<Triangle, PrimitiveData>.AddData(data, duration);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawWireTriangleCore(float3 position, quaternion rotation, float2 size, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, new Vector3(size.x, 1f, size.y), color);
            PreparableGizmo<WireTriangle, PrimitiveData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawSphereCore(float3 position, quaternion rotation, float radius, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, new Vector3(radius, radius, radius), color);
            Gizmo<Sphere, PrimitiveData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawCircleCore(float3 position, quaternion rotation, float radius, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, new Vector3(radius, 1f, radius), color);
            Gizmo<Circle, PrimitiveData>.AddData(data, duration);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawCircleCore(float3 position, quaternion rotation, float3 scale, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, scale, color);
            Gizmo<Circle, PrimitiveData>.AddData(data, duration);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawWireCircleCore(float3 position, quaternion rotation, float radius, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, new Vector3(radius, 1f, radius), color);
            Gizmo<WireCircle, PrimitiveData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawWireCircleCore(float3 position, quaternion rotation, float3 scale, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, scale, color);
            Gizmo<WireCircle, PrimitiveData>.AddData(data, duration);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawWireSphereCore(float3 position, quaternion rotation, float radius, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, new Vector3(radius, radius, radius), color);
            Gizmo<WireSphere, PrimitiveData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawCapsuleCore(float3 center, float3 upAxis, float height, float radius, Color color, float duration)
        {
            var data = new CapsuleData(center, upAxis, height, radius, color);
            PreparableGizmo<Capsule, CapsuleData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawWireCapsuleCore(float3 center, float3 upAxis, float height, float radius, Color color, float duration)
        {
            var data = new CapsuleData(center, upAxis, height, radius, color);
            PreparableGizmo<WireCapsule, CapsuleData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawCapsule2DCore(float3 center, quaternion rotation, float height, float radius, Color color, float duration)
        {
            var data = new Capsule2dData(center, rotation, height, radius, color);
            PreparableGizmo<Capsule2d, Capsule2dData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawWireCapsule2DCore(float3 center, quaternion rotation, float height, float radius, Color color, float duration)
        {
            var data = new Capsule2dData(center, rotation, height, radius, color);
            PreparableGizmo<WireCapsule2d, Capsule2dData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawConeCore(float3 origin, float3 direction, float distance, float angle, Color color, float duration)
        {
            var data = new ConeData(origin, direction, distance, angle, color);
            Gizmo<Cone, ConeData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawWireConeCore(float3 origin, float3 direction, float distance, float angle, Color color, float duration)
        {
            var data = new ConeData(origin, direction, distance, angle, color);
            PreparableGizmo<WireCone, ConeData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawPointCore(float3 position, quaternion rotation, float radius, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, new float3(radius, radius, radius), color);
            Gizmo<Point, PrimitiveData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawWireLineCore(float3 from, float3 to, Color color, float duration)
        {
            var data = new LineData(from, to, color);
            Gizmo<WireLine, LineData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void DrawWireLineRangeCore(LineData* data, int length, float duration)
        {
            Gizmo<WireLine, LineData>.AddDataRange(data, length, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawCubeCore(float3 position, quaternion rotation, float3 size, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, size, color);
            Gizmo<Cube, PrimitiveData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawWireCubeCore(float3 position, quaternion rotation, float3 size, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, size, color);
            Gizmo<WireCube, PrimitiveData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawWireCylinderCore(float3 center, float3 upAxis, float height, float radius, Color color, float duration)
        {
            var data = new CapsuleData(center, upAxis, height, radius, color);
            PreparableGizmo<WireCylinder, CapsuleData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawCylinderCore(float3 position, quaternion rotation, float3 size, Color color, float duration)
        {
            var data = new PrimitiveData(position, rotation, size, color);
            Gizmo<Cylinder, PrimitiveData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawFrustumCore(
            float3 center,
            Quaternion rotation,
            float fov,
            float farClipPlane,
            float nearClipPlane,
            float aspect,
            Color color,
            float duration)
        {
            var data = new FrustumData(center, rotation, fov, farClipPlane, nearClipPlane, aspect, color);
            PreparableGizmo<Frustum, FrustumData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawArrowCore(float3 from, float3 to, Color color, float headLength, float width, float duration)
        {
            var data = new ArrowData(from, to, color, headLength, width);
            PreparableGizmo<Arrow, ArrowData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawArrow2dCore(float3 from, float3 to, float3 normal, Color color, float headLength, float width, float duration)
        {
            var data = new Arrow2dData(from, to, normal, color, headLength, width);
            PreparableGizmo<Arrow2d, Arrow2dData>.AddData(data, duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawWireArrow2dCore(float3 from, float3 to, float3 normal, Color color, float headLength, float headWidth, float duration)
        {
            var data = new WireArrow2dData(from, to, normal, color, headLength, headWidth);
            PreparableGizmo<WireArrow2d, WireArrow2dData>.AddData(data, duration);
        }
    }
}