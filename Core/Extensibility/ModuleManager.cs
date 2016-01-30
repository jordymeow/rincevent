using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Collections.Specialized;

namespace Meow.FR.Rincevent.Core.Extensibility
{
    public class ModuleManager
    {
        readonly List<ModuleInfo> _moduleList = new List<ModuleInfo>();
        public List<ModuleInfo> ModuleList
        {
            get { return _moduleList; }
        }

        readonly List<DisplayModule> _displayModuleList = new List<DisplayModule>();
        public List<DisplayModule> LoadedDisplayModuleList
        {
            get { return _displayModuleList; }
        }

        readonly List<IOModule> _ioModuleList = new List<IOModule>();
        public List<IOModule> LoadedIOModuleList
        {
            get { return _ioModuleList; }
        }
        public string _currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);

        private void AddLoadableModule(string path, ModuleType type)
        {
            FileInfo file = new FileInfo(path);
            string moduleFullName = file.Name.Remove(file.Name.Length - 4, 4);
            string name = "";
            if (type == ModuleType.Display)
                name = moduleFullName.Remove(0, 8);
            else if (type == ModuleType.IO)
                name = moduleFullName.Remove(0, 3);
            else
                throw new NotSupportedException();
            ModuleInfo loadableModule = new ModuleInfo();
            loadableModule.Name = name;
            loadableModule.FullName = moduleFullName;
            loadableModule.Type = type;
            loadableModule.Path = path;
            _moduleList.Add(loadableModule);
        }

        /// <summary>
        /// Initializes the module Manager.
        /// </summary>
        /// <param name="cultureInfo">The culture.</param>
        public ModuleManager(CultureInfo cultureInfo, StringCollection modulesToLoad)
        {
            string[] fileDisplayList = Directory.GetFiles(_currentDirectory, "Display.*.dll", SearchOption.TopDirectoryOnly);
            string[] fileIOList = Directory.GetFiles(_currentDirectory, "IO.*.dll", SearchOption.TopDirectoryOnly);

            foreach (string path in fileDisplayList)
                AddLoadableModule(path, ModuleType.Display);
            foreach (string path in fileIOList)
                AddLoadableModule(path, ModuleType.IO);
            LoadModules(modulesToLoad);
        }

        public void LoadModules(StringCollection modulesToLoad)
        {
            if (modulesToLoad == null)
                modulesToLoad = new StringCollection();

            foreach (ModuleInfo moduleInfo in ModuleList)
            {
                if (modulesToLoad.Contains(moduleInfo.Name))
                {
                    // LOAD THE MODULE
                    if (moduleInfo.Status == ModuleStatus.NotLoaded)
                    {
                        try
                        {
                            if (moduleInfo.Type == ModuleType.Display)
                            {
                                moduleInfo.Instance = (DisplayModule)AppDomain.CurrentDomain.CreateInstanceFromAndUnwrap(moduleInfo.Path, "Meow.FR.Rincevent." + moduleInfo.FullName + ".Module", null);
                                _displayModuleList.Add((DisplayModule)moduleInfo.Instance);
                            }
                            else if (moduleInfo.Type == ModuleType.IO)
                            {
                                moduleInfo.Instance = (IOModule)AppDomain.CurrentDomain.CreateInstanceFromAndUnwrap(moduleInfo.Path, "Meow.FR.Rincevent." + moduleInfo.FullName + ".Module", null);
                                _ioModuleList.Add((IOModule)moduleInfo.Instance);
                            }
                            else
                                throw new NotSupportedException();
                            moduleInfo.Status = ModuleStatus.Loaded;
                        }
                        catch (Exception ex)
                        {
                            moduleInfo.Status = ModuleStatus.Failed;
                            moduleInfo.ErrorMessage = ex.Message;
                            moduleInfo.Instance = null;
                        }
                    }
                }
                else
                {
                    // UNLOAD THE MODULE
                    if (moduleInfo.Status == ModuleStatus.Loaded)
                    {
                        try
                        {
                            if (moduleInfo.Type == ModuleType.Display)
                                _displayModuleList.Remove((DisplayModule)moduleInfo.Instance);
                            else if (moduleInfo.Type == ModuleType.IO)
                                _ioModuleList.Remove((IOModule)moduleInfo.Instance);
                            else
                                throw new NotSupportedException();
                            //TODO: Comment faire pour unloader la lib ?
                            moduleInfo.Status = ModuleStatus.NotLoaded;
                        }
                        catch (Exception ex)
                        {
                            moduleInfo.Status = ModuleStatus.Failed;
                            moduleInfo.ErrorMessage = ex.Message;
                        }
                        finally
                        {
                            moduleInfo.Instance = null;
                        }
                    }
                }
            }
        }

        public DisplayModule GetDisplayModule(string name)
        {
            foreach (DisplayModule current in _displayModuleList)
                if (current.Name == name)
                {
                    return current;
                }
            return null;
        }

        public IOModule GetIOModule(string name)
        {
            foreach (IOModule current in _ioModuleList)
                if (current.Name == name)
                    return current;
            return null;
        }
    }
}
