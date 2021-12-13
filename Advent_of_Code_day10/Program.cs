using System;
using System.IO;

namespace FileApplication {
    class Program {
        static void Main(string[] args) {
            //get input data
            try {                
                using (StreamReader sr = new StreamReader("/Users/T452172/Downloads/Advent_of_code_day10/data.txt")) {
                    Console.WriteLine("****************************************");
                    Console.WriteLine("*         Advent of Code 2021          *");
                    Console.WriteLine("*        Day 10: Syntax Scoring        *");
                    Console.WriteLine("****************************************");

                    string line;
                    //List<string[]> dataList = new List<string[]>();
                    int totalCorruptContest = 0;
                    //int lineNumber = 1;
                    List<double> totalClosingContestList = new List<double>();
                                        
                    while ((line = sr.ReadLine()) != null) {
                        //determine if line is corrupted and process the data
                        int output = inputProcessorForCorruptData(line);
                        totalCorruptContest += output;                         
                        
                        //process the uncorrupted data
                        if (output == 0) {
                            totalClosingContestList.Add(inputProcessorForClosingData(line));
                        }

                        //Console.WriteLine("line: {0}", lineNumber);
                        //lineNumber++;

                    }

                    //determine the location of the middle score
                    int middle = (((totalClosingContestList.Count-1) / 2) + 1) - 1;
                    Console.WriteLine("middle: {0}", middle);
                    //sort the scores
                    totalClosingContestList.Sort();

                    //get the middle data
                    double middleScore = totalClosingContestList[middle];

                    Console.WriteLine("totalCorruptContest: {0}", totalCorruptContest);
                    Console.WriteLine("middleScore: {0}", middleScore);
                }              

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static int inputProcessorForCorruptData(string lineData) {
            int output = 0;
            string[] data = new string[lineData.Length];
            char errorBit = ' ';

            for (int x = 0; x< lineData.Length; x++) {
                data[x] = lineData.ElementAt(x) + "0";
            }

            //iterate thru the list
            for (int x = 0; x < data.Length; x++) {
                //check for the closing 
                if (data[x].ElementAt(0).Equals('}')) {
                    //find the opening by iterating back
                    int cnt0 = 0;
                    for (int y = x-1; y >= 0; y--) {    
                        if (data[y].Equals("{0")) {
                            //set the data to 1
                            data[y] = data[y].ElementAt(0) + "1";
                            data[x] = data[x].ElementAt(0) + "1"; 
                            break;
                        } else if (data[y].ElementAt(1).Equals('0')) {
                            cnt0++;
                        }
                    }

                    if (cnt0 > 0) {
                        errorBit = data[x].ElementAt(0);
                        break;
                    }
                } else if (data[x].ElementAt(0).Equals(']')) {
                    //find the opening by iterating back
                    int cnt0 = 0;
                    for (int y = x-1; y >= 0; y--) {    

                        if (data[y].Equals("[0")) {
                            //set the data to 1
                            data[y] = data[y].ElementAt(0) + "1";
                            data[x] = data[x].ElementAt(0) + "1"; 
                            break;
                        } else if (data[y].ElementAt(1).Equals('0')) {
                            cnt0++;
                        }
                    }

                    if (cnt0 > 0) {
                        errorBit = data[x].ElementAt(0);
                        break;
                    }
                } else if (data[x].ElementAt(0).Equals(')')) {
                    //find the opening by iterating back
                    int cnt0 = 0;
                    for (int y = x-1; y >= 0; y--) {        

                        if (data[y].Equals("(0")) {
                            //set the data to 1
                            data[y] = data[y].ElementAt(0) + "1";  
                            data[x] = data[x].ElementAt(0) + "1";   
                            break;
                        } else if (data[y].ElementAt(1).Equals('0')) {
                            cnt0++;
                        }
                    }

                    if (cnt0 > 0) {
                        errorBit = data[x].ElementAt(0);
                        break;
                    }
                } else if (data[x].ElementAt(0).Equals('>')) {
                    //find the opening by iterating back
                    int cnt0 = 0;
                    for (int y = x-1; y >= 0; y--) {    

                        if (data[y].Equals("<0")) {
                            //set the data to 1
                            data[y] = data[y].ElementAt(0) + "1";
                            data[x] = data[x].ElementAt(0) + "1"; 
                            break;
                        } else if (data[y].ElementAt(1).Equals('0')) {
                            cnt0++;
                        }
                    }

                    if (cnt0 > 0) {
                        errorBit = data[x].ElementAt(0);
                        break;
                    }
                }

            }

            if (errorBit.Equals(')')) {
                output = 3;
            } else if (errorBit.Equals(']')) {
                output = 57;
            } else if (errorBit.Equals('}')) {
                output = 1197;
            }  else if (errorBit.Equals('>')) {
                output = 25137;
            } 

            return output;

        }

        public static double inputProcessorForClosingData(string lineData){
            double output = 0;
            string[] data = new string[lineData.Length];

            for (int x = 0; x< lineData.Length; x++) {
                data[x] = lineData.ElementAt(x) + "0";
            }

            //iterate thru the list
            for (int x = 0; x < data.Length; x++) {
                //check for the closing 
                if (data[x].ElementAt(0).Equals('}')) {
                    //find the opening by iterating back
                    for (int y = x-1; y >= 0; y--) {    
                        if (data[y].Equals("{0")) {
                            //set the data to 1
                            data[y] = data[y].ElementAt(0) + "1";
                            data[x] = data[x].ElementAt(0) + "1"; 
                            break;
                        } 
                    }

                } else if (data[x].ElementAt(0).Equals(']')) {
                    //find the opening by iterating back
                    for (int y = x-1; y >= 0; y--) {    

                        if (data[y].Equals("[0")) {
                            //set the data to 1
                            data[y] = data[y].ElementAt(0) + "1";
                            data[x] = data[x].ElementAt(0) + "1"; 
                            break;
                        }
                    }

                } else if (data[x].ElementAt(0).Equals(')')) {
                    //find the opening by iterating back
                    for (int y = x-1; y >= 0; y--) {        

                        if (data[y].Equals("(0")) {
                            //set the data to 1
                            data[y] = data[y].ElementAt(0) + "1";  
                            data[x] = data[x].ElementAt(0) + "1";   
                            break;
                        }
                    }

                } else if (data[x].ElementAt(0).Equals('>')) {
                    //find the opening by iterating back
                    for (int y = x-1; y >= 0; y--) {    

                        if (data[y].Equals("<0")) {
                            //set the data to 1
                            data[y] = data[y].ElementAt(0) + "1";
                            data[x] = data[x].ElementAt(0) + "1"; 
                            break;
                        }
                    }
                }

            }

            // Console.Write("data: ");
            // for (int x = 0; x < data.Length; x++) {
            //     Console.Write(data[x]+ " ");
            // }
            // Console.WriteLine("");

            //get the details of the bits with a zero on it
            for (int x = data.Length-1; x >= 0; x--) {
                double tempOutput = output * 5;

                if (data[x].Equals("(0")) {
                    output = tempOutput + 1;
                } else if (data[x].Equals("[0")) {
                    output = tempOutput + 2;
                } else if (data[x].Equals("{0")) {
                    output = tempOutput + 3;
                } else if (data[x].Equals("<0")) {
                    output = tempOutput + 4;
                }
            }

            Console.WriteLine("output: {0}", output);
            return output;
        }

    }
}