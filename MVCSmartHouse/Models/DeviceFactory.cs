using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHomeEquipment;

namespace MVCSmartHouse.Models
{
    public class DeviceFactory
    {
        public TV GetTVInstance()
        {
            return new TV(true, "LG", new TVChanneChangel(), new Volume(10));
        }

        public TV GetTVInstance(string name)
        {
            return new TV(true, name, new TVChanneChangel(), new Volume(10));
        }

        public Radio GetRadioInstance()
        {
            return new Radio(true, "Radiola", new Volume(10), new RadioChannel(88.5F));
        }

        public Radio GetRadioInstance(string name)
        {
            return new Radio(true, name, new Volume(10), new RadioChannel(88.5F));
        }

        public Refrigerator GetRefrigiratorInstance()
        {
            return new Refrigerator(true, "Libher", new ChangeFrostMode(), false, false);
        }

        public Refrigerator GetRefrigiratorInstance(string name)
        {
            return new Refrigerator(true, name, new ChangeFrostMode(), false, false);
        }

        public AirCond GetCondicionerInstance()
        {
            return new AirCond(true, "Midea", new AirCondModeChng(), new Temperature());
        }

        public AirCond GetCondicionerInstance(string name)
        {
            return new AirCond(true, name, new AirCondModeChng(), new Temperature());
        }

        public MusicCenter GetMusicCenterInstance()
        {
            return new MusicCenter(true, "Sony", new Volume(10), new RadioChannel(88.5F), new ChangeCD(1), new MusicMode());
        }

        public MusicCenter GetMusicCenterInstance(string name)
        {
            return new MusicCenter(true, name, new Volume(10), new RadioChannel(88.5F), new ChangeCD(1), new MusicMode());
        }

    }
}