using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C3L3 : MonoBehaviour
{
    public int myInteger = 5;
    public float myFloat = 3.5f;
    public bool myBoolean = true;
    public string myString = "Hellow World";
    public int[] myArrayOfInts;

    //private int _myPrivateInteger = 10;
    //float _myPrivateFloat = -5.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Operators
        //Math operators; +, -, *, /, %
        //Last operators is module, it returns the rest of a division
        Debug.Log("let's sum 10 to myInteger. Rigth now its value is " + myInteger);
        myInteger = myInteger + 10;
        Debug.Log("After the sum the value is " + myInteger);

        //Calling a function
        IsEven(myInteger);

        //Calling a function inside an if
        if (IsEven(myInteger)) {
            MyDebug("myInteger is even!!");
        } else {
            MyDebug("myInteger is odd!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Funciones privadas
    bool IsEven(int num)
    {
        if (num % 2 == 0) {
            return true;
        }
        else {
            return false;
        }
    }

    void MyDebug(string message)
    {
        // Debug a message
        Debug.Log(message);
    }
}
