/*
 * Developed by : Thibault MOONTAUFRAY
 * Date: 22/06/2011
 */
using System;

namespace Droid_FlatFile
{
	/// <summary>
	/// Description of Tag.
	/// </summary>
	public class Tag
	{
		#region Attributes
		private string tagValue;
		private string description;
		private string label;
		#endregion;
		
		#region Properties
		public string TagValue
		{
		    get { return tagValue; }
		    set { tagValue = value; }
		}
		public string Description
		{
		    get { return description; }
		    set { description = value; }
		}
		public string Label
		{
		    get { return label; }
		    set { label = value; }
		}
		#endregion
		
		#region Constructors
		public Tag()
		{
		}
		
		public Tag(Tag t)
		{
			this.tagValue = t.TagValue;
			this.description = t.Description;
			this.label = t.Label;
		}
		#endregion
		
		#region Methods
		#endregion
	}
}
