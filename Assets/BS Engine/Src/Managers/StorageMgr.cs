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
/// If the Blackboard has a DataTable inside, that has been previosly loaded from a file  (IE: PlayerData or Save game files). This DataTable willbe saved again to the same file 
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
            m_blackboard = new DataTable("Blackboard", SerializationMode.XML, false);
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



        #region Public params

        #endregion

        #region Private params

        /// <summary>
        /// DataTable used as blackboard
        /// </summary>
        private DataTable m_blackboard;

        #endregion

        #region Public methods

        /// <summary>
        /// Public property to access to the Blackboard
        /// </summary>
        public DataTable Blackboard
        {
            get { return m_blackboard; }
        }

        
        /// <summary>
        /// Method used to save a DataTable to a file.
        /// 
        /// Using the Application.persistentDataPath
        /// </summary>
        /// <param name="info">DataTable to save</param>
        /// <param name="fileName">Name of the file to save</param>
        public void SaveToFile(DataTable info, string fileName)
        {
            string fullPath = Application.persistentDataPath + "/" + fileName;

            switch (info.SerializationMode)
            {
                case SerializationMode.XML:
                    fullPath += ".xml";
                    SaveToXMLFile(info, fullPath);
                    break;
                case SerializationMode.BIN:
                    //fullPath += ".bs";
                    //SaveToBinaryFile(info, fullPath);
                    break;
                case SerializationMode.BIN_XML:
                    fullPath += ".xml";
                    SaveToXMLFile(info, fullPath);

                    //fullPath = Application.persistentDataPath + "/" + fileName + ".bs";
                    //SaveToBinaryFile(info, fullPath);
                    break;
                case SerializationMode.NONE:
                    break;
            }

        }

        public DataTable LoadFile(string fileName)
        {
            string fullPath = Application.persistentDataPath + "/" + fileName;

            string[] split = fileName.Split('.');

            string extension = split[split.Length - 1];

            DataTable data = null;

            if (extension == "bs")
            {
                //TO DO
                //Load Binary file
            }
            else if (extension == "xml")
            {
                //TO DO
                //Load XML File
                data = LoadXMLFile(fullPath);
            }

            //TO DO - revisar esto por la referencia
            //if (data.LoadToBlackboard)
            //{
            //    Blackboard.Set<DataTable>(data.Name, data);
            //}

            return data;
        }

       

        #endregion

        #region Private methods

        //XML
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

        private DataTable LoadXMLFile(string fullPath)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(fullPath);

            XmlNode rootNode = doc.FirstChild;

            

            DataTable data = XMLSerializer.DeserializeDataTableFromXML(rootNode);


            return data;
        }


        
       


        //BINARY
        private DataTable LoadBinaryFile(string fileName)
        {
            string fullPath = Application.persistentDataPath + "/" + fileName + ".bs";
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

        private void SaveToBinaryFile(DataTable info, string fullPath)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fullPath, FileMode.Open);

            bf.Serialize(file, info);

            file.Close();
        }

        #endregion


    }
}
