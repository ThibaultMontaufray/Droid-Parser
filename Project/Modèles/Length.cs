/*
 * Created by SharpDevelop.
 * User: Amos
 * Date: 26/12/2012
 * Time: 22:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Tools4Libraries;
using System;
using System.Windows.Forms;

namespace Droid_FlatFile
{
	/// <summary>
	/// Description of Length.
	/// </summary>
	public class Length
	{
		#region Attributes
		private string val;
		private int min;
		private int max;
		private int randomsize;
		#endregion
		
		#region Properties
		public string Val
		{
			get { return val; }
			set 
			{
				val = value;
				setval();
			}
		}
		public int Min
		{
			get { return min; }
		}
		public int Max
		{
			get { return max; }
		}
		public int Randomsize
		{
			get { return randomsize; }
		}
		#endregion
		
		#region Constructor
		public Length()
		{
			val = "";
			min = 0;
			max = 0;
			randomsize = 0;
		}
		
		public Length(string l)
		{
			val = l;
			setval();
		}
		
		public Length(Length l)
		{
			this.max = l.max;
			this.min = l.min;
			this.val = l.val;
			this.randomsize = l.randomsize;
		}
		#endregion
		
		#region Methods private
		private void setval()
		{
			if (string.IsNullOrEmpty(val))
			{
				min = 0;
				max = 0;
				randomsize = 0;
			}
			else
			{
				try 
				{
					string[] parval = val.Split('.');
					if(parval.Length > 1)
					{
							// there is on ".." in our val so
							try 
							{
								min = 0;
								max = int.Parse(parval[2]);
								randomsize = max;	
							} 
							catch (Exception exp1)
							{
								Log.write("[ ERR : 0201 ] Error when parsing val size in parsing f file" + exp1.Message);
							}
					}
					else
					{
						parval = val.Split('-');
						if(parval.Length > 2)
						{
							// there is on "-" in our val so
							try 
							{
								min = int.Parse(parval[0]);
								max = int.Parse(parval[1]);
								randomsize = max - min;	
							} 
							catch (Exception exp2)
							{
								Log.write("[ ERR : 0202 ] Error when parsing val size in parsing f file" + exp2.Message);
							}
						}
						else
						{
							// then we have only one value. 
							try 
							{
								min = int.Parse(val);
								max = min;
								randomsize = 0;
								
							} 
							catch (Exception exp3)
							{
								Log.write("[ ERR : 0203 ] Error when parsing val size in parsing f file" + exp3.Message);
							}
						}
					}
				} 
				catch (Exception exp0) 
				{
					Log.write("[ ERR : 0200 ] Error when parsing val size in parsing f file" + exp0.Message);
				}
			}
		}
		#endregion
	}
}
