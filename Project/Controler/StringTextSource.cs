using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Droid_FlatFile
{
    /// <summary>
    /// Text source for displaying readonly text, given as string.
    /// </summary>
    public class StringTextSource : TextSource, IDisposable
    {
        List<int> sourceStringLinePositions = new List<int>();
        string sourceString;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public StringTextSource(FastColoredTextBox tb) : base(tb)
        {
            timer.Interval = 10000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            try
            {
                UnloadUnusedLines();
            }
            finally
            {
                timer.Enabled = true;
            }
        }

        private void UnloadUnusedLines()
        {
            const int margin = 2000;
            var iStartVisibleLine = CurrentTB.VisibleRange.Start.iLine;
            var iFinishVisibleLine = CurrentTB.VisibleRange.End.iLine;

            int count = 0;
            for (int i = 0; i < Count; i++)
                if (base.lines[i] != null && !base.lines[i].IsChanged && Math.Abs(i - iFinishVisibleLine) > margin)
                {
                    base.lines[i] = null;
                    count++;
                }
#if debug
            Console.WriteLine("UnloadUnusedLines: " + count);
#endif
        }

        public void OpenString(string sourceString)
        {
            Clear();

            this.sourceString = sourceString;

            //parse lines
            int index = -1;
            do
            {
                sourceStringLinePositions.Add(index + 1);
                base.lines.Add(null);
                index = sourceString.IndexOf('\n', index + 1);
            } while (index >= 0);

            OnLineInserted(0, Count);

            //load first lines for calc width of the text
            var linesCount = Math.Min(lines.Count, CurrentTB.Height / CurrentTB.CharHeight);
            for (int i = 0; i < linesCount; i++)
                LoadLineFromSourceString(i);

            NeedRecalc(new TextChangedEventArgs(0, linesCount - 1));
            if (CurrentTB.WordWrap)
                OnRecalcWordWrap(new TextChangedEventArgs(0, linesCount - 1));
        }

        public override void ClearIsChanged()
        {
            foreach (var line in lines)
                if (line != null)
                    line.IsChanged = false;
        }

        public override Line this[int i]
        {
            get
            {
                if (base.lines[i] != null)
                    return lines[i];
                else
                    LoadLineFromSourceString(i);

                return lines[i];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private void LoadLineFromSourceString(int i)
        {
            var line = CreateLine();

            string s;
            if (i == Count - 1)
                s = sourceString.Substring(sourceStringLinePositions[i]);
            else
                s = sourceString.Substring(sourceStringLinePositions[i], sourceStringLinePositions[i + 1] - sourceStringLinePositions[i] - 1);

            foreach (var c in s)
                line.Add(new FastColoredTextBoxNS.Char(c));

            base.lines[i] = line;

            if (CurrentTB.WordWrap)
                OnRecalcWordWrap(new TextChangedEventArgs(i, i));
        }

        public override void InsertLine(int index, Line line)
        {
            throw new NotImplementedException();
        }

        public override void RemoveLine(int index, int count)
        {
            if (count == 0) return;
            throw new NotImplementedException();
        }

        public override int GetLineLength(int i)
        {
            if (base.lines[i] == null)
                return 0;
            else
                return base.lines[i].Count;
        }

        public override bool LineHasFoldingStartMarker(int iLine)
        {
            if (lines[iLine] == null)
                return false;
            else
                return !string.IsNullOrEmpty(lines[iLine].FoldingStartMarker);
        }

        public override bool LineHasFoldingEndMarker(int iLine)
        {
            if (lines[iLine] == null)
                return false;
            else
                return !string.IsNullOrEmpty(lines[iLine].FoldingEndMarker);
        }

        public override void Dispose()
        {
            timer.Dispose();
        }

        internal void UnloadLine(int iLine)
        {
            if (lines[iLine] != null && !lines[iLine].IsChanged)
                lines[iLine] = null;
        }
    }
}
