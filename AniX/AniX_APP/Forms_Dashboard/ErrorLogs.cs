using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AniX_APP.CustomElements;
using AniX_APP.Forms_Utility;
using AniX_FormsLogic;
using Anix_Shared.DomainModels;
using AniX_Utility;

namespace AniX_APP.Forms_Dashboard
{
    public partial class ErrorLogs : Form
    {
        private User _loggedInUser;
        private IErrorLoggingService _loggingService;
        public ErrorLogs(User loggedInUser, IErrorLoggingService loggingService)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            btnUser.Text = $" {_loggedInUser.Username}";
            _loggingService = loggingService;
            LoadLogsIntoDataGridViewAsync();
            InitializeDataGridViewStyles();

            this.dgvErrors.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvErrors_CellDoubleClick);

        }

        private async Task<List<LogEntry>> ReadLogEntriesAsync()
        {
            string filePath = _loggingService.GetLogFilePath();
            var logEntries = new List<LogEntry>();
            var currentEntry = new StringBuilder();
            LogEntry logEntry = null;

            try
            {
                string[] lines = await File.ReadAllLinesAsync(filePath);

                foreach (var line in lines)
                {
                    if (line.StartsWith("Timestamp: "))
                    {
                        if (logEntry != null && logEntry.Severity == "Error")
                        {
                            logEntry.FullText = currentEntry.ToString().TrimEnd();
                            logEntries.Add(logEntry);
                        }

                        currentEntry.Clear();
                        logEntry = new LogEntry { Timestamp = line.Substring(11) };
                        currentEntry.AppendLine(line);
                    }
                    else if (line.StartsWith("Severity: "))
                    {
                        logEntry.Severity = line.Substring(10);
                        currentEntry.AppendLine(line);
                    }
                    else if (line.StartsWith("Message: "))
                    {
                        var messageStartIndex = line.IndexOf("Message: ") + "Message: ".Length;
                        logEntry.Message = line.Substring(messageStartIndex);
                        currentEntry.AppendLine(line);
                    }
                    else if (line.Trim() == "---------------------------------------------------")
                    {
                        currentEntry.AppendLine(line);
                    }
                    else
                    {
                        currentEntry.AppendLine(line);
                    }
                }

                if (logEntry != null && logEntry.Severity == "Error")
                {
                    logEntry.FullText = currentEntry.ToString().TrimEnd();
                    logEntries.Add(logEntry);
                }
            }
            catch (Exception ex)
            {
            }

            return logEntries;
        }




        private async void LoadLogsIntoDataGridViewAsync()
        {
            var logEntries = await ReadLogEntriesAsync();
            dgvErrors.DataSource = logEntries;
            dgvErrors.Columns["FullText"].Visible = false;
        }

        private void dgvErrors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var logEntry = dgvErrors.Rows[e.RowIndex].DataBoundItem as LogEntry;
                RJMessageBox.Show(logEntry.FullText, "Error Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region STYLE

        private void InitializeDataGridViewStyles()
        {
            #region COLORS DATAGRID
            dgvErrors.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 34, 83);
            dgvErrors.DefaultCellStyle.SelectionForeColor = dgvErrors.DefaultCellStyle.ForeColor;
            dgvErrors.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 34, 83);
            dgvErrors.RowHeadersDefaultCellStyle.SelectionForeColor = dgvErrors.RowHeadersDefaultCellStyle.ForeColor;
            dgvErrors.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvErrors.AdvancedRowHeadersBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single;
            dgvErrors.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvErrors.AdvancedColumnHeadersBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.Single;
            dgvErrors.BackgroundColor = Color.FromArgb(231, 34, 83);
            dgvErrors.GridColor = Color.FromArgb(11, 7, 17);
            dgvErrors.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(231, 34, 83);
            dgvErrors.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(11, 7, 17);
            dgvErrors.DefaultCellStyle.ForeColor = Color.White;
            dgvErrors.DefaultCellStyle.BackColor = Color.FromArgb(11, 7, 17);
            dgvErrors.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvErrors.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvErrors.EnableHeadersVisualStyles = false;
            dgvErrors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvErrors.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvErrors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvErrors.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvErrors.AllowUserToResizeRows = false;

            foreach (DataGridViewColumn column in dgvErrors.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in dgvErrors.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
            }

            foreach (DataGridViewRow row in dgvErrors.Rows)
            {
                row.Resizable = DataGridViewTriState.False;
            }
            #endregion
        }


        #endregion
    }
}
