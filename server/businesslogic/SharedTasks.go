package businesslogic

import (
	"acs560_course_project/server/datastore"
	"log"
)

func AddSharedTask( request *AddSharedTaskRequest, sessionEmail string) AddSharedTaskResult{
	task := datastore.MakeSharedTask( &sessionEmail, &(request.TaskName), (request.TaskTimeSpent), &(request.TaskDate), &(request.EmailTo))
	if (task == nil) {
		return AddSharedTaskResult{
			AddSharedTaskResult : "Failure",
		}
	}
	taskId, taskIdError, errormsg := datastore.ExistSharedTask(task)
	if (taskIdError != nil) {
		log.Println("There is an error with ExistsSharedTask " + taskIdError.Error())
		return AddSharedTaskResult{
			AddSharedTaskResult : errormsg,
		}
	}
	err := datastore.AddSharedTask(task, taskId)
	if err != nil {
		return AddSharedTaskResult{
			AddSharedTaskResult : "Failure",
		}
	} else {
			return AddSharedTaskResult{
			AddSharedTaskResult : "Success",
		}
	}
}

func GetAllTasksSharedWithMe( sessionEmail string ) GetAllSharedTasksByEmailToResult{
	result,err := datastore.SelectSharedTasksByEmailTo( sessionEmail )
	if err != nil {
		return GetAllSharedTasksByEmailToResult {
			GetAllSharedTasksResult: "Failure",
			SharedTaskList: nil,
		}
	} else {
		return GetAllSharedTasksByEmailToResult {
			GetAllSharedTasksResult: "Success",
			SharedTaskList: result[:],
		}
	}
}