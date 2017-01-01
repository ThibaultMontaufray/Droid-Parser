using System.Linq.Expressions;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using FastColoredTextBoxNS;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using Tools4Libraries;

namespace Droid_FlatFile
{
    /// <summary>
    /// Interface for Tobi Assistant application : take care, some french word here to allow Tobi to speak with natural langage.
    /// </summary>            
	public class Interface_parser : GPInterface
	{
		#region Attributes
		private GUI _gui;
		private string _action;
		private string _currentData;
		private ToolStripMenuFP _tsm;
		private Parsing _parser;
		private FastColoredTextBox _textBox;
		private DataGridView _datagridview;
		private TreeView _treeview;
		private List<Message> _listmessages;
		private Message _thismessage;
        private List<String> _listToolStrip;
        private SplitContainer _sp_Table;
        private SplitContainer _sp_document_map;
		private SplitContainer _sptree;
		private List<string> _listvisiblecomponents;
		private Stream _stream;
		private bool _openned;
		private Panel _panelTreeViewTop;
		private Panel _panelTreeViewBottom;
		private DataGridView _treeDGV;
		private TextBox _tbvaluehover;
		private Label _lblType;
		private Label _lblLengthDico;
		private Label _lblLengthValue;
		private Label _lblTraductor;
		private bool _textbuild;
		private bool _tablebuild;
		private bool _treebuild;
		private bool _textshown;
		private bool _tableshown;
		private bool _treeshown;
		private ColorLangage _colorlanguage;
		private string _path;
		private PanelHexa _panel_hexa;

        private DocumentMap _documentMap1;

        private const int maxBracketSearchIterations = 2000;
        private TextStyle _BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        private TextStyle _BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        private TextStyle _GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        private TextStyle _MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        private TextStyle _GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        private TextStyle _BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        private TextStyle _MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        private MarkerStyle _SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));
        private Random _rnd = new Random();
		#endregion
		
		#region Properties
		public string CurrentData
		{
			get { return _currentData; }
			set { _currentData = value; }
		}
		
		public RibbonTab Tsm
		{
			get { return _tsm; }
			set { _tsm = value as ToolStripMenuFP; }
		}
		
		public override bool Openned
		{
			get { return _openned; }
		}
		
		public override List<string> Listvisiblecomponents
		{
			get { return _listvisiblecomponents; }
		}
		
		public FastColoredTextBox SheetTextBox
		{
			get { return _textBox; }
			set { _textBox = value; }
		}
		
		public DataGridView Datagridview
		{
			get { return _datagridview; }
			set { _datagridview = value; }
		}
		
		public List<Message> Listmessages
		{
			get { return _listmessages; }
			set { _listmessages = value; }
		}
		#endregion
		
		#region Constructor
		public Interface_parser(List<String> lts, string pathfp)
		{
			_path = pathfp;
			_gui = new GUI();
            _listvisiblecomponents = new List<string>() { "dico", "crypto", "SwitchView", "text", "convert" };
            _listToolStrip = lts;
			_openned = false;
			_colorlanguage = new ColorLangage(this.Extention);
		}
		#endregion

        #region Methods Public
        public override bool Open(object o)
        {
            _stream = (Stream) o;
            //return LaunchOpenBigFileReadOnly();
            return LaunchOpenFile();
        }
		public override void Print()
		{
			AddTextBox();
		}
		public override bool Save()
		{
			return false;
		}
		public override void Close()
		{
			try
			{
				_stream.Close();
			}
			catch
			{
				
			}
		}
		public override void ZoomIn()
		{
			float value = 0;
			if (_textBox != null) value = _textBox.Font.Size+1;
			else if (_datagridview != null) value = _datagridview.Font.Size+1;
			if (value > 1)
			{
				if (_textBox != null) _textBox.Font  = new Font("Courier New", value, FontStyle.Regular, GraphicsUnit.Point);
				//if (reference.Datagridview != null) s.Datagridview.Font  = new Font("Courier New", value, FontStyle.Regular, GraphicsUnit.Point);
			}
		}
		public override void ZoomOut()
		{
			float value = 0;
			if (_textBox != null) value = _textBox.Font.Size-1;
			else if (_datagridview != null) value = _datagridview.Font.Size-1;
			if (value > 1)
			{
				if (_textBox != null) _textBox.Font  = new Font("Courier New", value, FontStyle.Regular, GraphicsUnit.Point);
				//if (reference.Datagridview != null) s.Datagridview.Font  = new Font("Courier New", value, FontStyle.Regular, GraphicsUnit.Point);
			}
		}
		public override void Copy()
		{
			_textBox.Copy();
		}
		public override void Cut()
		{
			_textBox.Cut();
		}
		public override void Paste()
		{
			_textBox.Paste();
		}
		public override void Resize()
		{
			if (_sp_Table != null)
            {
                _sp_Table.Size = new System.Drawing.Size(_tsm.CurrentTabPage.Parent.Width - 14, _tsm.CurrentTabPage.Parent.Height - 32);
                _sp_document_map.Size = new System.Drawing.Size(_tsm.CurrentTabPage.Parent.Width - 14, _tsm.CurrentTabPage.Parent.Height - 32);
//				if (_textBox != null) _textBox.Size = new System.Drawing.Size(_sp_Table.Panel1.Width-2, _sp_Table.Height-_sp_Table.SplitterDistance-5);
//				if (_datagridview != null) _datagridview.Size = new System.Drawing.Size(_sp_Table.Panel2.Width, _sp_Table.SplitterDistance);
			}
			if (_panel_hexa != null)
			{
				_panel_hexa.Refresh();
			}
			else
			{
				if (_textBox != null) _textBox.Size = new System.Drawing.Size(_tsm.CurrentTabPage.Parent.Width - 10, _tsm.CurrentTabPage.Parent.Height - 45);
				if (_datagridview != null) _datagridview.Size = new System.Drawing.Size(_tsm.CurrentTabPage.Parent.Width - 10, _tsm.CurrentTabPage.Parent.Height - 45);
			}
		}
		public override void GlobalAction(object sender, EventArgs e)
		{
			ToolBarEventArgs tbea = e as ToolBarEventArgs;
			_action = tbea.EventText;
			switch (_action.TrimEnd().ToLower())
            {
                case "documentmap":
                    LaunchDocumentMap();
                    break;
                case "highlight":
                    break;
                case "foldinglines":
                    break;
                case "chardisplay":
                    break;
                case "marklast":
                    break;
                case "marknext":
                    break;
				case "text":
					LaunchText();
					break;
				case "hexa to ascii":
					_action = "ascii";
					LaunchHexaToAscii();
					break;
				case "hexa to ebcdic":
					_action = "ebcdic";
					LaunchHexaToEbcdic();
					break;
				case "ascii to hexa":
					_action = "ascii";
					LaunchAsciiToHexa();
					break;
				case "ebcdic to hexa":
					_action = "ebcdic";
					LaunchEbcdicToHexa();
					break;
				case "ascii to ebcdic":
					LaunchAsciiToEbcdic();
					break;
				case "ebcdic to ascii":
					LaunchEbcdicToAscii();
					break;
				case "ascii to binary":
					LaunchAsciiToBinary();
					break;
				case "ebcdic to binary":
					LaunchEbcdicToBinary();
					break;
				case "lp01 43.1":
					LaunchLP01v431();
					break;
				case "colorationcs":
					LaunchColorationCS();
					break;
				case "switchview":
					LaunchSwitchView();
					break;
				case "crypto":
					LaunchCrypto();
					break;
                case "cb2a 1.3.1":
                    LaunchCB2Av131();
                    break;
                case "cbae 4.05":
					// TODO : add this protocol
					Log.write("Dictionnaire CBAE 4.05 non disponible.");
					break;
				case "cb2a 1.2.0":
					// TODO : add this protocol
					Log.write("Dictionnaire CB2A 1.2.0 non disponible.");
					break;
				case "cb2a 1.2.1":
					// TODO : add this protocol
					Log.write("Dictionnaire CB2A 1.2.1 non disponible.");
					break;
				case "cb2a 1.3.0":
					// TODO : add this protocol
					Log.write("Dictionnaire CB2A 1.3.0 non disponible.");
					break;
                case "replace":
                    LauncherReplace();
                    break;
                case "collapseselection":
                    LauncherCollapseSelectedBlock();
                    break;
                case "collapseall":
                    LauncherCollapseAllregion();
                    break;
                case "expandall":
                    LauncherExpandAllregion();
                    break;
                case "increaseindentation":
                    LauncherIncreaseIndentSiftTab();
                    break;
                case "decreaseindentation":
                    LauncherDecreaseIndentTab();
                    break;
                case "autoindent":
                    LauncherAutoIndent();
                    break;
                case "leftbacket":
                    LauncherGoLeftBracket();
                    break;
                case "rightbacket":
                    LauncherGoRightBracket();
                    break;
                case "print":
                    LauncherPrint();
                    break;
                case "changecolor":
                    LauncherChangeColors();
                    break;
                case "setreadonly":
                    LauncherSetSelectedAsReadonly();
                    break;
                case "setwritable":
                    LauncherSetSelectedAsWritable();
                    break;
                case "startstopmacro":
                    LauncherStartStopMacroRecording();
                    break;
                case "runmacro":
                    LauncherExecuteMacro();
                    break;
                case "hotkey":
                    LauncherChangeHotkeys();
                    break;
                case "comment":
                    LauncherCommentSelectedLines();
                    break;
                case "uncomment":
                    LauncherUncommentSelectedLines();
                    break;
			}
		}
		public System.Windows.Forms.RibbonTab BuildToolBar()
		{
			ToolStripMenuFP ts = new ToolStripMenuFP(_listToolStrip, _path);
			return ts;
		}
		#endregion
		
		#region Methods Private Launchers
        private bool LaunchOpenBigFileReadOnly()
        {
            try
            {
                _textbuild = false;
                _tablebuild = false;
                _treebuild = false;
                _textshown = false;
                _tableshown = false;
                _treeshown = false;

                this.AddTextBox();

                int index = 0;
                if (_stream != null)
                {
                    StringBuilder sb = new StringBuilder();
                    using (StreamReader monStreamReader = new StreamReader(_stream))
                    {
                        string ligne = monStreamReader.ReadLine();

                        while (ligne != null)
                        {
                            sb.AppendLine(ligne);

                            ligne = monStreamReader.ReadLine();

                            //this._textBox.AppendText(ligne + "\r\n");
                            ////this._textBox.HighlightCurrentLine(index);
                            index++;
                        }
                        this._textBox.WordWrap = false;
                        monStreamReader.Close();
                    }

                    var ts = new StringTextSource(_textBox);
                    ts.OpenString(sb.ToString());
                    _textBox.TextSource = ts;
                }
            }
            catch (Exception exp1700)
            {
                Log.write("[ ERR : 1700 ] " + exp1700.Message);
                _tsm.CurrentTabPage.Dispose();
                _openned = false;
                return false;
            }
            _openned = true;
            return true;
        }
        private bool LaunchOpenFile()
        {
            try
            {
                _textbuild = false;
                _tablebuild = false;
                _treebuild = false;
                _textshown = false;
                _tableshown = false;
                _treeshown = false;

                this.AddTextBox();

                int index = 0;
                if (_stream != null)
                {
                    StringBuilder sb = new StringBuilder();
                    using (StreamReader monStreamReader = new StreamReader(_stream))
                    {
                        string ligne = monStreamReader.ReadLine();

                        while (ligne != null)
                        {
                            sb.AppendLine(ligne);
                            ligne = monStreamReader.ReadLine();
                            index++;
                        }
                        this._textBox.WordWrap = false;
                        monStreamReader.Close();
                    }
                    _textBox.Language = FastColoredTextBoxNS.Language.CSharp;
                    _textBox.Text = sb.ToString();
                }
            }
            catch (Exception exp1700)
            {
                Log.write("[ ERR : 1700 ] " + exp1700.Message);
                _tsm.CurrentTabPage.Dispose();
                _openned = false;
                return false;
            }
            _openned = true;
            return true;
        }
		private void LaunchText()
        {
            if (_sp_Table != null && _textbuild)
            {
                _textshown = !_textshown;
                _textBox.Visible = _textshown;

                if (_textshown)
                {
                    _sp_Table.Panel1.Height = _sp_Table.Height * (2 / 3);
                }
                else
                {
                    _sp_Table.Panel1.Height = _sp_Table.Height;
                }
            }
            if (_sp_document_map != null && _textbuild)
            {
                _textshown = !_textshown;
                _textBox.Visible = _textshown;

                if (_textshown)
                {
                    _sp_document_map.Panel1.Height = _sp_document_map.Height * (2 / 3);
                }
                else
                {
                    _sp_document_map.Panel1.Height = _sp_document_map.Height;
                }
            }
		}
		private void LaunchEbcdicToBinary()
		{
			_parser = new Parsing(_path);
			_textBox.Text = _parser.ConvertEbcdicToBinary(_textBox.Text);
		}
		private void LaunchAsciiToBinary()
		{
			_parser = new Parsing(_path);
			_textBox.Text = _parser.ConvertAsciiToBinary(_textBox.Text);
		}
		private void LaunchHexaToAscii()
		{
			_parser = new Parsing(_path);
			_textBox.Text = _parser.ConvertHexaToAscii(_textBox.Text);
		}
		private void LaunchHexaToEbcdic()
		{
			_parser = new Parsing(_path);
			_textBox.Text = _parser.ConvertHexaToEbcdic(_textBox.Text);
		}
		private void LaunchAsciiToHexa()
		{
			_parser = new Parsing(_path);
			_textBox.Text = _parser.ConvertAsciiToHexa(_textBox.Text);
			_currentData = _textBox.Text;
			BuildInterfaceHexa();
		}
		private void LaunchEbcdicToHexa()
		{
			_parser = new Parsing(_path);
			_textBox.Text = _parser.ConvertEbcdicToHexa(_textBox.Text);
			_currentData = _textBox.Text;
			BuildInterfaceHexa();
		}
		private void LaunchAsciiToEbcdic()
		{
			_parser = new Parsing(_path);
			_textBox.Text = _parser.ConvertAsciiToEbcdic(_textBox.Text);
		}
		private void LaunchEbcdicToAscii()
		{
			_parser = new Parsing(_path);
			_textBox.Text = _parser.ConvertEbcdicToAscii(_textBox.Text);
		}
		private void LaunchCB2Av131()
		{
			_parser = new Parsing(_path);
			_parser.LoadSpec("CB2A 1.3.1");
			
			AddTree();
			AddTable();
			Listmessages = _parser.Parse(_textBox.Text);
			RefreshTree();
			RefreshTable();
		}
		private void LaunchLP01v431()
		{
			_parser = new Parsing(_path);
			_parser.LoadSpec("LP01 43.1");
			
			AddTree();
			AddTable();
			Listmessages = _parser.Parse(_textBox.Text);
			RefreshTree();
			RefreshTable();
		}
		private void LaunchColorationCS()
		{
			// TODO : coloration for this codeLanguage
		}
		private void LaunchCrypto()
		{
            Droid_cryptographie.CryptoGUI cryptoInterface = new Droid_cryptographie.CryptoGUI(_textBox.Text);
            _textBox.Text = cryptoInterface.TextValue;
            cryptoInterface.Dispose();
		}
		private void LaunchSwitchView()
		{
			if(_tablebuild && _treebuild)
			{
				_treeshown = !_treeshown;
				_tableshown = !_tableshown;
			}
			else
			{
				AddTable();
				AddTree();
			}
			
			if (_treeshown)
			{
				_sp_Table.Panel1.Controls.Remove(_datagridview);
				_sp_Table.Panel1.Controls.Add(_sptree);
			}
			else
			{
				_sp_Table.Panel1.Controls.Remove(_sptree);
				_sp_Table.Panel1.Controls.Add(_datagridview);
			}
		}
        private void LaunchDocumentMap()
        {
            _tsm.CurrentTabPage.SuspendLayout();
            if (_sp_document_map.Visible)
            {
                _sp_document_map.Visible = false;
                _tsm.CurrentTabPage.Controls.Remove(_sp_document_map);
                _sp_document_map.Panel1.Controls.Remove(_textBox);
                _tsm.CurrentTabPage.Controls.Add(_textBox);
                _textBox.Width = _tsm.CurrentTabPage.Width;
            }
            else
            {
                _sp_document_map.Visible = true;
                _tsm.CurrentTabPage.Controls.Remove(_textBox);
                _sp_document_map.Panel1.Controls.Add(_textBox);
                _tsm.CurrentTabPage.Controls.Add(_sp_document_map);
            }
            _tsm.CurrentTabPage.ResumeLayout();
        }

        private void GoLeftBracket(FastColoredTextBox tb, char leftBracket, char rightBracket)
        {
            Range range = tb.Selection.Clone();//need to clone because we will move caret
            int counter = 0;
            int maxIterations = maxBracketSearchIterations;
            while (range.GoLeftThroughFolded())//move caret left
            {
                if (range.CharAfterStart == leftBracket) counter++;
                if (range.CharAfterStart == rightBracket) counter--;
                if (counter == 1)
                {
                    //found
                    tb.Selection.Start = range.Start;
                    tb.DoSelectionVisible();
                    break;
                }
                //
                maxIterations--;
                if (maxIterations <= 0) break;
            }
            tb.Invalidate();
        }
        private void GoRightBracket(FastColoredTextBox tb, char leftBracket, char rightBracket)
        {
            var range = tb.Selection.Clone();//need clone because we will move caret
            int counter = 0;
            int maxIterations = maxBracketSearchIterations;
            do
            {
                if (range.CharAfterStart == leftBracket) counter++;
                if (range.CharAfterStart == rightBracket) counter--;
                if (counter == -1)
                {
                    //found
                    tb.Selection.Start = range.Start;
                    tb.Selection.GoRightThroughFolded();
                    tb.DoSelectionVisible();
                    break;
                }
                //
                maxIterations--;
                if (maxIterations <= 0) break;
            } while (range.GoRightThroughFolded());//move caret right

            tb.Invalidate();
        }
        private void LauncherReplace()
        {
            _textBox.ShowReplaceDialog();
        }
        private void LauncherCollapseSelectedBlock()
        {
            _textBox.CollapseBlock(_textBox.Selection.Start.iLine, _textBox.Selection.End.iLine);
        }
        private void LauncherCollapseAllregion()
        {
            //this example shows how to collapse all #region blocks (C#)
            //if (!lang.StartsWith("CSharp")) return;
            if (!_textBox.Language.Equals(FastColoredTextBoxNS.Language.CSharp)) return;
            for (int iLine = 0; iLine < _textBox.LinesCount; iLine++)
            {
                if (_textBox[iLine].FoldingStartMarker == @"#region\b")//marker @"#region\b" was used in SetFoldingMarkers()
                    _textBox.CollapseFoldingBlock(iLine);
            }
        }
        private void LauncherIncreaseIndentSiftTab()
        {
            _textBox.IncreaseIndent();
        }
        private void LauncherDecreaseIndentTab()
        {
            _textBox.DecreaseIndent();
        }
        private void LauncherAutoIndent()
        {
            _textBox.DoAutoIndent();
        }
        private void LauncherGoLeftBracket()
        {
            GoLeftBracket(_textBox, '{', '}');
        }
        private void LauncherGoRightBracket()
        {
            GoRightBracket(_textBox, '{', '}');
        }
        private void LauncherPrint()
        {
            _textBox.Print(new PrintDialogSettings() { ShowPrintPreviewDialog = true });
        }
        private void LauncherChangeColors()
        {
            var styles = new Style[] { _textBox.SyntaxHighlighter.BlueBoldStyle, _textBox.SyntaxHighlighter.BlueStyle, _textBox.SyntaxHighlighter.BoldStyle, _textBox.SyntaxHighlighter.BrownStyle, _textBox.SyntaxHighlighter.GrayStyle, _textBox.SyntaxHighlighter.GreenStyle, _textBox.SyntaxHighlighter.MagentaStyle, _textBox.SyntaxHighlighter.MaroonStyle, _textBox.SyntaxHighlighter.RedStyle };
            _textBox.SyntaxHighlighter.AttributeStyle = styles[_rnd.Next(styles.Length)];
            _textBox.SyntaxHighlighter.ClassNameStyle = styles[_rnd.Next(styles.Length)];
            _textBox.SyntaxHighlighter.CommentStyle = styles[_rnd.Next(styles.Length)];
            _textBox.SyntaxHighlighter.CommentTagStyle = styles[_rnd.Next(styles.Length)];
            _textBox.SyntaxHighlighter.KeywordStyle = styles[_rnd.Next(styles.Length)];
            _textBox.SyntaxHighlighter.NumberStyle = styles[_rnd.Next(styles.Length)];
            _textBox.SyntaxHighlighter.StringStyle = styles[_rnd.Next(styles.Length)];

            _textBox.OnSyntaxHighlight(new TextChangedEventArgs(_textBox.Range));
        }
        private void LauncherSetSelectedAsReadonly()
        {
            _textBox.Selection.ReadOnly = true;
        }
        private void LauncherSetSelectedAsWritable()
        {
            _textBox.Selection.ReadOnly = false;
        }
        private void LauncherStartStopMacroRecording()
        {
            _textBox.MacrosManager.IsRecording = !_textBox.MacrosManager.IsRecording;
        }
        private void LauncherExecuteMacro()
        {
            _textBox.MacrosManager.ExecuteMacros();
        }
        private void LauncherChangeHotkeys()
        {
            var form = new HotkeysEditorForm(_textBox.HotkeysMapping);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                _textBox.HotkeysMapping = form.GetHotkeys();
        }
        private void LauncherCommentSelectedLines()
        {
            _textBox.InsertLinePrefix(_textBox.CommentPrefix);
        }
        private void LauncherUncommentSelectedLines()
        {
            _textBox.RemoveLinePrefix(_textBox.CommentPrefix);
        }
        private void LauncherExpandAllregion()
        {
            //this example shows how to expand all #region blocks (C#)
            //if (!lang.StartsWith("CSharp")) return;
            if (!_textBox.Language.Equals(FastColoredTextBoxNS.Language.CSharp)) return;
            for (int iLine = 0; iLine < _textBox.LinesCount; iLine++)
            {
                if (_textBox[iLine].FoldingStartMarker == @"#region\b")//marker @"#region\b" was used in SetFoldingMarkers()
                    _textBox.ExpandFoldedBlock(iLine);
            }
        }
        #endregion
		
		#region Methods Private Refresh
		private void RefreshPanelHexa()
		{
			// TODO : this
		}
		private void RefreshTable()
		{
			try
			{
				int nbrows = -1;
				foreach (DataGridViewRow r in _datagridview.Rows)
				{
					foreach (DataGridViewCell cell in r.Cells)
					{
						cell.Style.BackColor = Color.White;
					}
				}
				if (_datagridview.Rows.Count > 1)
				{
					for (int i=0 ; i<_datagridview.RowCount ; i++)
					{
						_datagridview.Rows.RemoveAt(i);
					}
				}
				foreach (Message m in _listmessages)
				{
					nbrows ++;
					//_datagridview.Rows.Add(m.Label, m.Description, m.ListField[0].FieldValue, m.ListField[1].FieldValue, m.ListField[2].FieldValue);
					_datagridview.Rows.Add(m.Label, m.Description);
					_datagridview.Rows[nbrows].HeaderCell.Value = nbrows.ToString();
					int index = 0;
					foreach (Field f in m.ListField)
					{
						index = -1;
						foreach (DataGridViewColumn cln in _datagridview.Columns)
						{
							if (cln.Name.Equals(f.Label))
							{
								index = _datagridview.Columns.IndexOf(cln);
								break;
							}
						}
						if (index != -1)
						{
							_datagridview.Rows[nbrows].Cells[index].Value = f.FieldValue;
						}
						else
						{
							_datagridview.Columns.Add(f.Label,f.Label);
							_datagridview.Rows[nbrows].Cells[_datagridview.Columns.Count-1].Value = f.FieldValue;
						}
					}
				}
				_datagridview.AutoResizeColumnHeadersHeight();
				_datagridview.AutoResizeColumns();
				foreach (DataGridViewRow r in _datagridview.Rows)
				{
					foreach (DataGridViewCell cell in r.Cells)
					{
						if (cell.Value == null)
						{
							cell.Style.BackColor = Color.WhiteSmoke;
						}
						else
						{
							if (cell.Value.ToString().Length > 4)
							{
								string temp = cell.Value.ToString().Substring(cell.Value.ToString().Length - 5, 5);
								if (temp.Equals("ERROR"))
								{
									cell.Style.ForeColor = Color.Red;
								}
							}
						}
					}
				}
			}
			catch (Exception exp0400)
			{
				Log.write("[ ERR : 0400 ] Error during the table refresh.\nYou should close and reopen your file.\n" + exp0400.Message);
			}
		}
		private void RefreshTree()
		{
			try {				
				_sptree.Panel2.Controls.Clear();
				_panelTreeViewBottom = new Panel();
				_panelTreeViewBottom.Height = 60;
				_panelTreeViewBottom.Dock = DockStyle.Bottom;
				_panelTreeViewBottom.BackColor = Color.DarkGray;
				_sptree.Panel2.Controls.Add(_panelTreeViewBottom);
				
				_panelTreeViewTop = new Panel();
				_panelTreeViewTop.Dock = DockStyle.Fill;
				_panelTreeViewTop.BackColor = Color.DarkGray;
				_sptree.Panel2.Controls.Add(_panelTreeViewTop);
				
				_treeview.Nodes.Add(_tsm.CurrentTabPage.Text);
				foreach (Message m in _listmessages)
				{
					TreeNode tn = new TreeNode(m.Label);
					try
					{
						if (m.Deeping ==1) _treeview.Nodes[0].Nodes.Add(tn);
						else if (m.Deeping > 1) _treeview.Nodes[0].LastNode.Nodes.Add(tn);
					}
					catch (Exception)
					{
						_treeview.Nodes[0].Nodes.Add(tn);
						_treeview.Nodes[0].LastNode.ImageKey = "ko";
					}
					if (m.Succeed) tn.ImageKey = "ok";
					else if(!m.Succeed) tn.ImageKey = "warn";
				}
				_treeview.ExpandAll();
			} catch (Exception exp0408) 
			{
				Log.write("[ ERR : 0408 ] Error during the refresh of the interface : \n" + exp0408.Message);
			}
		}
		private void RefreshValueTB()
		{
			try{
				_lblLengthDico = new Label();
				_lblLengthDico.Top = 10;
				_lblLengthDico.Left = 10;
				_lblLengthDico.Text = "Length dico: __";
				_lblLengthDico.Width = 90;
				_lblLengthDico.BackColor = Color.Transparent;
				_panelTreeViewBottom.Controls.Add(_lblLengthDico);
				
				_lblLengthValue = new Label();
				_lblLengthValue.Top = 10;
				_lblLengthValue.Left = 100;
				_lblLengthValue.Text = "Length value: __";
				_lblLengthValue.Width = 90;
				_lblLengthValue.BackColor = Color.Transparent;
				_panelTreeViewBottom.Controls.Add(_lblLengthValue);
				
				_lblType = new Label();
				_lblType.Top = 10;
				_lblType.Left = 200;
				_lblType.Text = "Type : __";
				_lblType.Width = 200;
				_lblType.BackColor = Color.Transparent;
				_panelTreeViewBottom.Controls.Add(_lblType);
				
				_lblTraductor = new Label();
				_lblTraductor.Top = 10;
				_lblTraductor.Left = 400;
				_lblTraductor.Width = 200;
				_lblTraductor.Text = "";
				_lblTraductor.BackColor = Color.Transparent;
				_panelTreeViewBottom.Controls.Add(_lblTraductor);
				
				Label lbl = new Label();
				lbl.Top = 34;
				lbl.Left = 10;
				lbl.Width = 40;
				lbl.Text = "Value : ";
				lbl.BackColor = Color.Transparent;
				_panelTreeViewBottom.Controls.Add(lbl);
				
				_tbvaluehover = new TextBox();
				_tbvaluehover.Top = 32;
				_tbvaluehover.Left = 60;
				_tbvaluehover.Height = 10;
				_tbvaluehover.Width = 400;
				_panelTreeViewBottom.Controls.Add(_tbvaluehover);
			} catch (Exception exp1) {
				Log.write("[ ERR : 0403 ] error during the refresh of the interface : \n\n" + exp1.Message);
			}
		}
		private void RefreshDGV()
		{
			try
			{
				if (_treeDGV.CurrentCell != null)
				{
					_tbvaluehover.Text = _treeDGV[1, _treeDGV.CurrentCell.RowIndex].Value.ToString();
					foreach (Field fld in _thismessage.ListField)
					{
						if (_treeDGV[0, _treeDGV.CurrentCell.RowIndex].Value.ToString().Equals(fld.Label))
						{
							_lblLengthDico.Text = "Dico length : " + fld.Length.ToString();
							_lblLengthValue.Text = "Value length : " + _treeDGV[1, _treeDGV.CurrentCell.RowIndex].Value.ToString().Length;
							if (_tbvaluehover.Text.Equals("... ERROR") || fld.Length != (_treeDGV[1, _treeDGV.CurrentCell.RowIndex].Value.ToString().Length)) _panelTreeViewBottom.BackColor = Color.Gold;
							else _panelTreeViewBottom.BackColor = Color.GreenYellow;
							
							_lblType.Text = "Dico type : ";
							if(fld.Type.Contains("b"))	_lblType.Text += "binaire ";
							if(fld.Type.Contains("a")) _lblType.Text += "alpha";
							if(fld.Type.Contains("n")) _lblType.Text += "numerique ";
							if(fld.Type.Contains("x") || fld.Type.Contains("s")) _lblType.Text += "signé ";
							
							_lblTraductor.Text = "";
							try {
								foreach (string header in _parser.ListItems)
								{
									if (_treeDGV[0, _treeDGV.CurrentCell.RowIndex].Value.ToString().Contains(header))
									{
										foreach (string s in _parser.ListItems)
										{
											if ((_treeDGV[0, _treeDGV.CurrentCell.RowIndex].Value.ToString().Contains(s.Split('|')[0])) && (_treeDGV[1, _treeDGV.CurrentCell.RowIndex].Value.ToString().Equals(s.Split('|')[1])))
											{
												_lblTraductor.Text = "Traduction : " + s.Split('|')[2];
												break;
											}
										}
									}
								}
							}
							catch (Exception)
							{
								Log.write("[ERR : 0401] Warning on the managing the message.");
							}
							break;
						}
					}
				}
			}
			catch
			{
				// append when we arrive to the end of the table view
				// TODO : could add a managment of this kind of situation.
				//Log.write("Warning on the managing the message.", "Warning ID n°4", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
        private void GetFileFormat(string filename)
        {
            FileInfo fi = new FileInfo(filename);
            switch (fi.Extension.ToLower())
            {
                case "cs": _textBox.Language = FastColoredTextBoxNS.Language.CSharp; break;
                case "html": _textBox.Language = FastColoredTextBoxNS.Language.HTML; break;
                case "js": _textBox.Language = FastColoredTextBoxNS.Language.JS; break;
                case "lua": _textBox.Language = FastColoredTextBoxNS.Language.Lua; break;
                case "php": _textBox.Language = FastColoredTextBoxNS.Language.PHP; break;
                case "sql": _textBox.Language = FastColoredTextBoxNS.Language.SQL; break;
                case "vba": 
                case "vb": _textBox.Language = FastColoredTextBoxNS.Language.VB; break;
                case "xml": _textBox.Language = FastColoredTextBoxNS.Language.XML; break;
            }
        }
		#endregion
		
		#region Methods Private Add GUI components
		private void BuildInterfaceHexa()
		{
			foreach (Control c in _tsm.CurrentTabPage.Controls) 
			{
				c.Dispose();
			}
			
			_panel_hexa = new PanelHexa(_tsm, "variable", _action, _path);
			_panel_hexa.loadData(CurrentData);			
			
			_tsm.CurrentTabPage.Controls.Add(_panel_hexa);
		}
		private void AddTextBox()
		{
			try
			{
				if (!_textbuild)
				{
                    BuildTextBox();
                    BuildDocumentMap();

					if (_datagridview == null)
					{
						//_tsm.CurrentTabPage.Controls.Add(_textBox);

                        _sp_document_map = new SplitContainer();
                        _sp_document_map.BackColor = Color.Gray;
                        _sp_document_map.Dock = DockStyle.Fill;
                        _sp_document_map.Orientation = Orientation.Vertical;
                        _sp_document_map.Panel1.Controls.Add(_textBox);
                        _sp_document_map.Panel2.Controls.Add(_documentMap1);
                        _sp_document_map.Width = _tsm.CurrentTabPage.Width;
                        _sp_document_map.Height = _tsm.CurrentTabPage.Height;
                        _sp_document_map.Paint += new System.Windows.Forms.PaintEventHandler(this.ResizeSpliter);
                        _sp_document_map.IsSplitterFixed = true;
                        _tsm.CurrentTabPage.Controls.Clear();
                        _tsm.CurrentTabPage.Controls.Add(_sp_document_map);
                        _tsm.CurrentTabPage.Resize += CurrentTabPage_Resize;
					}
					else
					{
						_sp_Table = new SplitContainer();
						_sp_Table.BackColor = Color.Gray;
						_sp_Table.Dock = DockStyle.Fill;
						_sp_Table.Width = _tsm.CurrentTabPage.Width;
						_sp_Table.Height = _tsm.CurrentTabPage.Height;
						_sp_Table.Orientation = Orientation.Horizontal;
						_sp_Table.Panel1.Controls.Add(_datagridview);
						_sp_Table.Panel2.Controls.Add(_textBox);
						_tsm.CurrentTabPage.Controls.Clear();
						_tsm.CurrentTabPage.Controls.Add(_sp_Table);
					}
					_textbuild = true;
					_textshown = true;
					_listvisiblecomponents.Add("text");
				}
			} catch (Exception exp1) {
				// TODO : manage this case
				Log.write("error during the construction of the interface : \n\n" + exp1.Message);
			}
		}
		private void AddTable()
		{
			try
			{
				if (!_tablebuild)
				{
					_datagridview = new DataGridView();
					_datagridview.Columns.Add("", "Type de message");
					_datagridview.Columns.Add("", "Description");
					_datagridview.Location = new System.Drawing.Point(1, 1);
					_datagridview.MouseHover += new System.EventHandler(this.dgvhover);
					_listmessages = new List<Message>();
					
					if (_sp_Table == null)
					{
						_sp_Table = new SplitContainer();
						_sp_Table.BackColor = Color.Gray;
						_sp_Table.Dock = DockStyle.Fill;
						_sp_Table.Width = _tsm.CurrentTabPage.Width;
						_sp_Table.Height = _tsm.CurrentTabPage.Height;
						_sp_Table.Orientation = Orientation.Horizontal;
						_sp_Table.Panel1.Controls.Add(_datagridview);
						_sp_Table.Panel2.Controls.Add(_textBox);
						_sp_Table.Paint += new System.Windows.Forms.PaintEventHandler(this.ResizeSpliter);
						_sp_Table.SplitterDistance = (2*_sp_Table.Height)/3;
						_tsm.CurrentTabPage.Controls.Remove(_textBox);
						_tsm.CurrentTabPage.Controls.Add(_sp_Table);
					}
					_tablebuild = true;
					_tableshown = false;
					_listvisiblecomponents.Add("SwitchView");
				}
			} catch (Exception exp1) {
				// TODO : manage this case
				Log.write("error during the construction of the interface : \n\n" + exp1.Message);
			}
		}
		private void AddTree()
		{
			try
			{
				if (!_treebuild)
				{
					_sptree = new SplitContainer();
					_sptree.BackColor = Color.Gray;
					_sptree.Dock = DockStyle.Fill;
					_sptree.Orientation = Orientation.Vertical;
					_sptree.Panel1.BackColor = Color.DarkGray;
					_sptree.Panel2.BackColor = Color.WhiteSmoke;
					_sptree.Panel2.SizeChanged += new EventHandler(sptree_Panel2_SizeChanged);
					
					_treeview = new TreeView();
					_treeview.Dock = DockStyle.Fill;
					_treeview.AfterSelect += new TreeViewEventHandler(treeview_AfterSelect);
					_treeview.KeyDown += new KeyEventHandler(treeview_KeyDown);
					_treeview.KeyUp += new KeyEventHandler(treeview_KeyUp);
					_treeview.ImageList = _gui.imageListState;
					_sptree.Panel1.Controls.Add(_treeview);
					
					_listmessages = new List<Message>();
					
					if (_sp_Table == null)
					{
						_sp_Table = new SplitContainer();
						_sp_Table.BackColor = Color.Gray;
						_sp_Table.Dock = DockStyle.Fill;
						_sp_Table.Width = _tsm.CurrentTabPage.Width;
						_sp_Table.Height = _tsm.CurrentTabPage.Height;
						_sp_Table.Orientation = Orientation.Horizontal;
						_sp_Table.Panel1.Controls.Add(_sptree);
						_sp_Table.Panel2.Controls.Add(_textBox);
						_sp_Table.Paint += new System.Windows.Forms.PaintEventHandler(this.ResizeSpliter);
						_sp_Table.SplitterDistance = (2*_sp_Table.Height)/3;
						_tsm.CurrentTabPage.Controls.Remove(_textBox);
						_tsm.CurrentTabPage.Controls.Add(_sp_Table);
					}
					_treebuild = true;
					_treeshown = true;
					_listvisiblecomponents.Add("SwitchView");
				}
			} catch (Exception exp1) {
				// TODO : manage this case
				Log.write("error during the construction of the interface : \n\n" + exp1.Message);
			}
		}
        private void BuildTextBox()
        {
            _textBox = new FastColoredTextBox();
            _textBox.BackBrush = null;
            _textBox.CharHeight = 15;
            _textBox.CharWidth = 7;
            _textBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            _textBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            _textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            _textBox.Font = new System.Drawing.Font("Consolas", 9.75F);
            _textBox.IsReplaceMode = false;
            _textBox.LeftBracket = '(';
            _textBox.Location = new System.Drawing.Point(0, 0);
            _textBox.Name = "_textBox";
            _textBox.Paddings = new System.Windows.Forms.Padding(0);
            _textBox.RightBracket = ')';
            _textBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            _textBox.Size = new System.Drawing.Size(654, 481);
            _textBox.TabIndex = 2;
            _textBox.Zoom = 100;
        }
        private void BuildDocumentMap()
        {
            _documentMap1 = new FastColoredTextBoxNS.DocumentMap();
            _documentMap1.Dock = System.Windows.Forms.DockStyle.Right;
            _documentMap1.ForeColor = System.Drawing.Color.Maroon;
            _documentMap1.BackColor = System.Drawing.Color.WhiteSmoke;
            _documentMap1.Location = new System.Drawing.Point(657, 0);
            _documentMap1.Name = "documentMap1";
            _documentMap1.Size = new System.Drawing.Size(184, 481);
            _documentMap1.TabIndex = 1;
            _documentMap1.Target = _textBox;
            _documentMap1.Text = "documentMap1";            
        }

        private void InitStylesPriority()
        {
            //add this style explicitly for drawing under other styles
            _textBox.AddStyle(_SameWordsStyle);
        }
		#endregion

        #region Event
        private void CurrentTabPage_Resize(object sender, EventArgs e)
        {
            _sp_document_map.Width = _tsm.CurrentTabPage.Width;
            _sp_document_map.Height = _tsm.CurrentTabPage.Height;
            _sp_document_map.SplitterDistance = _tsm.CurrentTabPage.Width - _documentMap1.Width;
        }
        private void ResizeSpliter(object sender, EventArgs e)
		{
			try
			{
				if (_textBox != null) 
				{
                    //_textBox.Size = new System.Drawing.Size(_sp_Table.Panel1.Width - 2, _sp_Table.Height - _sp_Table.SplitterDistance - 6);
                    //_textBox.Size = new System.Drawing.Size(_sp_document_map.Panel1.Width - 2, _sp_document_map.Height - _sp_document_map.SplitterDistance - 6);
                    _textBox.WordWrap = false;
				}
				if (_datagridview != null) _datagridview.Size = new System.Drawing.Size(_sp_Table.Panel2.Width-2, _sp_Table.SplitterDistance-2);
			} catch (Exception) {
				// TODO : manage this case
				//Log.write("error during the construction of the interface : \n\n" + exp1.Message);
                Console.WriteLine("Wouuuuuups !");
			}
		}
		private void tbhover(object sender, System.EventArgs  e)
		{
			_textBox.Focus();
		}
		private void dgvhover(object sender, System.EventArgs  e)
		{
			_datagridview.Focus();
		}
		private void treeview_AfterSelect(object sender, EventArgs e)
		{
			try
			{
				int indexY = 0;
				int widestlabel = 0;
				int widestvalue = 0;
				_sptree.Panel2.Controls.Clear();
				
				_panelTreeViewBottom = new Panel();
				_panelTreeViewBottom.Height = 60;
				_panelTreeViewBottom.Dock = DockStyle.Bottom;
				_panelTreeViewBottom.BackColor = Color.DarkGray;
				_panelTreeViewBottom.BorderStyle = BorderStyle.FixedSingle;
				_sptree.Panel2.Controls.Add(_panelTreeViewBottom);
				
				RefreshValueTB();
				
				_panelTreeViewTop = new Panel();
				_panelTreeViewTop.Dock = DockStyle.Top;
				_panelTreeViewTop.BackColor = Color.WhiteSmoke;
				_panelTreeViewTop.AutoScroll = true;
				_panelTreeViewTop.Height = _sptree.Panel2.Height - _panelTreeViewBottom.Height;
				_panelTreeViewTop.BorderStyle = BorderStyle.None;
				_sptree.Panel2.Controls.Add(_panelTreeViewTop);
				
				_treeDGV = new DataGridView();
				_treeDGV.Dock = DockStyle.Fill;
				_treeDGV.Columns.Add("Label", "Label");
				_treeDGV.Columns.Add("Value", "Value");
				_treeDGV.BackColor = Color.WhiteSmoke;
				_treeDGV.BorderStyle = BorderStyle.None;
				_treeDGV.Columns[1].Width = _treeDGV.Width - _treeDGV.Columns[0].Width;
				_treeDGV.MouseClick += new MouseEventHandler(treeDGV_MouseClick);
				_treeDGV.KeyDown += new KeyEventHandler(treeDGV_KeyDown);
				_treeDGV.KeyUp += new KeyEventHandler(treeDGV_KeyUp);
				
				foreach (Message msg in _listmessages) {
					if (msg.Label.Equals(_treeview.SelectedNode.Text))
					{
						_thismessage = msg;
						foreach (Field fld in _thismessage.ListField)
						{
							_treeDGV.Rows.Add();
							_treeDGV[0, indexY].Value = fld.Label;
							_treeDGV[1, indexY].Value = fld.FieldValue;
							if ((fld.Length > fld.LengthDico.Min) && (fld.Length < fld.LengthDico.Max)) _treeDGV[0, indexY].Style.BackColor = Color.Gold;
							else _treeDGV[0, indexY].Style.BackColor = Color.GreenYellow;
								
							indexY ++;
							
							if (widestlabel < fld.Label.Length) widestlabel = fld.Label.Length;
							if (widestvalue < fld.FieldValue.Length) widestvalue = fld.FieldValue.Length;
						}
						_treeDGV.Columns[0].Width = widestlabel * 7;
						_treeDGV.Columns[1].Width = widestvalue * 7;
						_panelTreeViewTop.Controls.Add(_treeDGV);
						break;
					}
				}
			} catch (Exception exp1) {
				// TODO : manage this case
				Log.write("error during the refresh of the interface : \n\n" + exp1.Message);
			}
		}
		private void sptree_Panel2_SizeChanged(object sender, EventArgs e)
		{
			try
			{
				_panelTreeViewTop.Height = _sptree.Panel2.Height - _panelTreeViewBottom.Height;
			}
			catch (Exception)
			{
				// TODO : manage this case
                Console.WriteLine("Wouuuuuuuuuuuuuups!");
			}
		}
		private void treeview_KeyUp(object sender, KeyEventArgs e)
		{
			RefreshDGV();
		}
		private void treeview_KeyDown(object sender, KeyEventArgs e)
		{
			RefreshDGV();
		}
		private void treeDGV_MouseClick(object sender, EventArgs e)
		{
			RefreshDGV();
		}
		private void treeDGV_KeyUp(object sender, KeyEventArgs e)
		{
			RefreshDGV();
		}
		private void treeDGV_KeyDown(object sender, KeyEventArgs e)
		{
			RefreshDGV();
		}
		#endregion

        #region Event textbox
        private void _textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (_tsm.Rb_combo_lang.Text)
            {
                case "CSharp (custom highlighter)":
                    //For sample, we will highlight the syntax of C# manually, although could use built-in highlighter
                    CSharpSyntaxHighlight(e);//custom highlighting
                    break;
                default:
                    break;//for highlighting of other languages, we using built-in FastColoredTextBox highlighter
            }

            if (_textBox.Text.Trim().StartsWith("<?xml"))
            {
                _textBox.Language = FastColoredTextBoxNS.Language.XML;

                _textBox.ClearStylesBuffer();
                _textBox.Range.ClearStyle(StyleIndex.All);
                InitStylesPriority();
                _textBox.AutoIndentNeeded -= _textBox_AutoIndentNeeded;

                _textBox.OnSyntaxHighlight(new TextChangedEventArgs(_textBox.Range));
            }
        }
        private void CSharpSyntaxHighlight(TextChangedEventArgs e)
        {
            _textBox.LeftBracket = '(';
            _textBox.RightBracket = ')';
            _textBox.LeftBracket2 = '\x0';
            _textBox.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.ClearStyle(_BlueStyle, _BoldStyle, _GrayStyle, _MagentaStyle, _GreenStyle, _BrownStyle);

            //string highlighting
            e.ChangedRange.SetStyle(_BrownStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            //comment highlighting
            e.ChangedRange.SetStyle(_GreenStyle, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(_GreenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(_GreenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            //number highlighting
            e.ChangedRange.SetStyle(_MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            //attribute highlighting
            e.ChangedRange.SetStyle(_GrayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
            //class name highlighting
            e.ChangedRange.SetStyle(_BoldStyle, @"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");
            //keyword highlighting
            e.ChangedRange.SetStyle(_BlueStyle, @"\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b|#region\b|#endregion\b");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();

            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _textBox.ShowFindDialog();
        }
        
        //private void miLanguage_DropDownOpening(object sender, EventArgs e)
        //{
        //    foreach (ToolStripMenuItem mi in miLanguage.DropDownItems)
        //        mi.Checked = mi.Text == lang;
        //}
        //private void miCSharp_Click(object sender, EventArgs e)
        //{
        //    //set language
        //    lang = (sender as ToolStripMenuItem).Text;
        //    _textBox.ClearStylesBuffer();
        //    _textBox.Range.ClearStyle(StyleIndex.All);
        //    InitStylesPriority();
        //    _textBox.AutoIndentNeeded -= _textBox_AutoIndentNeeded;
        //    //
        //    switch (lang)
        //    {
        //        //For example, we will highlight the syntax of C# manually, although could use built-in highlighter
        //        case "CSharp (custom highlighter)":
        //            _textBox.Language = Language.Custom;
        //            _textBox.CommentPrefix = "//";
        //            _textBox.AutoIndentNeeded += _textBox_AutoIndentNeeded;
        //            //call OnTextChanged for refresh syntax highlighting
        //            _textBox.OnTextChanged();
        //            break;
        //        case "CSharp (built-in highlighter)": _textBox.Language = Language.CSharp; break;
        //        case "VB": _textBox.Language = Language.VB; break;
        //        case "HTML": _textBox.Language = Language.HTML; break;
        //        case "XML": _textBox.Language = Language.XML; break;
        //        case "SQL": _textBox.Language = Language.SQL; break;
        //        case "PHP": _textBox.Language = Language.PHP; break;
        //        case "JS": _textBox.Language = Language.JS; break;
        //        case "Lua": _textBox.Language = Language.Lua; break;
        //    }
        //    _textBox.OnSyntaxHighlight(new TextChangedEventArgs(_textBox.Range));
        //    miChangeColors.Enabled = lang != "CSharp (custom highlighter)";
        //}
        private void hTMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "HTML with <PRE> tag|*.html|HTML without <PRE> tag|*.html";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string html = "";

                if (sfd.FilterIndex == 1)
                {
                    html = _textBox.Html;
                }
                if (sfd.FilterIndex == 2)
                {

                    ExportToHTML exporter = new ExportToHTML();
                    exporter.UseBr = true;
                    exporter.UseNbsp = false;
                    exporter.UseForwardNbsp = true;
                    exporter.UseStyleTag = true;
                    html = exporter.GetHtml(_textBox);
                }
                File.WriteAllText(sfd.FileName, html);
            }
        }
        private void _textBox_SelectionChangedDelayed(object sender, EventArgs e)
        {
            _textBox.VisibleRange.ClearStyle(_SameWordsStyle);
            if (!_textBox.Selection.IsEmpty)
                return;//user selected diapason

            //get fragment around caret
            var fragment = _textBox.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;
            //highlight same words
            var ranges = _textBox.VisibleRange.GetRanges("\\b" + text + "\\b").ToArray();
            if (ranges.Length > 1)
                foreach (var r in ranges)
                    r.SetStyle(_SameWordsStyle);
        }
        private void goForwardCtrlShiftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _textBox.NavigateForward();
        }
        private void goBackwardCtrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _textBox.NavigateBackward();
        }
        private void _textBox_AutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            //block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$"))
                return;
            //start of block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{"))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            //end of block {}
            if (Regex.IsMatch(args.LineText, @"}[^""']*$"))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //label
            if (Regex.IsMatch(args.LineText, @"^\s*\w+\s*:\s*($|//)") &&
                !Regex.IsMatch(args.LineText, @"^\s*default\s*:"))
            {
                args.Shift = -args.TabLength;
                return;
            }
            //some statements: case, default
            if (Regex.IsMatch(args.LineText, @"^\s*(case|default)\b.*:\s*($|//)"))
            {
                args.Shift = -args.TabLength / 2;
                return;
            }
            //is unclosed operator in previous line ?
            if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|[\}\s]*else)\b[^{]*$"))
                if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)"))//operator is unclosed
                {
                    args.Shift = args.TabLength;
                    return;
                }
        }
        private void rTFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "RTF|*.rtf";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string rtf = _textBox.Rtf;
                File.WriteAllText(sfd.FileName, rtf);
            }
        }
        private void _textBox_CustomAction(object sender, CustomActionEventArgs e)
        {
            MessageBox.Show(e.Action.ToString());
        }
        #endregion
    }
}
