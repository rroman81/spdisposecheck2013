using Microsoft.FxCop.Sdk;

namespace SPDisposeCheckRules
{
    public abstract class SPDisposeCheckBaseRule : BaseIntrospectionRule
    {
        protected SPDisposeCheckBaseRule(string name) :
            base(
            name,
            "SPDisposeCheckRules.Rules",
            typeof(SPDisposeCheckBaseRule).Assembly)
        {
        }
    }
}
