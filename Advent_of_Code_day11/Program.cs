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
                    Console.WriteLine("*        Day 11: Dumbo Octopus         *");
                    Console.WriteLine("****************************************");

                    string line;
                    int[,] octopus = new int[12,12];

                    //set edge data to 11
                    for (int x = 0; x < 12; x++) {
                        for (int y = 0; y < 12; y++) {
                            if (x == 0) {
                                octopus[x,y] = 11;
                            } else if (x == 11) {
                                octopus[x,y] = 11;
                            } else if (y == 0) {
                                octopus[x,y] = 11;
                            } else if (y == 11) {
                                octopus[x,y] = 11;
                            }
                        }
                    }

                    int row = 1;

                    while ((line = sr.ReadLine()) != null) {
                        for (int y = 0; y < 10; y++) {
                            octopus[row,y+1] = Convert.ToInt32(line.ElementAt(y).ToString());
                        }
                        row++;
                    }

                    //print the grid
                    Console.WriteLine("Initial Octupus Energy Matrix:");
                    for (int x = 1; x < 11; x++) {
                        for (int y = 1; y < 11; y++) {
                            Console.Write(octopus[x,y] + "\t");
                        }
                        Console.WriteLine("");
                    }

                    //process the data
                    processData(octopus);
                    
                }              

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static void processData(int[,] octopusGrid) {
            int flashCount = 0;            
            int flashStep = 0;

            string[,] octupusFlashGrid = new string[12,12];
            //covert the data to string with 'flash'
            for (int x = 0; x < 12; x++) {
                for (int y = 0; y < 12; y++) {
                    octupusFlashGrid[x,y] = octopusGrid[x,y] + ",x";
                }
            }

            //temp set to 1000 steps to determine when all the octopuses flash at the same time
            for (int step = 0; step < 1000; step++) {
                //Console.WriteLine("step: {0}", step+1);
                //increase the flash level of each octopus within the grid
                for (int x = 1; x < 11; x++) {
                    for (int y = 1; y < 11; y++) {
                        string[] data = octupusFlashGrid[x,y].Split(',');
                        int energy = Convert.ToInt32(data[0]);
                        energy++;
                        octupusFlashGrid[x,y] = energy + ",x";

                        //if energy is more than 10, it should flash
                        if (energy == 10) {
                            octupusFlashGrid[x,y] = "0,x";
                            flashStep++;
                            flashCount++;
                        }                    
                    }
                }

                // //print the grid
                // for (int x = 0; x < 12; x++) {
                //     for (int y = 0; y < 12; y++) {
                //         Console.Write(octupusFlashGrid[x,y] + "\t");
                //     }
                //     Console.WriteLine("");
                // }

                //evaluate the flash by 'affecting' the octopus beside it 
                while (flashStep > 0) {
                    //re-initialize flashStep
                    flashStep = 0;

                    for (int x = 1; x < 11; x++) {
                        for (int y = 1; y < 11; y++) {
                            //evaluate the grid around the flash
                            if (octupusFlashGrid[x,y].Equals("0,x")) {
                                //convert 0x to 0f
                                octupusFlashGrid[x,y] = "0,f";
                                //lower the flash step since this one had been evaluated
                                string[] data1 = octupusFlashGrid[x-1,y-1].Split(',');
                                int topLeft = Convert.ToInt32(data1[0]);

                                string[] data2 = octupusFlashGrid[x-1,y].Split(',');
                                int top = Convert.ToInt32(data2[0]);

                                string[] data3 = octupusFlashGrid[x-1,y+1].Split(',');
                                int topRight = Convert.ToInt32(data3[0]);

                                string[] data4 = octupusFlashGrid[x,y-1].Split(',');
                                int left = Convert.ToInt32(data4[0]);

                                string[] data5 = octupusFlashGrid[x,y+1].Split(',');
                                int right = Convert.ToInt32(data5[0]);

                                string[] data6 = octupusFlashGrid[x+1,y-1].Split(',');
                                int bottomLeft = Convert.ToInt32(data6[0]);

                                string[] data7 = octupusFlashGrid[x+1,y].Split(',');
                                int bottom = Convert.ToInt32(data7[0]);

                                string[] data8 = octupusFlashGrid[x+1,y+1].Split(',');
                                int bottomRight = Convert.ToInt32(data8[0]);

                                // Console.Write("\ttopLeft:{0}", topLeft);
                                // Console.Write("\ttop:{0}", top);
                                // Console.Write("\ttopRight:{0}", topRight);
                                // Console.Write("\tleft:{0}", left);
                                // Console.Write("\tright:{0}", right);
                                // Console.Write("\tbottomLeft:{0}", bottomLeft);
                                // Console.Write("\tbottom:{0}", bottom);
                                // Console.Write("\tbottomRight:{0}", bottomRight);
                                // Console.WriteLine("");

                                //do not process the border data or if that octupus had already flashed
                                //process top left
                                if (topLeft != 11 && !octupusFlashGrid[x-1,y-1].Equals("0,f") && !octupusFlashGrid[x-1,y-1].Equals("0,x")) {
                                    int energy = topLeft;
                                    energy++;

                                    //if energy is more than 10, it should flash
                                    if (energy == 10) {
                                        octupusFlashGrid[x-1,y-1] = "0,x";
                                        flashStep++;
                                        flashCount++;
                                    } else {
                                        octupusFlashGrid[x-1,y-1] = energy + ",x";
                                    }

                                } 
                                //process top
                                if (top != 11 && !octupusFlashGrid[x-1,y].Equals("0,f") && !octupusFlashGrid[x-1,y].Equals("0,x")) {
                                    int energy = top;
                                    energy++;

                                    //if energy is more than 10, it should flash
                                    if (energy == 10) {
                                        octupusFlashGrid[x-1,y] = "0,x";
                                        flashStep++;
                                        flashCount++;
                                    } else {
                                        octupusFlashGrid[x-1,y] = energy + ",x";
                                    }

                                } 
                                //process top right
                                if (topRight != 11 && !octupusFlashGrid[x-1,y+1].Equals("0,f") && !octupusFlashGrid[x-1,y+1].Equals("0,x")) {
                                    int energy = topRight;
                                    energy++;

                                    //if energy is more than 10, it should flash
                                    if (energy == 10) {
                                        octupusFlashGrid[x-1,y+1] = "0,x";
                                        flashStep++;
                                        flashCount++;
                                    } else {
                                        octupusFlashGrid[x-1,y+1] = energy + ",x";
                                    }

                                } 
                                //process left
                                if (left != 11 && !octupusFlashGrid[x,y-1].Equals("0,f")  && !octupusFlashGrid[x,y-1].Equals("0,x")) {
                                    int energy = left;
                                    energy++;

                                    //if energy is more than 10, it should flash
                                    if (energy == 10) {
                                        octupusFlashGrid[x,y-1] = "0,x";
                                        flashStep++;
                                        flashCount++;
                                    } else {
                                        octupusFlashGrid[x,y-1] = energy + ",x";
                                    }

                                } 
                                //process right
                                if (right != 11 && !octupusFlashGrid[x,y+1].Equals("0,f")  && !octupusFlashGrid[x,y+1].Equals("0,x")) {
                                    int energy = right;
                                    energy++;

                                    //if energy is more than 10, it should flash
                                    if (energy == 10) {
                                        octupusFlashGrid[x,y+1] = "0,x";
                                        flashStep++;
                                        flashCount++;
                                    } else {
                                        octupusFlashGrid[x,y+1] = energy + ",x";
                                    }

                                } 
                                //process bottom left
                                if (bottomLeft != 11 && !octupusFlashGrid[x+1,y-1].Equals("0,f")  && !octupusFlashGrid[x+1,y-1].Equals("0,x")) {
                                    int energy = bottomLeft;
                                    energy++;

                                    //if energy is more than 10, it should flash
                                    if (energy == 10) {
                                        octupusFlashGrid[x+1,y-1] = "0,x";
                                        flashStep++;
                                        flashCount++;
                                    } else {
                                        octupusFlashGrid[x+1,y-1] = energy + ",x";
                                    }

                                } 
                                //process bottom
                                if (bottom != 11 && !octupusFlashGrid[x+1,y].Equals("0,f") && !octupusFlashGrid[x+1,y].Equals("0,x")) {
                                    int energy = bottom;
                                    energy++;

                                    //if energy is more than 10, it should flash
                                    if (energy == 10) {
                                        octupusFlashGrid[x+1,y] = "0,x";
                                        flashStep++;
                                        flashCount++;
                                    } else {
                                        octupusFlashGrid[x+1,y] = energy + ",x";
                                    }

                                } 
                                //process bottom right
                                if (bottomRight != 11 && !octupusFlashGrid[x+1,y+1].Equals("0,f") && !octupusFlashGrid[x+1,y+1].Equals("0,x")) {
                                    int energy = bottomRight;
                                    energy++;

                                    //if energy is more than 10, it should flash
                                    if (energy == 10) {
                                        octupusFlashGrid[x+1,y+1] = "0,x";
                                        flashStep++;
                                        flashCount++;
                                    } else {
                                        octupusFlashGrid[x+1,y+1] = energy + ",x";
                                    }

                                } 

                            }
                            

                        }
                    }                   
                }

                //print the grid
                if (step == 100) {
                    Console.WriteLine("Final Octupus Energy Matrix at step 100:");
                    for (int x = 1; x < 11; x++) {
                        for (int y = 1; y < 11; y++) {
                            string[] data = octupusFlashGrid[x,y].Split(',');
                            int energy = Convert.ToInt32(data[0]);
                            Console.Write(energy + "\t");
                        }
                        Console.WriteLine("");
                    }

                    Console.WriteLine("Flash Count at step 100: {0}", flashCount);
                }

                //count all the 0,f data
                int allFlashCount = 0;
                for (int x = 1; x < 11; x++) {
                    for (int y = 1; y < 11; y++) {
                        if (octupusFlashGrid[x,y].Equals("0,f")) {
                            allFlashCount++;
                        }
                    }
                }
                //if the count equals 100, all are flashing
                if (allFlashCount == 100) {
                    Console.WriteLine("All the octopuses are flashing at step {0}", step+1);
                    for (int x = 1; x < 11; x++) {
                        for (int y = 1; y < 11; y++) {
                            string[] data = octupusFlashGrid[x,y].Split(',');
                            int energy = Convert.ToInt32(data[0]);
                            Console.Write(energy + "\t");
                        }
                        Console.WriteLine("");
                    }
                    break;
                }
            }
        }

    }
}