using System;
using System.Linq;

using R5T.T0141;
using R5T.T0225.T000;


namespace R5T.S0106
{
    [ExplorationsMarker]
    public partial interface IExplorations : IExplorationsMarker
    {
        /// <summary>
        /// Get the generic type parameter constraints for the type parameter of a type,
        /// and then just manually inspect the results.
        /// Conclusion: type parameter constraints are represented as Type instances.
        /// </summary>
        public void Inspect_GenericTypeRestrictions()
        {
            var genericTypeWithConstrainedTypeParameter =
                // where T : struct, constraint type is System.ValueType
                //Instances.ExampleMembers.GenericClass_002_Open
                // where T : class, new(), class has no constraint, new has no constraint.
                //Instances.ExampleMembers.GenericClass_003_Open
                // where T : R5T.T0225.T000.IIInterface_001, just the type.
                //Instances.ExampleMembers.GenericClass_004_Open
                // where T : U, the generic type parameter U.
                Instances.ExampleMembers.GenericClass_005_Open
                ;

            var typeParameters = Instances.TypeOperator.Get_GenericTypeParameterTypes(genericTypeWithConstrainedTypeParameter);

            var typeParameterWithConstraints = typeParameters.First();

            var constraints = typeParameterWithConstraints.GetGenericParameterConstraints();

            Console.WriteLine(constraints);
        }

        /// <summary>
        /// Cannot select generic methods by name!
        /// </summary>
        public void NameOfGenericTypedMethods()
        {
            Console.WriteLine(nameof(MethodsClass_001.Method_006));

            //// Error CS0305  Using the generic method group 'Method_006' requires 1 type arguments
            //Console.WriteLine(nameof(MethodsClass.Method_006<>));

            //// Error CS8084  Type parameters are not allowed on a method group as an argument to 'nameof'.R5T.S0106
            //Console.WriteLine(nameof(MethodsClass.Method_006<string>));

            var method = typeof(MethodsClass_001).GetMethod(
                nameof(MethodsClass_001.Method_006),
                1,
                Instances.ArrayOperator.Empty<Type>());

            Console.WriteLine(method.Name);

            //// Error CS0103  The name 'nameof' does not exist in the current context
            //// Error CS1525  Invalid expression term ','
            //// Error CS1525  Invalid expression term '>'
            //// Error CS1525  Invalid expression term ')'
            //Console.WriteLine(nameof(MethodsClass.Method_006<,>));
        }

        /// <summary>
        /// How do the generic type inputs to closed generic types (generic type arguments) differ from generic type inputs to open generic types (generic type parameters)?
        /// </summary>
        public void GenericTypeArgumentsVsParameters()
        {
            var closedMember = Instances.ExampleMembers.GenericClass_001_Closed;
            var openMember = Instances.ExampleMembers.GenericClass_001_Open;

            var closedGenericTypeArguments = closedMember.GetGenericArguments();
            var openGenericTypeParameters = openMember.GetGenericArguments();

            var genericTypeArgument = closedGenericTypeArguments.First();
            // genericTypeArgument.IsGenericParameter, false
            // => These are *actual* types, like System.String. The only way to know they were type arguments to another type is the provenance
            //  (the fact they came from getting the generic arguments of a generic type).

            var genericTypeParameter = openGenericTypeParameters.First();
            // genericTypeParameter.IsGenericParameter, true

            Console.WriteLine(genericTypeArgument);
            Console.WriteLine(genericTypeParameter);
        }
    }
}
