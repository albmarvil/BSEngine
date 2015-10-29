///----------------------------------------------------------------------
/// @file Predicates.cs
///
/// This file contains the declaration of Predicates class.
/// 
/// This class is a method container class. All methods in this class are static and returns
/// a boolean value
///
/// @author Alberto Martinez Villaran <tukaram92@gmail.com>
/// @date 29/10/2015
///----------------------------------------------------------------------



using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BSEngine
{
    namespace Utils
    {
        public class Predicates {

            /// <summary>
            /// Definition of basic types of BSEngine.
            /// True if the given object is considered basic type
            /// </summary>
            /// <param name="o">object to evaluate</param>
            /// <returns>True: object is a basic type</returns>
            public static bool IsBasicType(object o)
            {
                return o.GetType() == typeof(string)
                    || o.GetType() == typeof(int)
                    || o.GetType() == typeof(short)
                    || o.GetType() == typeof(long)
                    || o.GetType() == typeof(float)
                    || o.GetType() == typeof(double)
                    || o.GetType() == typeof(bool)
                    || o.GetType() == typeof(char)
                    || o.GetType() == typeof(byte)
                    || o.GetType() == typeof(Vector2)
                    || o.GetType() == typeof(Vector3)
                    || o.GetType() == typeof(Vector4)
                    || o.GetType() == typeof(Quaternion)
                    || o.GetType() == typeof(Transform);
            }

            /// <summary>
            /// Predicate used to determine if an object is a List
            /// </summary>
            /// <param name="o">Ob ject to evaluate</param>
            /// <returns>True: object is a List</returns>
            public static bool IsList(object o)
            {
                if (o == null) return false;
                return o is IList &&
                       o.GetType().IsGenericType &&
                       o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
            }

            /// <summary>
            /// Predicate used to determine if an object is a Dictionary
            /// </summary>
            /// <param name="o">Object to evluate</param>
            /// <returns>True: object is a Dictionary</returns>
            public static bool IsDictionary(object o)
            {
                if (o == null) return false;
                return o is IDictionary &&
                       o.GetType().IsGenericType &&
                       o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
            }

        }
    }
}




