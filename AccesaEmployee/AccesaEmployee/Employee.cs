using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccesaEmployee
{
	public  class Employee
	{
        private  string _name;
        public  EmployeePosition _position;
        private float _capacity;
        private  List<string> _hobbies = new List<string>();
        public const string XmlName = "employee";
        public const string XmlHobbies = "hobby";
        public Employee() { }
        public Employee(XmlReader r) { ReadXml(r); }
        public virtual void ReadXml(XmlReader r)
        {
            r.ReadStartElement();
            _name = r.ReadElementContentAsString("name", "");
            _position= (EmployeePosition)Enum.Parse(typeof(EmployeePosition), r.ReadElementContentAsString("position", ""));
            _capacity = r.ReadElementContentAsFloat("capacity", "");
            if (r.Name == "Hobbies")
                while (r.NodeType == XmlNodeType.Element)
                {
                    if (r.Name == "Hobbies")
                        Hobbies.Add(r.ReadElementContentAsString("hobby", ""));
                }
           
            r.ReadEndElement();
        }
        public virtual void WriteXml(XmlWriter w)
        {
            w.WriteElementString("name", _name);
            w.WriteElementString("capacity", _capacity.ToString());
            w.WriteStartElement(Employee.XmlHobbies);
            foreach (var i in _hobbies)
            {
                w.WriteElementString("Hobby: ",i);
            }
            w.WriteEndElement();
        }
        public string Name => _name;
		public EmployeePosition Position => _position;
		public float Capacity => _capacity;
		public List<string> Hobbies => _hobbies;

		protected Employee(string name, EmployeePosition position, float capacity)
		{
			_name = name;
			_position = position;
			_capacity = capacity;
		}

		public virtual void DisplayInfo()
		{
			var sb= new StringBuilder();
			_hobbies.ForEach(x=>sb.Append(x+" "));
			Console.WriteLine($"{_name} ocupa pozitia {_position} si e angajat cu {_capacity} ore pe zi. Lui ii place {sb.ToString()}");
		}
	}
}
