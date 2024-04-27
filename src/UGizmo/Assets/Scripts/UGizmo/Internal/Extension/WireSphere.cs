﻿using UGizmo.Internal.Extension.Jobs;

namespace UGizmo.Internal.Extension
{
    internal sealed class WireSphereAsset : GizmoAsset<WireSphere, PrimitiveData>
    {
        public override string MeshName => "WireSphere";
        public override string MaterialName => "CommonWire";
    }

    internal sealed class WireSphere : GizmoRenderer<PrimitiveData>
    {
        public override int RenderQueue { get; protected set; } = 3000;
    }
}