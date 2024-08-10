using UGizmo.Internal.Extension;
using UGizmo.Internal.Extension.Jobs;
using Unity.Jobs;

namespace UGizmo.Internal
{
    internal sealed unsafe class PolyTriangle : PreparingJobScheduler<PolyTriangle, PolyTriangleData>
    {
        public override JobHandle Schedule()
        {
            PrimitiveData* triangleBufer = Gizmo<Triangle, PrimitiveData>.Reserve(InstanceCount * CreatePolyTriangleJob.K_TriangleCount);

            JobHandle jobHandle = new CreatePolyTriangleJob()
            {
                GizmoDataPtr = JobDataPtr,
                TriangleResult = triangleBufer,
            }.Schedule(InstanceCount, 16);

            return jobHandle;
        }
    }
}