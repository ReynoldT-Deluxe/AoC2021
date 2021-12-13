using System;
using System.IO;

namespace FileApplication {
   class Program {

        static void Main(string[] args) {            
            try {
                using (StreamReader sr = new StreamReader("/Users/T452172/Downloads/Advent_of_Code_day6b/data.txt")) {
                    Console.WriteLine("****************************************");
                    Console.WriteLine("*         Advent of Code 2021          *");
                    Console.WriteLine("*         Day 6: Lanternfish           *");
                    Console.WriteLine("****************************************");
                    Console.Write("How many days should the population be calculated for? : ");
                    int days = Convert.ToInt32(Console.ReadLine());

                    string data;
                    //set initial list on an array
                    while ((data = sr.ReadLine()) != null) {
                        string[] subs  = data.Split(",");                        
                        decimal[] fishLifeArray = new decimal[9];

                        //total count of fish for each lifecycle
                        for (int fishCnt = 0; fishCnt < subs.Length; fishCnt++){
                            decimal fishLife = Convert.ToInt32(subs[fishCnt]);

                            if (fishLife == 0) {
                                fishLifeArray[0]++;
                            } else if (fishLife == 1) {
                                fishLifeArray[1]++;
                            } else if (fishLife == 2) {
                                fishLifeArray[2]++;
                            } else if (fishLife == 3) {
                                fishLifeArray[3]++;
                            } else if (fishLife == 4) {
                                fishLifeArray[4]++;
                            } else if (fishLife == 5) {
                                fishLifeArray[5]++;
                            } else if (fishLife == 6) {
                                fishLifeArray[6]++;
                            } else if (fishLife == 7) {
                                fishLifeArray[7]++;
                            } else if (fishLife == 8) {
                                fishLifeArray[8]++;
                            }
                        }

                        //process the fish count based on the number of days 
                        processData(days, fishLifeArray);
                        //calculate the total number of fish
                        decimal total = totalFishCount(fishLifeArray);

                        Console.WriteLine("Fish count after {0} days: {1}", days, total);

                    }
                }

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static decimal[] processData(int days, decimal[] fishLifeArray) {
            
            for (int x = 1; x <= days; x++) {
                //get count for each lifecycle
                decimal zeroDays = fishLifeArray[0];
                decimal oneDays = fishLifeArray[1];
                decimal twoDays = fishLifeArray[2];
                decimal threeDays = fishLifeArray[3];
                decimal fourDays = fishLifeArray[4];
                decimal fiveDays = fishLifeArray[5];
                decimal sixDays = fishLifeArray[6];
                decimal sevenDays = fishLifeArray[7];
                decimal eightDays = fishLifeArray[8];

                //set each value to the next day
                fishLifeArray[0] = oneDays;
                fishLifeArray[1] = twoDays;
                fishLifeArray[2] = threeDays;
                fishLifeArray[3] = fourDays;
                fishLifeArray[4] = fiveDays;                
                fishLifeArray[5] = sixDays;
                //fish that reach zero reset to six days
                fishLifeArray[6] = sevenDays + zeroDays;
                fishLifeArray[7] = eightDays;
                fishLifeArray[8] = zeroDays;

            }      

            return fishLifeArray;
        }

        public static decimal totalFishCount(decimal[] fishLifeArray) {
            decimal totalCount = 0;

            for (int x = 0; x < fishLifeArray.Length; x++) {
                totalCount += fishLifeArray[x];
            }      

            return totalCount;
        }
 
   }
}