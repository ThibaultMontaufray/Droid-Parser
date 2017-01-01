// log code 09

using System;
using System.IO;
using System.Text;
using System.Linq.Expressions;
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;
using System.Windows.Forms;
using Tools4Libraries;

namespace Droid_FlatFile
{
	public class Parsing
	{
		#region Attributes
		private Dico dico;
		private string path;
		
		private List<Message> listMessageDico;
		private List<Message> listMessageFile;
		private List<string> listItems;
		
		private XmlTextReader reader;
		#endregion
		
		#region Properties
		public Dico Dictionnary
		{
			get { return dico; }
			set { dico = value; }
		}
		public List<string> ListItems
		{
			get { return listItems; }
		}
		#endregion
		
		#region Constructors
		public Parsing(string pathdico)
		{
			path = pathdico;
			listMessageDico = new List<Message>();
			listItems = new List<string>();
		}
		#endregion
		
		#region Methods public global
		public int LoadSpec(string spec)
		{
			Dictionnary = new Dico();
			Dictionnary.Name = spec;
			Dictionnary.PathFile = path + spec + ".xml";
			
			if (File.Exists(Dictionnary.PathFile))
			{
				try
				{
					reader = new XmlTextReader(Dictionnary.PathFile);
					while (reader.Read())
					{
						if(reader.NodeType == XmlNodeType.Element)
						{
							switch (reader.Name) 
							{
								case "description" :
									Dictionnary.AddDescription(readDescription());
									break;
								case "info" :
									Dictionnary.AddInfo(readInfo());
									break;
								case "message" :
									Dictionnary.AddMessage(readMessage());
									break;
								case "field" :
									Dictionnary.AddField(readField());
									break;
								case "type" :
									Dictionnary.AddType(readType());
									break;
								case "codeMsg" :
									Dictionnary.AddCodeMessage(readCodeMsg());
									break;
								case "item" :
									Dictionnary.AddItem(readItem());
									break;
								case "tag" :
									Dictionnary.AddTag(readTag());
									break;
								case "format" :
									Dictionnary.AddFormat(readFormat());
									break;
								default:
									Log.write("[ INF : 0906 ] Unknow node in the dictionnary : " + reader.Name);
									break;
							}
						}
					}
					reader.Close();
					return 0;
				}
				catch (Exception exp)
				{
					Log.write("[ ERR : 0902 ] Error during the loading of the protocol " + Dictionnary.Name + " messages : \n" + exp.Message);
				}
			}
			else
			{
				Log.write("[ WRN : 0903 ] Protocol " + Dictionnary.Name + " not found.");
				return 1;
			}			
			Log.write("[ WRN : 0904 ] Abnormaly end when openning dictionnary from protocol " + Dictionnary.Name + ".");
			return 2;
		}
		
		public string PrintDico()
		{
			string ret = "<html><head></head><body><h2>Cannot do it in this version</h2></body></html>";	
			return ret;
		}
		#endregion
		
		#region Methods public parsing classic
		public string ConvertAsciiToBinary(string data)
		{
			string retData = "";
			byte[] table = Encoding.ASCII.GetBytes(data);
			foreach (byte b in table) 
			{
				retData += Convert.ToString(Convert.ToInt32(int.Parse(b.ToString ()).ToString("X"), 16), 2);;
			}
			if(retData.Length > 1) retData = retData.Substring(0, retData.Length-1);
			return retData;
		}
		
		public string ConvertEbcdicToBinary(string data)
		{
			string retData = "";
			byte[] table = Encoding.GetEncoding("IBM037").GetBytes(data);
			foreach (byte b in table) 
			{
				retData += Convert.ToString(Convert.ToInt32(int.Parse(b.ToString ()).ToString("X"), 16), 2);;
			}
			if(retData.Length > 1) retData = retData.Substring(0, retData.Length-1);
			return retData;
		}
		
		public string ConvertBinaryToAscii(string data)
		{
			Log.write("[ DEB : 0910 ] convert binary to ascii non implemented");
			return data;
		}
		
		public string ConvertBinaryToEbcdic(string data)
		{
			Log.write("[ DEB : 0911 ] convert binary to ebcdic non implemented");
			return data;
		}
		
		public string ConvertHexaToAscii(string data)
		{
			try {
				string retData = "";
				string[] parts = data.Split('-');
				int x;
				
				for (int i=0 ; i<parts.Length-1; i++)
				{
					x = Convert.ToInt32(parts[i], 16);
					retData += (char)x;
				}
				return retData;	
			} catch (Exception exp0909) {
				Log.write("[ ERR : 0909 ] Error when convert hexa to ascii. \n" + exp0909.Message);
				return data;
			}
		}
		
		public string ConvertHexaToEbcdic(string data)
		{
			string retData = "";
			byte[] table = Encoding.GetEncoding("IBM037").GetBytes(data);
			retData = Encoding.GetEncoding("IBM037").GetString(table);
			return retData;
		}
		
		public string ConvertAsciiToHexa(string data)
		{
			string retData = "";
			byte[] table = Encoding.ASCII.GetBytes(data);
			foreach (byte b in table) 
			{
				retData += int.Parse(b.ToString()).ToString("X") + "-";
			}
			if(retData.Length > 1) retData = retData.Substring(0, retData.Length-1);
			return retData;
		}
		
		public string ConvertEbcdicToHexa(string data)
		{
			string retData = "";
			byte[] table = Encoding.GetEncoding("IBM037").GetBytes(data);
			foreach (byte b in table) 
			{
				retData += int.Parse(b.ToString()).ToString("X") + "-";
			}
			if(retData.Length > 1) retData = retData.Substring(0, retData.Length-1);
			return retData;
		}
		
		public string ConvertAsciiToEbcdic(string strASCIIString)
		{     
			int[] a2e = new int[256]{ 0, 1, 2, 3, 55, 45, 46, 47, 22, 5, 37, 11, 12, 13, 14, 15, 16, 17, 18, 19, 60, 61, 50, 38, 24, 25, 63, 39, 28, 29, 30, 31, 64, 79,127,123, 91,108, 80,125, 77, 93, 92, 78,107, 96, 75, 97, 240,241,242,243,244,245,246,247,248,249,122, 94, 76,126,110,111, 124,193,194,195,196,197,198,199,200,201,209,210,211,212,213,214, 215,216,217,226,227,228,229,230,231,232,233, 74,224, 90, 95,109, 121,129,130,131,132,133,134,135,136,137,145,146,147,148,149,150, 151,152,153,162,163,164,165,166,167,168,169,192,106,208,161, 7, 32, 33, 34, 35, 36, 21, 6, 23, 40, 41, 42, 43, 44, 9, 10, 27, 48, 49, 26, 51, 52, 53, 54, 8, 56, 57, 58, 59, 4, 20, 62,225, 65, 66, 67, 68, 69, 70, 71, 72, 73, 81, 82, 83, 84, 85, 86, 87, 88, 89, 98, 99,100,101,102,103,104,105,112,113,114,115,116,117, 118,119,120,128,138,139,140,141,142,143,144,154,155,156,157,158, 159,160,170,171,172,173,174,175,176,177,178,179,180,181,182,183, 184,185,186,187,188,189,190,191,202,203,204,205,206,207,218,219, 220,221,222,223,234,235,236,237,238,239,250,251,252,253,254,255 };
			char chrItem = Convert.ToChar("0"); 
			StringBuilder sb = new StringBuilder(); 
			for (int i = 0; i < strASCIIString.Length; i++) 
			{ 
				try 
				{ 
					chrItem = Convert.ToChar(strASCIIString.Substring(i, 1)); 
					sb.Append(Convert.ToChar(a2e[(int)chrItem])); 
				} 
				catch (Exception exp0910) 
				{ 
					Log.write("[ ERR : 0910 ] Error when convert ascii to ebcdic. \n" + exp0910.Message);
					return strASCIIString;
				} 
			} 
			string result = sb.ToString(); 
			sb = null; 
			return result;
		}     
		
		public string ConvertEbcdicToAscii(string strEBCDICString)
		{         
			int[] e2a = new int[256]{0, 1, 2, 3,156, 9,134,127,151,141,142, 11, 12, 13, 14, 15,	16, 17, 18, 19,157,133, 8,135, 24, 25,146,143, 28, 29, 30, 31, 128,129,130,131,132, 10, 23, 27,136,137,138,139,140, 5, 6, 7, 144,145, 22,147,148,149,150, 4,152,153,154,155, 20, 21,158, 26, 32,160,161,162,163,164,165,166,167,168, 91, 46, 60, 40, 43, 33, 38,169,170,171,172,173,174,175,176,177, 93, 36, 42, 41, 59, 94, 45, 47,178,179,180,181,182,183,184,185,124, 44, 37, 95, 62, 63, 186,187,188,189,190,191,192,193,194, 96, 58, 35, 64, 39, 61, 34, 195, 97, 98, 99,100,101,102,103,104,105,196,197,198,199,200,201, 202,106,107,108,109,110,111,112,113,114,203,204,205,206,207,208, 209,126,115,116,117,118,119,120,121,122,210,211,212,213,214,215, 216,217,218,219,220,221,222,223,224,225,226,227,228,229,230,231, 123, 65, 66, 67, 68, 69, 70, 71, 72, 73,232,233,234,235,236,237, 125, 74, 75, 76, 77, 78, 79, 80, 81, 82,238,239,240,241,242,243, 92,159, 83, 84, 85, 86, 87, 88, 89, 90,244,245,246,247,248,249, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57,250,251,252,253,254,255};
			
			char chrItem = Convert.ToChar("0");
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < strEBCDICString.Length; i++)
			{
				try
				{
//					chrItem = Convert.ToChar(strEBCDICString.Substring(i, 1));
//					int test = (int)chrItem;
//					sb.Append(Convert.ToChar(e2a[(int)chrItem]));
					
					chrItem = Convert.ToChar(int.Parse( ConvertEbcdicToHexa(strEBCDICString.Substring(i, 1)), System.Globalization.NumberStyles.HexNumber ));
					sb.Append(Convert.ToChar(e2a[(int)chrItem]));
				}
				catch (Exception ex0911)
				{
					Log.write("[ ERR : 0911 ] Error when convert ebcdic to ascii. \n" + ex0911.Message);
					return strEBCDICString;
				}
			}
			string result = sb.ToString();
			sb = null;
			return result;
		}
		#endregion
		
		#region Methods public parsing flat files
		// TODO : tej cette 'tin de fonction !!!
		public List<Message> Parse_LP01(string txt)
		{
			try
			{
				bool flag;
				int deepindex = 0;
				string[] tabTxt = txt.Split('\n');
				listMessageFile = new List<Message>();
				
				foreach (string sourceMessageText in tabTxt)
				{
					Console.WriteLine(sourceMessageText);
					if (sourceMessageText.Length > 2)
					{
						foreach (Message msg in listMessageDico)
						{
							flag = false;
							if (((Field)msg.ListField[0]).Description.Equals(sourceMessageText.Substring(0, 2)))
							{
								flag = true;
							}
							else if (msg.ListField[0].ListTag != null)
							{
								foreach (Tag tag in msg.ListField[0].ListTag)
								{
									if (tag.TagValue.Equals(sourceMessageText.Substring(0, 2)))
									{
										flag = true;
										break;
									}
								}
							}
							if (flag)
							{
								bool correctMsg = false;
								int rank1 = 0;
								int rank2 = 0;
								foreach (Message msgbis in listMessageDico)
								{
									if ((((Field)msgbis.ListField[0]).Description.Equals(sourceMessageText.Substring(0, 2))))
									{
										rank1++;
										try {
											if (((Field)msgbis.ListField[5]).Description.Equals(sourceMessageText.Substring(21, 2)))
											{
												rank2 ++;
											}
										} catch (Exception exp0908)
										{
											// append when the length is too short : it depend of the message 
											// TODO : could add a managment of this kind of situation.
											Log.write("[ WRN : 0908 ] Warning on the managing the message :" + exp0908.Message);
										}
									}
									else if (msgbis.ListField[0].ListTag != null)
									{
										foreach (Tag tag in msg.ListField[0].ListTag)
										{
											if (tag.TagValue.Equals(sourceMessageText.Substring(0, 2)))
											{
												correctMsg = true;
												break;
											}
										}
									}
								}
								bool crit1 = false;
								bool crit2 = false;
								bool crit3 = false;
								
								try {
									crit1 = (((Field)msg.ListField[0]).Description.Equals(sourceMessageText.Substring(0, 2)));
									crit2 = (((Field)msg.ListField[5]).Description.Equals(sourceMessageText.Substring(21, 2)));
									crit3 = (((Field)msg.ListField[6]).Description.Equals(sourceMessageText.Substring(23, 2)));
								} 
								catch (Exception exp0907)
								{
									// append when the length is too short : it depend of the message 
									// TODO : could add a managment of this kind of situation.
									Log.write("[ WRN : 0907 ] Warning on the managing the message :" + exp0907.Message);
								}
								
								if (rank1 == 1)
								{
									if (crit1)
										correctMsg = true;
								}
								else if(rank1 > 1 && rank2 == 1)
								{
									if (crit1 && crit2)
										correctMsg = true;
								}
								else if(rank1 > 1 && rank2 > 1)
								{
									if (crit1 && crit2 && crit3)
										correctMsg = true;
								}
								
								Message m = new Message();
								m.Description = msg.Description;
								m.Label = msg.Label;
								m.Length = msg.Length;
								m.MessageValue = sourceMessageText;
								m.Deeping = msg.Deeping;
								//							if (sourceMessageText.Length == msg.Length) m.Succeed = correctMsg;
								//							else m.Succeed = false;
								
								checkdeeping(msg.Deeping, deepindex);
								deepindex = msg.Deeping;
								
								m.Succeed = correctMsg;
								if (correctMsg)
								{
									// header message
									int index = 0;
									foreach(Field f in msg.ListField)
									{
										string temp = sourceMessageText;
										string s = "";
										if ((temp.Length - index - 1) > 0)
										{
											if ((index + f.Length) < temp.Length) s = temp.Substring(index, f.Length);
											else if(index <= temp.Length) s = temp.Substring(index, temp.Length - index - 1);
											else s = "... ERROR";
											index += f.Length;
											Field newfield = new Field(f);
											newfield.FieldValue = s;
											newfield.Length = newfield.FieldValue.Length;
											m.ListField.Add(newfield);
											if ((newfield.Length < newfield.LengthDico.Min) || (newfield.Length > newfield.LengthDico.Max)) m.Succeed = false;
										}
									}
									if (m.ListField.Count > 0) m.Label += " - " + m.ListField[1].FieldValue;
									listMessageFile.Add(m);
									break;
								}
							}
						}
					}
				}
				return listMessageFile;
			} catch (Exception exp0903) {
				// TODO : manage this case
				Log.write("[ ERR : 0903 ] error during the message parsing : \n\n" + exp0903.Message);
				return null;
			}
		}
		
		public List<Message> Parse(string txt)
		{
			try
			{
				string[] tabTxt = txt.Split('\n');
				string line;
				listMessageFile = new List<Message>();
				
				foreach (string sourceMessageText in tabTxt)
				{
					if (sourceMessageText.Length > 2)
					{
						line = ConvertAsciiToString(sourceMessageText);
					}
					else
					{
						Log.write("[ ERR : 0904 ] error during the message parsing : \n\n");
					}
				}
			} 
			catch (Exception exp0903)
			{
				// TODO : manage this case
				Log.write("[ ERR : 0903 ] error during the message parsing : \n\n" + exp0903.Message);
				return null;
			}
			return null;
		}
		#endregion
		
		#region Methods private dictionnary for flat files
		private Message readMessage()
		{
			Message msg = new Message();
			if (msg.Label != null)
			{
				listMessageDico.Add(msg);
				msg = null;
			}
			msg = new Message();
			try {
				msg.Label = reader.GetAttribute("label");
				msg.ID =  reader.GetAttribute("id");
				msg.Description = reader.GetAttribute("desc");
				msg.Length = int.Parse(reader.GetAttribute("length"));
				msg.Deeping = int.Parse(reader.GetAttribute("deeping"));
			} 
			catch (Exception exp900)
			{
				Log.write("[ ERR : 0900 ] error when loading dictionnary, format not conform : " + exp900.Message);
				throw;
			}
			return msg;
		}
			
		private Field readField()
		{
			Field field = new Field();
			try
			{
				field = new Field();
				field.Code = reader.GetAttribute("id");
				field.Label = reader.GetAttribute("label");
				field.Type = reader.GetAttribute("org");
				
				Format f = new Format(reader.GetAttribute("format"), Dictionnary.ListFormat);
				field.Format = f;
				
				Length l = new Length(reader.GetAttribute("length"));
				field.LengthDico = l;
				
				return field;
			}
			catch (Exception exp)
			{
				Console.WriteLine("[ ERR : 0901 ]Erreur lors du chargement des champs du protocol." + exp.Message);
			}
			return null;
		}
		
		private Tag readTag()
		{
			Tag tag = new Tag();
			tag.TagValue = reader.GetAttribute("val");
			tag.Description = reader.GetAttribute("description");
			tag.Label = reader.GetAttribute("label");
			return tag;
		}
		
		private Type readType()
		{
			Type type = new Type();
			type.Label = reader.GetAttribute("label");
			Format f = new Format(reader.GetAttribute("format"));
			type.Format = f;
			Length l = new Length(reader.GetAttribute("length"));
			type.Length = l;
			return type;
		}
		
		private Format readFormat()
		{
			Format format = new Format(reader.GetAttribute("notation"));
			format.Notation = reader.GetAttribute("notation");
			format.Description = reader.GetAttribute("description");
			format.ListVal = reader.GetAttribute("listval");
			return format;
		}
		
		private CodeMessage readCodeMsg()
		{
			CodeMessage codeMessage = new CodeMessage();
			codeMessage.ID = reader.GetAttribute("id");
			codeMessage.Position = reader.GetAttribute("pos");
			codeMessage.Description = reader.GetAttribute("desc");
			return codeMessage;
		}
		
		private Info readInfo()
		{
			Info info = new Info();
			info.Name = reader.GetAttribute("value");
			return info;
		}
		
		private Item readItem()
		{
			Item item = new Item();
			item.Description = reader.GetAttribute("description");
			item.Val = reader.GetAttribute("value");
			return item;
		}
		
		private string readDescription()
		{
			return reader.GetAttribute("value");
		}
		#endregion
		
		#region Methods private parsing
		private string ConvertEbcdicToString(string ebcdicData)
		{
			string data = "";
			byte[] table = Encoding.GetEncoding("IBM037").GetBytes(ebcdicData);
			foreach (byte b in table) 
			{
				data += int.Parse(b.ToString()).ToString("X");
			}
			return data;
		}
				
		private string ConvertAsciiToString(string asciiData)
		{
			string data = "";
			byte[] table = Encoding.ASCII.GetBytes(asciiData);
			foreach (byte b in table) 
			{
				data += int.Parse(b.ToString()).ToString("X");
			}
			return data;
		}
		
		private int checkdeeping(int msgdeeping, int relativedeeping)
		{
			if (msgdeeping != relativedeeping)
			{
				if ((relativedeeping < msgdeeping) && (relativedeeping != msgdeeping - 1))
				{
					relativedeeping++;
					addmissingmessage(relativedeeping);
				}
				else if ((relativedeeping > msgdeeping) && (relativedeeping != msgdeeping + 1))
				{
					relativedeeping--;
					addmissingmessage(relativedeeping);
				}
				if ((relativedeeping != msgdeeping + 1) && (relativedeeping != msgdeeping - 1))
				{
					relativedeeping = checkdeeping(msgdeeping, relativedeeping);
				}
			}
			return relativedeeping;
		}
		
		private void addmissingmessage(int deep)
		{
			Message m = new Message();
			m.Deeping = deep;
			m.Description = "Missing message";
			m.Label = "Missing message";
			m.MessageValue = "Missing message";
			listMessageFile.Add(m);
		}
		#endregion
	}
}
