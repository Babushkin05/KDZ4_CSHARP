using System;
namespace CollegeLib
{
	/// <summary>
	/// Class of tools for Colleges.
	/// </summary>
	public class Tools
	{
		/// <summary>
		/// Filter table by name in some fields.
		/// </summary>
		/// <param name="selectedIndex">Index of field to filter.</param>
		/// <param name="selectedName">Name to filter.</param>
		/// <param name="colleges">Table to filter.</param>
		/// <returns>Filtered table.</returns>
		public static List<College> Filtration(int selectedIndex, string selectedName, List<College> colleges)
		{
			List<College> filteredColleges = new List<College>();

			filteredColleges.Add(colleges[0]);

			for(int i = 1; i < colleges.Count(); i++)
			{
				if (colleges[i][selectedIndex] == selectedName)
					filteredColleges.Add(colleges[i]);
			}

			return filteredColleges;
		}

		/// <summary>
		/// Sort table by 'rayon'.
		/// </summary>
		/// <param name="colleges">Table to sort.</param>
		/// <param name="reversed">Is needed reversed sort.</param>
		public static void Sorting(ref List<College> colleges, bool reversed = false)
		{
			colleges.Sort(1,colleges.Count()-1,Comparer<College>.Create((a,b)
					=> String.Compare(b[4], a[4])));

			if (reversed)
				colleges.Reverse();
        }
	}
}

