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
                return IsBasicType(o.GetType().ToString());
            }

            /// <summary>
            /// Definition of basic types of BSEngine.
            /// True if the given object is considered basic type
            /// </summary>
            /// <param name="o">object to evaluate</param>
            /// <returns>True: object is a basic type</returns>
            public static bool IsBasicType(string t)
            {
                return t == typeof(string).ToString()
                    || t == typeof(int).ToString()
                    || t == typeof(short).ToString()
                    || t == typeof(long).ToString()
                    || t == typeof(float).ToString()
                    || t == typeof(double).ToString()
                    || t == typeof(bool).ToString()
                    || t == typeof(char).ToString()
                    || t == typeof(byte).ToString()
                    || t == typeof(Vector2).ToString()
                    || t == typeof(Vector3).ToString()
                    || t == typeof(Vector4).ToString()
                    || t == typeof(Quaternion).ToString();
                    //|| t == typeof(Transform).ToString();
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
            /// Predicate used to determine if an object is a List
            /// </summary>
            /// <param name="o">Ob ject to evaluate</param>
            /// <returns>True: object is a List</returns>
            public static bool IsList(Type t)
            {
                if (t == null) return false;
                return t is IList &&
                       t.IsGenericType &&
                       t.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
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

            /// <summary>
            /// Predicate used to determine if an object is a Dictionary
            /// </summary>
            /// <param name="o">Object to evluate</param>
            /// <returns>True: object is a Dictionary</returns>
            public static bool IsDictionary(Type t)
            {
                if (t == null) return false;
                return t is IDictionary &&
                       t.IsGenericType &&
                       t.GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
            }

        }
    }
}




