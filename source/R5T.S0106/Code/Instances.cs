using System;


namespace R5T.S0106
{
    public class Instances :
        L0055.Instances
    {
        public static L0053.IArrayOperator ArrayOperator => L0053.ArrayOperator.Instance;
        public static T0226.IExampleMembers ExampleMembers => T0226.ExampleMembers.Instance;
        public static L0053.ITypeOperator TypeOperator => L0053.TypeOperator.Instance;
    }
}