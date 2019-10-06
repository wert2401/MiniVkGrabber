using System;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;


namespace MiniGraber
{
    public struct Person
    {
        public int id;
        public string first_name;
        public string last_name;
        public bool is_closed;
        public bool can_access_closed;
        int online;
        public string track_code;
        public string bdate;
        public City city;
        public string photo_200_orig;
        public string FullName {
            get
            {
                return first_name + " " + last_name;
            }

            private set { }
        }

        public override string ToString()
        {
            string output = FullName;
            if (city.title != null)
            {
                output += $" This person is from {city.title}. ";
            }
            if (bdate != null)
            {
                output += $"Birthday: {bdate}"; 
            }
            return output + "\n";
        }
    }

    public struct City
    {
        public int id;
        public string title;
    }
        
}
