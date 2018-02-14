using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        public db_AAZEntities bd = new db_AAZEntities();
        public List<Sensors> GetlistSensor()
        {
            try
            {
                List<Sensors> result = new List<Sensors>();
                List<Sensors> lv = bd.Sensors.ToList();
                for (int i = 0; i < lv.Count; i++)
                {
                        Sensors temp = new Sensors();
                        temp.id_sensor = lv[i].id_sensor;
                        temp.name_sensor = lv[i].name_sensor;
                        result.Add(temp);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Steels> GetlistSteel()
        {
            try
            {
                List<Steels> result = new List<Steels>();
                List<Steels> lv = bd.Steels.ToList();
                for (int i = 0; i < lv.Count; i++)
                {
                    Steels temp = new Steels();
                    temp.id_steel = lv[i].id_steel;
                    temp.name_steel = lv[i].name_steel;
                    result.Add(temp);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SensorSteel> GetlistSensor_Steel( int Id_Sensors, int Id_Steel)
        {
            try
            {
                

                List<SensorSteel> result = new List<SensorSteel>();
                List<SensorSteel> lv = bd.SensorSteel.Where(o => o.id_sensor == Id_Sensors && o.id_steel == Id_Steel).ToList();
                for (int i = 0; i < lv.Count; i++)
                {
                    SensorSteel temp = new SensorSteel();
                    temp.id_sensor_steel = lv[i].id_sensor_steel;
                    temp.id_sensor = lv[i].id_sensor;
                    temp.R01 = lv[i].R01;
                    temp.R02 = lv[i].R02;
                    result.Add(temp);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
