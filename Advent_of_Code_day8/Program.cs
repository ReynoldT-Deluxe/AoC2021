using System;
using System.IO;

namespace FileApplication {
    class Program {
        static void Main(string[] args) {
            //get input data
            try {                
                using (StreamReader sr = new StreamReader("/Users/T452172/Downloads/Advent_of_code_day8/data.txt")) {
                    Console.WriteLine("****************************************");
                    Console.WriteLine("*         Advent of Code 2021          *");
                    Console.WriteLine("*     Day 8: Seven Segment Search      *");
                    Console.WriteLine("****************************************");

                    string line;
                    int oneFourSevenEight = 0;
                    int totalOutput = 0;
                                        
                    while ((line = sr.ReadLine()) != null) {
                        string[] subs = line.Split(" | ");

                        //get only the output values for now
                        string[] outputDataArray = subs[1].Split(" ");
                        //add the count of the 1,4,7,8 digitd from the output
                        oneFourSevenEight += getOneFourSevenEight(outputDataArray);       

                        // //get the unique signal data
                        string[] signalDataArray = subs[0].Split(" ");
                        //determine the data for each unique signal and calculate the final value of each entry
                        totalOutput += getOutputValue(signalDataArray, outputDataArray);

                        //Console.WriteLine("totalOutput: {0}", totalOutput);

                    }

                    Console.WriteLine("oneFourSevenEight: {0}", oneFourSevenEight);
                    Console.WriteLine("totalOutput: {0}", totalOutput);
                }              

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static int getOneFourSevenEight(string[] outputData) {
            int count = 0;

            for (int x = 0; x< outputData.Length; x++) {
                //Console.WriteLine("outputData[x]: {0}", outputData[x]);
                //one is always 2 segments
                if (outputData[x].Length == 2) {
                    count++;
                //four is always 4 segments
                } else if (outputData[x].Length == 4) {
                    count++;
                //seven is always 3 segments
                } else if (outputData[x].Length == 3) {
                    count++;
                //eight is always 7 segments
                } else if (outputData[x].Length == 7) {
                    count++;
                }                
                
            }            

            return count;
        }

        public static int getOutputValue(string[] signalDataArray, string[] outputDataArray) {
            string data1 = "";
            string data4 = "";
            string data7 = "";
            string data8 = "";
            string[] data235 = new string[3];
            string[] data960 = new string[3];
            int count235 = 0;
            int count960 = 0;
            
            //get known data points
            for (int x = 0; x < signalDataArray.Length; x++) {
                if (signalDataArray[x].Length == 2) {
                    data1 = signalDataArray[x];
                //four is always 4 segments
                } else if (signalDataArray[x].Length == 4) {
                    data4 = signalDataArray[x];
                //seven is always 3 segments
                } else if (signalDataArray[x].Length == 3) {
                    data7 = signalDataArray[x];
                //eight is always 7 segments
                } else if (signalDataArray[x].Length == 7) {
                    data8 = signalDataArray[x];
                // 9,6,0 is always 6 segments
                } else if (signalDataArray[x].Length == 6) {
                    data960[count960] = signalDataArray[x];
                    count960++;
                // 2,3,5 is always 5 segments
                } else if (signalDataArray[x].Length == 5) {
                    data235[count235] = signalDataArray[x];
                    count235++;
                }
            }

            //evaluate data for 2,3,5
            string data3 = "";
            string data5 = "";
            string data2 = "";
            //get data for 3 first
            for(int x = 0; x < data235.Length; x++){
                int count3 = 0;
                //get data3
                for(int y = 0; y < data235[x].Length; y++){
                    //for data3
                    if (data1.Contains(data235[x].ElementAt(y))) {
                        count3++;
                    }
                }

                //data3 has all points of reference to the data
                if (count3 == 2) {
                    data3 = data235[x];
                }

            }

            //get data for data5 and data2
            for(int x = 0; x < data235.Length; x++){
                int count25 = 0;
                //process only those that are not equal to data3
                if (!data3.Equals(data235[x])) {
                    for(int y = 0; y < data235[x].Length; y++){
                        //for data5 and data2
                        if (data4.Contains(data235[x].ElementAt(y))) {
                            count25++;
                        }
                    }
                }

                //data5 has 3 points of reference to the data
                if (count25 == 3) {
                    data5 = data235[x];
                //data2 has only 2 points of reference to the data
                } else if (count25 == 2) {
                    data2 = data235[x];
                }

            }

            // //get the other data
            // for(int x = 0; x < data235.Length; x++){
            //     //data5 was set prior
            //     if (data5.Length > 0) {
            //         if (!data3.Equals(data235[x]) || !data5.Equals(data235[x]) ) {
            //             data2 = data235[x];
            //         }
            //     //data 2 was set prior
            //     } else if (data2.Length > 0) {
            //         if (!data3.Equals(data235[x]) || !data2.Equals(data235[x]) ) {
            //             data5 = data235[x];
            //         }
            //     }
            // }

            //evaluate data for 0,6,9
            string data0 = "";
            string data6 = "";
            string data9 = "";
            int element = 0;
            //get data for 6 first
            for(int x = 0; x < data960.Length; x++){
                int count6 = 0;
                //get data6
                for(int y = 0; y < data960[x].Length; y++){
                    //for data6
                    if (data1.Contains(data960[x].ElementAt(y))) {
                        count6++;
                    }
                }

                //Console.WriteLine("count6: {0}", count6);
                //data6 only matches with 1 element of 1
                if (count6 == 1) {
                    element = x;
                }

            }

            //set data6
            data6 = data960[element];

            //get data for data9 and data0
            for(int x = 0; x < data960.Length; x++){
                int count90 = 0;
                //process only those that are not equal to data6
                if (!data6.Equals(data960[x])) {
                    for(int y = 0; y < data960[x].Length; y++){
                        //for data9 and data0
                        if (data4.Contains(data960[x].ElementAt(y))) {
                            count90++;
                        }
                    }
                }
                
                //Console.WriteLine("count90: {0}", count90);
                //data9 has 4 points of reference to the data
                if (count90 == 4) {
                    data9 = data960[x];
                //data0 has only 3 points of reference to the data
                } else if (count90 == 3) {
                    data0 = data960[x];
                }

            }

            // //get the other data
            // for(int x = 0; x < data960.Length; x++){
            //     //data9 was set prior
            //     if (data9.Length > 0) {
            //         if (!data6.Equals(data960[x]) || !data9.Equals(data960[x]) ) {
            //             data0 = data960[x];
            //         }
            //     //data 0 was set prior
            //     } else if (data0.Length > 0) {
            //         if (!data6.Equals(data960[x]) || !data0.Equals(data960[x]) ) {
            //             data9 = data960[x];
            //         }
            //     }
            // }

            // Console.WriteLine("data1: {0}", data1);
            // Console.WriteLine("data4: {0}", data4);
            // Console.WriteLine("data7: {0}", data7);
            // Console.WriteLine("data8: {0}", data8);     
            // // Console.WriteLine("data235[0]: {0}", data235[0]);
            // // Console.WriteLine("data235[1]: {0}", data235[1]);
            // // Console.WriteLine("data235[2]: {0}", data235[2]);
            // // Console.WriteLine("data960[0]: {0}", data960[0]);
            // // Console.WriteLine("data960[1]: {0}", data960[1]);
            // // Console.WriteLine("data960[2]: {0}", data960[2]);

            // //compared 1 to 7 to get the value for 'top'
            // char top = 'x';
            // int topCharLoc = 0;
            // for (int y = 0; y < data7.Length; y++) {
            //     if (!data1.Contains(data7.ElementAt(y))) {
            //         top = data7.ElementAt(y);
            //         topCharLoc = y;
            //     }
            // }

            // char tempRightTop = 'x';
            // char tempRightBottom = 'x';
            // //set temp one data
            // if (topCharLoc == 0) {
            //     tempRightTop = data7.ElementAt(1);
            //     tempRightBottom = data7.ElementAt(2);
            // } else if (topCharLoc == 1) {
            //     tempRightTop = data7.ElementAt(0);
            //     tempRightBottom = data7.ElementAt(2);
            // } else if (topCharLoc == 2) {
            //     tempRightTop = data7.ElementAt(0);
            //     tempRightBottom = data7.ElementAt(1);
            // }

            // // Console.WriteLine("tempRightTop: {0}", tempRightTop);
            // // Console.WriteLine("tempRightBottom: {0}", tempRightBottom);

            // //compare 1 and 4 to get temp center and temp top left
            // char tempCenter = 'x';
            // char tempLeftTop = 'x'; 

            // for (int x = 0; x < data4.Length; x++) {
            //     if (data4.ElementAt(x).Equals(tempRightTop) || data4.ElementAt(x).Equals(tempRightBottom)) {
            //         continue;
            //     } else if (tempCenter.Equals('x')) {
            //         tempCenter = data4.ElementAt(x);
            //     } else if (tempLeftTop.Equals('x')) {
            //         tempLeftTop = data4.ElementAt(x);
            //     }
            // }

            // // Console.WriteLine("tempCenter: {0}", tempCenter);
            // // Console.WriteLine("tempLeftTop: {0}", tempLeftTop);

            // //get temp left bottom and temp bottom
            // char tempLeftBottom = 'x';
            // char tempBottom = 'x'; 

            // // Console.WriteLine("data8: {0}", data8);
            
            // for (int x = 0; x < data8.Length; x++) {

            //     if (!data8.ElementAt(x).Equals(tempRightTop)) {
            //         if (!data8.ElementAt(x).Equals(tempRightBottom)) {
            //             if (!data8.ElementAt(x).Equals(tempCenter)) {
            //                 if (!data8.ElementAt(x).Equals(tempLeftTop)) {
            //                     if (!data8.ElementAt(x).Equals(top)) {
            //                          if (tempLeftBottom.Equals('x')) {
            //                             tempLeftBottom = data8.ElementAt(x);
            //                         } else if (tempBottom.Equals('x')) {
            //                             tempBottom = data8.ElementAt(x);
            //                         }
            //                     }
            //                 }
            //             }
            //         }
            //     }
            // }

            // // Console.WriteLine("top: {0}", top);
            // // Console.WriteLine("tempRightTop: {0}", tempRightTop);
            // // Console.WriteLine("tempRightBottom: {0}", tempRightBottom);
            // // Console.WriteLine("tempCenter: {0}", tempCenter);
            // // Console.WriteLine("tempLeftTop: {0}", tempLeftTop);
            // // Console.WriteLine("tempLeftBottom: {0}", tempLeftBottom);
            // // Console.WriteLine("tempBottom: {0}", tempBottom);

            // //deduce data for 2, 3, and 5
            // //get common data (center and bottom only since top is already known)
            // char commonData1 = 'x';
            // char commonData2 = 'x';

            // for (int x = 0; x < 5; x++) {
            //     char compareChar = data235[0].ElementAt(x);

            //     //top is already known
            //     if (!compareChar.Equals(top)) {
            //         if (data235[1].Contains(compareChar) && data235[2].Contains(compareChar)) {
            //             if (commonData1.Equals('x')) {
            //                 commonData1 = compareChar;
            //             } else if (commonData2.Equals('x')) {
            //                 commonData2 = compareChar;
            //             }
            //         }
            //     }
               
            // }

            // Console.WriteLine("commonData1: {0}", commonData1);
            // Console.WriteLine("commonData2: {0}", commonData2);
            // Console.WriteLine("tempCenter: {0}",  tempCenter);
            // Console.WriteLine("tempLeftTop: {0}",  tempLeftTop);
            // Console.WriteLine("tempBottom: {0}",  tempBottom);
            // Console.WriteLine("tempLeftBottom: {0}",  tempLeftBottom);

            // char center = 'x';
            // char leftTop = 'x';
            // char bottom = 'x';
            // //compare to temp data for 4 to determine if the data is the center
            // if (commonData1.Equals(tempCenter) || commonData1.Equals(tempLeftTop)) {
            //     if (commonData1.Equals(tempCenter)) {
            //         center = tempCenter;
            //         leftTop = tempLeftTop;
            //     } else if (commonData1.Equals(tempLeftTop)) {
            //         center = tempLeftTop;
            //         leftTop = tempCenter;
            //     }
            //     //since commonData1 is for 4, then commonData2  is the bottom
            //     bottom = commonData2;                
            
            // } else if (commonData2.Equals(tempCenter) || commonData2.Equals(tempLeftTop)) {
            //     if (commonData2.Equals(tempCenter)) {
            //         center = tempCenter;
            //         leftTop = tempLeftTop;
            //     } else if (commonData2.Equals(tempLeftTop)) {
            //         center = tempLeftTop;
            //         leftTop = tempCenter;
            //     }
            //     //since commonData1 is for 4, then commonData2 can be used for the last temp data
            //     bottom = commonData1;

            // }  

            // Console.WriteLine("center {0}", center);
            // Console.WriteLine("left top {0}", leftTop);
            // Console.WriteLine("bottom {0}", bottom);
            
            // //deduce data for 9, 6, and 0
            // //get right bottom from common data (top, right bottom, bottom, left top)
            // char rightBottom = 'x';

            // for (int x = 0; x < 5; x++) {
            //     char compareChar = data960[0].ElementAt(x);
            //     //top, bottom, and left top is already known at this point
            //     if (!compareChar.Equals(top) && !compareChar.Equals(bottom) && !compareChar.Equals(leftTop) ) {
            //         if (data960[1].Contains(compareChar) && data960[2].Contains(compareChar)) {
            //             if (rightBottom.Equals('x')) {
            //                 rightBottom = compareChar;
            //             } 
            //         }
            //     }
               
            // }  

            // char rightTop = 'x';
            // //since right bottom is already know, we can now pinpoint the right top
            // if (rightBottom.Equals(tempRightBottom)) {
            //     rightTop = tempRightTop;
            // } else if (rightBottom.Equals(tempRightTop)) {
            //     rightTop = tempRightBottom;
            // }

            // //left bottom is the one one left
            // char leftBottom = 'x';
            // for (int x = 0; x < data8.Length; x++) {
            //     if (!top.Equals(data8.ElementAt(x)) && !leftTop.Equals(data8.ElementAt(x)) && !rightTop.Equals(data8.ElementAt(x))
            //         && !center.Equals(data8.ElementAt(x)) && !leftBottom.Equals(data8.ElementAt(x))
            //         && !bottom.Equals(data8.ElementAt(x))) {
            //             leftBottom = data8.ElementAt(x);
            //     }
            // }

            // Console.WriteLine("left bottom {0}", leftBottom);
            // // Console.WriteLine("top: {0}", top);
            // // Console.WriteLine("right top {0}", rightTop);
            // // Console.WriteLine("right bottom {0}", rightBottom);


            // string data2 = "";
            // string data3 = "";
            // string data5 = "";
            // string data9 = "";
            // string data6 = "";
            // string data0 = "";
            // //get other numbers
            // for (int x = 0; x < signalDataArray.Length; x++) {
            //     if (signalDataArray[x].Length == 5) {
            //         if (signalDataArray[x].Contains(leftBottom)) {
            //             data2 = signalDataArray[x];
            //         } else if (signalDataArray[x].Contains(leftTop)) {
            //             data5 = signalDataArray[x];
            //         } else {
            //             data3 = signalDataArray[x];
            //         }
                    
            //     } else if (signalDataArray[x].Length == 6) {
            //         if (!signalDataArray[x].Contains(center)) {
            //             data0 = signalDataArray[x];
            //         } else if (!signalDataArray[x].Contains(leftBottom)) {
            //             data9 = signalDataArray[x];
            //         } else if (!signalDataArray[x].Contains(rightTop)) {
            //             data6 = signalDataArray[x];
            //         }
            //     }
            // }

            // Console.WriteLine("data0: {0}", data0);
            // Console.WriteLine("data1 {0}", data1);
            // Console.WriteLine("data2 {0}", data2);
            // Console.WriteLine("data3 {0}", data3);
            // Console.WriteLine("data4 {0}", data4);
            // Console.WriteLine("data5 {0}", data5);
            // Console.WriteLine("data6 {0}", data6);
            // Console.WriteLine("data7 {0}", data7);
            // Console.WriteLine("data8 {0}", data8);
            // Console.WriteLine("data9 {0}", data9);            

            //now that we have the data for each souce, we can get the equivalent data on the output
            int[] outputArray = new int[4];
            
            // Console.WriteLine("outputDataArray[0]: {0}", outputDataArray[0]);
            // Console.WriteLine("outputDataArray[1]: {0}", outputDataArray[1]);
            // Console.WriteLine("outputDataArray[2]: {0}", outputDataArray[2]);
            // Console.WriteLine("outputDataArray[3]: {0}", outputDataArray[3]);

            //first value
            for (int x = 0; x < outputDataArray.Length; x++) {
                //value of 1
                if (outputDataArray[x].Length == 2) {
                    outputArray[x] = 1;         
                } else if (outputDataArray[x].Length == 3) {
                    outputArray[x] = 7;
                } else if (outputDataArray[x].Length == 4) {
                    outputArray[x] = 4;
                } else if (outputDataArray[x].Length == 7) {
                    outputArray[x] = 8;
                } else if (outputDataArray[x].Length == 5) {
                    int cnt2 = 0;
                    int cnt3 = 0;
                    int cnt5 = 0;

                    for (int y = 0; y < 5; y++) {
                        if (data2.Contains(outputDataArray[x].ElementAt(y))) {
                            cnt2++;
                        }
                        if (data3.Contains(outputDataArray[x].ElementAt(y))) {
                            cnt3++;
                        }
                        if (data5.Contains(outputDataArray[x].ElementAt(y))) {
                            cnt5++;
                        }
                    }

                    // Console.Write("cnt2: {0}", cnt2);
                    // Console.Write("cnt3: {0}", cnt3);
                    // Console.Write("cnt5: {0}", cnt5);

                    if (cnt2 == 5) {
                        outputArray[x] = 2;
                    } else if (cnt3 == 5) {
                        outputArray[x] = 3;
                    } else if (cnt5 == 5) {
                        outputArray[x] = 5;
                    }

                } else if (outputDataArray[x].Length == 6) {
                    int cnt0 = 0;
                    int cnt6 = 0;
                    int cnt9 = 0;

                    for (int y = 0; y < 6; y++) {
                        if (data0.Contains(outputDataArray[x].ElementAt(y))) {
                            cnt0++;
                        }
                        if (data6.Contains(outputDataArray[x].ElementAt(y))) {
                            cnt6++;
                        }
                        if (data9.Contains(outputDataArray[x].ElementAt(y))) {
                            cnt9++;
                        }

                    }

                    // Console.Write("cnt0: {0}", cnt0);
                    // Console.Write("cnt6: {0}", cnt6);
                    // Console.Write("cnt9: {0}", cnt9);

                    if (cnt0 == 6) {
                        outputArray[x] = 0;
                    } else if (cnt6 == 6) {
                        outputArray[x] = 6;
                    } else if (cnt9 == 6) {
                        outputArray[x] = 9;
                    }
                }

            }

            // //first value            
            // if (data0.Equals(outputDataArray[0])) {
            //     output += 0;
            // } else if (data1.Equals(outputDataArray[0])) {
            //     output += 1000;
            // } else if (data2.Equals(outputDataArray[0])) {
            //     output += 2000;
            // } else if (data3.Equals(outputDataArray[0])) {
            //     output += 3000;
            // } else if (data4.Equals(outputDataArray[0])) {
            //     output += 4000;
            // } else if (data5.Equals(outputDataArray[0])) {
            //     output += 5000;
            // } else if (data6.Equals(outputDataArray[0])) {
            //     output += 6000;
            // } else if (data7.Equals(outputDataArray[0])) {
            //     output += 7000;
            // } else if (data8.Equals(outputDataArray[0])) {
            //     output += 8000;
            // } else if (data9.Equals(outputDataArray[0])) {
            //     output += 9000;
            // }

            // //second value            
            // if (data0.Equals(outputDataArray[1])) {
            //     output += 0;
            // } else if (data1.Equals(outputDataArray[1])) {
            //     output += 100;
            // } else if (data2.Equals(outputDataArray[1])) {
            //     output += 200;
            // } else if (data3.Equals(outputDataArray[1])) {
            //     output += 300;
            // } else if (data4.Equals(outputDataArray[1])) {
            //     output += 400;
            // } else if (data5.Equals(outputDataArray[1])) {
            //     output += 500;
            // } else if (data6.Equals(outputDataArray[1])) {
            //     output += 600;
            // } else if (data7.Equals(outputDataArray[1])) {
            //     output += 700;
            // } else if (data8.Equals(outputDataArray[1])) {
            //     output += 800;
            // } else if (data9.Equals(outputDataArray[1])) {
            //     output += 900;
            // }

            // //third value            
            // if (data0.Equals(outputDataArray[2])) {
            //     output += 0;
            // } else if (data1.Equals(outputDataArray[2])) {
            //     output += 10;
            // } else if (data2.Equals(outputDataArray[2])) {
            //     output += 20;
            // } else if (data3.Equals(outputDataArray[2])) {
            //     output += 30;
            // } else if (data4.Equals(outputDataArray[2])) {
            //     output += 40;
            // } else if (data5.Equals(outputDataArray[2])) {
            //     output += 50;
            // } else if (data6.Equals(outputDataArray[2])) {
            //     output += 60;
            // } else if (data7.Equals(outputDataArray[2])) {
            //     output += 70;
            // } else if (data8.Equals(outputDataArray[2])) {
            //     output += 80;
            // } else if (data9.Equals(outputDataArray[2])) {
            //     output += 90;
            // }

            // //fourth value            
            // if (data0.Equals(outputDataArray[3])) {
            //     output += 0;
            // } else if (data1.Equals(outputDataArray[3])) {
            //     output += 1;
            // } else if (data2.Equals(outputDataArray[3])) {
            //     output += 2;
            // } else if (data3.Equals(outputDataArray[3])) {
            //     output += 3;
            // } else if (data4.Equals(outputDataArray[3])) {
            //     output += 4;
            // } else if (data5.Equals(outputDataArray[3])) {
            //     output += 5;
            // } else if (data6.Equals(outputDataArray[3])) {
            //     output += 6;
            // } else if (data7.Equals(outputDataArray[3])) {
            //     output += 7;
            // } else if (data8.Equals(outputDataArray[3])) {
            //     output += 8;
            // } else if (data9.Equals(outputDataArray[3])) {
            //     output += 9;
            // }
            
            int output = outputArray[0] * 1000
                        + outputArray[1] * 100
                        + outputArray[2] * 10
                        + outputArray[3] * 1;

            //Console.WriteLine("output: {0}", output);

            return output;
        }

    }
}  
