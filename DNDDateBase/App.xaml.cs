using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;
using DNDDateBase.Serialization;
using DNDDateBase.Utility;

namespace DNDDateBase
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Temp path string. This is used for quick save
        private string CurrentSaveFilePath { get; set; }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // A catch all exceptions function to deal with unexcepted errors
            _ = MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Main Exception Catcher", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }

        /// <summary>
        /// Handles the logic when the application closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            // As the app closes, save the working data
            SerializationHandler.SaveDataToFile(GetSerializedData(), CurrentSaveFilePath);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Load standard file
            CurrentSaveFilePath = SerializationHandler.DefaultSaveFilePath;
            SerializedDataContainer appData = SerializationHandler.LoadData(CurrentSaveFilePath);
            if (appData != null)
            {
                SetFileData(appData);
            }
            else
            {
                SetFileData(new SerializedDataContainer());
            }

            // Set file filter
            Application.Current.Resources.Add(Constants.FileFilterName, "Xml file (*.xml)|*.xml| All Files |*.*| Text file (*.txt)|*.txt");

            // TODO
            // Create object relation
            // Load standard items into the app

            // Load main window
            MainWindow win = new MainWindow();
            win.Show();
        }

        /// <summary>
        /// Sets the app resources to the chosen data
        /// </summary>
        /// <param name="appData">Data to be loaded in the app</param>
        private void SetFileData(SerializedDataContainer appData)
        {
            SetDataHelper(nameof(appData.Characters), appData.Characters);
            SetDataHelper(nameof(appData.Cities),appData.Cities);
            SetDataHelper(nameof(appData.Locations), appData.Locations);
            SetDataHelper(nameof(appData.Items),appData.Items);
        }

        private void SetDataHelper<T>(string resourceName, List<T> coll)
        {
            List<T> tempList = coll;
            // Ensures list for application if none exist
            if (tempList == null)
            {
                tempList = new List<T>();
            }
            if (Application.Current.Resources.Contains(resourceName))
            {
                Application.Current.Resources[resourceName] = tempList;
            }
            else
            {
                Application.Current.Resources.Add(resourceName, tempList);
            }
        }

        private SerializedDataContainer GetSerializedData()
        {
            // Find the current data stored in the application resources.
            SerializedDataContainer data = new SerializedDataContainer
            {
                Characters = (List<Character>)Application.Current.FindResource("Characters"),
                Cities = (List<City>)Application.Current.FindResource("Cities"),
                Locations = (List<Location>)Application.Current.FindResource("Locations"),
                Items = (List<Item>)Application.Current.FindResource("Items")
            };

            return data;
        }

        #region Public file methods

        public bool LoadDataFromFile(string fileName)
        {
            SerializedDataContainer appData = SerializationHandler.LoadData(fileName);
            if (appData != null)
            {
                SetFileData(appData);
                CurrentSaveFilePath = fileName;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void NewFile(string fileName)
        {
            CurrentSaveFilePath = fileName;
            SetFileData(new SerializedDataContainer());
        }

        public bool SaveFile(string fileName = null)
        {
            if(fileName != null)
            {
                CurrentSaveFilePath = fileName;
            }
            SerializationHandler.SaveDataToFile(GetSerializedData(), CurrentSaveFilePath);
            return true;
        }

        #endregion
    }
}
