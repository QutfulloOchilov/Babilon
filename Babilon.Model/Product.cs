﻿namespace Babilon.Model
{
    public class Product : EntityBase
    {
        private string name;
        public string Name { get { return name; } set { name = value; NotifyPropertyChanged(); } }
    }
}
