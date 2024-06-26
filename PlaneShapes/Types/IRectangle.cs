﻿namespace CsabaDu.FooVaria.PlaneShapes.Types
{
    public interface IRectangle : IPlaneShape, IRectangularShape<IRectangle, ICircle>, IHorizontalRotation<IRectangle>, ICommonBase<IRectangle>
    {
        IExtent Length { get; init; }
        IExtent Width { get; init; }
        IRectangleFactory Factory { get; init; }

        IRectangle GetRectangle(IExtent length, IExtent width);
    }
}
