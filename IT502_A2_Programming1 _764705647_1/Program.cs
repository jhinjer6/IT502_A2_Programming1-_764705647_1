/* 
 * Project Name: LANGHAM Hotel Management System
 * Author Name:  Jhinjer Jashan
 * Date: 21-07-2024
 * Application Purpose: Apply the fundamental principles of software development, including fundamental mathematical
 *                      and logical concepts that underpin computational and systems thinking, to plan, create, test and
 *                      document simple working code.
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assessment2Task2
{
    // Custom Class - Room
    public class Room
    {
        public int RoomNo { get; set; }
        public bool IsAllocated { get; set; }

    }

    // Custom Class - Customer
    public class Customer
    {
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }

    }

    // Custom Class - RoomAllocation
    public class RoomlAllocaltion
    {
        public int AllocatedRoomNo { get; set; }
        public Customer AllocatedCustomer { get; set; }

    }

    // Custom Main Class - Program
    class Program
    {
        // Variables declaration and initialization
        public static Room[] listofRooms = new Room[0];
        public static int[] listOfRoomlAllocaltions = new int[0];
        public static string filePath;
        public static int[] listOfCustomerNumbers = new int[0];
        public static string[] listOfCustomerNames = new string[0];

        // Main function
        static void Main(string[] args)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(folderPath, "HotelManagement.txt");

            char ans = 'Y';

            do
            {
                Console.Clear();
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("                 LANGHAM HOTEL MANAGEMENT SYSTEM                  ");
                Console.WriteLine("                            MENU                                 ");
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("1. Add Rooms");
                Console.WriteLine("2. Display Rooms");
                Console.WriteLine("3. Allocate Rooms");
                Console.WriteLine("4. De-Allocate Rooms");
                Console.WriteLine("5. Display Room Allocation Details");
                Console.WriteLine("6. Billing");
                Console.WriteLine("7. Save the Room Allocations To a File");
                Console.WriteLine("8. Show the Room Allocations From a File");
                Console.WriteLine("9. Exit");
                // Add new option 0 for Backup 
                Console.WriteLine("0. Backup Room Allocation File");
                Console.WriteLine("***********************************************************************************");
                Console.Write("Enter Your Choice Number Here:");
                int choice;
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        // adding Rooms function
                        try
                        {
                            Console.Write("Please Enter the Total Number of Rooms in the Hotel: ");
                            int totalRooms = Convert.ToInt32(Console.ReadLine());

                            Room[] newRooms = new Room[listofRooms.Length + totalRooms];
                            Array.Copy(listofRooms, newRooms, listofRooms.Length);

                            for (int i = listofRooms.Length; i < newRooms.Length; i++)
                            {
                                Console.Write("Please enter the Room Number: ");
                                int roomNo = Convert.ToInt32(Console.ReadLine());
                                newRooms[i] = new Room { RoomNo = roomNo };
                            }

                            listofRooms = newRooms;
                            Console.WriteLine("Rooms added successfully.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Unhandled Exception: System.FormatException: Input string was not in a correct format.");
                        }
                        break;
                    case 2:
                        // display Rooms function;
                        if (listofRooms.Length == 0)
                        {
                            Console.WriteLine("No rooms available.");
                            break;
                        }

                        Console.WriteLine("List of Rooms:");
                        foreach (var room in listofRooms)
                        {
                            Console.WriteLine($"Room Number: {room.RoomNo}, Is Allocated: {room.IsAllocated}");
                        }
                        break;
                    case 3:
                        // allocate Room To Customer function
                        try
                        {
                            Console.Write("Please Enter the Room Number to Allocate: ");
                            int roomNo = Convert.ToInt32(Console.ReadLine());

                            var room = listofRooms.FirstOrDefault(r => r.RoomNo == roomNo);
                            if (room == null || room.IsAllocated)
                            {
                                throw new InvalidOperationException("Room is either not available or already allocated.");
                            }
                            Console.Write("Please Enter the Customer Number: ");
                            int customerNo = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Please Enter the Customer Name: ");
                            string customerName = Console.ReadLine();

                            Array.Resize(ref listOfRoomlAllocaltions, listOfRoomlAllocaltions.Length + 1);
                            Array.Resize(ref listOfCustomerNumbers, listOfCustomerNumbers.Length + 1);
                            Array.Resize(ref listOfCustomerNames, listOfCustomerNames.Length + 1);

                            listOfRoomlAllocaltions[listOfRoomlAllocaltions.Length - 1] = roomNo;
                            listOfCustomerNumbers[listOfCustomerNumbers.Length - 1] = customerNo;
                            listOfCustomerNames[listOfCustomerNames.Length - 1] = customerName;

                            room.IsAllocated = true;
                            Console.WriteLine("Room allocated successfully.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Unhandled Exception: System.FormatException: Input string was not in a correct format.");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine("Unhandled Exception: System.InvalidOperationException: Sequence contains no matching element.");
                        }
                        break;
                    case 4:
                        // De-Allocate Room From Customer function
                        try
                        {
                            Console.Write("Please Enter the Room Number to De-Allocate: ");
                            int roomNo = Convert.ToInt32(Console.ReadLine());

                            int index = Array.FindIndex(listOfRoomlAllocaltions, ra => ra == roomNo);
                            if (index == -1)
                            {
                                throw new InvalidOperationException("Room is not allocated.");
                            }

                            listOfRoomlAllocaltions = listOfRoomlAllocaltions.Where((val, idx) => idx != index).ToArray();
                            listOfCustomerNumbers = listOfCustomerNumbers.Where((val, idx) => idx != index).ToArray();
                            listOfCustomerNames = listOfCustomerNames.Where((val, idx) => idx != index).ToArray();

                            var room = listofRooms.FirstOrDefault(r => r.RoomNo == roomNo);
                            room.IsAllocated = false;

                            Console.WriteLine("Room de-allocated successfully.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Unhandled Exception: System.FormatException: Input string was not in a correct format.");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine("Unhandled Exception: System.InvalidOperationException: Sequence contains no matching element.");
                        }
                        break;
                    case 5:
                        // display Room Alocations function;
                        if (listOfRoomlAllocaltions.Length == 0)
                        {
                            Console.WriteLine("No room allocations available.");
                            break;
                        }

                        Console.WriteLine("Room Allocations:");
                        for (int i = 0; i < listOfRoomlAllocaltions.Length; i++)
                        {
                            Console.WriteLine($"Room Number: {listOfRoomlAllocaltions[i]}, Customer Name: {listOfCustomerNames[i]}");
                        }
                        break;
                    case 6:
                        //  Display "Billing Feature is Under Construction and will be added soon…!!!"
                        Console.WriteLine("Billing Feature is Under Construction and will be added soon…!!!");
                        break;
                    case 7:
                        // SaveRoomAllocationsToFile
                        try
                        {
                            using (StreamWriter writer = new StreamWriter(filePath, true))
                            {
                                writer.WriteLine("Room Allocations as of " + DateTime.Now);
                                for (int i = 0; i < listOfRoomlAllocaltions.Length; i++)
                                {
                                    writer.WriteLine($"Room Number: {listOfRoomlAllocaltions[i]}, Customer Name: {listOfCustomerNames[i]}");
                                }
                            }

                            Console.WriteLine("Room allocations saved to file successfully.");
                        }
                        catch (UnauthorizedAccessException)
                        {
                            Console.WriteLine("Unhandled Exception: System.ArgumentException: Stream was not writable.");
                        }
                        break;
                    case 8:
                        //Show Room Allocations From File
                        try
                        {
                            if (!File.Exists(filePath))
                            {
                                throw new FileNotFoundException("Unhandled Exception: System.FileNotFoundException: Could not find file.");
                            }

                            using (StreamReader reader = new StreamReader(filePath))
                            {
                                string content = reader.ReadToEnd();
                                Console.WriteLine("Room Allocations from File:");
                                Console.WriteLine(content);
                            }
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine("Unhandled Exception: System.FileNotFoundException: Could not find file.");
                        }
                        break;
                    case 9:
                        // Exit Application
                        Console.WriteLine("Exiting application...\nThank You!!");
                        break;
                    case 0:
                        try
                        {
                            if (!File.Exists(filePath))
                            {
                                throw new FileNotFoundException("\"Unhandled Exception: System.FileNotFoundException: Could not find file.");
                            }

                            string backupFilePath = Path.Combine(Path.GetDirectoryName(filePath), "lhms_studentid_backup.txt");

                            File.Copy(filePath, backupFilePath, true);

                            Console.WriteLine("Room allocation file backed up successfully.");
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine("Unhandled Exception: System.FileNotFoundException: Could not find file.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.Write("\nWould You Like To Continue(Y/N):");
                ans = Convert.ToChar(Console.ReadLine());
            } while (ans == 'y' || ans == 'Y');
        }
    }
}