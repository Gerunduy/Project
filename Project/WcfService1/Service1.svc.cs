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
        public zabolotinEntities bd = new zabolotinEntities();
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
        public List<Steels> GetlistSteelM1(int id_Sensor)
        {
            try
            {
                List<Steels> result = new List<Steels>();
                List<Steels> lv = bd.Steels.ToList();
                for (int i = 0; i < lv.Count; i++)
                {
                    int id_steel = lv[i].id_steel;
                    SensorSteel sensorSteel = bd.SensorSteel.Where(o => o.id_sensor == id_Sensor && o.id_steel== id_steel).FirstOrDefault();
                    if (sensorSteel != null)
                    {
                        Steels temp = new Steels();
                        temp.id_steel = lv[i].id_steel;
                        temp.name_steel = lv[i].name_steel;
                        result.Add(temp);
                    }
                    
                }
                return result;
            }
            catch (Exception ex)
            {
                List<Steels> result2 = new List<Steels>();
                Steels temp = new Steels();
                temp.id_steel = 1;
                temp.name_steel = ex.Message;
                result2.Add(temp);
                return result2;
            }
        }
        public List<Steels> GetlistSteelM3(int id_Sensor)
        {
            try
            {
                List<Steels> result = new List<Steels>();
                List<Steels> lv = bd.Steels.ToList();
                for (int i = 0; i < lv.Count; i++)
                {
                    int id_steel = lv[i].id_steel;
                    SensorSteelM3 sensorSteel = bd.SensorSteelM3.Where(o => o.id_sensor == id_Sensor && o.id_steel == id_steel).FirstOrDefault();
                    if (sensorSteel != null)
                    {
                        Steels temp = new Steels();
                        temp.id_steel = lv[i].id_steel;
                        temp.name_steel = lv[i].name_steel;
                        result.Add(temp);
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                List<Steels> result2 = new List<Steels>();
                Steels temp = new Steels();
                temp.id_steel = 1;
                temp.name_steel = ex.Message;
                result2.Add(temp);
                return result2;
            }
        }
        public List<SensorSteel> GetlistSensor_SteelM1( int Id_Sensors, int Id_Steel)
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
        public List<SensorSteelM3> GetlistSensor_SteelM3(int Id_Sensors, int Id_Steel)
        {
            try
            {


                List<SensorSteelM3> result = new List<SensorSteelM3>();
                List<SensorSteelM3> lv = bd.SensorSteelM3.Where(o => o.id_sensor == Id_Sensors && o.id_steel == Id_Steel).ToList();
                for (int i = 0; i < lv.Count; i++)
                {
                    SensorSteelM3 temp = new SensorSteelM3();
                    temp.id_sensor_steelM3 = lv[i].id_sensor_steelM3;
                    temp.id_sensor = lv[i].id_sensor;
                    temp.W0 = lv[i].W0;
                    temp.Wf = lv[i].Wf;
                    result.Add(temp);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Boolean GetConnection()
        {
            try
            {


                List<SensorSteelM3> result = bd.SensorSteelM3.ToList();
                if(result.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
