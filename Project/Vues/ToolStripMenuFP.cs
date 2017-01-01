// Log code 11 00

using System;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using FastColoredTextBoxNS;
using Tools4Libraries;
using Tools4Libraries.Resources;

namespace Droid_FlatFile
{
	public class ToolStripMenuFP : RibbonTab
	{
		#region Attributes
        private GUI gui;
        private Panel _currentTabPage;
        private RibbonPanel panelView;
        private RibbonButton rb_foldingLinesShown;
        private RibbonButton rb_linesHighLighted;
        private RibbonButton rb_charsDisplayed;
        private RibbonButton rb_documentMapView;
        private RibbonButton rb_markLast;
        private RibbonButton rb_markNext;
        private RibbonPanel panelMode;
		private RibbonButton ts_textButton;
        private RibbonButton ts_switchButton;
        private RibbonButton ts_cryptoButton;
        private RibbonPanel panelConvertion;
        private RibbonButton ts_menudico;
        private RibbonButton ts_menuconvert;
        private RibbonPanel panelEdition;
        private RibbonComboBox rb_combo_lang;
        private RibbonTextBox rb_search;
        private RibbonButton rb_replace;
        private RibbonButton rb_collapseSelection;
        private RibbonButton rb_collapseAll;
        private RibbonButton rb_expandAll;
        private RibbonButton rb_increaseIndentation;
        private RibbonButton rb_decreaseIndentation;
        private RibbonButton rb_autoIndentation;
        private RibbonButton rb_leftBacket;
        private RibbonButton rb_rightBacket;
        private RibbonButton rb_print;
        private RibbonButton rb_changeColor;
        private RibbonButton rb_setReadOnly;
        private RibbonButton rb_setWritable;
        private RibbonButton rb_startStopMacro;
        private RibbonButton rb_runMacro;
        private RibbonButton rb_changeHotKeys;
        private RibbonButton rb_commentSelection;
        private RibbonButton rb_uncommentSelection;

        private bool buttonswitch;

        private RibbonDescriptionMenuItem tmae;        
        private RibbonDescriptionMenuItem tmea;
        private RibbonDescriptionMenuItem tmah;
        private RibbonDescriptionMenuItem tmeh;
        private RibbonDescriptionMenuItem tmha;
        private RibbonDescriptionMenuItem tmhe;
        private RibbonDescriptionMenuItem tmab;
        private RibbonDescriptionMenuItem tmeb;
        private RibbonDescriptionMenuItem tmba;
        private RibbonDescriptionMenuItem tmbe;

		private string path;
		
		private List<string> listDico;
		#endregion
		
		#region Properties
        public RibbonComboBox Rb_combo_lang
        {
            get { return rb_combo_lang; }
            set { rb_combo_lang = value; }
        }
        public Panel CurrentTabPage
        {
            get { return _currentTabPage; }
            set { _currentTabPage = value; }
        }
        #endregion

        #region Constructor
        public ToolStripMenuFP(List<string> theList, string pathfp)
		{
			gui = new GUI();
			buttonswitch = true;
			path = pathfp;

            BuildView();
            BuildModifications();
            BuildModes();
            BuildEdition();

            this.Panels.Add(panelView);
            this.Panels.Add(panelConvertion);
            this.Panels.Add(panelMode);
            this.Panels.Add(panelEdition);
            
            this.Text = "Text";
		}
		#endregion
		
		#region Methods Public
		public void RefreshComponent(List<string> ListComponents)
		{
			try
			{
				ts_textButton.Enabled = ListComponents.Contains("text");
				ts_switchButton.Enabled = ListComponents.Contains("SwitchView");
				ts_cryptoButton.Enabled = ListComponents.Contains("crypto");
			}
			catch
			{
				ts_textButton.Enabled = false;
				ts_switchButton.Enabled = false;
				ts_cryptoButton.Enabled = false;
			}
		}
		public void Dispose(List<string> theList)
		{
			this.Dispose();
			//theList.Remove("manager_parser_" + CurrentTabPage.Text);
		}
		#endregion
		
		#region Methods Private
        private void BuildView()
        {
            addLinesHighLighted();
            addFoldingLinesShown();
            addCharsDisplayed();
            addDocumentMap();
            addMarkLast();
            addMarkNext();

            panelView = new System.Windows.Forms.RibbonPanel();
            panelView.Text = "View";
            panelView.Items.Add(rb_documentMapView);
            panelView.Items.Add(rb_charsDisplayed);
            panelView.Items.Add(rb_foldingLinesShown);
            panelView.Items.Add(rb_linesHighLighted);
            panelView.Items.Add(rb_markLast);
            panelView.Items.Add(rb_markNext);
        }
        private void BuildModifications()
        {
            addDicoMenu();
            addConvertMenu();

            panelConvertion = new System.Windows.Forms.RibbonPanel();
            panelConvertion.Text = "Convertion";
            panelConvertion.Items.Add(ts_menuconvert);
            panelConvertion.Items.Add(ts_menudico);
        }
        private void BuildModes()
        {
            addTextButton();
            addSwitchButton();
            addCryptoButton();

            panelMode = new System.Windows.Forms.RibbonPanel();
            panelMode.Text = "Mode";
            panelMode.Items.Add(ts_textButton);
            panelMode.Items.Add(ts_switchButton);
            panelMode.Items.Add(ts_cryptoButton);
        }
        private void BuildEdition()
        {   
            add_combo_lang();
            add_search();
            add_replace();
            add_collapseSelection();
            add_collapseAll();
            add_expandAll();
            add_increaseIndentation();
            add_decreaseIndentation();
            add_autoIndentation();
            add_leftBacket();
            add_rightBacket();
            add_print();
            add_changeColor();
            add_setReadOnly();
            add_setWritable();
            add_startStopMacro();
            add_runMacro();
            add_changeHotKeys();
            add_commentSelection();
            add_uncommentSelection();

            panelEdition = new System.Windows.Forms.RibbonPanel();
            panelEdition.Text = "Edition";
            panelEdition.Items.Add(rb_combo_lang);
            panelEdition.Items.Add(rb_search);
            panelEdition.Items.Add(rb_replace);
            panelEdition.Items.Add(rb_collapseSelection);
            panelEdition.Items.Add(rb_collapseAll);
            panelEdition.Items.Add(rb_expandAll);
            panelEdition.Items.Add(rb_increaseIndentation);
            panelEdition.Items.Add(rb_decreaseIndentation);
            panelEdition.Items.Add(rb_autoIndentation);
            panelEdition.Items.Add(rb_leftBacket);
            panelEdition.Items.Add(rb_rightBacket);
            panelEdition.Items.Add(rb_print);
            panelEdition.Items.Add(rb_changeColor);
            panelEdition.Items.Add(rb_setReadOnly);
            panelEdition.Items.Add(rb_setWritable);
            panelEdition.Items.Add(rb_startStopMacro);
            panelEdition.Items.Add(rb_runMacro);
            panelEdition.Items.Add(rb_changeHotKeys);
            panelEdition.Items.Add(rb_commentSelection);
            panelEdition.Items.Add(rb_uncommentSelection);
        }

		private void addTextButton()
		{
            ts_textButton = new RibbonButton("Text");
            ts_textButton.Click += new EventHandler(tsb_Text_Click);
            ts_textButton.Image = gui.imageListManager.Images[gui.imageListManager.Images.IndexOfKey("text")];
        }
		private void addSwitchButton()
        {
            ts_switchButton = new RibbonButton("Switch");
            ts_switchButton.Click += new EventHandler(tsb_ts_switchButton_Click);
            ts_switchButton.Image = gui.imageListManager.Images[gui.imageListManager.Images.IndexOfKey("table")];
		}
		private void addCryptoButton()
        {
            ts_cryptoButton = new RibbonButton("Crypto");
            ts_cryptoButton.Click += new EventHandler(tsb_Crypto_Click);
            ts_cryptoButton.Image = gui.imageListManager.Images[gui.imageListManager.Images.IndexOfKey("key")];
		}
		private void addDicoMenu()
		{
            try
            {
                ts_menudico = new RibbonButton("dico");
                ts_menudico.Image = ResourceIconSet32Default.books;
                ts_menudico.MinSizeMode = RibbonElementSizeMode.Large;
                ts_menudico.MaxSizeMode = RibbonElementSizeMode.Large;
                ts_menudico.Style = RibbonButtonStyle.SplitDropDown;
                ts_menudico.TextAlignment = RibbonItem.RibbonItemTextAlignment.Left;            

                getListDico();
                if (listDico != null)
                {
                    RibbonDescriptionMenuItem dicoElement;
                    foreach (string dic in listDico)
                    {
                        dicoElement = new RibbonDescriptionMenuItem();
                        dicoElement.Description = "Convert to " + dic;
                        dicoElement.Text = dic + "                       ";
                        dicoElement.MinSizeMode = RibbonElementSizeMode.Large;
                        dicoElement.Click += new EventHandler(this.ts_menu_Click);
                        dicoElement.Image = ResourceIconSet32Default.book_open;

                        ts_menudico.DropDownItems.Add(dicoElement); 
                    }
                }
            }
            catch (Exception exp1100)
            {
                Log.write("[ ERR : 1100 ] Cannot create the menu dico. \n" + exp1100.Message);
            }
		}
		private void addConvertMenu()
		{
			try
            {
                // add ascii to ebcdic
                tmae = new RibbonDescriptionMenuItem();
                tmae.Image = ResourceIconSet32Default.key_e;
                tmae.Text = "Ascii to Ebcdic                                           ";
                tmae.Description = "Convert Ascii to Ebcdic";
                tmae.MinSizeMode = RibbonElementSizeMode.Large;
                tmae.Click += new EventHandler(this.ts_menu_Click);

                // add ebcdic to ascii
                tmea = new RibbonDescriptionMenuItem();
                tmea.Description = "Convert Ebcdic to Ascii";
                tmea.Text = "Ebcdic to Ascii";
                tmea.MinSizeMode = RibbonElementSizeMode.Large;
                tmea.Click += new EventHandler(this.ts_menu_Click);
                tmea.Image = ResourceIconSet32Default.key_a;
                //ts_menuconvert.DropDownItems.Add(tmea);

                // add ascii to hexa
                tmah = new RibbonDescriptionMenuItem();
                tmah.Description = "Convert Ascii to Hexa";
                tmah.Text = "Ascii to Hexa";
                tmah.MinSizeMode = RibbonElementSizeMode.Large;
                tmah.Click += new EventHandler(this.ts_menu_Click);
                tmah.Image = ResourceIconSet32Default.key_x;
                //ts_menuconvert.DropDownItems.Add(tmah);

                // add ebcdic to hexa
                tmeh = new RibbonDescriptionMenuItem();
                tmeh.Description = "Convert Ebcdic to Hexa";
                tmeh.Text = "Ebcdic to Hexa";
                tmeh.MinSizeMode = RibbonElementSizeMode.Large;
                tmeh.Click += new EventHandler(this.ts_menu_Click);
                tmeh.Image = ResourceIconSet32Default.key_x;
                //ts_menuconvert.DropDownItems.Add(tmeh);

                // add ascii to hexa
                tmha = new RibbonDescriptionMenuItem();
                tmha.Description = "Convert Hexa to Ascii";
                tmha.Text = "Hexa to Ascii";
                tmha.MinSizeMode = RibbonElementSizeMode.Large;
                tmha.Click += new EventHandler(this.ts_menu_Click);
                tmha.Image = ResourceIconSet32Default.key_x;
                //ts_menuconvert.DropDownItems.Add(tmha);

                // add ebcdic to hexa
                tmhe = new RibbonDescriptionMenuItem();
                tmhe.Description = "Convert Hexa to Ebcdic";
                tmhe.Text = "Hexa to Ebcdic";
                tmhe.MinSizeMode = RibbonElementSizeMode.Large;
                tmhe.Click += new EventHandler(this.ts_menu_Click);
                tmhe.Image = ResourceIconSet32Default.key_x;
                //ts_menuconvert.DropDownItems.Add(tmhe);

                // add ascii to binary
                tmab = new RibbonDescriptionMenuItem();
                tmab.Description = "Convert Ascii to Binary";
                tmab.Text = "Ascii to Binary";
                tmab.MinSizeMode = RibbonElementSizeMode.Large;
                tmab.Click += new EventHandler(this.ts_menu_Click);
                tmab.Image = ResourceIconSet32Default.key_b;
                //ts_menuconvert.DropDownItems.Add(tmab);

                // add ebcdic to binary
                tmeb = new RibbonDescriptionMenuItem();
                tmeb.Description = "Convert Ebcdic to Binary";
                tmeb.Text = "Ebcdic to Binary";
                tmeb.MinSizeMode = RibbonElementSizeMode.Large;
                tmeb.Click += new EventHandler(this.ts_menu_Click);
                tmeb.Image = ResourceIconSet32Default.key_b;
                //ts_menuconvert.DropDownItems.Add(tmeb);

                // add binary to ascii
                tmba = new RibbonDescriptionMenuItem();
                tmba.Description = "Convert Binary to Ascii";
                tmba.Text = "Binary to Ascii";
                tmba.MinSizeMode = RibbonElementSizeMode.Large;
                tmba.Click += new EventHandler(this.ts_menu_Click);
                tmba.Image = ResourceIconSet32Default.key_a;
                //ts_menuconvert.DropDownItems.Add(tmba);

                // add binary to ebcdic
                tmbe = new RibbonDescriptionMenuItem();
                tmbe.Description = "Convert Binary to Ebcdic";
                tmbe.Text = "Binary to Ebcdic";
                tmbe.MinSizeMode = RibbonElementSizeMode.Large;
                tmbe.Click += new EventHandler(this.ts_menu_Click);
                tmbe.Image = ResourceIconSet32Default.key_e;
                //ts_menuconvert.DropDownItems.Add(tmbe);

                ts_menuconvert = new RibbonButton("convert");
                ts_menuconvert.Image = ResourceIconSet32Default.sort_columns;
                ts_menuconvert.MinSizeMode = RibbonElementSizeMode.Large;
                ts_menuconvert.MaxSizeMode = RibbonElementSizeMode.Large;
                ts_menuconvert.Style = RibbonButtonStyle.SplitDropDown;
                ts_menuconvert.DropDownItems.Add(tmae);                        
                ts_menuconvert.DropDownItems.Add(tmea);
                ts_menuconvert.DropDownItems.Add(tmah);
                ts_menuconvert.DropDownItems.Add(tmeh);
                ts_menuconvert.DropDownItems.Add(tmha);
                ts_menuconvert.DropDownItems.Add(tmhe);
                ts_menuconvert.DropDownItems.Add(tmab);
                ts_menuconvert.DropDownItems.Add(tmeb);
                ts_menuconvert.DropDownItems.Add(tmba);
                ts_menuconvert.DropDownItems.Add(tmbe);
                //ts_menuconvert.DrawIconsBar = false;
                ts_menuconvert.TextAlignment = RibbonItem.RibbonItemTextAlignment.Left;            
			}
			catch (Exception exp1105)
			{
				Log.write("[ WRN : 1105 ] error when create the tool strip menu.\n" + exp1105.Message);
			}
		}        
        private void addDocumentMap()
        {
            rb_documentMapView = new RibbonButton("Document map");
            rb_documentMapView.Click += new EventHandler(tsb_DocumentMap_Click);
            rb_documentMapView.Image = ResourceIconSet32Default.application_side_tree;
            rb_documentMapView.SmallImage = ResourceIconSet16Default.folders_explorer;
        }
        private void addLinesHighLighted()
        {
            rb_linesHighLighted = new RibbonButton("Highlight lines");
            rb_linesHighLighted.Click += new EventHandler(tsb_HighLight_Click);
            rb_linesHighLighted.Image = ResourceIconSet32Default.text_horizontalrule;
            rb_linesHighLighted.SmallImage = ResourceIconSet16Default.text_horizontalrule;
        }
        private void addFoldingLinesShown()
        {
            rb_foldingLinesShown = new RibbonButton("Folding lines");
            rb_foldingLinesShown.Click += new EventHandler(rb_edition_Click);
            rb_foldingLinesShown.Image = ResourceIconSet32Default.document_index;
            rb_foldingLinesShown.SmallImage = ResourceIconSet16Default.document_index;
        }
        private void addCharsDisplayed()
        {
            rb_charsDisplayed = new RibbonButton("Display chars");
            rb_charsDisplayed.Click += new EventHandler(tsb_CharsDisplayed_Click);
            rb_charsDisplayed.Image = ResourceIconSet32Default.pilcrow;
            rb_charsDisplayed.SmallImage = ResourceIconSet16Default.pilcrow;
        }
        private void addMarkLast()
        {
            rb_markLast = new RibbonButton("Last mark");
            rb_markLast.Click += new EventHandler(tsb_MarkLast_Click);
            rb_markLast.Image = ResourceIconSet32Default.document_page;
            rb_markLast.SmallImage = ResourceIconSet16Default.document_page;
        }
        private void addMarkNext()
        {
            rb_markNext = new RibbonButton("Next mark");
            rb_markNext.Click += new EventHandler(rb_edition_Click);
            rb_markNext.Image = ResourceIconSet32Default.document_page_last;
            rb_markNext.SmallImage = ResourceIconSet16Default.document_page_last;
        }

        private void add_combo_lang() 
        {
            rb_combo_lang = new RibbonComboBox();
            rb_combo_lang.TextBoxValidated += rb_combo_lang_TextBoxValidated;

            RibbonLabel rl;

            rl = new RibbonLabel();
            rl.Text = FastColoredTextBoxNS.Language.CSharp.ToString();
            rb_combo_lang.DropDownItems.Add(rl);

            rl = new RibbonLabel();
            rl.Text = FastColoredTextBoxNS.Language.HTML.ToString();
            rb_combo_lang.DropDownItems.Add(rl);

            rl = new RibbonLabel();
            rl.Text = FastColoredTextBoxNS.Language.JS.ToString();
            rb_combo_lang.DropDownItems.Add(rl);

            rl = new RibbonLabel();
            rl.Text = FastColoredTextBoxNS.Language.Lua.ToString();
            rb_combo_lang.DropDownItems.Add(rl);

            rl = new RibbonLabel();
            rl.Text = FastColoredTextBoxNS.Language.PHP.ToString();
            rb_combo_lang.DropDownItems.Add(rl);

            rl = new RibbonLabel();
            rl.Text = FastColoredTextBoxNS.Language.SQL.ToString();
            rb_combo_lang.DropDownItems.Add(rl);

            rl = new RibbonLabel();
            rl.Text = FastColoredTextBoxNS.Language.VB.ToString();
            rb_combo_lang.DropDownItems.Add(rl);

            rl = new RibbonLabel();
            rl.Text = FastColoredTextBoxNS.Language.XML.ToString();
            rb_combo_lang.DropDownItems.Add(rl);   
        }
        private void add_search() 
        {
            rb_search = new RibbonTextBox();
            rb_search.Click += rb_search_Click;
        }
        private void add_replace() 
        {
            rb_replace = new RibbonButton("Replace");
            rb_replace.SmallImage = ResourceIconSet16Default.text_replace;
            rb_replace.Click += new EventHandler(rb_edition_Click);
        }
        private void add_collapseSelection() 
        {
            rb_collapseSelection = new RibbonButton("CollapseSelection");
            rb_collapseSelection.SmallImage = ResourceIconSet16Default.derivatives___minus;
            rb_collapseSelection.Click += new EventHandler(rb_edition_Click);
        }
        private void add_collapseAll() 
        {
            rb_collapseAll = new RibbonButton("CollapseAll");
            rb_collapseAll.SmallImage = ResourceIconSet16Default.derivatives___minus;
            rb_collapseAll.Click += new EventHandler(rb_edition_Click);
        }
        private void add_expandAll() 
        {
            rb_expandAll = new RibbonButton("ExpandAll");
            rb_expandAll.SmallImage = ResourceIconSet16Default.non_derivative;
            rb_expandAll.Click += new EventHandler(rb_edition_Click);
        }
        private void add_increaseIndentation() 
        {
            rb_increaseIndentation = new RibbonButton("IncreaseIndentation");
            rb_increaseIndentation.SmallImage = ResourceIconSet16Default.text_indent;
            rb_increaseIndentation.Click += new EventHandler(rb_edition_Click);
        }
        private void add_decreaseIndentation()
        {
            rb_decreaseIndentation = new RibbonButton("DecreaseIndentation");
            rb_decreaseIndentation.SmallImage = ResourceIconSet16Default.text_indent;
            rb_decreaseIndentation.Click += new EventHandler(rb_edition_Click);
        }
        private void add_autoIndentation() 
        {
            rb_autoIndentation = new RibbonButton("AutoIndent");
            rb_autoIndentation.SmallImage = ResourceIconSet16Default.text_linespacing;
            rb_autoIndentation.Click += new EventHandler(rb_edition_Click);
        }
        private void add_leftBacket() 
        {
            rb_leftBacket = new RibbonButton("LeftBacket");
            rb_leftBacket.SmallImage = ResourceIconSet16Default.control_start;
            rb_leftBacket.Click += new EventHandler(rb_edition_Click);
        }
        private void add_rightBacket() 
        {
            rb_rightBacket = new RibbonButton("RightBacket");
            rb_rightBacket.SmallImage = ResourceIconSet16Default.control_end;
            rb_rightBacket.Click += new EventHandler(rb_edition_Click);
        }
        private void add_print() 
        {
            rb_print = new RibbonButton("Print");
            rb_print.SmallImage = ResourceIconSet16Default.printer;
            rb_print.Click += new EventHandler(rb_edition_Click);
        }
        private void add_changeColor() 
        {
            rb_changeColor = new RibbonButton("ChangeColor");
            rb_changeColor.SmallImage = ResourceIconSet16Default.color_wheel;
            rb_changeColor.Click += new EventHandler(rb_edition_Click);
        }
        private void add_setReadOnly() 
        {
            rb_setReadOnly = new RibbonButton("SetReadOnly");
            rb_setReadOnly.SmallImage = ResourceIconSet16Default.page_key;
            rb_setReadOnly.Click += new EventHandler(rb_edition_Click);
        }
        private void add_setWritable() 
        {
            rb_setWritable = new RibbonButton("SetWritable");
            rb_setWritable.SmallImage = ResourceIconSet16Default.page_edit;
            rb_setWritable.Click += new EventHandler(rb_edition_Click);
        }
        private void add_startStopMacro() 
        {
            rb_startStopMacro = new RibbonButton("StartStopMacro");
            rb_startStopMacro.SmallImage = ResourceIconSet16Default.resultset_next;
            rb_startStopMacro.Click += new EventHandler(rb_edition_Click);
        }
        private void add_runMacro() 
        {
            rb_runMacro = new RibbonButton("RunMacro");
            rb_runMacro.SmallImage = ResourceIconSet16Default.script_go;
            rb_runMacro.Click += new EventHandler(rb_edition_Click);
        }
        private void add_changeHotKeys() 
        {
            rb_changeHotKeys = new RibbonButton("HotKey");
            rb_changeHotKeys.SmallImage = ResourceIconSet16Default.keyboard_magnify;
            rb_changeHotKeys.Click += new EventHandler(rb_edition_Click);
        }
        private void add_commentSelection() 
        {
            rb_commentSelection = new RibbonButton("Comment");
            rb_commentSelection.SmallImage = ResourceIconSet16Default.comment_code_add;
            rb_commentSelection.Click += new EventHandler(rb_edition_Click);
        }
        private void add_uncommentSelection() 
        {
            rb_uncommentSelection = new RibbonButton("Uncomment");
            rb_uncommentSelection.SmallImage = ResourceIconSet16Default.comment_code_remove;
            rb_uncommentSelection.Click += new EventHandler(rb_edition_Click);
        }

		private void getListDico()
		{
			if (Directory.Exists(path))
			{
				string[] filePaths = Directory.GetFiles(path);
				listDico = new List<string>();
				try {
					foreach (string dir in filePaths)
					{
						listDico.Add(dir.Substring(dir.LastIndexOf("\\") + 1).Substring(0, dir.Substring(dir.LastIndexOf("\\") + 1).Length - 4));
					}
				} catch (Exception exp1100) {
					Log.write("[ WRN : 1100 ] Error when loading dictionnary list. \n" + exp1100.Message);
				}
			}
		}
		#endregion

        #region Events
        public void rb_edition_Click(object sender, EventArgs e)
        {
            RibbonButton rb = sender as RibbonButton;
            ToolBarEventArgs action = new ToolBarEventArgs(rb.Text);
            OnAction(action);
        }
        private void rb_search_Click(object sender, EventArgs e)
        {
            ToolBarEventArgs action = new ToolBarEventArgs(e.ToString());
            OnAction(action);
        }
        private void rb_combo_lang_TextBoxValidated(object sender, EventArgs e)
        {
            ToolBarEventArgs action = new ToolBarEventArgs(e.ToString());
            OnAction(action);
        }
        public void tsb_DocumentMap_Click(object sender, EventArgs e)
        {
            ToolBarEventArgs action = new ToolBarEventArgs("DocumentMap");
            OnAction(action);
        }
        public void tsb_HighLight_Click(object sender, EventArgs e)
        {
            ToolBarEventArgs action = new ToolBarEventArgs("HighLight");
            OnAction(action);
        }
        public void tsb_FoldingLiines_Click(object sender, EventArgs e)
        {
            ToolBarEventArgs action = new ToolBarEventArgs("FoldingLines");
            OnAction(action);
        }
        public void tsb_CharsDisplayed_Click(object sender, EventArgs e)
        {
            ToolBarEventArgs action = new ToolBarEventArgs("CharDisplay");
            OnAction(action);
        }
        public void tsb_MarkLast_Click(object sender, EventArgs e)
        {
            ToolBarEventArgs action = new ToolBarEventArgs("MarkLast");
            OnAction(action);
        }
        public void tsb_MarkNext_Click(object sender, EventArgs e)
        {
            ToolBarEventArgs action = new ToolBarEventArgs("MarkNext");
            OnAction(action);
        }
        public event EventHandlerAction ActionAppened;
		public void ts_menu_Click(object sender, EventArgs e)
		{
			RibbonDescriptionMenuItem element = sender as RibbonDescriptionMenuItem;
			ToolBarEventArgs action = new ToolBarEventArgs(element.Text);
			OnAction(action);
		}
		public void tsb_Crypto_Click(object sender, EventArgs e)
		{
			ToolBarEventArgs action = new ToolBarEventArgs("Crypto");
			OnAction(action);
		}
		public void tsb_Text_Click(object sender, EventArgs e)
		{
			ToolBarEventArgs action = new ToolBarEventArgs("Text");
			OnAction(action);
		}
		public void tsb_ts_switchButton_Click(object sender, EventArgs e)
		{
			buttonswitch = !buttonswitch;
			if (buttonswitch)
			{
				ts_switchButton.Image = gui.imageListManager.Images[gui.imageListManager.Images.IndexOfKey("tree")];
			}
			else
			{
				ts_switchButton.Image = gui.imageListManager.Images[gui.imageListManager.Images.IndexOfKey("table")];
			}
			
			ToolBarEventArgs action = new ToolBarEventArgs("SwitchView");
			OnAction(action);
		}
		public void OnAction(EventArgs e)
		{
			if (ActionAppened != null) ActionAppened(this, e);
		}
		#endregion
	}
}
