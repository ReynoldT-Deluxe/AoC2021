using System;
using System.IO;

namespace FileApplication {
    class Program {
        static void Main(string[] args) {
            //get input data
            try {                
                using (StreamReader sr = new StreamReader("/Users/T452172/Downloads/Advent_of_code_day13/data.txt")) {
                    Console.WriteLine("****************************************");
                    Console.WriteLine("*         Advent of Code 2021          *");
                    Console.WriteLine("*     Day 13: Transparent Origami      *");
                    Console.WriteLine("****************************************");

                    List<string> coordinatesList = new List<string>();
                    List<string> foldList = new List<string>();
                    int maxCol = 0;
                    int maxRow = 0;

                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        if (line.Contains(",")) {
                            coordinatesList.Add(line);
                            string[] subs = line.Split(",") ;
                            int col = Convert.ToInt32(subs[0]);
                            int row = Convert.ToInt32(subs[1]);

                            if ( col > maxCol) {
                                maxCol = col;
                            }
                            if (row > maxRow) {
                                maxRow = row;
                            }
                        } else if (line.Contains("fold along ")) {
                            string[] subs = line.Split("fold along ") ;   
                            foldList.Add(subs[1]);
                            //Console.WriteLine("subs1: {0}", subs[1]);
                        }
                    }

                    string[,] paper = new string[maxCol+1, maxRow+1];

                    //set the empty paper
                    for (int x = 0; x < maxCol+1; x++) {
                        for (int y = 0; y < maxRow+1; y++) {
                            paper[x,y] = ".";
                        }
                    }

                    //set the dots
                    for (int x = 0; x < coordinatesList.Count; x++) {
                        string[] subs = coordinatesList[x].Split(",") ;
                        int col = Convert.ToInt32(subs[0]);
                        int row = Convert.ToInt32(subs[1]);

                        paper[col,row] = "#";
                    }

                    //print the paper
                    // for (int x = 0; x < maxRow+1; x++) {
                    //     for (int y = 0; y < maxCol+1; y++) {
                    //         Console.Write(paper[y,x] + " ");                            
                    //     }
                    //     Console.WriteLine("");
                    // }

                    int newMaxRow = maxRow;
                    int newMaxCol = maxCol;
                    int foldCnt = 1;
                    //get the list of folds
                    while(foldList.Count > 0) {
                        string foldData = foldList[0];
                        string[] fold = foldData.Split("=");
                        string foldLine = fold[0];
                        int foldPosition = Convert.ToInt32(fold[1]);
                        
                        List<string> dotPosition = new List<string>();

                        //x = col; y = row
                        if (foldLine.Equals("y")) {
                            //get all the dots below the line
                            int mirrorRow = 0;
                            for (int y = newMaxRow; y > foldPosition; y--) {
                                //Console.WriteLine("currentRow: {0}", y);                                
                                for (int x = 0; x < newMaxCol+1; x++) {
                                    if (paper[x,y].Equals("#")) {
                                        //place a dot in the mirror location
                                        paper[x, mirrorRow] = "#";
                                    }
                                }
                                mirrorRow++;
                            }
                            //set the new max row
                            newMaxRow = foldPosition-1;

                        } else if (foldLine.Equals("x")) {
                            //get all the dots on the right
                            for (int y = 0; y < newMaxRow; y++) {     
                                int mirrorCol = 0;                           
                                for (int x = newMaxCol; x > foldPosition; x--) {                                    
                                    if (paper[x,y].Equals("#")) {
                                        //dotPosition.Add(y + "," + x);
                                        //place a dot in the mirror location
                                         paper[mirrorCol, y] = "#";
                                    }
                                    mirrorCol++;
                                }
                            }
                            //set the new dotPositions 
                            for (int x = 0; x < dotPosition.Count; x++) {
                                //Console.WriteLine("doPositon: {0}", dotPosition[x]);
                                string[] pos = dotPosition[x].Split(",");
                                int newPosition = Convert.ToInt32(pos[1]) - foldPosition;
                                //update the position with a dot
                                paper[newPosition,Convert.ToInt32(pos[0])] = "#";
                            }
                            //set the new max col
                            newMaxCol = foldPosition-1;
                        }

                        int dotCnt = 0;
                        //print the updated paper
                        for (int x = 0; x < newMaxRow+1; x++) {
                            for (int y = 0; y < newMaxCol+1; y++) {
                                //Console.Write(paper[y,x] + " "); 
                                if (paper[y,x].Equals("#")) {
                                    dotCnt++;
                                }                           
                            }
                            //Console.WriteLine("");
                        }

                        Console.WriteLine("Dot count after {0} fold(s) is: {1}", foldCnt, dotCnt);

                        foldCnt++;
                        foldList.Remove(foldData);
                    }

                     //print the updated paper
                        for (int x = 0; x < newMaxRow+1; x++) {
                            for (int y = 0; y < newMaxCol+1; y++) {
                                Console.Write(paper[y,x] + " "); 
                            }
                            Console.WriteLine("");
                        }

                }     
                 

                    


            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

    }
}