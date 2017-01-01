/*
 * Created by SharpDevelop.
 * User: C357555
 * Date: 17/11/2011
 * Time: 10:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Droid_FlatFile
{
	/// <summary>
	/// Description of ColorLangage.
	/// </summary>
	public class ColorLangage
	{
		#region Attributes
		private TextColoration tc;
		private string langage;
		private string text;
		private int firstCar;
		private Regex regKeywords;
		private Match regMatch;

		private Font fontRegular;
		private Font fontBold;
		
		private Color color_default = Color.Black;
		private Color color_comments = Color.Green;
		private Color color_types = Color.Red;
		private Color color_keywords = Color.Blue;
		private Color color_toolswords = Color.Green;
		private Color color_specificswords = Color.DarkCyan;
		private Color color_hardwords = Color.DarkGray;
		private Color color_functions = Color.DarkBlue;
		private Color color_text = Color.DarkViolet;
		#endregion
		
		#region Properties
		#endregion
		
		#region Constructor
		public ColorLangage(string lg)
		{
			langage = lg;
		}
		#endregion
		
		#region Methods
		public void toColour(TextColoration textcoloration)
		{
			tc = textcoloration;
			fontRegular = new Font(tc.Font, FontStyle.Regular);			
			fontBold = new Font(tc.Font, FontStyle.Bold);
			firstCar = tc.SelectionStart;
			text = tc.SelectedText;
			switch (langage.ToLower())
			{
				case "cs":
					to_color_CSharp();
					break;
				case "xml":
					to_color_XML();
					break;
				default:
					to_color_default();
					break;
			}
		}
		
		private void to_color_default()
		{
			
		}
		
		private void to_color_batch()
		{	
			// for tools words
			launchRule(@"(private|public|protected|sub|function|option|explicite)[ |\n]", color_toolswords, fontBold);
					
			// for key words
			launchRule(@"(if|elseif|else|end|next|echo|pause|title|set|goto|exit)[ |\n]", color_keywords, fontBold);
			
			// for specifics words
			launchRule(@"(true|false)[ |\n]", color_specificswords, fontRegular);

			// for types
			launchRule(@"(bool|byte|char|class|decimal|double|float|int|long|object|sbyte|short|string|uint|ulong|ushort|void)[ |\n]", color_types, fontBold);
			
			// for hard words
			launchRule(@"(this|null|on error resume)[ |\n]", color_functions, fontBold);	
		}
		
		private void to_color_CSharp()
		{
			// for tools words
			launchRule(@"(#region|#endregion|using|namespace)[ |\n]", color_toolswords, fontBold);
			
			// for specifics words
			launchRule(@"(true|false)[ |\n]", color_specificswords, fontRegular);
			
			// for types
			launchRule(@"(bool|byte|char|class|decimal|double|float|int|long|object|sbyte|short|string|uint|ulong|ushort|void)[ |\n]", color_types, fontBold);
			
			// for key words
			launchRule(@"(delegate|default|abstract|for|foreach|if|while|do|breack|case|else|enum|event|goto|continue|finalise|interface|virtual|switch|case|struct|private|public|protected)[ |\n]", color_keywords, fontBold);
			
			// for hard words
			launchRule(@"(this|null)[ |\n]", color_functions, fontBold);
			
			// for hard words
			launchRule("([\"].*[\"])", color_text, fontRegular);
			
			// for comments
			launchRule("([^\"]//.*|[^\"]///.*)", color_comments, fontRegular);
			launchRule(@"((/\*)[^\*/]*(\*/|.*))", color_comments, fontRegular);
			launchRule(@"(([^\*/]*(\*/))|([ |\t]\*).*)", color_comments, fontRegular);
			
			// for functions
			launchRule(@"(.+\()[ |\n]", color_functions, fontBold);
		}
		
		private void to_color_XML()
		{						
			// for balises
			launchRule("(<.*[^ ])|([^ ]*/>)", color_keywords, fontRegular);
			
			// for types
			launchRule(@"([^ ]*=)", color_types, fontRegular);
			
			// for text
			launchRule("([\"][^\"^/]*[\"]?)", color_text, fontBold);
			launchRule("([^\"^=^<]*[\"])", color_text, fontBold);
						
			// for tpyes
			launchRule("(=)", color_default, fontRegular);
		}
		
		private void launchRule(string str, Color col, Font font)
		{				
			regKeywords = new Regex(str, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			for (regMatch = regKeywords.Match(text); regMatch.Success; regMatch = regMatch.NextMatch())
			{
				tc.SelectionStart = firstCar + regMatch.Index;
				tc.SelectionLength = regMatch.Length;
				tc.SelectionColor = col;
				tc.SelectionFont = font;
			}
		}
		#endregion
	}
}
