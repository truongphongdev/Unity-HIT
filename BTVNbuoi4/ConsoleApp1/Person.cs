using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public class Person
    {
        private string hoTen;
        private int tuoi;
        private string diaChi;

        public string HoTen
        {
            get { return hoTen; } 
            set { hoTen = value; }
        }

        public int Tuoi
        {
            get { return tuoi; }
            set { tuoi = value; }
        }

        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }

       

        public Person()
        {
        }

      
        public Person(string hoTen, int tuoi, string diaChi)
        {
            this.hoTen = hoTen;
            this.tuoi = tuoi;
            this.diaChi = diaChi;
        }

    }

}
