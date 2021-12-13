namespace FileApplication {
    class Program {
        static void Main(string[] args) {
            //grid map size is 100x100
            int[,] map = new int[102,102];

            //get input data
            try {                
                using (StreamReader sr = new StreamReader("/Users/T452172/Downloads/Advent_of_code_day9/data.txt")) {
                    Console.WriteLine("****************************************");
                    Console.WriteLine("*         Advent of Code 2021          *");
                    Console.WriteLine("*         Day 9: Smoke Basin           *");
                    Console.WriteLine("****************************************");

                    string line;
                    int lineCnt = 1;

                    //set edge data to 9
                    for (int x = 0; x < 102; x++) {
                        for (int y = 0; y < 102; y++) {
                            if (x == 0) {
                                map[x,y] = 9;
                            } else if (x == 101) {
                                map[x,y] = 9;
                            } else if (y == 0) {
                                map[x,y] = 9;
                            } else if (y == 101) {
                                map[x,y] = 9;
                            }
                        }
                    }
                                                            
                    while ((line = sr.ReadLine()) != null) {
                        //get data and set it on the map
                        for (int x = 0; x < line.Length; x++) {                            
                            map[lineCnt,x+1] = int.Parse(line.ElementAt(x).ToString());
                        }
                        //increment the line count
                        lineCnt++;
                    }

                    //--Testing the matrix view--
                    // for (int u = 0; u < 102; u++) {
                    //     for (int o = 0; o < 102; o++) {
                    //         Console.Write(map[u, o] + " ");
                    //     }
                    // Console.WriteLine();
                    // }

                    //get the low points based on the map 
                    int totalRiskLevel = getLowPointTotalRiskLevel(map);
                    Console.WriteLine("totalRiskLevel: {0}", totalRiskLevel);

                    //get a list of all the low points
                    List<string> lowPointList = getLowPointList(map);
                    //List<string> lowPointList = new List<string>();
                    //lowPointList.Add("1,19");

                    //iterate thru the low pointList and get a list of the basin
                    int basinProduct = productOfTopThreeBasins(lowPointList, map);
                    Console.WriteLine("basinProduct: {0}", basinProduct);

                }              

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static int productOfTopThreeBasins(List<string> lowPointList, int[,] map) {
            int productOfTopThree = 1;
            List<int> basinCountArray = new List<int>();

            //iterate thru the low point list
            for (int x = 0; x < lowPointList.Count; x++) {
                //Console.Write("lowPoint: {0}", lowPointList[x]);
                int count = 0;
                int index = 0;

                //create a list of map coordinates  
                List<string> checkList = new List<string>();
                checkList.Add(lowPointList[x]);

                while (checkList.Count > 0) {
                    string[] tempData = checkList[0].Split(",");
                    int startX = Convert.ToInt32(tempData[0]);
                    int startY = Convert.ToInt32(tempData[1]);  
                    checkList.Remove(startX + "," + startY);

                    int top = map[startX-1,startY];
                    int left = map[startX,startY-1];
                    int right = map[startX,startY+1];
                    int bottom = map[startX+1,startY];

                    if (top != 9) {
                        if (top != 11) {
                            checkList.Add((startX-1) + "," + (startY));
                            map[startX-1,startY] = 11;
                            count++;
                        }
                    }
                    if (left != 9) {
                        if (left != 11) {
                            checkList.Add((startX) + "," + (startY-1));
                            map[startX,startY-1] = 11;
                            count++;
                        }
                    }
                    if (right != 9) {
                        if (right != 11) {
                            checkList.Add((startX) + "," + (startY+1));
                            map[startX,startY+1] = 11;
                            count++;
                        }
                    }
                    if (bottom != 9) {
                        if (bottom != 11) {
                            checkList.Add((startX+1) + "," + (startY));
                            map[startX+1,startY] = 11;
                            count++;
                        }
                    }
                    
                    index++;
                }

                //Console.WriteLine("\tcount: {0}", count);
                basinCountArray.Add(count);

            }

            basinCountArray.Sort();
            basinCountArray.Reverse();

            // for (int x = 0; x < basinCountArray.Count; x++) {
            //     Console.WriteLine("basinCountArray: {0}", basinCountArray[x]);
            // }

            //get top 3
            for (int x = 0; x < 3; x++) {
                productOfTopThree *= basinCountArray[x];
            }

            return productOfTopThree;
        }

        public static int getLowPointTotalRiskLevel(int[,] map) {
            int totalRiskLevel = 0;

            //get data for inner rows and columns
            for (int row = 1; row < 101; row++) {
                for (int col = 1; col < 101; col++) {
                    int risk = 1;
                    int data = map[row,col];
                    int above = 0;
                    int below = 0;
                    int left = 0;
                    int right = 0;

                    //get data for first row
                    if (row == 1) {
                        //get data for upper left corner
                        if (col == 1) {
                            right = map[row,(col+1)];
                            below = map[(row+1),col]; 

                            if (below > data && right > data) {
                                //calculate risk level
                                totalRiskLevel += (risk + data);
                            }
                        //get data for upper right corner    
                        } else if (col == 100) {
                            below = map[(row+1),col];
                            left = map[row,(col-1)];

                            if (below > data && left > data) {
                                //calculate risk level
                                totalRiskLevel += (risk + data);
                            }
                        //get data for all other first row
                        } else {
                            below = map[(row+1),col];
                            left = map[row,(col-1)];
                            right = map[row,(col+1)];

                            if (below > data && left > data && right > data) {
                                //calculate risk level
                                totalRiskLevel += (risk + data);
                            }
                        }
                    //get data for bottom row
                    } else if (row == 100) {
                        //get data for lower left corner
                        if (col == 1) {
                            right = map[row,(col+1)];
                            above = map[(row-1),col];

                            if (above > data && right > data) {
                                //calculate risk level
                                totalRiskLevel += (risk + data);
                            }
                        //get data for lower right corner    
                        } else if (col == 100) {
                            above = map[(row-1),col];
                            left = map[row,(col-1)];

                            if (above > data && left > data) {
                                //calculate risk level
                                totalRiskLevel += (risk + data);
                            }
                        //get data for all other first row
                        } else {
                            above = map[(row-1),col];
                            left = map[row,(col-1)];
                            right = map[row,(col+1)];

                            if (above > data && left > data && right > data) {
                                //calculate risk level
                                totalRiskLevel += (risk + data);
                            }
                        }
                    //get data for all other first column (removing the corners as they have already been calculated)
                    } else if (col == 1) {
                        if (row != 1 || row != 100) {
                            above = map[(row-1),col];
                            below = map[(row+1),col];
                            right = map[row,(col+1)];

                            if (below > data && above > data && right > data) {
                                //calculate risk level
                                totalRiskLevel += (risk + data);
                            }
                        }
                    //get data for all other last column (removing the corners as they have already been calculated)
                    } else if (col == 100) {
                        if (row != 1 || row != 100) {
                            above = map[(row-1),col];
                            below = map[(row+1),col];
                            left = map[row,(col-1)];

                            if (below > data && left > data && above > data) {
                                //calculate risk level
                                totalRiskLevel += (risk + data);
                            }
                        }
                    //everything else in the middle    
                    } else {                    
                        data = map[row,col];
                        above = map[(row-1),col];
                        below = map[(row+1),col];
                        left = map[row,(col-1)];
                        right = map[row,(col+1)];

                        if (above > data && below > data && left > data && right > data) {
                            //calculate risk level
                            totalRiskLevel += (risk + data);
                        }
                    }
                }
            }

            return totalRiskLevel;
        }

        public static List<string> getLowPointList(int[,] map) {
            List<string> lowPointList = new List<string>();

            //get data for inner rows and columns
            for (int row = 1; row < 101; row++) {
                for (int col = 1; col < 101; col++) {
                    int data = map[row,col];
                    int above = 0;
                    int below = 0;
                    int left = 0;
                    int right = 0;

                    //get data for first row
                    if (row == 1) {
                        //get data for upper left corner
                        if (col == 1) {
                            right = map[row,(col+1)];
                            below = map[(row+1),col]; 

                            if (below > data && right > data) {
                                //add low point info to the list
                                lowPointList.Add(row + "," + col);
                                //Console.WriteLine("low point row: {0} \t col: {1}", row, col);
                            }
                        //get data for upper right corner    
                        } else if (col == 100) {
                            below = map[(row+1),col];
                            left = map[row,(col-1)];

                            if (below > data && left > data) {
                                //add low point info to the list
                                lowPointList.Add(row + "," + col);
                                //Console.WriteLine("low point row: {0} \t col: {1}", row, col);
                            }
                        //get data for all other first row
                        } else {
                            below = map[(row+1),col];
                            left = map[row,(col-1)];
                            right = map[row,(col+1)];

                            if (below > data && left > data && right > data) {
                                //add low point info to the list
                                lowPointList.Add(row + "," + col);
                                //Console.WriteLine("low point row: {0} \t col: {1}", row, col);
                            }
                        }
                    //get data for bottom row
                    } else if (row == 100) {
                        //get data for lower left corner
                        if (col == 1) {
                            right = map[row,(col+1)];
                            above = map[(row-1),col];

                            if (above > data && right > data) {
                                //add low point info to the list
                                lowPointList.Add(row + "," + col);
                                //Console.WriteLine("low point row: {0} \t col: {1}", row, col);
                            }
                        //get data for lower right corner    
                        } else if (col == 100) {
                            above = map[(row-1),col];
                            left = map[row,(col-1)];

                            if (above > data && left > data) {
                                //add low point info to the list
                                lowPointList.Add(row + "," + col);
                                //Console.WriteLine("low point row: {0} \t col: {1}", row, col);
                            }
                        //get data for all other first row
                        } else {
                            above = map[(row-1),col];
                            left = map[row,(col-1)];
                            right = map[row,(col+1)];

                            if (above > data && left > data && right > data) {
                                //add low point info to the list
                                lowPointList.Add(row + "," + col);
                                //Console.WriteLine("low point row: {0} \t col: {1}", row, col);
                            }
                        }
                    //get data for all other first column (removing the corners as they have already been calculated)
                    } else if (col == 1) {
                        if (row != 1 || row != 100) {
                            above = map[(row-1),col];
                            below = map[(row+1),col];
                            right = map[row,(col+1)];

                            if (below > data && above > data && right > data) {
                                //add low point info to the list
                                lowPointList.Add(row + "," + col);
                                //Console.WriteLine("low point row: {0} \t col: {1}", row, col);
                            }
                        }
                    //get data for all other last column (removing the corners as they have already been calculated)
                    } else if (col == 100) {
                        if (row != 1 || row != 100) {
                            above = map[(row-1),col];
                            below = map[(row+1),col];
                            left = map[row,(col-1)];

                            if (below > data && left > data && above > data) {
                                //add low point info to the list
                                lowPointList.Add(row + "," + col);
                                //Console.WriteLine("low point row: {0} \t col: {1}", row, col);
                            }
                        }
                    //everything else in the middle    
                    } else {                    
                        data = map[row,col];
                        above = map[(row-1),col];
                        below = map[(row+1),col];
                        left = map[row,(col-1)];
                        right = map[row,(col+1)];

                        if (above > data && below > data && left > data && right > data) {
                            //add low point info to the list
                                lowPointList.Add(row + "," + col);
                                //Console.WriteLine("low point row: {0} \t col: {1}", row, col);
                        }
                    }

                    
                }
            }

            return lowPointList;
        }

    }
}