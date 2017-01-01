/*
 * Developed by : Thibault MOONTAUFRAY
 * Date: 22/06/2011
 */
using System;
using System.Collections.Generic;

namespace Droid_FlatFile
{
	/// <summary>
	/// Description of Fields.
	/// </summary>
	public class Field
	{
		#region Attributes
		private List<Tag> listTag;
		private List<Type> listType;
		private string code;
		private string label;
		private string type;
		private int length;
		private string description;
		private string fieldValue;
		private string org;
		private Length lengthDico;
		private Format format;
		#endregion
		
		#region Properties
		public List<Type> ListType
		{
		    get { return listType; }
		    set { listType = value; }
		}
		public List<Tag> ListTag
		{
		    get { return listTag; }
		    set { listTag = value; }
		}
		public string Code
		{
		    get { return code; }
		    set { code = value; }
		}
		public Format Format
		{
		    get { return format; }
		    set { format = value; }
		}
		public string Org
		{
		    get { return org; }
		    set { org = value; }
		}
		public string Label
		{
		    get { return label; }
		    set { label = value; }
		}
		public string Type
		{
		    get { return type; }
		    set { type = value; }
		}
		public int Length
		{
		    get { return length; }
		    set { length = value; }
		}
		public Length LengthDico
		{
		    get { return lengthDico; }
		    set { lengthDico = value; }
		}
		public string Description
		{
		    get { return description; }
		    set { description = value; }
		}
		public string FieldValue
		{
		    get { return fieldValue; }
		    set { fieldValue = value; }
		}
		#endregion
		
		#region Constructors
		public Field()
		{
			listTag = new List<Tag>();
			listType = new List<Type>();
		}
		
		public Field(Field f)
		{			
			this.code = f.Code;
			this.label = f.Label;
			this.type = f.Type;
			this.length = f.Length;
			this.description = f.Description;
			this.fieldValue = f.FieldValue;
			this.listTag = new List<Tag>();  
			foreach (Tag tg in f.listTag) 
			{
				this.listTag.Add(new Tag(tg));
			}
			this.listType = new List<Type>();  
			foreach (Type tp in f.listType) 
			{
				this.listType.Add(new Type(tp));
			}
		}
		#endregion
		
		#region Methods
		public void AddTag(Tag t)
		{
			listTag.Add(t);
		}
		#endregion
	}
}
