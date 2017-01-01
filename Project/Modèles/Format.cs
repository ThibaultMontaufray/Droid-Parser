using Tools4Libraries;
using System;
using System.Collections.Generic;

namespace Droid_FlatFile
{
	/// <summary>
	/// Description of obj.
	/// </summary>
	/// 
	public class Format
	{ 
		#region Attributes
		private string notation;
		private string description;
		private string listval;
		
		private string[] typeList = { "a", "c", "n", "p", "s", "b", "z", "an", "as", "ns", "anp", "ans", "ansc", "ansb", "anscb", "AA", "MM", "JJ", "hh", "mm", "ss", "x", "L", "LL", "VAR" };
		private string typeRef;
		#endregion
		
		#region Properties
		public string Notation
		{
			get { return notation; }
			set { notation = value; }
		}
		public string Description
		{
			get { return description; }
			set { description = value; }
		}
		public string ListVal
		{
			get { return listval; }
			set { listval = value; }
		}
		#endregion
		
		#region Constructor
		public Format(string type)
		{
			typeRef = type;
			bool flag = false;
			foreach (string s in typeList) 
			{
				if (s.Equals(typeRef)) 
				{
					flag = true;
					break;
				}
			}
			if(!flag) { Log.write("[ WRN : 0600 ] this designation is not corret, you have to check your dictionnary or you're file. correction on the type : " + type); }
		}
		
		public Format(string desc, List<Format> list)
		{
			foreach (Format f in list) 
			{
				if ( f.typeRef.Equals(desc))
				{
					this.typeRef = f.typeRef;
					this.listval = f.listval;
					this.notation = f.notation;
					this.description = f.description;
					break;
				}
			}
		}
		
		public Format(Format f)
		{
			this.description = f.description;
			this.notation = f.notation;
			this.listval = f.listval;
			this.typeRef = f.typeRef;
			this.typeList = f.typeList;
		}
		#endregion
		
		#region Methods public
		public bool isRespected()
		{
			switch (typeRef) 
			{
				case "a":
					break;
				case "c":
					break;
				case "n":
					break;
				case "p":
					break;
				case "s":
					break;
				case "b":
					break;
				case "z":
					break;
				case "an":
					break;
				case "as":
					break;
				case "ns":
					break;
				case "anp":
					break;
				case "ans":
					break;
				case "ansc":
					break;
				case "ansb":
					break;
				case "anscb":
					break;
				case "AA":
					break;
				case "MM":
					break;
				case "JJ":
					break;
				case "hh":
					break;
				case "mm":
					break;
				case "ss":
					break;
				case "x":
					break;
				case "L":
					break;
				case "LL":
					break;
				case "VAR":
					break;
				default:
					Log.write("[ WRN : 0601 ] this designation is not corret, you have to check your dictionnary or you're file. correction on the type : " + typeRef);
					break;
			}
			return false;
		}
		#endregion
		
		#region Methods private
		private void initcontrols()
		{
			
		}
		
		private void containAlpha()
		{
			
		}
		
		private void containNumeric()
		{
			
		}
		
		private void containSpace()
		{
			
		}
		
		private void containSpecial()
		{
			
		}
		
		private void containControlCarac()
		{
			
		}
		
		private void containBinary()
		{
			
		}
		
		private void containMagCarac()
		{
			
		}
		#endregion
	}
}
