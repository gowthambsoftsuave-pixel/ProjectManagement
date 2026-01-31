using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using ProjectManagement.BLL.Interface;
using ProjectManagement.BLL.Service;
using ProjectManagement.Common.Enums;

namespace ProjectManagement.Functions;

public class NotificationFunction
{
    private readonly ITaskService _taskService;
    private readonly ILogger<NotificationFunction> _logger;

    public NotificationFunction(
        ITaskService taskService,
        ILogger<NotificationFunction> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    [Function("SendTaskNotification")]
    public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("--- Notification Cycle Started at {Time} ---", DateTime.Now);

        var tasks = await _taskService.GetAllTasksAsync();
        var taskList = tasks.ToList();

        // 1. Handle Backlog
        var backlogTasks = taskList.Where(t => t.Status == TaskStatusEnum.Backlog);
        foreach (var task in backlogTasks)
        {
            _logger.LogInformation("[BACKLOG] Task {TaskId}: {TaskName} is waiting in the backlog.", task.TaskId, task.TaskName);
        }

        // 2. Handle Not Started (Warning)
        var notStartedTasks = taskList.Where(t => t.Status == TaskStatusEnum.NotStarted);
        foreach (var task in notStartedTasks)
        {
            _logger.LogWarning("[NOT STARTED] REMINDER: Task {TaskId}: {TaskName} needs to be started!", task.TaskId, task.TaskName);
        }

        // 3. Handle In Progress
        var inProgressTasks = taskList.Where(t => t.Status == TaskStatusEnum.InProgress);
        foreach (var task in inProgressTasks)
        {
            _logger.LogInformation("[IN PROGRESS] Task {TaskId}: {TaskName} is currently being worked on.", task.TaskId, task.TaskName);
        }

        // 4. Handle Completed (Summary)
        var completedCount = taskList.Count(t => t.Status == TaskStatusEnum.Completed);
        _logger.LogInformation("[COMPLETED] Summary: {Count} tasks have been finished so far.", completedCount);

        _logger.LogInformation("--- Notification Cycle Complete ---");
    }
}
