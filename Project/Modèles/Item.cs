using System;

namespace Droid_FlatFile
{
	/// <summary>
	/// Description of Item.
	/// </summary>
	public class Item
	{
		#region Attribute
		private string val;
		private string description;
		#endregion
		
		#region Properties
		public string Val
		{
			get { return val; }
			set { val = value; }
		}
		public string Description
		{
			get { return description; }
			set { description = value; }
		}
		#endregion
		
		#region Constructor
		public Item()
		{
		}
		#endregion
	}
}
