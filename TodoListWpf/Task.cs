using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListWpf
{
    public class Task
    {
        public enum importanceLevel
        {
            important = 1,
            mid_important=2,
            not_important=3
        }

        public static int id_counter = 0;
        public importanceLevel importance { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public bool isDone { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string description { get; set; }

        public Task(string name, importanceLevel importance)
        {
            this.id = id_counter++;
            this.name = name;
            this.isDone = false;
            this.startDate = DateTime.Now;
            this.importance = importance;
        }
        public Task(string name, DateTime endDate, importanceLevel importance)
        {
            this.id = id_counter++;
            this.name = name;
            this.isDone = false;
            this.startDate = DateTime.Now;
            this.endDate = endDate;
            this.importance = importance;

        }
        public Task(string name, DateTime endDate, string description, importanceLevel importance)
        {
            this.id = id_counter++;
            this.name = name;
            this.isDone = false;
            this.startDate = DateTime.Now;
            this.endDate = endDate;
            this.description = description;
            this.importance = importance;
        }
        public Task(string name, string description, importanceLevel importance)
        {
            this.id = id_counter++;
            this.name = name;
            this.isDone = false;
            this.startDate = DateTime.Now;
            this.description = description;
            this.importance = importance;
        }
    }
}
