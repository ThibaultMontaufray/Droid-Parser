using System;

namespace Droid_FlatFile
{
	public class CodeMessage
	{
		#region Attribute
		private string id;
		private string info;
		private string position;
		private string description;
		#endregion
		
		#region Properties
		public string ID
		{
			get { return id; }
			set { id = value; }
		}
		public string Position
		{
			get { return position; }
			set { position = value; }
		}
		public string Description
		{
			get { return description; }
			set { description = value; }
		}
		public string Info
		{
			get { return info; }
			set { info = value; }
		}
		#endregion
		
		#region Constructor
		public CodeMessage()
		{
		}
		#endregion
		
		#region Methods
		#endregion
	}
}
