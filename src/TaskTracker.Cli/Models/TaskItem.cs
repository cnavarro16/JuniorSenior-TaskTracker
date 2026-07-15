namespace TaskTracker.Cli.Models;

public class TaskItem
{
	public TaskItem(string description, DateTime dueDate, bool isCompleted = false)
	{
		_description = description;
		_dueDate = dueDate;
		_isCompleted = isCompleted;
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
}
