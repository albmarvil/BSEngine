///----------------------------------------------------------------------
/// @file XMLSerializer.cs
///
/// This file contains the declaration of XMLSerializer class.
///     XMLSerializer class is a static method container. All the methods implemented are used to serialize an
///     object to an XML.
/// 
/// @author Alberto Martinez Villaran <tukaram92@gmail.com>
/// @date 29/10/2015
///----------------------------------------------------------------------



using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using System.IO;
using System.Xml;
using System;

namespace BSEngine
{
    namespace Utils
    {
        public class XMLSerializer
        {
            /// <summary>
            /// Serializes an object to XML format. This object should be a basic Type.
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="o">Object to serialize</param>
            public static void SerializeBasicTypeToXML(ref XmlDocument doc, ref XmlNode elementNode, object o)
            {
                Type type = o.GetType();

                if (type == typeof(string))
                {
                    string value = (string)o;
                    SerializeStringToXML(ref doc, ref elementNode, value);
                }
                else if (type == typeof(int))
                {
                    int value = (int)o;
                    SerializeIntegerToXML(ref doc, ref elementNode, value);
                }
                else if (type == typeof(float))
                {
                    float value = (float)o;
                    SerializeFloatToXML(ref doc, ref elementNode, value);
                }
                else if (type == typeof(double))
                {
                    double value = (double)o;
                    SerializeDoubleToXML(ref doc, ref elementNode, value);
                }
                else if (type == typeof(long))
                {
                    long value = (long)o;
                    SerializeLongToXML(ref doc, ref elementNode, value);
                }
                else if (type == typeof(short))
                {
                    short value = (short)o;
                    SerializeShortToXML(ref doc, ref elementNode, value);
                }
                else if (type == typeof(char))
                {
                    char value = (char)o;
                    SerializeCharToXML(ref doc, ref elementNode, value);
                }
                else if (type == typeof(byte))
                {
                    byte value = (byte)o;
                    SerializeByteToXML(ref doc, ref elementNode, value);
                }
                else if (type == typeof(bool))
                {
                    bool value = (bool)o;
                    SerializeBoolToXML(ref doc, ref elementNode, value);
                }
                else if (type == typeof(Vector2))
                {
                    Vector2 value = (Vector2)o;
                    SerializeVector2ToXML(ref doc, ref elementNode, value);
                }
                else if (type == typeof(Vector3))
                {
                    Vector3 value = (Vector3)o;
                    SerializeVector3ToXML(ref doc, ref elementNode, value);
                }
                else if (type == typeof(Vector4))
                {
                    Vector4 value = (Vector4)o;
                    SerializeVector4ToXML(ref doc, ref elementNode, value);
                }
                else if (type == typeof(Quaternion))
                {
                    Quaternion value = (Quaternion)o;
                    SerializeQuaternionToXML(ref doc, ref elementNode, value);
                }
                else if (type == typeof(Transform))
                {
                    Transform value = (Transform)o;
                    SerializeTransformToXML(ref doc, ref elementNode, value);
                }
            }

            #region Basic Types serialization
            /// <summary>
            /// String serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">string to serialize</param>
            public static void SerializeStringToXML(ref XmlDocument doc, ref XmlNode elementNode, string value)
            {
                elementNode.InnerText = value;
            }

            /// <summary>
            /// Integer serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">int to serialize</param>
            public static void SerializeIntegerToXML(ref XmlDocument doc, ref XmlNode elementNode, int value)
            {
                elementNode.InnerText = value.ToString();
            }

            /// <summary>
            /// Float serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">float to serialize</param>
            public static void SerializeFloatToXML(ref XmlDocument doc, ref XmlNode elementNode, float value)
            {
                elementNode.InnerText = value.ToString();
            }

            /// <summary>
            /// Double serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">double to serialize</param>
            public static void SerializeDoubleToXML(ref XmlDocument doc, ref XmlNode elementNode, double value)
            {
                elementNode.InnerText = value.ToString();
            }

            /// <summary>
            /// Short serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">short to serialize</param>
            public static void SerializeShortToXML(ref XmlDocument doc, ref XmlNode elementNode, short value)
            {
                elementNode.InnerText = value.ToString();
            }

            /// <summary>
            /// Long serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">long to serialize</param>
            public static void SerializeLongToXML(ref XmlDocument doc, ref XmlNode elementNode, long value)
            {
                elementNode.InnerText = value.ToString();
            }


            /// <summary>
            /// Char serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">char to serialize</param>
            public static void SerializeCharToXML(ref XmlDocument doc, ref XmlNode elementNode, char value)
            {
                elementNode.InnerText = value.ToString();
            }

            /// <summary>
            /// Byte serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">byte to serialize</param>
            public static void SerializeByteToXML(ref XmlDocument doc, ref XmlNode elementNode, byte value)
            {
                elementNode.InnerText = value.ToString();
            }

            /// <summary>
            /// Bool serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">boolean to serialize</param>
            public static void SerializeBoolToXML(ref XmlDocument doc, ref XmlNode elementNode, bool value)
            {
                elementNode.InnerText = value.ToString();
            }

            /// <summary>
            /// Vector2 serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">vector2 to serialize</param>
            public static void SerializeVector2ToXML(ref XmlDocument doc, ref XmlNode elementNode, Vector2 value)
            {
                elementNode.InnerText = value.ToString();
            }

            /// <summary>
            /// Vector3 serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">vector3 to serialize</param>
            public static void SerializeVector3ToXML(ref XmlDocument doc, ref XmlNode elementNode, Vector3 value)
            {
                elementNode.InnerText = value.ToString();
            }

            /// <summary>
            /// Vector4 serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">vector4 to serialize</param>
            public static void SerializeVector4ToXML(ref XmlDocument doc, ref XmlNode elementNode, Vector4 value)
            {
                elementNode.InnerText = value.ToString();
            }

            /// <summary>
            /// Quaternion serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">Quaternion to serialize</param>
            public static void SerializeQuaternionToXML(ref XmlDocument doc, ref XmlNode elementNode, Quaternion value)
            {
                elementNode.InnerText = value.ToString();
            }

            /// <summary>
            /// Transform serialization to XML
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="elementNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="value">Transform to serialize</param>
            public static void SerializeTransformToXML(ref XmlDocument doc, ref XmlNode elementNode, Transform value)
            {
                XmlNode posNode = doc.CreateElement("position");
                Vector3 pos = value.position;
                SerializeVector3ToXML(ref doc, ref posNode, pos);
                elementNode.AppendChild(posNode);

                XmlNode scaleNode = doc.CreateElement("scale");
                Vector3 scale = value.localScale;
                SerializeVector3ToXML(ref doc, ref scaleNode, scale);
                elementNode.AppendChild(scaleNode);

                XmlNode rotationNode = doc.CreateElement("rotation");
                Quaternion rotation = value.rotation;
                SerializeQuaternionToXML(ref doc, ref rotationNode, rotation);
                elementNode.AppendChild(rotationNode);
            }

            #endregion

            #region collections serialization
            /// <summary>
            /// DataTable serialization to XML.
            /// 
            /// This is the main type to be serialized on XMLs. This method relies on basic types serialization.
            /// </summary>
            /// <param name="doc">(REF) Document where object is serialized</param>
            /// <param name="rootNode">(REF) rootNode where to include the object inside the document</param>
            /// <param name="info">(REF) DataTable to serialize</param>
            public static void SerializeDataTableToXML(ref XmlDocument doc, ref XmlNode rootNode, ref DataTable info)
            {
                //root node
                XmlNode tableNode = doc.CreateElement("DataTable");
                rootNode.AppendChild(tableNode);

                //Name node
                XmlNode nameNode = doc.CreateElement("name");
                XmlAttribute typeAttribute = doc.CreateAttribute("type");
                typeAttribute.Value = typeof(string).ToString();
                nameNode.Attributes.Append(typeAttribute);
                nameNode.InnerText = info.Name;
                tableNode.AppendChild(nameNode);

                //SerializationMode node
                XmlNode modeNode = doc.CreateElement("SerializationMode");
                //reusing "typeAttribute" variable
                typeAttribute = doc.CreateAttribute("type");
                typeAttribute.Value = typeof(SerializationMode).ToString();
                modeNode.Attributes.Append(typeAttribute);
                modeNode.InnerText = info.SerializationMode.ToString();
                tableNode.AppendChild(modeNode);

                //loadToBlackboard node
                XmlNode loadNode = doc.CreateElement("LoadToBlackboard");
                typeAttribute = doc.CreateAttribute("type");
                typeAttribute.Value = typeof(bool).ToString();
                loadNode.Attributes.Append(typeAttribute);
                loadNode.InnerText = info.LoadToBlackboard.ToString();
                tableNode.AppendChild(loadNode);

                //Data Node
                XmlNode dataNode = doc.CreateElement("Data");
                tableNode.AppendChild(dataNode);

                foreach (string key in info.Data.Keys)
                {
                    XmlNode elementNode = doc.CreateElement("DataElement");
                    XmlAttribute nameAttrib = doc.CreateAttribute("name");
                    nameAttrib.Value = key;

                    elementNode.Attributes.Append(nameAttrib);

                    Type type = info.Data[key].GetType();
                    XmlAttribute typeAttrib = doc.CreateAttribute("type");

                    typeAttrib.Value = type.ToString();

                    elementNode.Attributes.Append(typeAttrib);


                    if (Predicates.IsBasicType(info.Data[key]))
                    {
                        SerializeBasicTypeToXML(ref doc, ref elementNode, info.Data[key]);
                    }
                    else if (type == typeof(DataTable))
                    {
                        DataTable data = (DataTable)info.Data[key];
                        SerializeDataTableToXML(ref doc, ref elementNode, ref data);
                    }
                    else if (Predicates.IsList(info.Data[key]))
                    {
                        SerializeListToXML(ref doc, ref elementNode, info.Data[key]);

                    }
                    else if (Predicates.IsDictionary(info.Data[key]))
                    {
                        SerializeDictionaryToXML(ref doc, ref elementNode, info.Data[key]);
                    }
                    else
                    {
                        Debug.LogError("Type: " + type + " NOT supported on XML serialization. Please use Binary files instead");
                    }


                    dataNode.AppendChild(elementNode);
                }
            }

            /// <summary>
            /// List serialization to XML
            /// </summary>
            /// <param name="doc">(REF) XML Document where to serialize</param>
            /// <param name="elementNode">(REF) rootNode to serialize inside the document</param>
            /// <param name="list">List to serialize</param>
            private static void SerializeListToXML(ref XmlDocument doc, ref XmlNode elementNode, object list)
            {
                Type innerType = list.GetType().GetGenericArguments()[0];

                XmlNode listNode = doc.CreateElement("List");

                XmlAttribute innerAttrib = doc.CreateAttribute("innerType");
                innerAttrib.Value = innerType.ToString();
                listNode.Attributes.Append(innerAttrib);

                elementNode.AppendChild(listNode);


                IList objList = (IList)list;

                foreach (object o in objList)
                {
                    XmlNode listElementNode = doc.CreateElement("ListElement");

                    if (Predicates.IsBasicType(o))
                    {
                        SerializeBasicTypeToXML(ref doc, ref listElementNode, o);
                    }
                    else if (innerType == typeof(DataTable))
                    {
                        DataTable data = (DataTable)o;
                        SerializeDataTableToXML(ref doc, ref listElementNode, ref data);
                    }
                    else if (Predicates.IsList(o))
                    {
                        SerializeListToXML(ref doc, ref listElementNode, o);
                    }
                    else if (Predicates.IsDictionary(o))
                    {
                        SerializeDictionaryToXML(ref doc, ref elementNode, o);
                    }
                    else
                    {
                        Debug.LogError("Type: " + o.GetType() + " NOT supported on XML serialization. Please use Binary files instead");
                    }

                    listNode.AppendChild(listElementNode);
                }
            }

            /// <summary>
            /// Dictionary serialization to XML
            /// </summary>
            /// <param name="doc">(REF) XML Document where to serialize</param>
            /// <param name="elementNode">(REF) rootNode to serialize inside the document</param>
            /// <param name="dictionary">Dictionary to serialize</param>
            private static void SerializeDictionaryToXML(ref XmlDocument doc, ref XmlNode elementNode, object dictionary)
            {
                Type keyType = dictionary.GetType().GetGenericArguments()[0];
                Type valueType = dictionary.GetType().GetGenericArguments()[1];

                XmlNode dicNode = doc.CreateElement("Dictionary");

                XmlAttribute keyAttrib = doc.CreateAttribute("keyType");
                keyAttrib.Value = keyType.ToString();
                dicNode.Attributes.Append(keyAttrib);

                XmlAttribute valueAttrib = doc.CreateAttribute("valueType");
                valueAttrib.Value = valueType.ToString();
                dicNode.Attributes.Append(valueAttrib);

                elementNode.AppendChild(dicNode);


                IDictionary objDic = (IDictionary)dictionary;

                foreach (object key in objDic.Keys)
                {
                    XmlNode pairNode = doc.CreateElement("KeyValuePair");
                    dicNode.AppendChild(pairNode);

                    XmlNode keyNode = doc.CreateElement("key");
                    XmlAttribute keyAttribute = doc.CreateAttribute("type");
                    keyAttribute.Value = keyType.ToString();
                    keyNode.Attributes.Append(keyAttribute);

                    XmlNode valueNode = doc.CreateElement("value");
                    XmlAttribute valueAttribute = doc.CreateAttribute("type");
                    valueAttribute.Value = valueType.ToString();
                    valueNode.Attributes.Append(valueAttribute);

                    #region key serialization

                    if (Predicates.IsBasicType(key))
                    {
                        SerializeBasicTypeToXML(ref doc, ref keyNode, key);
                    }
                    else if (keyType == typeof(DataTable))
                    {
                        DataTable data = (DataTable)key;
                        SerializeDataTableToXML(ref doc, ref keyNode, ref data);
                    }
                    else if (Predicates.IsList(key))
                    {
                        SerializeListToXML(ref doc, ref keyNode, key);
                    }
                    else if (Predicates.IsDictionary(key))
                    {
                        SerializeDictionaryToXML(ref doc, ref elementNode, key);
                    }
                    else
                    {
                        Debug.LogError("Type: " + key.GetType() + " NOT supported on XML serialization. Please use Binary files instead");
                    }

                    pairNode.AppendChild(keyNode);
                    #endregion


                    #region value serialization
                    if (Predicates.IsBasicType(objDic[key]))
                    {
                        SerializeBasicTypeToXML(ref doc, ref valueNode, objDic[key]);
                    }
                    else if (keyType == typeof(DataTable))
                    {
                        DataTable data = (DataTable)objDic[key];
                        SerializeDataTableToXML(ref doc, ref valueNode, ref data);
                    }
                    else if (Predicates.IsList(objDic[key]))
                    {
                        SerializeListToXML(ref doc, ref valueNode, objDic[key]);
                    }
                    else if (Predicates.IsDictionary(objDic[key]))
                    {
                        SerializeDictionaryToXML(ref doc, ref elementNode, objDic[key]);
                    }
                    else
                    {
                        Debug.LogError("Type: " + objDic[key].GetType() + " NOT supported on XML serialization. Please use Binary files instead");
                    }

                    pairNode.AppendChild(valueNode);
                    #endregion

                }
            }
            #endregion
        }
    }
}

