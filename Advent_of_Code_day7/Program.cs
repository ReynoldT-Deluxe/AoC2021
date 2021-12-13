using System;
using System.IO;

namespace FileApplication {
    class Program {
        static void Main(string[] args) {
            //get input data
            try {                
                using (StreamReader sr = new StreamReader("/Users/T452172/Downloads/Advent_of_code_day7/data.txt")) {
                    Console.WriteLine("****************************************");
                    Console.WriteLine("*         Advent of Code 2021          *");
                    Console.WriteLine("*    Day 7: The Treachery of Whales    *");
                    Console.WriteLine("****************************************");

                    string line;
                                        
                    while ((line = sr.ReadLine()) != null) {
                        string[] subs = line.Split(',');
                        int[] crabSubPositionArray = new int[subs.Length];

                        for (int data = 0; data < subs.Length; data++) {
                            crabSubPositionArray[data] = Convert.ToInt32(subs[data]);
                        }

                        //deterine min/max of array
                        int maxPosition = getMax(crabSubPositionArray);
                        //Console.WriteLine("max: {0}", maxPosition);
                        int minPosition = getMin(crabSubPositionArray);
                        //Console.WriteLine("min: {0}", minPosition);

                        // ------- (Day 7 A) -------
                        //determine the fuel cost per position
                        int[] fuelConsumptionArray = getFuelConsumptionArray(minPosition, maxPosition, crabSubPositionArray);

                        //determine the consumption at the convergence position 
                        int minFuelConsumption = getMinFuelConsumption(fuelConsumptionArray);
                        int minFuelConsumptionPosition = getMinConsumptionHorizontalPosition(minPosition, fuelConsumptionArray);
                        Console.WriteLine("The least fuel consumption for all the 'normal' submarines is: {0} at position {1}", minFuelConsumption, minFuelConsumptionPosition);

                        // ------- (Day 7 B) -------
                        //determine the fuel cost per position for crab submarine
                        int[] crabSubFuelConsumptionArray = getCrabSubmarineFuelConsumptionArray(minPosition, maxPosition, crabSubPositionArray);
                        
                        //determine the consumption at the convergence position 
                        int minCrabSubConsumption = getMinFuelConsumption(crabSubFuelConsumptionArray);
                        int minCrrabSubFuelConsumptionPosition = getMinConsumptionHorizontalPosition(minPosition, fuelConsumptionArray);
                        Console.WriteLine("The least fuel consumption for all the crab submarines is: {0} at position {1}", minCrabSubConsumption, minCrrabSubFuelConsumptionPosition);

                    }
                }              

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static int getMin(int[] positionArray) {
            int previousNum = positionArray[0];
            int currentNum;
            
            for (int data = 1; data < positionArray.Length; data++) {                
                currentNum = positionArray[data];

                if (currentNum < previousNum) {
                    previousNum = currentNum;
                }
            }

            return previousNum;
        }

         public static int getMax(int[] positionArray) {
            int previousNum = positionArray[0];
            int currentNum;
            
            for (int data = 1; data < positionArray.Length; data++) {
                currentNum = positionArray[data];
 
                if (currentNum > previousNum) {
                    previousNum = currentNum;
                }
            }

            return previousNum;
        }

        public static int[] getFuelConsumptionArray(int min, int max, int[] positionArray) {
            int[] fuelConsumptionArray = new int[(max - min) + 1];            

            //iterate thru the possible positions
            int index = 0;
            for (int targetPosition = min; targetPosition <= max; targetPosition++) {
                int fuelConsumption = 0;
                //iterate thru each crab
                for (int data = 0; data < positionArray.Length; data++) {
                    int currentPosition = positionArray[data];
                    int fuelCost = 0;

                    if (currentPosition > targetPosition) {
                        fuelCost = currentPosition - targetPosition;
                        fuelConsumption += fuelCost;
                    } else if (currentPosition < targetPosition) {
                        fuelCost = targetPosition - currentPosition;
                        fuelConsumption += fuelCost;
                    }                
                    
                    //Console.WriteLine("position: {0}    fuel consumption for crab {1}: {2}", targetPosition, data, fuelCost);                    
                }

                fuelConsumptionArray[index] = fuelConsumption;
                index++;
            }

            return fuelConsumptionArray;
        }

        public static int getMinFuelConsumption(int[] consumptionArray) {
            int previousNum = consumptionArray[0];
            int currentNum;

            for (int data = 1; data < consumptionArray.Length; data++) {                
                currentNum = consumptionArray[data];
                
                if (currentNum < previousNum) {
                    previousNum = currentNum;
                }
            }

            return previousNum;
        }

        public static int getMinConsumptionHorizontalPosition(int min, int[] consumptionArray) {
            int previousNum = consumptionArray[0];
            int currentNum;
            int position = min;

            for (int data = 1; data < consumptionArray.Length; data++) {                
                currentNum = consumptionArray[data];
                
                if (currentNum < previousNum) {
                    previousNum = currentNum;
                    position = min;
                }

                min++;
            }

            return position;
        }

        public static int getCrabSubmarineFuelConsumption(int positions) {
            int consumption = 0;

            if (positions == 0) {
                consumption = 0;
            } else {
                for (int step = 1; step <= positions; step++) {
                    consumption += step;
                }                
            }

            return consumption;
        }

        public static int[] getCrabSubmarineFuelConsumptionArray(int min, int max, int[] positionArray) {
            int[] fuelConsumptionArray = new int[(max - min) + 1];            

            //iterate thru the possible positions
            int index = 0;
            for (int targetPosition = min; targetPosition <= max; targetPosition++) {
                int fuelConsumption = 0;
                //iterate thru each crab submarine
                for (int data = 0; data < positionArray.Length; data++) {
                    int currentPosition = positionArray[data];
                    int positions = 0;

                    if (currentPosition > targetPosition) {
                        positions = currentPosition - targetPosition;                        
                        fuelConsumption += getCrabSubmarineFuelConsumption(positions);
                    } else if (currentPosition < targetPosition) {
                        positions = targetPosition - currentPosition;
                        fuelConsumption += getCrabSubmarineFuelConsumption(positions);
                    }                
                    
                    //Console.WriteLine("position: {0}    fuel consumption for crab {1}: {2}", targetPosition, data, fuelCost);                    
                }

                fuelConsumptionArray[index] = fuelConsumption;
                index++;
            }

            return fuelConsumptionArray;
        }
    }
}  
