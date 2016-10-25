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
using DNDDateBase.Utility;

namespace DNDDateBase
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    [Serializable()]
    public class SerializedDataContainer
    {
      public List<Character> chars { get; set; }
      public List<City> cities { get; set; }
      public List<Location> locs { get; set; }
      public List<Item> items { get; set; }

    }

    private string saveFilePath = @"..\..\..\DNDDateBase\MySerializedStuff.xml";

    private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
      // A catch all exceptions function to deal with unexcepted errors
      MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Main Exception Catcher", MessageBoxButton.OK, MessageBoxImage.Warning);
      e.Handled = true;
    }

    /// <summary>
    /// Handles the logic when the application closes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Application_Exit(object sender, ExitEventArgs e)
    {
      saveDataToFile();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
      // Load standard file
      SerializedDataContainer appData = loadFile(saveFilePath);
      if(appData != null)
      {
        setFileData(appData);
      } else
      {
        setFileData(new SerializedDataContainer());
      }

      // Set filter
      Application.Current.Resources.Add("filter", "Xml file (*.xml)|*.xml| All Files |*.*| Text file (*.txt)|*.txt");

      // TODO
      // enable to save and load data 
      // enable serialization

      // enable creation of multiple defined classes 
      // Load data from files

      // Load main window
      MainWindow win = new MainWindow();
      win.Show();
    }
    
    private void setFileData(SerializedDataContainer appData)
    {
      List<Character> chars = appData.chars;
      // Ensures list for application if none exist
      if (chars == null)
      {
        chars = new List<Character>();
      }
      if (Application.Current.Resources.Contains("Characters"))
      {
        Application.Current.Resources["Characters"] = chars;
      }
      else
      {
        Application.Current.Resources.Add("Characters", chars);
      }


      List<City> cities = appData.cities;
      if (cities == null)
      {
        cities = new List<City>();
      }
      if (Application.Current.Resources.Contains("Cities"))
      {
        Application.Current.Resources["Cities"] = cities;
      }
      else
      {
        Application.Current.Resources.Add("Cities", cities);
      }


      List<Location> locs = appData.locs;
      if (locs == null)
      {
        locs = new List<Location>();
      }
      if (Application.Current.Resources.Contains("Locations"))
      {
        Application.Current.Resources["Locations"] = locs;
      }
      else
      {
        Application.Current.Resources.Add("Locations", locs);
      }


      List<Item> items = appData.items;
      if (items == null)
      {
        items = new List<Item>();
      }
      if (Application.Current.Resources.Contains("Items"))
      {
        Application.Current.Resources["Items"] = items;
      }
      else
      {
        Application.Current.Resources.Add("Items", items);
      }
    }

    private SerializedDataContainer loadFile(string fileName)
    {
      SerializedDataContainer appData = null;
      if (File.Exists(fileName))
      {
        XmlSerializer deserializer = new XmlSerializer(typeof(SerializedDataContainer));

        StreamReader dataReader = new StreamReader(fileName);
        XmlReader xmlDataReader = new XmlTextReader(dataReader);

        if (deserializer.CanDeserialize(xmlDataReader))
        {
          appData = (SerializedDataContainer)deserializer.Deserialize(xmlDataReader);
        }
        dataReader.Close();
        xmlDataReader.Close();
      }
      return appData;
    }

    private void saveDataToFile()
    {
      // Save the data stored in the different object list
      SerializedDataContainer data = new SerializedDataContainer();
      data.chars = (List<Character>)Application.Current.FindResource("Characters");
      data.cities = (List<City>)Application.Current.FindResource("Cities");
      data.locs = (List<Location>)Application.Current.FindResource("Locations");
      data.items = (List<Item>)Application.Current.FindResource("Items");

      XmlSerializer serializer = new XmlSerializer(typeof(SerializedDataContainer));
      TextWriter writer = new StreamWriter(saveFilePath);
      serializer.Serialize(writer, data);
      writer.Close();
    }

    #region Public file methods

    public bool LoadDataFromFile(string fileName)
    {
      SerializedDataContainer appData = loadFile(fileName);
      if (appData != null)
      {
        setFileData(appData);
        saveFilePath = fileName;
        return true;
      }
      else
      {
        return false;
      }
    }

    public void NewFile(string fileName)
    {
      saveFilePath = fileName;
      setFileData(new SerializedDataContainer());
    }

    public bool SaveFile()
    {
      saveDataToFile();
      return true;
    }

    public bool SaveFile(string fileName)
    {
      saveFilePath = fileName;
      saveDataToFile();
      return true;
    }

    #endregion
  }
}
