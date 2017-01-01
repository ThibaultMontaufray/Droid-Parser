using System;
using System.Collections.Generic;

namespace Droid_FlatFile
{
	/// <summary>
	/// Description of Info.
	/// </summary>
	public class Info
	{
		#region Attribute
		private List<Item> listItem;
		private string name;
		#endregion
		
		#region Properties
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		public List<Item> ListItem
		{
			get { return listItem; }
			set { listItem = value; }
		}
		#endregion
		
		#region Constructor
		public Info()
		{
			
		}
		#endregion
	}
}
