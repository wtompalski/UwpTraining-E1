using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpTraining_E1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MemoryUsage : Page
    {
        private List<int> numbers = new List<int>();

        public MemoryUsage()
        {
            this.InitializeComponent();
        }

        private void Allocate_Click(object sender, RoutedEventArgs e)
        {
            numbers.AddRange(Enumerable.Range(0, 5000000));
            this.UpdateUI();
        }

        private void Deallocate_Click(object sender, RoutedEventArgs e)
        {
            this.numbers.Clear();
            GC.Collect();
            this.UpdateUI();
        }

        private void UpdateUI()
        {
            this.Status.Text = $"Count: {numbers.Count}, Memory usage: {MemoryManager.AppMemoryUsageLevel}";

            var memoryReport = MemoryManager.GetAppMemoryReport();

            this.Memory.Text = $"Usage {memoryReport.TotalCommitUsage / 1000000} / {memoryReport.TotalCommitLimit / 1000000} ({100 * memoryReport.TotalCommitUsage / memoryReport.TotalCommitLimit }%)";
        }
    }
}
