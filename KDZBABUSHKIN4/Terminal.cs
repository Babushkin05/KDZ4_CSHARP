using System;
using System.Xml.Linq;
using CollegeLib;
namespace KDZBABUSHKIN4
{
	/// <summary>
	/// Class to communicate with user.
	/// </summary>
	public class Terminal
	{
		/// <summary>
		/// Slider for choosing comething from a array.
		/// </summary>
		/// <param name="list">Array for variants.</param>
		/// <returns>Chosed index</returns>
		static int Slider(string[] list)
		{
			int isChoosen = -1;

			ConsoleKey pressed = ConsoleKey.UpArrow;
			do
			{
				Console.Clear();

				// Process user activities.
				if (pressed == ConsoleKey.DownArrow)
					isChoosen = Math.Min(isChoosen + 1, list.Length-1);
				else if (pressed == ConsoleKey.UpArrow)
                    isChoosen = Math.Max(isChoosen - 1, 0);

				for(int i = 0; i < list.Length; i++)
				{
					// Current chosed index has another theme.
					if(i == isChoosen)
					{
						Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(list[i]);
					}
					else
					{
						Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(list[i]);
					}
				}

				pressed = Console.ReadKey().Key;

            } while (pressed != ConsoleKey.Enter);

			// Back to auto settings.
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            return isChoosen;
        }

		/// <summary>
		/// Bulean function useful for saving stage.
		/// </summary>
		/// <param name="name">Name of file.</param>
		/// <param name="choosen">Choosen type of saving.</param>
		/// <returns>Is good parameters to save file.</returns>
        static bool F(string name, int choosen)
        {
            if (name is null || name == "")
                return false;

            char sep = Path.DirectorySeparatorChar;
            string path = $"..{sep}..{sep}..{sep}..{sep}" + name;

            return ((choosen == 0 & !File.Exists(path)) || (choosen != 0 & File.Exists(path))) & FileWork.IsCorrectFileName(name);
        }

		/// <summary>
		/// Writes table in the console.
		/// </summary>
		/// <param name="colleges">Table to write it.</param>
        static void PrintTable(College[] colleges)
        {
            Console.Clear();

			// Check is array is empty.
            if (colleges.Length == 1)
            {
                Console.WriteLine("Table is empty(");
                Console.WriteLine("Type something to continue");
                return;
            }

			// Write headline.
            Console.WriteLine("ROWNUM  name    adress  okrug   rayon   form... submi...tip_u...vid_u...telep...web_s...e_mail  X       Y       globa...");

			// Write fields with using same tabulation.
			for (int i = 1; i < colleges.Length; i++)
            {
                Console.Write(i);
                for (int j = 0; j < 7 - Math.Log10(i); j++) Console.Write(" ");

                for (int j = 1; j < 15; j++)
                {
                    if (colleges[i][j].Length > 5) Console.Write(colleges[i][j][..5] + "...");

                    else
                    {
                        Console.Write(colleges[i][j]);
                        for (int k = 0; k < 8 - colleges[i][j].Length; k++) Console.Write(' ');
                    }
                }

                Console.Write('\n');
            }

            Console.WriteLine("Type something to continue");
        }

		/// <summary>
		/// Redifinition of method for lists. Convert lisr to array and use same method for arrays.
		/// </summary>
		/// <param name="colleges">List to print.</param>
        static void PrintTable(List<College> colleges)
        {
            College[] collegesArr = new College[colleges.Count()];

            for (int i = 0; i < colleges.Count(); i++)
            {
                collegesArr[i] = colleges[i];
            }

            PrintTable(collegesArr);
        }

		/// <summary>
		/// Gets name to read file.
		/// </summary>
		/// <returns>Name of file.</returns>
        public static string GetPathToRead()
        {
			// Using these configuration for beauty.
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            string? path;
            do
            {
                Console.Write("Type name of file (with '.csv') :: ");

                char sep = Path.DirectorySeparatorChar;
                path = $"..{sep}..{sep}..{sep}..{sep}" + Console.ReadLine();

            } while (!File.Exists(path));

            return path;
        }

		/// <summary>
		/// Write head of bottom of table.
		/// </summary>
		/// <param name="colleges">Table to write.</param>
        public static void WritePartOfTable(List<College> colleges)
		{
			int choosenPart = Slider(new string[] { "Print top of the table", "Print bottom of the table" });

			Console.Clear();

			int n;
			do
			{
				Console.Write("Type n in range from 2 to table size :: ");
			} while (!int.TryParse(Console.ReadLine(), out n) || n > colleges.Count()-1);

			// n+1 because not counting head.
			College[] collegesToPrint = new College[n+1];

			for(int i = 0; i < n+1; i++)
			{
				collegesToPrint[i] = colleges[(colleges.Count() - n-1) * choosenPart + i];
			}

			PrintTable(collegesToPrint);
		}

		/// <summary>
		/// Make activities with table.
		/// </summary>
		/// <param name="colleges">Table of colleges.</param>
		/// <returns>Changed table.</returns>
		public static List<College> ChangeList(List<College> colleges)
		{
			Console.ReadKey();

			int choosenTool = Slider(new string[] { "Sort table by 'rayon'","Reveresed sort table by 'rayon'",
				"Filtration by 'form_of_incorporation'", "Filtration by 'submission'" });

			string? filter = "";

			// If filtration.
			if (choosenTool > 1)
			{
                do
                {
                    Console.Write("Type name for filtration :: ");
                    filter = Console.ReadLine();
                } while (filter is null);
            }

			// Making changes.
			switch (choosenTool)
			{
				case 0:
					Tools.Sorting(ref colleges);
					break;
				case 1:
					Tools.Sorting(ref colleges);
					break;
				case 2:
					colleges = Tools.Filtration(5, filter, colleges);
					break;
				case 3:
					colleges = Tools.Filtration(6, filter, colleges);
					break;
			}

			PrintTable(colleges);

			return colleges;
		}

		/// <summary>
		/// Save file.
		/// </summary>
		/// <param name="colleges"> file to save.</param>
		public static void FileSaving(List<College> colleges)
		{
			Console.ReadKey();
			int choosenType = Slider(new string[] { "Create new file", "Rewrite data to file", "Add lines to file" });

			string? fPath;
			do
			{
				Console.Write("Type file name (with '.csv'):: ");

				fPath =  Console.ReadLine();
			} while (!F(fPath,choosenType));

            char sep = Path.DirectorySeparatorChar;
			fPath = $"..{sep}..{sep}..{sep}..{sep}" + fPath;

            FileWork.SaveFile(fPath, colleges, choosenType);

			Console.WriteLine("Save correctly");
		}

		
	}
}

