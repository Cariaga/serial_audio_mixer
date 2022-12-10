public static class LerpMethod
{
    public static decimal Map( decimal value, decimal fromSource, decimal toSource, decimal fromTarget, decimal toTarget)
    {
       var computed = (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
       return computed;
    }
    public static float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
       var computed = (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
       return computed;
    }
    public static int Map( int value, int fromSource, int toSource, int fromTarget, int toTarget)
    {
       var computed = (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
     
       return computed;
    }
}
