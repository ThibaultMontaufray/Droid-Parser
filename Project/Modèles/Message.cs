/*
 * Developed by : Thibault MOONTAUFRAY
 * Date: 22/06/2011
 */
using System;
using System.Collections.Generic;

namespace Droid_FlatFile
{
	/// <summary>
	/// Description of Message.
	/// </summary>
	public class Message
	{
		#region Attributes
		private List<Field> listField;
		private string label;
		private string description;
		private string messageValue;
		private string id;
		private int deeping;
		private int length;
		private bool succeed;
		#endregion
			
		#region Properties
		public string Label
		{
		    get { return label; }
		    set { label = value; }
		}
		public bool Succeed
		{
		    get { return succeed; }
		    set { succeed = value; }
		}
		public string ID
		{
		    get { return id; }
		    set { id = value; }
		}
		public int Length
		{
		    get { return length; }
		    set { length = value; }
		}
		public int Deeping
		{
		    get { return deeping; }
		    set { deeping = value; }
		}
		public string Description
		{
		    get { return description; }
		    set { description = value; }
		}
		public List<Field> ListField
		{
		    get { return listField; }
		    set { listField = value; }
		}
		public string MessageValue
		{
		    get { return messageValue; }
		    set { messageValue = value; }
		}
		#endregion
			
		#region Constructors
		public Message()
		{
			listField = new List<Field>();
		}
		
		public Message(Message m)
		{
			this.label = m.Label;
			this.succeed = m.Succeed;
			this.length = m.Length;
			this.deeping = m.Deeping;
			this.description = m.Description;
			this.messageValue = m.MessageValue;
			this.id = m.ID;
			
			this.listField = new List<Field>();
			foreach (Field f in m.ListField) 
			{
				this.listField.Add(new Field(f));
			}
		}
		#endregion
		
		#region Methods
		public void AddField(Field f)
		{
			listField.Add(f);
		}
		#endregion
	}
}
