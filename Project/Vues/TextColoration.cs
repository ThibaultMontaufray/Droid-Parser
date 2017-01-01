// Log code 16

using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Tools4Libraries;

namespace Droid_FlatFile
{
	#region class event
	public class LineChangedEventArgs : EventArgs
	{
		public int NewLineNr;
		public LineChangedEventArgs(int newLineNr)
		{
			NewLineNr = newLineNr;
		}
	}
	#endregion
	
	/// <summary>
	/// Description of TextColoration.
	/// </summary>
	public class TextColoration : System.Windows.Forms.RichTextBox
	{
		#region Attributes
		public delegate void LineChangedEventHandler(object sender, LineChangedEventArgs e);
		public event LineChangedEventHandler LineChanged;
		
		private ColorLangage colorlangage;
		private bool enableHighlightCurrentLine;
		private int OldLineIndex;
		private int OldLineSelStart;
		private int OldLineSelLength;
		
		private string langage;
		#endregion
		
		#region Properties
		public bool EnableHighlightCurrentLine
		{
			get { return enableHighlightCurrentLine; }
			set { enableHighlightCurrentLine = value; }
		}
		#endregion
		
		#region Constructors
		public TextColoration(string lg)
		{
			this.langage = lg;
			this.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
			this.RightMargin = 1000000000;
			
			EnableHighlightCurrentLine = true;
			SelectionChanged += new EventHandler(TextColoration_SelectionChanged);
			MouseDown += new MouseEventHandler(TextColoration_MouseDown);
			MouseUp += new MouseEventHandler(TextColoration_MouseUp);
			TextChanged += new EventHandler(TextColoration_TextChanged);
			LineChanged += new LineChangedEventHandler(TextColoration_LineChanged);
			//KeyUp += new KeyEventHandler(TextColoration_KeyUp);
		}
		#endregion
		
		# region Methods Private
		private void TextColoration_TextChanged(object sender, EventArgs e)
		{
			int NoLine;
			int FirstCar;
			
			FirstCar = GetFirstCharIndexOfCurrentLine();
			NoLine = GetLineFromCharIndex(FirstCar);
			LineChanged(this, new LineChangedEventArgs(NoLine));			
		}
		
		private void TextColoration_KeyUp(object sender, EventArgs e)
		{
			TextColoration_SelectionChanged(sender, e);
		}
		
		private void TextColoration_LineChanged(object sender, LineChangedEventArgs e)
		{
			//EnableHighlightCurrentLine = true;
			HighlightCurrentLine(e.NewLineNr);
		}
		
		private void TextColoration_MouseUp(object sender, MouseEventArgs e)
		{
			//Desactive le surlignage de la ligne en cours (qui supprime la sélection après surlignage)
			//pour permettre la sélection de plusieurs lignes
			EnableHighlightCurrentLine = true;
		}
		
		private void TextColoration_MouseDown(object sender, MouseEventArgs e)
		{
			//Réactive le surlignage de la ligne en cours (desactivé dans MouseDown)
			EnableHighlightCurrentLine = false;
		}
		
		private void TextColoration_SelectionChanged(object sender, EventArgs e)
		{
			int NoLine;
			int FirstCar;
			
			FirstCar = GetFirstCharIndexOfCurrentLine();
			NoLine = GetLineFromCharIndex(FirstCar);
			if ((NoLine != OldLineIndex) && (LineChanged != null))
			{
				//LineChanged(this, new LineChangedEventArgs(NoLine));
			}
			
			//Doit rester en fin de fonction
			OldLineIndex = NoLine;
		}
		
		public void HighlightCurrentLine(int NewLineNr)
		{
			try{
				int OldSelStart;
				int FirstCar;
				int LastCar;
				
//				FirstCar = GetFirstCharIndexOfCurrentLine();
//				LastCar = GetFirstCharIndexFromLine(NewLineNr);
				LastCar = GetFirstCharIndexFromLine(NewLineNr + 1);
				FirstCar = 0;
							
				if (EnableHighlightCurrentLine && (LastCar >= FirstCar))
				{
					try {
							
						EnableHighlightCurrentLine = false; //pour éviter une récursivité dans TRTBPlus_SelectionChanged
						OldSelStart = SelectionStart;
						
						// clean the global coloration
		//				SelectionStart = 0;
		//				SelectionLength = Text.Length;
		//				SelectionBackColor = Color.WhiteSmoke;
		
						// color the selected line
						SelectionStart = FirstCar;
						OldLineSelStart = SelectionStart;
						//SelectionLength = LastCar - FirstCar;
						SelectionLength = Text.Length;
						OldLineSelLength = SelectionLength;
						// start coloration of the word we need
						colorlangage = new ColorLangage(langage);
						colorlangage.toColour(this);
						
						//SelectionBackColor = Color.FromKnownColor(KnownColor.ActiveBorder);
						SelectionLength = 0;
						SelectionStart = OldSelStart; //remise du curseur à son ancienne position
						SelectionColor = Color.Black;
						SelectionFont = DefaultFont;
						EnableHighlightCurrentLine = true;
						
					} 
					catch (Exception exp1600) 
					{
						Log.write("[ WRN : 1600 ] Error in coloration.\n" + exp1600.Message);
						return;
					}
				}
				
			} 
			catch (Exception exp1601)
			{
				Log.write("[ WRN : 1601 ] Error in coloration.\n" + exp1601.Message);
				return;
			}
		}
		# endregion
	}
}
