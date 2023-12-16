// Variant 15.
using CollegeLib;
namespace KDZBABUSHKIN4
{
    /// <summary>
    /// Class to run program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            do
            {
                // Clear Console for new solve iteration.
                Console.Clear();

                // Read Colleges from file.
                List<College> colleges = FileWork.ReadCsv();

                // Print some colleges on console.
                Terminal.WritePartOfTable(colleges);

                // Make action with table.
                List<College> changedColleges = Terminal.ChangeList(colleges);

                // Save changes.
                Terminal.FileSaving(changedColleges);

                // Solve repeating.
                Console.Write("type 'y' to repeat program");
            } while (Console.ReadKey().Key == ConsoleKey.Y);
        }
    }
}