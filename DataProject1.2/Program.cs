﻿using System;using System.Collections;using System.IO;using System.Linq;namespace DataProject1._2{    class Program    {        static string[] lines = System.IO.File.ReadAllLines(path: @"iris.txt");        static int numberofline = 0;        static string[,] data = new string[lines.Length, 5];//We create a two dimensional string                        static double[,] calculatekNN(string[,] array, double[] array2, int start, int finish, int arraylength)//We calculate the distance for guessing type of each flower and this fuction returns the distance and flowers index in data array.        {            double[,] kNNs = new double[arraylength, 2];            for (int a = 0; a < arraylength ; a++)            {                double total1 = 0;                 for (int b = start; b < finish; b++)                {                    total1 += Math.Pow(((Double.Parse(array[a, b]) - array2[b])), 2);                }                double distance = Math.Sqrt(total1);                kNNs[a, 0] = distance;                kNNs[a, 1] = a;//for index            }            return kNNs;        }        static string Sort(double[,] n, int o, string[,] array)        {            for (int i = 0; i < n.GetLength(0) - 1; i++)            {                for (int j = i; j < n.GetLength(0); j++)                {                    if (n[i, 0] > n[j, 0]) // sort by ascending by first index of each row                    {                        for (int r = 0; r < n.GetLength(1); r++)                        {                            var temp = n[i, r];                            n[i, r] = n[j, r];                            n[j, r] = temp;                        }                    }                }            }            int versicolor = 0;            int setosa = 0;            int virginica = 0;            Console.WriteLine("The features of the " + o + " closest flower:");            for (int a = 0; a < o; a++)            {                int index = (int)n[a, 1];                float distance = (float)n[a, 0];                Console.WriteLine("Sepal Length: " + array[index, 0] + " Sepal Width: " + array[index, 1] + " Petal Length: " + array[index, 2] + " Petal Width: " + array[index, 3] + " Type of This Flower: " + array[index, 4] + " Distance: " + distance );                if (array[index, 4] == "Iris-setosa")                {                    setosa++;                }                else if (array[index, 4] == "Iris-versicolor")                {                    versicolor++;                }                else                {                    virginica++;                }            }            string max = " ";            if (versicolor == setosa || setosa == virginica || virginica == versicolor)            {                int index1 = (int)n[0, 1];                max = array[index1, 4];            }            else            {                if (setosa > versicolor && setosa > virginica)                {                    max = "Iris-setosa";                }                else if (versicolor > setosa && versicolor > virginica)                {                    max = "Iris-versicolor";                }                else                {                    max = "Iris-virginica";                }            }            Console.WriteLine("Guess for this flower: " + max);            return max;        }//We sorted the array which remains flowers distance and index for distance.        static void Start()        {                        Console.Write("Enter K value : ");            string kinputstring = Console.ReadLine();            int k = Int32.Parse(kinputstring);            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++");            Console.WriteLine("Press 1 for entering all values.");            Console.WriteLine("Press 2 for entering sepal values.");            Console.WriteLine("Press 3 for entering petal values.");            Console.WriteLine("Press 4 for listing the data.");            Console.Write("Enter (1-2-3-4) : ");            string situationinputstring = Console.ReadLine();            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++");            int situation = Int32.Parse(situationinputstring);            double[] inputvalues = new double[4];            switch (situation)            {                case 1:// This case is for all features.                    Console.Write("Enter the sepal length : ");                    string sepallength = Console.ReadLine();                    inputvalues[0] = Double.Parse(sepallength);                    Console.Write("Enter the sepal width : ");                    string sepalwidth = Console.ReadLine();                    inputvalues[1] = Double.Parse(sepalwidth);                    Console.Write("Enter the petal length : ");                    string petallength = Console.ReadLine();                    inputvalues[2] = Double.Parse(petallength);                    Console.Write("Enter the petal width : ");                    string petalwidth = Console.ReadLine();                    inputvalues[3] = Double.Parse(petalwidth);                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++ ");                    int start = 0;                    int finish = 4;                    Sort(calculatekNN(data, inputvalues, start, finish, 150), k,data);                    break;                case 2://This case is for just sepal features.                    Console.Write("Enter the sepal length : ");                    sepallength = Console.ReadLine();                    inputvalues[0] = Double.Parse(sepallength);                    Console.Write("Enter the sepal width : ");                    sepalwidth = Console.ReadLine();                    inputvalues[1] = Double.Parse(sepalwidth);                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++ ");                    start = 0;                    finish = 2;                    Sort(calculatekNN(data, inputvalues, start, finish, 150), k,data);                    break;                case 3://This case is for just petal features.                    Console.Write("Enter the petal length : ");                    petallength = Console.ReadLine();                    inputvalues[2] = Double.Parse(petallength);                    Console.Write("Enter the petal width : ");                    petalwidth = Console.ReadLine();                    inputvalues[3] = Double.Parse(petalwidth);                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++ ");                    start = 2;                    finish = 4;                    Sort(calculatekNN(data, inputvalues, start, finish, 150), k,data);                    break;                case 4://This case is for listing all flowers in data.                    Console.WriteLine("The list of the data:");                    for (int i = 0; i < 150; i++)                    {                        Console.WriteLine("Sepal Length: " + data[i,0] + " Sepal Width : " + data[i,1] + " Petal Length: " + data[i, 2] + " Petal Width: " + data[i, 3] + " Type: " + data[i, 4]);                    }                    break;            }            Console.WriteLine("**********************************************************************************************************************************************");            Console.WriteLine("**********************************************************************************************************************************************");            Console.Write("Enter K value for success testing: ");            string kinputstring1 = Console.ReadLine();            //We created the testing array for success rate            string[,] testingarray = new string[30, 5];            int startingindex = 0;            for (int start = 40; start < 150; start++)            {                switch (start)                {                    case 50:                        start = 90;                        break;                    case 100:                        start = 140;                        break;                }                for (int count = 0; count < 5; count++)                {                    testingarray[startingindex, count] = data[start, count];                }                startingindex++;            }            //We created the data set that contains the %80 of our data for success rate            string[,] otherdata = new string[120, 5];            int startingindex1 = 0;            for (int start = 0; start < 140; start++)            {                switch (start)                {                    case 40:                        start = 50;                        break;                    case 90:                        start = 100;                        break;                }                for (int count = 0; count < 5; count++)                {                    otherdata[startingindex1, count] = data[start, count];                }                startingindex1++;            }            //Calculating of the success rate            double successpoint = 0;            Console.WriteLine("The features of the flower: ");            for (int i = 0; i < 30; i++)            {                double[] temp = new double[4];                for (int a = 0; a < 4; a++)                {                    temp[a] = Double.Parse(testingarray[i, a]);                                    }                String max = Sort(calculatekNN(otherdata, temp, 0, 4, 120), Int32.Parse(kinputstring1),otherdata);                Console.WriteLine("Real type of this flower : " + testingarray[i, 4]);                                Console.WriteLine("###################################################################################################################################");                if (testingarray[i, 4] == max)                {                    successpoint++;                }            }            Console.WriteLine("The success rate : " + successpoint / 30);        }        static void Main(string[] args)        {            foreach (string line in lines)            {                string[] transporter = line.Split(',');                for (int i = 0; i < transporter.Length; i++)                {                    data[numberofline, i] = transporter[i];                }                numberofline++;            }            string exit = " ";            while (exit != "0")            {                                Start();                Console.Write("For exit please press 0 : ");                exit = Console.ReadLine();                Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");            }                              }    }                                                }    