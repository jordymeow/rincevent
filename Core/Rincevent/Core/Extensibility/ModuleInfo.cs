using System;
using System.Collections.Generic;
using System.Text;

namespace Meow.FR.Rincevent.Core.Extensibility
{
    public enum ModuleType
    {
        IO,
        Display,
        Unknown
    }

    public enum ModuleStatus
    {
        NotLoaded,
        Loaded,
        Failed
    }

    public class ModuleInfo
    {
        public ModuleInfo()
        {
            Status = ModuleStatus.NotLoaded;
            Type = ModuleType.Unknown;
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _FullName;

        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        private ModuleType _Type;

        public ModuleType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private ModuleStatus _Status;

        public ModuleStatus Status
        {
            get 
            { 
                return _Status; 
            }
            set 
            {
                if (value == ModuleStatus.Loaded || value == ModuleStatus.NotLoaded)
                    ErrorMessage = "";
                _Status = value; 
            }
        }

        private string _ErrorMessage;

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        private BaseModule _Instance;

        public BaseModule Instance
        {
            get { return _Instance; }
            set { _Instance = value; }
        }

        private string _Path;

        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }

        public override string ToString()
        {
            return _Name;
        }
    }
}
