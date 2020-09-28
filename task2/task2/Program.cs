using System;
using System.Collections.Generic;

namespace task2
{
	internal abstract class Student
	{

		public Student(string studentFullName)
		{
			fullName = studentFullName;
			academicState = new AcademicState("");
		}

		public Student(string studentFullName, AcademicState studentAcademicState)
		{
			fullName = studentFullName;
			academicState = studentAcademicState;
		}

		public abstract void Study();

		public void Read()
		{
			SetActionState(new ActionState("Read"));
		}

		public void Write()
		{
			SetActionState(new ActionState("Write"));
		}

		public void Relax()
		{
			SetActionState(new ActionState("Relax"));
		}

		public string GetFullName()
		{
			return fullName;
		}

		public string GetState()
		{
			return academicState.ToString() + " " + actionState.ToString();
		}

		public void SetActionState(ActionState state)
		{
			actionState = state;
		}

		private string fullName;

		private AcademicState academicState;
		private ActionState actionState = new ActionState("");
	}
	internal class State
	{

		public State(string stateTitle)
		{
			title = stateTitle;
		}

		public override string ToString()
		{
			return title;
		}

		public string title { get; private set; }
	}

	internal class ActionState : State
	{

		public ActionState(string stateTitle) : base(stateTitle)
		{
		}
	}

	internal class AcademicState : State
	{

		public AcademicState(string stateTitle) : base(stateTitle)
		{
		}
	}

	internal class GoodStudent : Student
	{

		public GoodStudent(string full) : base(full, new AcademicState("Good"))
		{
		}

		public override void Study()
		{
			Read();
			Write();
			Read();
			Write();
			Relax();
		}
	}

	internal class BadStudent : Student
	{

		public BadStudent(string full) : base(full, new AcademicState("Bad"))
		{
		}

		public override void Study()
		{
			Relax();
			Relax();
			Relax();
			Relax();
			Read();
		}
	}

	internal class Group
	{

		public Group(string groupName)
		{
			name = groupName;
		}

		public void AddStudent(Student student)
		{
			students.Add(student);
		}

		public string GetGroupName()
		{
			return name;
		}

		public void GetInfo()
		{
			Console.WriteLine("Group: " + GetGroupName());

			if (students.Count > 0)
			{
				Console.WriteLine("Students:");
				foreach (Student st in students)
				{
					Console.WriteLine("Name: " + st.GetFullName());
				}
			}
			else
			{
				Console.WriteLine("No students yet.");
			}
		}

		public void GetFullInfo()
		{
			Console.WriteLine("Group: " + GetGroupName());

			if (students.Count > 0)
			{
				Console.WriteLine("Students:");
				foreach (Student st in students)
				{
					Console.WriteLine("Name: " + st.GetFullName());
					Console.WriteLine("State: " + st.GetState());
				}
			}
			else
			{
				Console.WriteLine("No students yet.");
			}
		}

		public string name { get; private set; }

		private List<Student> students = new List<Student>();
	}

	internal class GroupManager
	{
		public List<Group> groups { get; private set; }

		public GroupManager()
		{
			groups = new List<Group>();
		}

		public List<Group> GetGroups()
		{
			return groups;
		}

		public void InteractiveAddGroup()
		{
			Console.WriteLine("> Enter Group Name: ");
			groups.Add(new Group(Console.ReadLine()));
		}

		public void ActionAddStudent(Group group)
		{
			bool isActive = true;
			do
			{
				isActive = false;

				Console.WriteLine("> Enter Student Name: ");

				string studentName = Console.ReadLine();

				Console.Write("> Enter Student Status(Good or Bad): ");

				string studentType = Console.ReadLine();

				if (studentType.ToLower() == "good")
				{
					group.AddStudent(new GoodStudent(studentName));
				}
				else if (studentType.ToLower() == "bad")
				{
					group.AddStudent(new BadStudent(studentName));
				}
				else
				{
					Console.WriteLine("Adding new student failed. Try again");
					isActive = true;
				}
			} while (isActive);
		}

		public void EnterAction()
		{
			Console.WriteLine("Select group: ");

			for (int i = 0; i < groups.Count; ++i)
			{
				Console.WriteLine(String.Format("[{0}] {1}", i + 1, groups[i].name));
			}

			Group selectedGroup = groups[Convert.ToInt32(Console.ReadLine()) - 1];

			bool isShown = true;

			do
			{
				Console.Clear();

				Console.WriteLine("Active Group: " + selectedGroup.GetGroupName());

				Console.WriteLine("Get name info (Press 1)");
				Console.WriteLine("Get full info (Press 2)");
				Console.WriteLine("Add student (Press 3)");
				Console.WriteLine("Go back (Press 4)");

				ConsoleKey selectedOption = Console.ReadKey().Key;

				Console.Clear();
				switch (selectedOption)
				{
					case ConsoleKey.D1:
						selectedGroup.GetInfo();
						Console.ReadKey();
						break;

					case ConsoleKey.D2:
						selectedGroup.GetFullInfo();
						Console.ReadKey();
						break;

					case ConsoleKey.D3:
						ActionAddStudent(selectedGroup);
						break;
					case ConsoleKey.D4:
						isShown = false;
						break;
					default:
						break;
				}
			} while (isShown);
		}

		public void PrintGroups()
		{
			if (groups.Count > 0)
			{
				Console.WriteLine("Groups: ");
				foreach (Group group in groups)
				{
					Console.WriteLine(String.Format("{0}", group.GetGroupName()));
				}
			}
			else
			{
				Console.WriteLine("No groups. Create group");
			}
		}
	}

	internal class Program
	{

		private static void Main(string[] args)
		{
			GroupManager groupManager = new GroupManager();

			do
			{
				Console.Clear();
				Console.WriteLine("Choose action: ");
				Console.WriteLine("List groups (Press 1)");
				Console.WriteLine("Create group (Press 2)");
				Console.WriteLine("Select group (Press 3)");
				Console.WriteLine("Exit (Press 4)");

				ConsoleKey selectedOption = Console.ReadKey().Key;
				Console.WriteLine();
				Console.Clear();

				if (selectedOption == ConsoleKey.D1)
				{
					groupManager.PrintGroups();
					Console.ReadKey();
				}
				else if (selectedOption == ConsoleKey.D2)
				{
					groupManager.InteractiveAddGroup();
				}
				else if (selectedOption == ConsoleKey.D3)
				{
					groupManager.EnterAction();
				}
				else if (selectedOption == ConsoleKey.D4)
				{
					break;
				}
				else
				{
					continue;
				}
			} while (true);
		}
	}
}
