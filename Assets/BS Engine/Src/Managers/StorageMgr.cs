///----------------------------------------------------------------------
/// @file StorageMgr.cs
///
/// This file contains the declaration of StorageMgr class.
/// 
/// StorageMgr has two main features:
/// -Blackboard (volatile memory) management
/// -File management (Load/Save)
/// 
/// 1 -Blackboard
/// 
/// The blackboard is a huge DataTable used to hold gamespecific information. It's objective is to be used as a "central bucket of info", where you can write info into it and recover
/// it lately from other part of the game.
/// 
/// Blackboard memory is volatile. Everything on it will be flushed away after the application is shutted down.
/// 
/// If the Blackboard has a DataTable inside, that has been previosly loaded from a file  (IE: PlayerData or Save game files). This DataTable will be saved again to the same file 
/// before clearing the blackboard.
/// 
/// 
/// 2 - File management
/// 
/// This manager allows to serialize data in form of "DataTables" to a binary file or XML file
///
/// @author Alberto Martinez Villaran <tukaram92@gmail.com>
/// @date 29/10/2015
///----------------------------------------------------------------------


using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System;

using BSEngine.Utils;

namespace BSEngine
{
    /// <summary>
    /// Serialization mode. uised to indicate how to serialize a DataTable
    /// </summary>
    [System.Serializable]
    public enum SerializationMode
    {
        NONE,
        XML,
        BIN,
        BIN_XML
    }

    public class StorageMgr
    {


        #region Singleton

        /// <summary>
        /// Singleton instance of the class
        /// </summary>
        private static StorageMgr m_instance = null;

        /// <summary>
        /// Property to get the singleton instance of the class.
        /// </summary>
        public static StorageMgr Singleton { get { return m_instance; } }

        // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
        static StorageMgr() { }

        /// <summary>
        /// Used to initialize the StorageMgr singleton instance
        /// </summary>
        ///<returns>True if everything went ok</returns>
        public static bool Init()
        {
            if (m_instance != null)
            {
                Debug.LogError("Second initialisation not allowed");
                return false;
            }
            else
            {
                m_instance = new StorageMgr();
                return m_instance.open();
            }
        }

        /// <summary>
        /// Used to deinitialize the StorageMgr singleton data.
        /// </summary>
        public static void Release()
        {
            if (m_instance != null)
            {
                m_instance.close();
                m_instance = null;
            }
        }



        /// <summary>
        /// Used as second step on singleton initialisation. Used to specific code of the different Engine & Game managers
        /// </summary>
        /// <returns>Should return true if everything went ok</returns>
        private bool open()
        {
            m_blackboard = null;
            m_blackboard = new DataTable("Blackboard", SerializationMode.BIN_XML, false);
            return true;
        }

        /// <summary>
        /// Used as second step on singleton initialisation. Used to specific code of the different Engine & Game managers
        /// </summary>
        private void close()
        {
            SaveToFile(m_blackboard, "Blackboard");
            m_blackboard = null;
        }

        #endregion

        #region Private params

        /// <summary>
        /// DataTable used as blackboard
        /// </summary>
        private static DataTable m_blackboard;

        #endregion

        #region Public methods

        /// <summary>
        /// Public property to access to the Blackboard
        /// </summary>
        public static DataTable Blackboard
        {
            get { return m_blackboard; }
        }

        
        /// <summary>
        /// Method used to save a DataTable to a file. (Blocking operation)
        /// 
        /// Using the Application.persistentDataPath
        /// </summary>
        /// <param name="info">DataTable to save</param>
        /// <param name="fileName">Name of the file to save</param>
        public void SaveToFile(DataTable info, string fileName)
        {
            string fullPath = Application.persistentDataPath + "/" + fileName;

            DataTable toSerialize = TypeSerializationChanger.DataTableTypesToBSEngine(info);

            switch (info.SerializationMode)
            {
                case SerializationMode.XML:
                    fullPath += ".xml";
                    SaveToXMLFile(toSerialize, fullPath);
                    break;
                case SerializationMode.BIN:
                    fullPath += ".bs";
                    SaveToBinaryFile(toSerialize, fullPath);
                    break;
                case SerializationMode.BIN_XML:
                    fullPath += ".xml";
                    SaveToXMLFile(toSerialize, fullPath);

                    fullPath = Application.persistentDataPath + "/" + fileName + ".bs";
                    SaveToBinaryFile(toSerialize, fullPath);
                    break;
                case SerializationMode.NONE:
                    break;
            }

        }

        /// <summary>
        /// Method used to load a DataTable from a file. (Blocking operation)
        /// 
        /// Using the Application.persistentDataPath
        /// 
        /// It will load an xml or an binary file depending on the file given
        /// </summary>
        /// <param name="fileName">File name to load</param>
        /// <returns>DataTable loaded from the given file</returns>
        public DataTable LoadFile(string fileName)
        {
            string fullPath = Application.persistentDataPath + "/" + fileName;

            string[] split = fileName.Split('.');

            string extension = split[split.Length - 1];

            DataTable data = null;

            if (extension == "bs")
            {
                data = TypeSerializationChanger.DataTableTypesToUnity(LoadBinaryFile(fullPath));
            }
            else if (extension == "xml")
            {
                data = TypeSerializationChanger.DataTableTypesToUnity(LoadXMLFile(fullPath));
            }

            if (data.LoadToBlackboard)
            {
                Blackboard.Set<DataTable>(data.Name, data);
            }

            return data;
        }

       

        #endregion

        #region Private methods

        /// <summary>
        /// XML serialization method.
        /// 
        /// Method used to save a DataTable in XML format.
        /// 
        /// It uses XMLSerializer helper class.
        /// </summary>
        /// <param name="info">DataTable to serialize</param>
        /// <param name="fullPath">Fullpath of the file in the system</param>
        private void SaveToXMLFile(DataTable info, string fullPath)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode rootNode = doc.CreateElement("BSEngine_XML");

            XmlAttribute dateAttribute = doc.CreateAttribute("date");
            dateAttribute.Value = System.DateTime.Now.ToString();
            rootNode.Attributes.Append(dateAttribute);

            doc.AppendChild(rootNode);

            XMLSerializer.SerializeDataTableToXML(ref doc, ref rootNode, ref info);

            doc.Save(fullPath);

        }

        /// <summary>
        /// XML deserialization method.
        /// 
        /// Method used to load a DataTable from a XML
        /// 
        /// It uses XMLSerializer helper class.
        /// </summary>
        /// <param name="fullPath">file's fullpath in the system</param>
        /// <returns>DataTable loaded from XML</returns>
        private DataTable LoadXMLFile(string fullPath)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(fullPath);

            XmlNode rootNode = doc.FirstChild;

            DataTable data = XMLSerializer.DeserializeDataTableFromXML(rootNode);

            return data;
        }

        /// <summary>
        /// Binary serialization method.
        /// </summary>
        /// <param name="info">DataTable to serialize</param>
        /// <param name="fullPath">File's fullpath in the system</param>
        private void SaveToBinaryFile(DataTable info, string fullPath)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fullPath, FileMode.OpenOrCreate);

            bf.Serialize(file, info);

            file.Close();
        }

        /// <summary>
        /// Binary deserialization method
        /// </summary>
        /// <param name="fullPath">File's fullpath in the system</param>
        /// <returns>DataTable loaded from binary file</returns>
        private DataTable LoadBinaryFile(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(fullPath, FileMode.Open);

                DataTable data = (DataTable)bf.Deserialize(file);

                file.Close();

                return data;
            }
            else
            {
                return null;
            }
        }

        

        #endregion


    }
}
