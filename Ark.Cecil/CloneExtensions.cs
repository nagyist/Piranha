﻿using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;
using System.Collections.Generic;
using System.Linq;

namespace Ark.Cecil {
    public static class CloneExtensions {
        public static void CloneFrom(this ICollection<GenericParameter> destinationCollection, IEnumerable<GenericParameter> sourceCollection) {
            CloneTo(sourceCollection, destinationCollection);
        }

        public static void CloneFrom(this ICollection<ParameterDefinition> destinationCollection, IEnumerable<ParameterDefinition> sourceCollection) {
            CloneTo(sourceCollection, destinationCollection);
        }

        public static void CloneTo(this IEnumerable<GenericParameter> sourceCollection, ICollection<GenericParameter> destinationCollection) {
            destinationCollection.Clear();
            foreach (var element in sourceCollection) {
                destinationCollection.Add(element.Clone());
            }
        }

        public static void CloneTo(this IEnumerable<ParameterDefinition> sourceCollection, ICollection<ParameterDefinition> destinationCollection) {
            destinationCollection.Clear();
            foreach (var element in sourceCollection) {
                destinationCollection.Add(element.Clone());
            }
        }

        public static GenericParameter Clone(this GenericParameter genericParameter) {
            return new GenericParameter(genericParameter.Name, null);
        }

        public static ParameterDefinition Clone(this ParameterDefinition parameterDef) {
            return new ParameterDefinition(parameterDef.Name, parameterDef.Attributes, parameterDef.ParameterType);
        }

        public static MethodReference Clone(this MethodReference methodRef) {
            var clone = new MethodReference(methodRef.Name, methodRef.ReturnType, methodRef.DeclaringType) {
                HasThis = methodRef.HasThis,
                ExplicitThis = methodRef.ExplicitThis,
                CallingConvention = methodRef.CallingConvention
            };
            //need to clone .MethodReturnType

            clone.Parameters.CloneFrom(methodRef.Parameters);
            clone.GenericParameters.CloneFrom(methodRef.GenericParameters);
            return clone;
        }
    }
}
