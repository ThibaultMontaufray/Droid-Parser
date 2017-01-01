// code log 10

using Tools4Libraries;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Droid_FlatFile
{
	/// <summary>
	/// Description of Dico.
	/// </summary>
	public class Dico
	{
		#region Attribute
		private string name;
		private string pathfile;
		private string description;
		
		private List<Message> listMessages;
		private List<Field> listFields;
		private List<CodeMessage> listCodeMsg;
		private List<Info> listInfo;
		private List<Format> listFormat;
		
		private string lastObject;
		#endregion
		
		#region Properties
		public string Name
		{
			get { return name; }
			set 
			{
				name = value; 
				//lastObject = "Name";
			}
		}
		public string PathFile
		{
			get { return pathfile; }
			set 
			{
				pathfile = value; 
				//lastObject = "pathfile";
			}
		}
		public string Description
		{
			get { return description; }
			set 
			{ 
				description = description + "|" + value; 
				//lastObject = "description";
			}
		}
		public List<Format> ListFormat
		{
			get { return listFormat; }
			set 
			{
				listFormat = value; 
				//lastObject = "listformat";
			}
		}
		public List<Message> ListMessages
		{
			get { return listMessages; }
			set 
			{
				listMessages = value; 
				//lastObject = "listmessage";
			}
		}
		public List<Field> ListFields
		{
			get { return listFields; }
			set 
			{ 
				listFields = value; 
				//lastObject = "listfield";
			}
		}
		public List<CodeMessage> ListCodeMessage
		{
			get { return listCodeMsg; }
			set 
			{ 
				listCodeMsg = value; 
				//lastObject = "listcodemessage";
			}
		}
		#endregion
		
		#region Constructor
		public Dico()
		{
			lastObject = "";
			
			listFields = new List<Field>();
			listMessages = new List<Message>();
			listCodeMsg = new List<CodeMessage>();
			listInfo = new List<Info>();
			listFormat = new List<Format>();
		}
		#endregion
		
		#region Methods
		public void AddField(Field f)
		{
			listFields.Add(f);
			lastObject = "field";
		}
		
		public void AddMessage(Message m)
		{
			listMessages.Add(m);
			//lastObject = "message";
		}
		
		public void AddCodeMessage(CodeMessage cm)
		{
			listCodeMsg.Add(cm);
			//lastObject = "codemessage";
		}
		
		public void AddInfo(Info i)
		{
			listInfo.Add(i);
			//lastObject = "info";
		}
		
		public void AddItem(Item i)
		{
			Log.write("TODO !");
			//lastObject = "item";
		}
		
		public void AddTag(Tag t)
		{
			if (lastObject.Equals("field"))
			{
				listFields[listFields.Count - 1].ListTag.Add(t);
			}
			else if (lastObject.Equals("type"))
			{
				Field f = listFields[listFields.Count - 1];
				f.ListType[f.ListType.Count - 1].ListTag.Add(t);
			}
			else
			{
				Log.write("[ WRN : 1000 ] enchainement non prévu : " + lastObject);
			}
			//lastObject = "tag";
		}
		
		public void AddType(Type t)
		{
			listFields[listFields.Count - 1].ListType.Add(t);
			lastObject = "type";
		}
		
		public void AddFormat(Format f)
		{
			listFormat.Add(f);
			//lastObject = "format";
		}
		
		public void AddDescription(string s)
		{
			if (lastObject.Equals("field"))
			{
				listFields[listFields.Count - 1].Description = s;
			}
			else if (lastObject.Equals("type"))
			{
				Field f = listFields[listFields.Count - 1];
				f.ListType[f.ListType.Count - 1].Description = s;
			}
			else
			{
				this.description = s;
			}
		}
		#endregion
	}
}
