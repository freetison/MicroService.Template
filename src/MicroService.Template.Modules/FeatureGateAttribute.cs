namespace MicroService.Template.Modules
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class FeatureGateAttribute : Attribute
    {
        public string FeatureFlag { get; }

        public FeatureGateAttribute(string featureFlag)
        {
            FeatureFlag = featureFlag;
        }
    }
}
