using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoDownload.Common;

namespace Upload.gui
{
    public partial class ProgressDialogForm : Form
    {
        private readonly CancellationTokenSource cancellationToken;
        private int _maximum;

        public int Maximum { 
            get => _maximum; 
            set
            { 
                _maximum = value >= 1 ? value : 1;
                Util.SafeInvoke(this.progressBar, () =>
                {
                    this.progressBar.Maximum = _maximum;
                });
            }
        }

        public ProgressDialogForm(string title)
        {
            InitializeComponent();
            cancellationToken = new CancellationTokenSource();
            Maximum = 1;
            this.Text = title;
        }

        private void ReportCallback(int count, string mess)
        {
            int progressValue = count;
            if (progressValue > _maximum)
            {
                progressValue = _maximum;
            }
            if (progressValue < 0)
            {
                progressValue = 0;
            }
            Util.SafeInvoke(this.progressBar, () => 
            { 
                this.progressBar.Value = progressValue; 
            });
            Util.SafeInvoke(this.lbCount, () => { this.lbCount.Text = $"{count}/{_maximum}"; });
            Util.SafeInvoke(this.lbMess, () => { this.lbMess.Text = mess; });
        }

        public async Task<T> DoworkAsync<T>(Func<Action<int, string>, CancellationToken, Task<T>> report)
        {
            Util.SafeInvoke(this,this.Show);
            T rs = await report.Invoke(ReportCallback, cancellationToken.Token);
            await Task.Delay(500);
            Util.SafeInvoke(this,this.Close);
            return rs;
        }
        
        public async Task DoworkAsync(Func<Action<int, string>, CancellationToken, Task> report)
        {
            Util.SafeInvoke(this, this.Show);
            await report.Invoke(ReportCallback, cancellationToken.Token);
            await Task.Delay(500);
            Util.SafeInvoke(this, this.Close);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            cancellationToken.Cancel();
        }

        private void ProgressDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancellationToken.Cancel();
        }
    }

}
