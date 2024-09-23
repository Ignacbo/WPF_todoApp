using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static TodoListWpf.Task;

namespace TodoListWpf
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Task_group> Observable_task_groups { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            Task_group group_1 = new Task_group("lista zakupow");
            group_1.add_task(new Task("ziemniaki", Task.importanceLevel.important));
            group_1.add_task(new Task("ogórki", Task.importanceLevel.mid_important));
            group_1.add_task(new Task("Jajka", Task.importanceLevel.not_important));
            group_1.add_task(new Task("Piwo", Task.importanceLevel.important));
            Task_group group_2 = new Task_group("Zadania domowe");
            group_2.add_task(new Task("Polski", Task.importanceLevel.not_important));
            group_2.add_task(new Task("Matematyka", Task.importanceLevel.not_important));
            group_2.add_task(new Task("Programowanie obiektowe", Task.importanceLevel.important));
            group_2.add_task(new Task("Informatyka", Task.importanceLevel.important));
            Task_group group_3 = new Task_group("inne");
            group_3.add_task(new Task("posprzątać pokój", Task.importanceLevel.not_important));
            group_3.add_task(new Task("kupić rower", Task.importanceLevel.important));
            group_3.add_task(new Task("Wysłać dokumenty", Task.importanceLevel.mid_important));

            TaskListTreeView.ItemsSource = Task_group.taskGroupList;
            DataContext = this;
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Task_group? selectedTaskGroup = (sender as TreeViewItem)?.DataContext as Task_group;
            if (selectedTaskGroup == null)
            {
                expanderAddTask.Visibility = Visibility.Collapsed;
                return;
            }

            Task_group.selectedTaskGroup = selectedTaskGroup;
            expanderAddTask.Visibility = Visibility.Visible;

            importantTasksGrid.ItemsSource = selectedTaskGroup.importantSubtasksList;
            midImportantTasksGrid.ItemsSource = selectedTaskGroup.midImportantSubtasksList;
            notImportantTasksGrid.ItemsSource = selectedTaskGroup.notImportantSubtasksList;

            taskInfoSection.Visibility = Visibility.Collapsed;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string taskName = txtTaskName.Text;
            string importanceLevel = cmbImportance.Text;
            string description = txtDescription.Text;
            DateTime? endDate = dpEndDate.SelectedDate;
            Task.importanceLevel importance = Task.importanceLevel.not_important;

            if (importanceLevel.Equals("Important"))
            {
                importance = Task.importanceLevel.important;
            }
            else if (importanceLevel.Equals("Mid Important"))
            {
                importance = Task.importanceLevel.mid_important;
            }
            else if (importanceLevel.Equals("Not Important"))
            {
                importance = Task.importanceLevel.not_important;
            }

            Task newTask = new Task(taskName, importance);

            if (description != null)
            {
                newTask.description = description;
            }
            if (endDate != null)
            {
                newTask.endDate = (DateTime)endDate;
            }

            Task_group.selectedTaskGroup.add_task(newTask);
            ClearInputFields();

        }

        private void ClearInputFields()
        {
            txtTaskName.Text = string.Empty;
            cmbImportance.SelectedItem = null;
            txtDescription.Text = string.Empty;
            dpEndDate.SelectedDate = null;
        }

        private void TaskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            Task selectedTask = (Task)listBox.SelectedItem;
            if (selectedTask == null) return;

            txtStartDate.Text = selectedTask.startDate.ToString("yyyy-MM-dd");
            string edate = selectedTask.endDate.ToString("yyyy-MM-dd");
            txtEndDate.Text = edate == "0001-01-01" ? "not set" : edate;
            txtTaskDescription.Text = selectedTask.description == null ? "no description" : selectedTask.description.ToString();

            taskInfoSection.Visibility = Visibility.Visible;

        }
        private void MarkAsComplete_Click(object sender, RoutedEventArgs e)
        {
            Task? task = ((FrameworkElement)sender).DataContext as Task;

            task.isDone = true;
            Task_group.selectedTaskGroup.mark_task_as_done(task);
        }

        private void AddTaskGroup_Click(object sender, RoutedEventArgs e)
        {

            string name = txtTaskGroupName.Text;
            if (!String.IsNullOrEmpty(name))
            {
                Task_group newTaskGroup = new Task_group(name);
                txtTaskGroupName.Text = string.Empty;
            }

        }

        private void RemoveTaskGroup_Click(Object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete task group?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        
            if (result == MessageBoxResult.Yes)
            {
                Task_group group = Task_group.selectedTaskGroup;
                if (group != null)
                {
                    Task_group.taskGroupList.Remove(group);
                }
            }
        }
        
    }


}