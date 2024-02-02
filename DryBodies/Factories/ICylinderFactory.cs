﻿namespace CsabaDu.FooVaria.DryBodies.Factories;

public interface ICylinderFactory : IDryBodyFactory<ICylinder, ICircle>, ICircularShapeFactory<ICylinder, ICuboid>/*, IFactory<ICylinder>*/
{
    ICylinder Create(IExtent radius, IExtent height);
    ICircle CreateBaseFace(IExtent radius);
    IRectangle CreateVerticalProjection(ICylinder cylinder);
}
