using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartHomeEquipment;
using MVCSmartHouse.Models;

namespace MVCSmartHouse.Controllers
{
    public class DevicesController : Controller
    {
        //
        // GET: /Devices/
        public ActionResult Index()
        {
            IDictionary<int, Device> devDictionary; //= new Dictionary<int, Device>();
            DeviceFactory Devises = new DeviceFactory();

            if (Session["Devices"] == null)
            {
                devDictionary = new SortedDictionary<int, Device>();
                devDictionary.Add(1, Devises.GetRadioInstance());
                devDictionary.Add(2, Devises.GetTVInstance());
                devDictionary.Add(3, Devises.GetMusicCenterInstance());
                devDictionary.Add(4, Devises.GetCondicionerInstance());
                devDictionary.Add(5, Devises.GetRefrigiratorInstance());

                Session["Devices"] = devDictionary;
                Session["NextId"] = 6;
            }
            else
            {
                devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            }

            SelectListItem[] DevisesList = new SelectListItem[5];
            DevisesList[0] = new SelectListItem { Text = "Radio", Value = "radio", Selected = true };
            DevisesList[1] = new SelectListItem { Text = "TV", Value = "tv" };
            DevisesList[2] = new SelectListItem { Text = "Music Center", Value = "musiccenter" };
            DevisesList[3] = new SelectListItem { Text = "Condicioner", Value = "condicioner" };
            DevisesList[4] = new SelectListItem { Text = "Refrigirator", Value = "refrigirator" };
            ViewBag.DevisesList = DevisesList;

            return View(devDictionary);
        }

        public ActionResult Add(string deviceType, string deviceName)
        {
            Device newDevice;
            DeviceFactory Devises = new DeviceFactory();
            switch (deviceType)
            {
                default:
                    newDevice = Devises.GetRadioInstance(deviceName);
                    break;
                case "tv":
                    newDevice = Devises.GetTVInstance(deviceName);
                    break;
                case "refrigirator":
                    newDevice = Devises.GetRefrigiratorInstance(deviceName);
                    break;
                case "condicioner":
                    newDevice = Devises.GetCondicionerInstance(deviceName);
                    break;
                case "musiccenter":
                    newDevice = Devises.GetMusicCenterInstance(deviceName);
                    break;
            }

            int id = (int)Session["NextId"];
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            devDictionary.Add(id, newDevice);
            id++;
            Session["NextId"] = id;

            return RedirectToAction("Index");
        }

        public ActionResult On(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            if (devDictionary[id].state)
            {
                devDictionary[id].Off();
            }
            else
            {
                devDictionary[id].On();
            }
            return RedirectToAction("Index");
        }

        public ActionResult VolumeUp(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ((IVolume)devDictionary[id]).UpVolume();
            return RedirectToAction("Index");
        }

        public ActionResult VolumeDown(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ((IVolume)devDictionary[id]).DownVolume();
            return RedirectToAction("Index");
        }

        public ActionResult RadioChanelUP(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ((IRadioChannel)devDictionary[id]).NextChannal();
            return RedirectToAction("Index");
        }

        public ActionResult RadioChanelDown(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ((IRadioChannel)devDictionary[id]).PrevChannel();
            return RedirectToAction("Index");
        }

        public ActionResult TVChanelUP(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ((ITVChangeChannel)devDictionary[id]).NextChannal();
            return RedirectToAction("Index");
        }

        public ActionResult TVChanelDown(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ((ITVChangeChannel)devDictionary[id]).PrevChannel();
            return RedirectToAction("Index");
        }

        public ActionResult ChangeMusicCenterMode(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ((IMusicCenterMode)devDictionary[id]).ChangeMode();
            return RedirectToAction("Index");
        }

        public ActionResult ChangeCD(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ((IChangeCD)devDictionary[id]).NextCD();
            return RedirectToAction("Index");
        }

        public ActionResult ChangeModeAirConditioner(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ((IDeviceChangeMode)devDictionary[id]).ChangeMode();
            return RedirectToAction("Index");
        }

        public ActionResult TemperatureUpAirConditioner(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ((ITemperature)devDictionary[id]).UpTemperature();
            return RedirectToAction("Index");
        }

        public ActionResult TemperatureDownAirConditioner(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ((ITemperature)devDictionary[id]).DownTemperature();
            return RedirectToAction("Index");
        }

        public ActionResult RefregeratorMode(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ((IFrostMode)devDictionary[id]).ChangeMode();
            return RedirectToAction("Index");
        }

        public ActionResult DoorOpenClose(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];

            if ((devDictionary[id] as IDoorOpen).DoorState == false)
            {
                ((IDoorOpen)devDictionary[id]).RefrigeratDoorOpen();
            }
            else if ((devDictionary[id] as IDoorOpen).DoorState == true)
            {
                ((IDoorOpen)devDictionary[id]).RefrigeratDoorClose();
            }
            return RedirectToAction("Index");
        }


        public ActionResult FrostDoorOpenClose(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];

            if ((devDictionary[id] as IFrostdoorOpen).FrostDoorState == false)
            {
                ((IFrostdoorOpen)devDictionary[id]).FrostDoorStateOpen();
            }
            else if ((devDictionary[id] as IFrostdoorOpen).FrostDoorState == true)
            {
                ((IFrostdoorOpen)devDictionary[id]).FrostDoorStateClose();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            IDictionary<int, Device> devDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            devDictionary.Remove(id);
            return RedirectToAction("Index");
        }

	}
}