using System;
using System.Collections.Generic;
using System.Text;

namespace com.alexleekt.aideNotebook
{
    class PowerProfile
    {
        private String _Name;
        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        private String _Guid;
        public String Guid
        {
            get
            {
                return _Guid;
            }
            set
            {
                _Guid = value;
            }
        }

        public PowerProfile()
        {
        }

        public PowerProfile(String guid, String name)
        {
            Name = name;
            Guid = guid;
        }
    }
}
