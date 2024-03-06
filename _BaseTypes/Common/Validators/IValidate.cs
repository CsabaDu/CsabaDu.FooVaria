namespace CsabaDu.FooVaria.BaseTypes.Common.Validators
{
    internal interface IValidate
    {
        bool IsValid();

        void Validate(IRootObject rootObject, string paramName);
    }
}
