using System;
using System.IO;

namespace FileApplication {
    class Program {
        static void Main(string[] args) {
            //get input data
            try {                
                using (StreamReader sr = new StreamReader("/Users/T452172/Downloads/Advent_of_code_day11/data.txt")) {
                    Console.WriteLine("****************************************");
                    Console.WriteLine("*         Advent of Code 2021          *");
                    Console.WriteLine("*        Day 12:          *");
                    Console.WriteLine("****************************************");

                    string line;
                    List<string> caveConnectionList = new List<string>();

                    while ((line = sr.ReadLine()) != null) {
                        string[] subs = line.Split('-');
                        caveConnectionList.Add(line);
                        caveConnectionList.Add(subs[1]+"-"+subs[0]);
                    }

                    //process the data
                    processData();
                    
                }              

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static void processData() {

        }

    }
}