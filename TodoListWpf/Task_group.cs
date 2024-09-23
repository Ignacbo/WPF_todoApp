using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListWpf
{
    public class Task_group
    {
        public static Task_group selectedTaskGroup;
        private static int id_counter = 0;
        public static ObservableCollection<Task_group> taskGroupList = new ObservableCollection<Task_group>();
        private int id { get; set; }
        public string name { get; set; }
        private DateTime createDateTime { get; set; }
        public ObservableCollection<Task> subtaskList { get; set; }
        public ObservableCollection<Task> importantSubtasksList { get; set; }
        public ObservableCollection<Task> midImportantSubtasksList { get; set; }
        public ObservableCollection<Task> notImportantSubtasksList { get; set; }
        public ObservableCollection<Task> subtaskListCompleted { get; set; }

        public Task_group(string name)
        {
            subtaskList = new ObservableCollection<Task>();
            subtaskListCompleted = new ObservableCollection<Task>();
            importantSubtasksList = new ObservableCollection<Task>();
            midImportantSubtasksList = new ObservableCollection<Task>();
            notImportantSubtasksList = new ObservableCollection<Task>();
            this.id = id_counter++;
            this.name = name;
            this.createDateTime = DateTime.Now;
            AddTaskGroup(this);
        }

        public void add_task(Task task)
        {
            if (task == null) return;
            if(task.importance == Task.importanceLevel.important)
            {
                importantSubtasksList.Add(task);
            }
            else if(task.importance == Task.importanceLevel.mid_important)
            {
                midImportantSubtasksList.Add(task);
            }
            else if (task.importance == Task.importanceLevel.not_important) 
            {
                notImportantSubtasksList.Add(task);
            }
            subtaskList.Add(task);
        }

        void add_task_list(List<Task> tasklist)
        {
            foreach(Task task in tasklist)
            {
                add_task(task);
            }
        }

        public void mark_task_as_done(Task task)
        {
            subtaskListCompleted.Add(task);
            subtaskList.RemoveAt(subtaskList.IndexOf(task));
            if (task.importance == Task.importanceLevel.important)
            {
                importantSubtasksList.RemoveAt(importantSubtasksList.IndexOf(task));
            }
            else if (task.importance == Task.importanceLevel.mid_important)
            {
                midImportantSubtasksList.RemoveAt(midImportantSubtasksList.IndexOf(task));
            }
            else if (task.importance == Task.importanceLevel.not_important)
            {
                notImportantSubtasksList.RemoveAt(notImportantSubtasksList.IndexOf(task));
            }
        }

        public static void AddTaskGroup(Task_group taskGroup)
        {
            taskGroupList.Add(taskGroup);
        }

    }
}
