// Log code 14

using System;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tools4Libraries;

namespace Droid_FlatFile
{
	public class PanelHexa : System.Windows.Forms.Panel
	{
		#region Attribute
		private DataGridView dgv;
		private DataTable datatable;
		private ToolStripMenuFP tsm;
		private string datavalues;
		private string display_mode;
		private string encoding;
		private string path;
		#endregion
		
		#region Properties
		private int Nb_col_Hexa
		{
			get { return ((this.tsm.CurrentTabPage.Width / 25) * 5) / 6; }
			set { Nb_col_Hexa = value; }
		}
		#endregion
		
		#region Constructor
		public PanelHexa(ToolStripMenuFP my_tsm, string mode, string encoding_type, string pathdico)
		{
			path = pathdico;
			//mode = "fixed";
			display_mode = "variable";
			//mode = "defined";
			
			encoding = encoding_type;
			
			tsm = my_tsm;
			
			dgv = new DataGridView();
			datatable = new DataTable();
			
			buildComponent();
		}
		#endregion
		
		#region Methods public
		public override void Refresh()
		{
			loadData(datavalues);
		}
		
		public void loadData(string data_in)
		{
			try
			{
				datavalues = data_in;
				
				buildTable();
				
				switch (display_mode) 
				{
					case "variable" : insertDataVariable(); break;
					case "fixed" 	: insertDataFixed(); break;
					case "defined" 	: insertDataDefined(); break;
					default 		: Log.write("[ DEB : 1405 ] type de formatage du fichier Hexa non connu."); break;
				}
				refreshComponent();
			} 
			catch (Exception exp1401)
			{
				Log.write("[ ERR : 1401 ] Cannot load hexa values.\n" + exp1401.Message);
			}
		}
		#endregion
		
		#region Methods private
		private void insertDataFixed()
		{
			Nb_col_Hexa = 18;
			insertDataVariable();
		}
		
		private void insertDataDefined()
		{
			// TODO ; here we have to add the possibility for user to chose how many columns he want
			Nb_col_Hexa = 30;
			insertDataVariable();
		}
		
		private void insertDataVariable()
		{
			string[] tmp;
			string val_end = "";
			int rowIndex = -1;
			
			tmp = datavalues.Split('-');
			for(int i=0 ; i<tmp.Length ; i++) 
			{
				rowIndex ++;
				dgv.Rows.Add();
				for(int j=0 ; (j<Nb_col_Hexa-2 && i<tmp.Length-1) ; j++)
				{
					if (tmp[i].ToString().Length == 1) dgv.Rows[rowIndex].Cells[j].Value = "0" + tmp[i].ToString();
					else dgv.Rows[rowIndex].Cells[j].Value = tmp[i].ToString();
					val_end += tmp[i].ToString() + "-";
					dgv.Rows[rowIndex].Cells[dgv.Columns.Count-2].Style.BackColor = Color.Black;
					i++;
				}
				Parsing p = new Parsing(path);
				val_end = val_end.Substring(0, val_end.Length-1);
				switch (encoding.ToLower())
				{
					case "ascii" : 
						dgv.Rows[rowIndex].Cells[Nb_col_Hexa - 1].Value = p.ConvertHexaToAscii(val_end);	
						break;
					case "ebcdic" : 
						dgv.Rows[rowIndex].Cells[Nb_col_Hexa - 1].Value = p.ConvertHexaToEbcdic(val_end);	
						break;
					default : 
						Log.write("[ DEB : 1406 ] type d'encodage du fichier Hexa non connu."); 
						break;
				}
				dgv.Rows[rowIndex].Cells[Nb_col_Hexa - 1].Value = p.ConvertHexaToAscii(val_end);	
				val_end = "";
			}
		}
		
		private void refreshComponent()
		{
			foreach (DataGridViewColumn dgvc in dgv.Columns)
			{
				dgvc.Width = 24;
			}
			dgv.Columns[dgv.Columns.Count-2].Width = 4;
			dgv.Columns[dgv.Columns.Count-1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dgv.Columns[dgv.Columns.Count-1].HeaderText = "";
		}
		
		private void buildTable()
		{
			while (dgv.Columns.Count > 0) 
			{
				dgv.Columns.RemoveAt(0);
			}
			
			for (int i=0; i<Nb_col_Hexa ; i++)
			{
				dgv.Columns.Add(i.ToString(), i.ToString());
				dgv.Columns[i].ReadOnly = true;
			}
		}
		
		private void buildComponent()
		{
			this.Dock = DockStyle.Fill;
			this.BackColor = Color.LightGray;
			this.Width = tsm.CurrentTabPage.Width;
			this.Height = tsm.CurrentTabPage.Height;
			this.Paint += new PaintEventHandler(PanelHexa_Paint);
			this.Controls.Add(dgv);
			
			dgv.Dock = DockStyle.Fill;
			dgv.Location = new System.Drawing.Point(1, 1);
			dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; 
			dgv.ColumnHeadersVisible = true;
			dgv.RowHeadersVisible = false;
			dgv.AllowUserToResizeColumns = false;
			dgv.AllowUserToResizeRows = false;
			dgv.AllowUserToOrderColumns = false;
			dgv.AllowUserToDeleteRows = false;
			dgv.AllowUserToAddRows = false;
			dgv.SelectionChanged += new EventHandler(dgv_SelectionChanged);
		}
		#endregion
		
		#region Event
		private void PanelHexa_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				this.Width = tsm.CurrentTabPage.Width;
				this.Height = tsm.CurrentTabPage.Height;
			} catch (Exception exp1400) {
				Log.write(" [ ERR : 1400 ] Error during the construction of the interface : \n" + exp1400.Message);
			}
		}

		private void dgv_SelectionChanged(object sender, EventArgs e)
		{
			try 
			{
				if (dgv.SelectedCells[0].Value != null && dgv.SelectedCells.Count == 1)
				{
					dgv.SelectedCells[0].Style.SelectionBackColor = Color.DarkGreen;
					dgv.SelectedCells[0].Style.SelectionForeColor = Color.GreenYellow;
					string val = dgv.SelectedCells[0].Value.ToString();
					foreach (DataGridViewRow row in dgv.Rows) 
					{
						for (int i=0 ; i<dgv.Columns.Count-2 ; i++)
						{
							if (row.Cells[i].Value != null)
							{
								if (row.Cells[i].Value.Equals(val))row.Cells[i].Style.BackColor = Color.GreenYellow;
								else row.Cells[i].Style.BackColor = Color.White;
							}
						}
					}
				}				
			} 
			catch (Exception exp1402) 
			{
				Log.write("[ WRN : 1402 ] error in coloration of the hexa table.\n" + exp1402.Message);
			}
		}
		#endregion
	}
}
