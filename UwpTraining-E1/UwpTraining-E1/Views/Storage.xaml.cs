using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
    public sealed partial class Storage : Page
    {
        private StorageFolder localFolder = null;
        private StorageFolder localCacheFolder = null;
        private StorageFolder roamingFolder = null;
        private StorageFolder temporaryFolder = null;

        private ApplicationDataContainer localSettings = null;
        private ApplicationDataContainer roamingSettings = null;

        private const string SettingsKey = "SettingsKey";

        private const string FileNameKey = "textfile.txt";

        public Storage()
        {
            this.InitializeComponent();

            localFolder = ApplicationData.Current.LocalFolder;
            localCacheFolder = ApplicationData.Current.LocalCacheFolder;
            roamingFolder = ApplicationData.Current.RoamingFolder;
            temporaryFolder = ApplicationData.Current.TemporaryFolder;

            localSettings = ApplicationData.Current.LocalSettings;
            roamingSettings = ApplicationData.Current.RoamingSettings;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            object setting;
            if (localSettings.Values.TryGetValue(SettingsKey, out setting))
            {
                this.SettingsContent.Text = setting.ToString();
            }

            UpdateUI();
        }

        private void SaveSetting_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.SettingsContent.Text))
            {
                localSettings.Values[SettingsKey] = this.SettingsContent.Text;
            }
        }

        private async void ReadFile_Click(object sender, RoutedEventArgs e)
        {
            StorageFile file = await localFolder.CreateFileAsync(FileNameKey, CreationCollisionOption.OpenIfExists);
            this.FileContent.Text = await FileIO.ReadTextAsync(file);
        }

        private async void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            StorageFile file = await localFolder.CreateFileAsync(FileNameKey, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, this.FileContent.Text);
        }

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            // var str = File.ReadAllText(@"C:\Users\Public\stuff\myfile.txt");
            // this.FileContent.Text = str;

            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".txt");

            StorageFile file = await picker.PickSingleFileAsync();
            this.FileContent.Text = await FileIO.ReadTextAsync(file);
        }

        private async Task UpdateUI()
        {
            var list = await localFolder.GetFilesAsync();
            this.listOfFiles.ItemsSource = list.Select(x => x.Name);
        }

        private async void Import_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");

            StorageFile file = await picker.PickSingleFileAsync();

            await file.CopyAsync(localFolder);
            await UpdateUI();
        }
    }
}
