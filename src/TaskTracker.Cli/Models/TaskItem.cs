using TaskTracker.Cli.Enums;

namespace TaskTracker.Cli.Models;

public class TaskItem
{
	public TaskItem(int id, string description, DateTime dueDate, PriorityLevel priority, bool isCompleted = false)
	{
		_id = id;
		_description = description;
		_dueDate = dueDate;
		_priority = priority;
		_isCompleted = isCompleted;
	}

	private int _id;

	public int Id
	{
		get { return _id; }
		init { _id = value; }
	}


	private string _description;

	public string Description
	{
		get { return _description; }
		set { _description = value; }
	}

	private DateTime _dueDate;

	public DateTime DueDate
	{
		get { return _dueDate; }
		set { _dueDate = value; }
	}

	private bool _isCompleted;

	public bool IsCompleted
	{
		get { return _isCompleted; }
		private set { _isCompleted = value; }
	}

	public void MarkComplete()
	{
		if (!_isCompleted)
		{
			IsCompleted = true;
		}
	}

	private PriorityLevel _priority;

	public PriorityLevel Priority
	{
		get { return _priority; }
		set { _priority = value; }
	}
}
