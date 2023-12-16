using System;
using CollegeLib;
namespace KDZBABUSHKIN4
{
    /// <summary>
    /// Class to work with files.
    /// </summary>
	public class FileWork
	{
        // Headline in file must be this.
		static string headLine = "ROWNUM;name;adress;okrug;rayon;form_of_incorporation;submission;tip_uchrezhdeniya;" +
			"vid_uchrezhdeniya;telephone;web_site;e_mail;X;Y;global_id;";

        /// <summary>
        /// Check is line from file correct.
        /// </summary>
        /// <param name="line">String line from file.</param>
        /// <returns>Is line correct.</returns>
        static bool IsCorrectLine(string line)
        {
            int quotes = 0;
            int semicolon = 0;

            // Count only useful semicolons.
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                    quotes++;

                if (line[i] == ';')
                {
                    if (quotes % 2 == 0)
                        semicolon++;
                }
            }

            return semicolon == 15;
        }

        /// <summary>
        /// Convert line from file to exempler of class College.
        /// </summary>
        /// <param name="line">String line from file.</param>
        /// <returns>Exempler of class College.</returns>
        static College ConvertLineToClass(string line)
        {
            // Count semicolons smart to split line.
            int quotes = 0;
            string[] splitedLine = new string[15];
            int lastInd = 0;
            string str = "";

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"') quotes++;
                if (line[i] == ';' && quotes % 2 == 0)
                {
                    splitedLine[lastInd] = str;
                    str = "";
                    lastInd++;
                }
                else str += line[i];
            }

            // Make required class field.
            Contacts contacts = new Contacts
            {
                Adress = splitedLine[2].Trim('"'),
                WebSite = splitedLine[10].Trim('"'),
                PhoneNumbers = splitedLine[9].Trim('"'),
                Email = splitedLine[11].Trim('"')
            };


            College college = new College(contacts);

            // Fill class.
            for (int i = 1; i < 15; i++)
            {
                college[i] = splitedLine[i].Trim('"');
            }

            return college;
        }

        /// <summary>
        /// Read list of colleges from file.
        /// </summary>
        /// <returns>List of colleges from file.</returns>
        public static List<College> ReadCsv()
		{
            List<College> colleges = new List<College>();

            // Repeat Cycle until user give correct data.
			bool isRepeat;
			do
			{
				isRepeat = false;
                // Read file by user's input.
				string? readedFile = File.ReadAllText(Terminal.GetPathToRead());

				string[] readedLines = readedFile.Split('\n');

				isRepeat = readedLines[0] != headLine;

				colleges.Add(ConvertLineToClass(readedLines[0]));

				for (int i = 1; i < readedLines.Length; i++)
				{
                    // In the example file last string is empty.
					if (readedLines[i] == "")
						continue;

					isRepeat |= !IsCorrectLine(readedLines[i]);
					if (isRepeat)
						break;

					colleges.Add(ConvertLineToClass(readedLines[i]));

				}
			} while (isRepeat);

			return colleges;
		}

        /// <summary>
        /// Save list of colleges to file.
        /// </summary>
        /// <param name="path">Path to save file.</param>
        /// <param name="colleges">List to save.</param>
        /// <param name="choosenType">Choosen type of saving (1: Create new file, 2: Rewrite data to file, 3: Add lines to file</param>
		public static void SaveFile(string path, List<College> colleges, int choosenType)
		{
            // Write file headline.
			if (choosenType != 2)
				File.WriteAllText(path,headLine+'\n');

            // Make text to append it in file.
			string lines = "";
			for(int i = 1; i < colleges.Count(); i++)
			{
				lines += i.ToString() + ';' + colleges[i]+'\n';
			}

			File.AppendAllText(path,lines);
		}

        /// <summary>
        /// Check is file name. correct.
        /// </summary>
        /// <param name="Name">Name of file.</param>
        /// <returns>Is file name corrrect.</returns>
		public static bool IsCorrectFileName(string Name)
		{
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char chr in Name)
            {
                if (invalidChars.Contains(chr))
                {
					return false;
                }
            }

			return Name[^4..]==".csv";
        }


	}
}

