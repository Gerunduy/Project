//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfService1
{
    using System;
    using System.Collections.Generic;
    
    public partial class SensorSteel
    {
        public int id_sensor_steel { get; set; }
        public int id_sensor { get; set; }
        public int id_steel { get; set; }
        public int R01 { get; set; }
        public int R02 { get; set; }
    
        public virtual Sensors Sensors { get; set; }
        public virtual Steels Steels { get; set; }
    }
}