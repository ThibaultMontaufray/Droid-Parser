// log code 11

using System;
using System.Collections.Generic;

namespace Droid_FlatFile
{
	/// <summary>
	/// Description of Type.
	/// </summary>
	public class Type
	{
		#region Attributes
		private List<Tag> listTag;
		private string label;
		private Format format;
		private Length length;
		private string description;
		#endregion
		
		#region Properties
		public List<Tag> ListTag
		{
			get { return listTag; }
			set { listTag = value; }
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
		public Format Format
		{
			get { return format; }
			set { format = value; }
		}
		public Length Length
		{
			get { return length; }
			set { length = value; }
		}
		#endregion
		
		#region Constructor
		public Type()
		{
			listTag = new List<Tag>();
		}
		
		public Type(Type t)
		{
			this.label = t.label;
			this.format = new Format(t.format);
			this.length = new Length(t.length);
			listTag = new List<Tag>();
			foreach (Tag te in t.listTag) 
			{
				this.listTag.Add(new Tag(te));
			}			
		}
		#endregion
		
		#region Methods
		#endregion
	}
}
