using UnityEngine;
using System.Collections.Generic;
using System;
using BSEngine;

public class TestScript : MonoBehaviour {



	// Use this for initialization
	void Start () {


        //Debug.Log(Application.persistentDataPath);

        ///Escribir algo en la pizarra
        //StorageMgr.Singleton.Blackboard.Set<float>("health", 99.9f);
        //StorageMgr.Singleton.Blackboard.Set<int>("experience", 120233131);

        //muestro por pantalla lo que hay en la pizarra
        //Debug.Log(StorageMgr.Singleton.Blackboard.Get<float>("health"));
        

        ///Escribo mi propia data table
        DataTable data = new DataTable("dataTest", SerializationMode.XML, true);

        data.Set<string>("dataTest1", "hola");
        data.Set<string>("dataTest2", "mundo");
        data.Set<double>("dataasdasdadeeee", 258895.33);
        data.Set<long>("dataTest3", 1458);
        data.Set<short>("dataTest4", 144);
        data.Set<char>("dataTest5", 'c');
        data.Set<byte>("dataTest6", 5);
        data.Set<Vector2>("dataTest7", new Vector2(0.0f, 3.12526899955966f));
        data.Set<Vector3>("dataTest8", new Vector3(0.0f, 3.0f, 3.2f));
        data.Set<Vector4>("dataTest9", new Vector4(0.0f, 3.0f, 3.3f, 4.5f));
        data.Set<Quaternion>("datasdasda", new Quaternion(1.0f, 5.0f, -0.025f, 0.3f));
        //data.Set<Transform>("dataTest0", gameObject.transform);


        List<int> a = new List<int>();


        for (int i = 0; i < 10; ++i)
        {
            a.Add(i);
        }

        data.Set<List<int>>("lista", a);

        //la serializo
        StorageMgr.Singleton.SaveToFile(data, "dataTest");



        ///cargo la tabla!!
        DataTable datLoaded = StorageMgr.Singleton.LoadFile("dataTest.xml");

        List<int> aa = datLoaded.Get<List<int>>("lista");


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    
}
