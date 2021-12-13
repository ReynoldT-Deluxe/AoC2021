using System;
using System.IO;

namespace FileApplication {
    class Program {
        static void Main(string[] args) {
            //this is assuming that the grid starts from 0,0 and the maximum location is 999,999
            int[,] arr1 = new int[1000, 1000];
                    
            try {
                using (StreamReader sr = new StreamReader("/Users/T452172/Downloads/Advent_of_Code_day5/data.txt")) {                
                    Console.WriteLine("****************************************");
                    Console.WriteLine("*         Advent of Code 2021          *");
                    Console.WriteLine("*     Day 5: Hydrothermal Venture      *");
                    Console.WriteLine("****************************************");
                    Console.Write("Do you want to include diagonal lines in calculating the number of intersections (Y/N): ");
                    
                    string input = Console.ReadLine();
                    string line;

                    while ((line = sr.ReadLine()) != null) {
                        string[] subs = line.Split(" -> ");
                        // Console.WriteLine("subs1: {0}",subs[0]);
                        // Console.WriteLine("subs2: {0}",subs[1]);

                        //separate per endpoint
                        string[] sub1 = subs[0].Split(',');
                        string[] sub2 = subs[1].Split(',');

                        //separate x and y components
                        int x1 = Convert.ToInt32(sub1[0]);
                        int y1 = Convert.ToInt32(sub1[1]);
                        int x2 = Convert.ToInt32(sub2[0]);
                        int y2 = Convert.ToInt32(sub2[1]);
                        
                        //process vertical endpoints
                        if (x1 == x2) {
                            if (y2 > y1){
                                for (int a = y1; a <= y2; a++) {
                                    int z = arr1[x1, a];

                                    if (z == 0) {
                                        arr1[x1, a] = 1;
                                    } else {
                                        arr1[x1, a] = z+1;
                                    } 
                                }

                            } else if (y1 > y2) {
                                for (int a = y1; a >= y2; a--) {
                                    int z = arr1[x1, a];

                                    if (z == 0) {
                                        arr1[x1, a] = 1;
                                    } else {
                                        arr1[x1, a] = z+1;
                                    } 
                                }
                            }

                        //process horizontal endpoints
                        } else if (y1 == y2) {
                            if (x2 > x1) {
                                for (int b = x1; b <= x2; b++) {
                                    int q = arr1[b, y1]; 

                                    if (q == 0) {
                                        arr1[b, y1] = 1;
                                    } else {
                                        arr1[b, y1] = q+1;
                                    } 
                                }

                            } else if (x1 > x2) {
                                for (int b = x1; b >= x2; b--) {
                                    int q = arr1[b, y1];

                                    if (q == 0) {
                                        arr1[b, y1] = 1;
                                    } else {
                                        arr1[b, y1] = q+1;
                                    } 
                                }
                            }

                        //process diagonal endpoints    
                        } else if (input == "Y" || input == "y") {
                            int r = y1;
                            if (x2 > x1){
                                for (int d = x1; d <= x2; d++) {
                                    int p = arr1[d, r];

                                    if (p == 0) {
                                        arr1[d, r] = 1;
                                    } else {
                                        arr1[d, r] = p+1;
                                    } 
                                    //increase or decrease y
                                    if (y2 > y1) {
                                        r++;
                                    } else if (y2 < y1) {
                                        r--;
                                    }
                                }

                            } else if (x1 > x2) {
                                for (int d = x1; d >= x2; d--) {
                                    int p = arr1[d, r];

                                    if (p == 0) {
                                        arr1[d, r] = 1;
                                    } else {
                                        arr1[d, r] = p+1;
                                    } 
                                    //increase or decrease y
                                    if (y2 > y1){
                                        r++;
                                    } else if (y2 < y1) {
                                        r--;
                                    }
                                }
                            }
                        }
                    }
                }

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            
            int c = 0;
            //count the number of intersect points (values in the grid > 2)
            for (int i = 0; i < 1000; i++) {
                for (int j = 0; j < 1000; j++) {    
                    int m = arr1[i, j];

                    if (m > 1) {
                        c++;
                    }
                }
            }

            Console.WriteLine("\nThe intersect count is: {0} \n", c);
            
            //--Testing the matrix view--
            //for (int u = 0; u < 10; u++) {
            // for (int o = 0; o < 10; o++) {
            //    Console.Write(arr1[u, o] + "\t");
            // }
            // Console.WriteLine();
            //}
        }

    }
}
